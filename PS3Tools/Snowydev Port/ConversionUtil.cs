using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public class ConversionUtil
    {
        public static string byteToBinString(byte[] b, int offset)
        {
            string key = "";
            for (int i = 0; i < 8; i++)
                key = (((b[offset] >> i) & 1) != 0 ? "1" : "0") + key;
            return key;
        }

        public static ushort byteToUShort(byte[] b, int offset)
        {
            ushort a = (ushort)(b[offset] << 8);
            a |= (ushort)b[offset + 1];
            return a;
        }

        public static uint byteToUInt(byte[] b, int offset)
        {
            uint a = (uint)b[offset] << 24;
            a |= (uint)b[offset + 1] << 16;
            a |= (uint)b[offset + 2] << 8;
            a |= (uint)b[offset + 3] << 0;
            return a;
        }

        public static ushort byteToUShort(byte[] b)
        {
            return byteToUShort(b, 0);
        }

        public static uint byteToUInt(byte[] b)
        {
            return byteToUInt(b, 0);
        }

        public static byte[] uintToByte(uint i)
        {
            byte[] b = new byte[4];
            b[0] = (byte)((i >> 24) & 0xFF);
            b[1] = (byte)((i >> 16) & 0xFF);
            b[2] = (byte)((i >> 8) & 0xFF);
            b[3] = (byte)((i) & 0xFF);
            return b;
        }

        public static int binStringToInt(string bin)
        {
            int v = 0;
            int binStart = 1;
            char[] I18C = bin.ToCharArray();
            for (int i = I18C.Length - 1; i >= 0; i--)
            {
                if (I18C[i] == '1')
                    v += binStart;
                binStart <<= 1;
            }
            return v;
        }
    }
}
