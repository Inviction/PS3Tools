using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Snowydev_Port
{
    public class RAP
    {

        private static byte[] rapKey = {
        (byte) 0x86, (byte) 0x9F, (byte) 0x77, (byte) 0x45,
        (byte) 0xC1, (byte) 0x3F, (byte) 0xD8, (byte) 0x90,
        (byte) 0xCC, (byte) 0xF2, (byte) 0x91, (byte) 0x88,
        (byte) 0xE3, (byte) 0xCC, (byte) 0x3E, (byte) 0xDF
    };
        private static int[] indexTable = {
        (byte) 0x0C, (byte) 0x03, (byte) 0x06, (byte) 0x04,
        (byte) 0x01, (byte) 0x0B, (byte) 0x0F, (byte) 0x08,
        (byte) 0x02, (byte) 0x07, (byte) 0x00, (byte) 0x05,
        (byte) 0x0A, (byte) 0x0E, (byte) 0x0D, (byte) 0x09
    };
        private static byte[] key1 = {
        (byte) 0xA9, (byte) 0x3E, (byte) 0x1F, (byte) 0xD6,
        (byte) 0x7C, (byte) 0x55, (byte) 0xA3, (byte) 0x29,
        (byte) 0xB7, (byte) 0x5F, (byte) 0xDD, (byte) 0xA6,
        (byte) 0x2A, (byte) 0x95, (byte) 0xC7, (byte) 0xA5
    };
        private static byte[] key2 = {
        (byte) 0x67, (byte) 0xD4, (byte) 0x5D, (byte) 0xA3,
        (byte) 0x29, (byte) 0x6D, (byte) 0x00, (byte) 0x6A,
        (byte) 0x4E, (byte) 0x7C, (byte) 0x53, (byte) 0x7B,
        (byte) 0xF5, (byte) 0x53, (byte) 0x8C, (byte) 0x74
    };

        public byte[] getKey(String rapFile)
        {
            BinaryReader raf = new BinaryReader(File.OpenRead(rapFile));
            byte[] read = raf.ReadBytes(0x10);
            //        RandomAccessFile raf = new RandomAccessFile(rapFile, "r");
            //        byte[] read = new byte[0x10];
            //        raf.readFully(read);
            //        raf.close();
            raf.Close();
            byte[] intermediate = new byte[read.Length];
            ToolsImpl.aesecbDecrypt(rapKey, read, 0, intermediate, 0, read.Length);
            for (int loop = 0; loop < 5; loop++)
            {

                for (int loop2 = 0; loop2 < 0x10; loop2++)
                {
                    int index = indexTable[loop2];
                    intermediate[index] = (byte)(intermediate[index] ^ key1[index]);
                }
                for (int loop2 = 0xF; loop2 > 0; loop2--)
                {
                    int index1 = indexTable[loop2];
                    int index2 = indexTable[loop2 - 1];
                    intermediate[index1] = (byte)(intermediate[index1] ^ intermediate[index2]);
                }
                int acum = 0;
                for (int loop2 = 0; loop2 < 0x10; loop2++)
                {
                    int index = indexTable[loop2];
                    byte current = (byte)(intermediate[index] - acum);
                    intermediate[index] = current;
                    if (acum != 1 || current != 0xFF)
                    {
                        int aux1 = current & 0xFF;
                        int aux2 = key2[index] & 0xFF;
                        acum = (aux1 < aux2) ? 1 : 0;
                    }
                    current = (byte)(current - key2[index]);
                    intermediate[index] = current;
                }
            }
            return intermediate;
        }

    }
}
