using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
namespace Snowydev_Port
{
    class AppLoader {

        private Decryptor dec;
        private Hash hash;
        private bool hashDebug = false;

        public bool doAll(int hashFlag, int cryptoFlag, byte[] i, int inOffset, byte[] o, int outOffset, int len, byte[] key, byte[] iv, byte[] hash, byte[] expectedHash, int hashOffset) 
        {
            doInit(hashFlag, cryptoFlag, key, iv, hash);
            doUpdate(i, inOffset, o, outOffset, len);
            return doFinal(expectedHash, hashOffset);
        }

        public void doInit(int hashFlag, int cryptoFlag, byte[] key, byte[] iv, byte[] hashKey) {
            byte[] calculatedKey = new byte[key.Length];
            byte[] calculatedIV = new byte[iv.Length];
            byte[] calculatedHash = new byte[hashKey.Length];
            getCryptoKeys(cryptoFlag, calculatedKey, calculatedIV, key, iv);
            getHashKeys(hashFlag, calculatedHash, hashKey);
            setDecryptor(cryptoFlag);
            setHash(hashFlag);
            Debug.WriteLine("ERK:  " + ConversionUtils.getHexString(calculatedKey));
            Debug.WriteLine("IV:   " + ConversionUtils.getHexString(calculatedIV));
            Debug.WriteLine("HASH: " + ConversionUtils.getHexString(calculatedHash));
            
            dec.doInit(calculatedKey, calculatedIV);
            hash.doInit(calculatedHash);

            
        }

        public void doUpdate(byte[] i, int inOffset, byte[] o, int outOffset, int len) {
            hash.doUpdate(i, inOffset, len);
            dec.doUpdate(i, inOffset, o, outOffset, len);
        }

        public bool doFinal(byte[] expectedhash, int hashOffset) {
            return hash.doFinal(expectedhash, hashOffset, hashDebug);
        }

        private void getCryptoKeys(int cryptoFlag, byte[] calculatedKey, byte[] calculatedIV, byte[] key, byte[] iv) {
            uint mode = (uint) cryptoFlag & 0xF0000000;
            switch (mode) {
                case 0x10000000:
                    ToolsImpl.aescbcDecrypt(EDATKeys.EDATKEY, EDATKeys.EDATIV, key, 0, calculatedKey, 0, calculatedKey.Length);
                    ConversionUtils.arraycopy(iv, 0, calculatedIV, 0, calculatedIV.Length);
                    Debug.WriteLine("MODE: Encrypted ERK");
                    break;
                case 0x20000000:
                    ConversionUtils.arraycopy(EDATKeys.EDATKEY, 0, calculatedKey, 0, calculatedKey.Length);
                    ConversionUtils.arraycopy(EDATKeys.EDATIV, 0, calculatedIV, 0, calculatedIV.Length);
                    Debug.WriteLine("MODE: Default ERK");
                    break;
                case 0x00000000:
                    ConversionUtils.arraycopy(key, 0, calculatedKey, 0, calculatedKey.Length);
                    ConversionUtils.arraycopy(iv, 0, calculatedIV, 0, calculatedIV.Length);
                    Debug.WriteLine("MODE: Unencrypted ERK");
                    break;
                default:
                    throw new Exception("Crypto mode is not valid: Undefined keys calculator");
            }
        }

        private void getHashKeys(int hashFlag, byte[] calculatedHash, byte[] hash) {
            uint mode = (uint) hashFlag & 0xF0000000;
            switch (mode) {
                case 0x10000000:
                    ToolsImpl.aescbcDecrypt(EDATKeys.EDATKEY, EDATKeys.EDATIV, hash, 0, calculatedHash, 0, calculatedHash.Length);
                    Debug.WriteLine("MODE: Encrypted HASHKEY");
                    break;
                case 0x20000000:
                    ConversionUtils.arraycopy(EDATKeys.EDATHASH, 0, calculatedHash, 0, calculatedHash.Length);
                    Debug.WriteLine("MODE: Default HASHKEY");
                    break;
                case 0x00000000:
                    ConversionUtils.arraycopy(hash, 0, calculatedHash, 0, calculatedHash.Length);
                    Debug.WriteLine("MODE: Unencrypted HASHKEY");
                    break;
                default:
                    throw new Exception("Hash mode is not valid: Undefined keys calculator");
            }
        }

        private void setDecryptor(int cryptoFlag) {
            int aux = cryptoFlag & 0xFF;
            switch (aux) {
                case 0x01:
                    dec = new NoCrypt();
                    Debug.WriteLine("MODE: Decryption Algorithm NONE");
                    break;
                case 0x02:
                    dec = new AESCBC128Decrypt();
                    Debug.WriteLine("MODE: Decryption Algorithm AESCBC128");
                    break;
                default:
                    throw new Exception("Crypto mode is not valid: Undefined decryptor");

            }
            

        }

        private void setHash(int hashFlag) {
            int aux = hashFlag & 0xFF;
            switch (aux) {
                case 0x01:
                    hash = new HMAC();
                    hash.setHashLen(0x14);
                    Debug.WriteLine("MODE: Hash HMAC Len 0x14");
                    break;
                case 0x02:
                    hash = new CMAC();
                    hash.setHashLen(0x10);
                    Debug.WriteLine("MODE: Hash CMAC Len 0x10");
                    break;
                case 0x04:
                    hash = new HMAC();
                    hash.setHashLen(0x10);
                    Debug.WriteLine("MODE: Hash HMAC Len 0x10");
                    break;
                default:
                    throw new Exception("Hash mode is not valid: Undefined hash algorithm");
            }
            if ((hashFlag & 0x0F000000) != 0) hashDebug = true;
        }

    }

}