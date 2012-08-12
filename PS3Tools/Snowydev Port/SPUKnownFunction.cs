using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SPU_simulation
{
    public class SPUKnownFunction
    {
        public string[] mnemonics;
        public string name;
        LoadingScreen ls;

        public SPUKnownFunction(string path)
        {
            mnemonics = path.Split(("/").ToCharArray());
            name = mnemonics[mnemonics.Length - 1];
            ls = new LoadingScreen();

            FileStream fs = File.OpenRead(path);
            byte[] cmd = new byte[4];
            mnemonics = new string[fs.Length / 4];

            for (int i = 0; i < mnemonics.Length; i++)
            {
                fs.Read(cmd, 0, 4);
                mnemonics[i] = SPUOpcodeTable.Instance.getMnemonic(cmd);
            }
            fs.Close();
        }

        private bool findYourselfCheckDeeper(string[] code, int foundFirstMnemonic)
        {
            for (int ii = 0, iii = foundFirstMnemonic; ii < mnemonics.Length; ii++, iii++)
            {
                while (code[iii].StartsWith("hb") || code[iii] == "nop" || code[iii] == "lnop")
                    iii++;
                while (mnemonics[ii].StartsWith("hb") || mnemonics[ii] == "nop" || mnemonics[ii] == "lnop")
                    ii++;
                if (code[iii] != mnemonics[ii])
                    return false;
            }
            return true;
        }

        private int findYourselfCheckDeeper(SPU spu, int foundFirstMnemonic)
        {
            int ii, iii;
            for (ii = 0, iii = foundFirstMnemonic; ii < mnemonics.Length; ii++, iii++)
            {
                while (spu.LocalStorageCommands[iii].mnemonics.StartsWith("hb") || spu.LocalStorageCommands[iii].mnemonics == "nop" || spu.LocalStorageCommands[iii].mnemonics == "lnop")
                    iii++;
                while (mnemonics[ii].StartsWith("hb") || mnemonics[ii] == "nop" || mnemonics[ii] == "lnop")
                    ii++;
                if (spu.LocalStorageCommands[iii].mnemonics != mnemonics[ii])
                    return -1;
            }
            return iii;
        }

        public void findYourself(SPU spu, LoadingScreen ls)
        {
            for (int i = 0; i < spu.LocalStorageCommands.Length; i++)
            {
                if (spu.LocalStorageCommands[i].mnemonics == mnemonics[0])
                {
                    int end = findYourselfCheckDeeper(spu, i);
                    if (end != -1)
                    {
                        SPUDumper.Instance.FunctionFoundDump(i<<2, end<<2, name);
                        for (; i < end; i++)
                            spu.LocalStorageCommands[i].functionName = name;
                    }
                }
                if ((i & 0xFF) == 0)
                {
                    ls.progressBar1.Value = i;
                    ls.Refresh();
                }
            }
        }

        public int findYourself(string[] code)
        {
            for (int i = 0; i < code.Length - mnemonics.Length + 1; i++)
            {
                if (code[i] == mnemonics[0])
                {
                    if (findYourselfCheckDeeper(code, i))
                        return i;
                }
            }
            return -1;
        }
    }
}
