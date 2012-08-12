using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SPU_simulation
{
    public class SPUCommandHelper
    {
        public static uint LSLR = 256 * 1024 - 1;

        public static void reg2ls(SPU spu, int r, uint addr)
        {
            addr &= SPUCommandHelper.LSLR & 0xfffffff0;
            byte[] r0 = ConversionUtil.uintToByte(spu.Register[r, 0]);
            byte[] r1 = ConversionUtil.uintToByte(spu.Register[r, 1]);
            byte[] r2 = ConversionUtil.uintToByte(spu.Register[r, 2]);
            byte[] r3 = ConversionUtil.uintToByte(spu.Register[r, 3]);


            for(int i = 0; i < 4; i++)
            {
                spu.LocalStorage[addr + i] = r0[i];
                spu.LocalStorage[addr + 4 + i] = r1[i];
                spu.LocalStorage[addr + 8 + i] = r2[i];
                spu.LocalStorage[addr + 12 + i] = r3[i];
            }

            for(uint i = 0; i < 16; i+=4)
            {
                uint cmdI = (addr + i) >> 2;
                spu.LocalStorageCommands[cmdI] = new SPUCommand(spu.LocalStorage, (int) (cmdI << 2));
            }
        }

        public static void ls2reg(SPU spu, int r, uint addr)
        {
            addr &= SPUCommandHelper.LSLR & 0xfffffff0;
            byte[] ls = new byte[4];
            System.Array.Copy(spu.LocalStorage, addr, ls, 0, 4);
            spu.Register[r, 0] = ConversionUtil.byteToUInt(ls);
            System.Array.Copy(spu.LocalStorage, addr + 4, ls, 0, 4);
            spu.Register[r, 1] = ConversionUtil.byteToUInt(ls);
            System.Array.Copy(spu.LocalStorage, addr + 8, ls, 0, 4);
            spu.Register[r, 2] = ConversionUtil.byteToUInt(ls);
            System.Array.Copy(spu.LocalStorage, addr + 12, ls, 0, 4);
            spu.Register[r, 3] = ConversionUtil.byteToUInt(ls);
        }

        public static byte[] reg_to_byte(SPU spu, int r)
        {
            int i, j;
            byte[] b = new byte[16];
            for (i = 0; i < 4; ++i)
                for (j = 0; j < 4; ++j)
                    b[i * 4 + j] = (byte) ((spu.Register[r, i] >> (24 - j*8)) & 0xFF);
            return b;
        }

        public static void byte_to_reg(SPU spu, int r, byte[] b)
        {
            int i, j;
            for (i = 0; i < 4; ++i)
            {
                spu.Register[r, i] = 0;
                for (j = 0; j < 4; ++j)
                    spu.Register[r, i] |= ((uint) b[i * 4 + j]) << (24 - j*8);
            }
        }

        public static ushort[] reg_to_half(SPU spu, int r)
        {
            int i, j;
            ushort[] d = new ushort[8];
            for (i = 0; i < 4; ++i)
                for (j = 0; j < 2; ++j)
                    d[i * 2 + j] = (ushort)((spu.Register[r, i] >> (16 - j * 16)) & 0xFFFF);
            return d;
        }

        public static void half_to_reg(SPU spu, int r, ushort[] d)
        {
            int i, j;
            for (i = 0; i < 4; ++i)
            {
                spu.Register[r, i] = 0;
                for (j = 0; j < 2; ++j)
                    spu.Register[r, i] |= ((uint)d[i * 2 + j]) << (16 - j * 16);
            }
        }

        public static byte[] reg_to_Bits(SPU spu, int r)
        {
            byte[] d = new byte[128];
            int i, j;
            for (i = 0; i < 4; ++i)
                for (j = 0; j < 32; ++j)
                {
                    d[i * 32 + j] &= 0x01;
                    d[i * 32 + j] = (byte) ((spu.Register[r, i] >> (31 - j)) & 0x01);
                }
            return d;
        }

        public static void Bits_to_reg(SPU spu, int r, byte[] d)
        {
            int i, j;
            for (i = 0; i < 4; ++i)
            {
                spu.Register[r, i] = 0;
                for (j = 0; j < 32; ++j)
                {
                    d[i * 32 + j] &= 0x01;
                    spu.Register[r, i] |= ((uint) d[i * 32 + j]) << (31 - j);
                }
            }
        }

        public static void float_to_reg(SPU spu, int r, float[] d)
        {
            for(int i = 0; i < 4; i++)
                spu.Register[r, i] = ConversionUtil.byteToUInt(BitConverter.GetBytes(d[i]));
        }

        public static float[] reg_to_float(SPU spu, int r)
        {
            float[] d = new float[4];
            for (int i = 0; i < 4; i++)
                d[i] = BitConverter.ToSingle(ConversionUtil.uintToByte(spu.Register[r, i]), 0);
            return d;
        }

        public static double[] reg_to_double(SPU spu, int r)
        {
            byte[] b = new byte[4];
            byte[] b2 = new byte[4];
            double[] d = new double[2];
            byte[] db = new byte[8];
            b = ConversionUtil.uintToByte(spu.Register[r, 1]);
            b2 = ConversionUtil.uintToByte(spu.Register[r, 0]);
            for (int i = 0; i < 4; i++)
            {
                db[i] = b[i];
                db[i + 4] = b2[i];
            }
            d[0] = BitConverter.ToDouble(db, 0);
            b = ConversionUtil.uintToByte(spu.Register[r, 3]);
            b2 = ConversionUtil.uintToByte(spu.Register[r, 2]);
            for (int i = 0; i < 4; i++)
            {
                db[i] = b[i];
                db[i + 4] = b2[i];
            }
            d[1] = BitConverter.ToDouble(db, 0);
            return d; 
        }

        public static void double_to_reg(SPU spu, int r, double[] d)
        {
            byte[] b;
            byte[] b2 = new byte[4];
            b = BitConverter.GetBytes(d[0]);
            b2[0] = b[4]; b2[1] = b[5]; b2[2] = b[6]; b2[3] = b[7];
            spu.Register[r, 1] = ConversionUtil.byteToUInt(b);
            spu.Register[r, 0] = ConversionUtil.byteToUInt(b2);
            b = BitConverter.GetBytes(d[1]);
            b2[0] = b[4]; b2[1] = b[5]; b2[2] = b[6]; b2[3] = b[7];
            spu.Register[r, 3] = ConversionUtil.byteToUInt(b);
            spu.Register[r, 2] = ConversionUtil.byteToUInt(b2);
        }
    }
}
