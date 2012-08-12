using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SPU_simulation
{
    public class FileLoader
    {
        private FileLoader()
        {
        }

        public static int LoadElfPHDR(BinaryReader br, SPU spu, uint phdr_offset, uint i)
        {
            byte[] phdr = new byte[0x20];
            uint offset, paddr, size;

            br.BaseStream.Seek(phdr_offset + 0x20 * i, SeekOrigin.Begin);
            br.Read(phdr, 0, phdr.Length);
            if (ConversionUtil.byteToUInt(phdr) != 1)
                return 1;
            offset = ConversionUtil.byteToUInt(phdr, 0x04);
            paddr = ConversionUtil.byteToUInt(phdr, 0x0C);
            size = ConversionUtil.byteToUInt(phdr, 0x10);

            if ((offset + size) > spu.LocalStorage.Length)
                return 2;
            br.BaseStream.Seek(offset, SeekOrigin.Begin);
            br.Read(spu.LocalStorage, (int)paddr, (int)size);
            return 0;
        }

        public static byte[] LoadBin(string FileName)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(FileName));
            br.BaseStream.Seek(0, SeekOrigin.Begin);

            int len = (int)br.BaseStream.Length;
            byte[] buf = new byte[len];
            br.Read(buf, 0, len);

            br.Close();
            return buf;
        }

        public static void LoadBin(int lsStart, string FileName, SPU spu)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(FileName));
            br.BaseStream.Seek(0, SeekOrigin.Begin);

            int len = (int) br.BaseStream.Length;
            byte[] buf = new byte[len];
            br.Read(buf, 0, len);

            for (int i = 0; i < len; i++)
                spu.LocalStorage[lsStart + i] = buf[i];

            br.Close();
        }

        public static void LoadElf(string FileName, SPU spu, bool setProgramCounter)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(FileName));
            // GetElfHeader
            byte[] elfMagic = new byte[4];
            br.Read(elfMagic, 0, 4);
            if (elfMagic[0] != 0x7F ||
                elfMagic[0] != 0x7F ||
                elfMagic[0] != 0x7F ||
                elfMagic[0] != 0x7F)
            {
                MessageBox.Show("Elf Magic Wrong (" + FileName + ")");
                return;
            }
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            byte[] eHDR = new byte[0x34];
            br.Read(eHDR, 0, eHDR.Length);
            uint phdr_offset = ConversionUtil.byteToUInt(eHDR, 0x1C);
            ushort n_phdrs = ConversionUtil.byteToUShort(eHDR, 0x2C);
            for (ushort i = 0; i < n_phdrs; i++)
            {
                int error = LoadElfPHDR(br, spu, phdr_offset, i);
                if (error == 1)
                    MessageBox.Show("Didn't Load phdr " + i + " of File " + FileName);
                else if (error == 2)
                    MessageBox.Show("Local Storage Overflow!");
            }

            if (setProgramCounter)
                spu.IP = ConversionUtil.byteToUInt(eHDR, 0x18);
            br.Close();
        }

        public static uint ParseUInt(string value)
        {
            if (value.StartsWith("0x"))
            {
                value = value.Substring(2);
                return Convert.ToUInt32(value, 16);
            }
            else if (value.StartsWith("0b"))
            {
                value = value.Substring(2);
                return Convert.ToUInt32(value, 2);
            }
            return Convert.ToUInt32(value);
        }

        public static void LoadScriptFile(string FileName, SPU spu)
        {
            StreamReader fs = new StreamReader(File.OpenRead(FileName));
            string input;
            char[] tokens = { ',' };
            while((input = fs.ReadLine()) != null)
            {
                string unModdedInput = input;
                input = input.Trim();
                input = input.Replace("\t", " ");
                input = input.Replace(" ", "");
                string inputLower = input.ToLower();
                if(input != "")
                {
                    SPUDumperCmd dcmd;
                    string[] token = input.Split(tokens);
                    switch (token[0])
                    {
                        case "r": // r,0,1,0xAFCE
                            int registerSelect = int.Parse(token[1]);
                            int registerByteSelect = int.Parse(token[2]);
                            uint valueSelect = ParseUInt(token[3]);
                            spu.Register[registerSelect, registerByteSelect] = valueSelect;
                            break;
                        case "elf": // elf,blub.elf,true
                            FileLoader.LoadElf(token[1], spu, (token.Length > 2 && token[2] == "true"));
                            break;
                        case "bin":
                            int lsStart = (int) ParseUInt(token[1]);
                            FileLoader.LoadBin(lsStart, token[2], spu);
                            break;
                        case "mfc":
                            byte[] data = FileLoader.LoadBin(token[3]);
                            uint EAH = ParseUInt(token[1]);
                            uint EAL = ParseUInt(token[2]);
                            spu.MFC.WriteMemory(EAH, EAL, data);
                            break;
                        case "ip": // ip,0x400
                            spu.IP = ParseUInt(token[1]);
                            break;
                        case "mBox": // mBox, 0x0000
                            spu.mBox.Push(ParseUInt(token[1]));
                            break;
                        case "print": // print, 0x12345678, [string]
                            string[] printToken = unModdedInput.Split(tokens, 3);
                            dcmd = new SPUDumperCmd(token[0], new string[] { printToken[2] });
                            SPUDumper.Instance.Add(ParseUInt(token[1]), dcmd);
                            break;
                        case "print_r": // print_r, 0x12345678, [register], [register_part]
                        case "print_ls": // print_ls, 0x12345678, [spezial_addr], [spezial_size]
                            dcmd = new SPUDumperCmd(token[0], new string[] { token[2], token[3] });
                            SPUDumper.Instance.Add(ParseUInt(token[1]), dcmd);
                            break;
                        case "print_dma": // print_ls, 0x12345678, [spezial_eah], [spezial_eal], [spezial_size]
                            dcmd = new SPUDumperCmd(token[0], new string[] { token[2], token[3], token[4] });
                            SPUDumper.Instance.Add(ParseUInt(token[1]), dcmd);
                            break;
                        case "setls":
                            byte b = (byte)ParseUInt(token[2]);
                            spu.LocalStorage[ParseUInt(token[1])] = b;
                            break;
                        case "setls32":
                            uint val = ParseUInt(token[2]);
                            uint addr = ParseUInt(token[1]);
                            byte[] bb = ConversionUtil.uintToByte(val);
                            for(int i = 0; i < 4; i++)
                                spu.LocalStorage[addr+i] = bb[i];
                            break;
                    }
                }
            }
            fs.Close();
        }
    }
}
