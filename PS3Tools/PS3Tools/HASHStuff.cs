using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Numerics;

namespace Snowydev_Port
{

        abstract class Hash {
            public bool compareBytes(byte[] value1, int offset1, byte[] value2, int offset2, int len)
            {
                for (int i = 0; i < len; i++)
                {
                    if (value1[i + offset1] != value2[i + offset2])
                    {
                        return false;
                    }
                }
                return true;
            }

            public virtual void setHashLen(int len) { }

            public virtual void doInit(byte[] key) { }

            public virtual void doUpdate(byte[] i, int inOffset, int len) { }

            public virtual bool doFinal(byte[] expectedhash, int hashOffset, bool hashDebug) { return false;  }
        }

        class HMAC : Hash {

            private int hashLen;
            private HMACSHA1 mac;
            private byte[] result;

            public override void setHashLen(int len) {
                if (len == 0x10 || len == 0x14) {
                    hashLen = len;
                    // mac.HashSize = len; needed oO?!?!?!
                } else {
                    throw new Exception("Hash len must be 0x10 or 0x14");
                }
            }

            public override void doInit(byte[] key) {
                try {
                    mac = new HMACSHA1(key);
                } catch (Exception ex) {
                    throw ex;
                }
            }

            public override void doUpdate(byte[] i, int inOffset, int len) {
                result = mac.ComputeHash(i, inOffset, len);
            }

            public override bool doFinal(byte[] expectedhash, int hashOffset, bool hashDebug) {
                return (hashDebug || compareBytes(result, 0, expectedhash, hashOffset, hashLen));
            }
        }


        class CMAC : Hash {

            int hashLen;
            byte[] key;
            byte[] K1;
            byte[] K2;
            byte[] nonProcessed;
            byte[] previous;

            public CMAC() {
                hashLen = 0x10;
            }

            public override void setHashLen(int len) {
                if (len == 0x10) {
                    hashLen = len;
                } else {
                    throw new Exception("Hash len must be 0x10");
                }
            }

            public override void doInit(byte[] key) {
                this.key = key;
                K1 = new byte[0x10];
                K2 = new byte[0x10];
                calculateSubkey(key, K1, K2);
                nonProcessed = null;
                previous = new byte[0x10];
            }

            private void calculateSubkey(byte[] key, byte[] K1, byte[] K2) {
                byte[] zero = new byte[0x10];
                byte[] L = new byte[0x10];
                ToolsImpl.aesecbEncrypt(key, zero, 0, L, 0, zero.Length);
                BigInteger aux = new BigInteger(ConversionUtils.reverseByteWithSizeFIX(L));

                if ((L[0] & 0x80) != 0) {
                    //Case MSB is set
                    aux = (aux << 1) ^ (new BigInteger(0x87));
                } else {
                    aux = aux << 1;
                }
                byte[] aux1 = ConversionUtils.reverseByteWithSizeFIX(aux.ToByteArray());
                if (aux1.Length >= 0x10) {
                    ConversionUtils.arraycopy(aux1, aux1.Length - 0x10, K1, 0, 0x10);
                } else {
                    ConversionUtils.arraycopy(zero, 0, K1, 0, zero.Length);
                    ConversionUtils.arraycopy(aux1, 0, K1, 0x10 - aux1.Length, aux1.Length);
                }
                aux = new BigInteger(ConversionUtils.reverseByteWithSizeFIX(K1));

                if ((K1[0] & 0x80) != 0) {
                    aux = (aux << 1) ^ (new BigInteger(0x87));
                } else {
                    aux = aux << 1;
                }
                aux1 = ConversionUtils.reverseByteWithSizeFIX(aux.ToByteArray());
                if (aux1.Length >= 0x10) {
                    ConversionUtils.arraycopy(aux1, aux1.Length - 0x10, K2, 0, 0x10);
                } else {
                    ConversionUtils.arraycopy(zero, 0, K2, 0, zero.Length);
                    ConversionUtils.arraycopy(aux1, 0, K2, 0x10 - aux1.Length, aux1.Length);
                }
            }

            public override void doUpdate(byte[] i, int inOffset, int len) {
                byte[] data;
                if (nonProcessed != null) {
                    int totalLen = len + nonProcessed.Length;
                    data = new byte[totalLen];
                    ConversionUtils.arraycopy(nonProcessed, 0, data, 0, nonProcessed.Length);
                    ConversionUtils.arraycopy(i, inOffset, data, nonProcessed.Length, len);
                } else {
                    data = new byte[len];
                    ConversionUtils.arraycopy(i, inOffset, data, 0, len);
                }
                int count = 0;
                while (count < data.Length - 0x10) {
                    byte[] aux = new byte[0x10];
                    ConversionUtils.arraycopy(data, count, aux, 0, aux.Length);
                    ToolsImpl.XOR(aux, aux, previous);
                    ToolsImpl.aesecbEncrypt(key, aux, 0, previous, 0, aux.Length);
                    count += 0x10;
                }
                nonProcessed = new byte[data.Length - count];
                ConversionUtils.arraycopy(data, count, nonProcessed, 0, nonProcessed.Length);
            }

            public override bool doFinal(byte[] expectedhash, int hashOffset, bool hashDebug) {
                byte[] aux = new byte[0x10];
                ConversionUtils.arraycopy(nonProcessed, 0, aux, 0, nonProcessed.Length);
                if (nonProcessed.Length == 0x10)
                {
                    ToolsImpl.XOR(aux, aux, K1);
                } else {
                    aux[nonProcessed.Length] = (byte)0x80;
                    ToolsImpl.XOR(aux, aux, K2);
                }
                ToolsImpl.XOR(aux, aux, previous);
                byte[] calculatedhash = new byte[0x10];
                ToolsImpl.aesecbEncrypt(key, aux, 0, calculatedhash, 0, aux.Length);
                return (hashDebug || compareBytes(expectedhash, hashOffset, calculatedhash, 0, hashLen));
            }
        }

}
