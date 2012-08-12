using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SPU_simulation
{
    public class SPUDumperCmd
    {
        public string Command;
        public string[] Parameter;

        public SPUDumperCmd(string Command, string[] Parameter)
        {
            this.Command = Command;
            this.Parameter = Parameter;
        }

        public uint ParseUInt(string value, SPU spu)
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
            else if (value.StartsWith("r["))
            {
                value = value.Substring(2, value.Length - 3);
                string[] p = value.Split(new string[] { "][" }, StringSplitOptions.RemoveEmptyEntries);
                return spu.Register[ParseUInt(p[0], spu), ParseUInt(p[1], spu)];
            }
            return Convert.ToUInt32(value);
        }

        public string To32BitHex(uint val)
        {
            string str = "00000000" + val.ToString("X");
            return str.Substring(str.Length - 8);
        }

        public string To8BitHex(byte b)
        {
            string str = "00" + b.ToString("X");
            return str.Substring(str.Length - 2);
        }

        public void execute(SPU spu)
        {
            StreamWriter fs = new StreamWriter(new FileStream("spudumper.txt", FileMode.Append));
            switch (Command)
            {
                case "print": // print, 0x12345678, [string]
                    fs.Write(Parameter[0].Replace("\\n", "\r\n"));
                    break;
                case "print_r": // print_r, 0x12345678, [register]
                    uint register = ParseUInt(Parameter[0], spu);
                    uint registerPart = ParseUInt(Parameter[1], spu);
                    fs.Write(To32BitHex(spu.Register[register, registerPart]));
                    break;
                case "print_ls": // print_ls, 0x12345678, [spezial_addr], [spezial_size]
                    uint addr = ParseUInt(Parameter[0], spu);
                    uint size = ParseUInt(Parameter[1], spu);
                    for (uint i = 0; i < size && (i + addr) < spu.LocalStorage.Length; i++)
                    {
                        if ((i & 0xF) == 0x0)
                            fs.Write("0x" + To32BitHex(i + addr) + ": ");
                        fs.Write(To8BitHex(spu.LocalStorage[i + addr]) + " ");
                        if ((i & 0xF) == 0xF)
                            fs.Write("\n");
                    }
                    break;
                case "print_dma": // print_ls, 0x12345678, [spezial_eah], [spezial_eal], [spezial_size]
                    System.Windows.Forms.MessageBox.Show("DMA Dumping not implemented yet.");
                    break;
            }
            fs.Close();
        }
    }
}
