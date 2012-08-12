using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Snowydev_Port
{
    abstract class Decryptor
    {

        public virtual void doInit(byte[] key, byte[] iv) { }

        public virtual void doUpdate(byte[] i, int inOffset, byte[] o, int outOffset, int len) { }
    }

    class NoCrypt : Decryptor
    {

        public override void doInit(byte[] key, byte[] iv)
        {
            //Do nothing
        }

        public override void doUpdate(byte[] i, int inOffset, byte[] o, int outOffset, int len)
        {
            ConversionUtils.arraycopy(i, inOffset, o, outOffset, len);
        }
    }

    class AESCBC128Decrypt : Decryptor
    {
        RijndaelManaged c;
        ICryptoTransform ct;
        public override void doInit(byte[] key, byte[] iv)
        {
            try
            {
                c = new RijndaelManaged();
                c.Padding = PaddingMode.None;
                c.Mode = CipherMode.CBC;
                c.Key = key;
                c.IV = iv;
                ct = c.CreateDecryptor();
            }
            catch (Exception)
            {
            }
        }

        public override void doUpdate(byte[] i, int inOffset, byte[] o, int outOffset, int len)
        {
            try
            {
                ct.TransformBlock(i, inOffset, len, o, outOffset);
            }
            catch (Exception)
            {
            }
        }
    }
}
