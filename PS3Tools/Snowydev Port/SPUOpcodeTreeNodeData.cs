using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public struct SPUOpcodeTreeNodeData
    {
        public string opcode;
        public string mnemonic;
        public SPUOpcodeType type;
        public bool signed;
        public int size;
        public int shift;
        public bool stop;
        public bool trap;

        public static string getRegisterString(int v)
        {
            if (v == 0)
                return "lr";
            else if (v == 1)
                return "sp";
            else
                return "r" + v;
        }

        public string getChannelString(int v)
        {
            switch (v)
            {
                case 0:
                    return "SPU_RdEventStat";
                case 1:
                    return "SPU_WrEventMask";
                case 2:
                    return "SPU_WrEventAck";
                case 3:
                    return "SPU_RdSigNotify1";
                case 4:
                    return "SPU_RdSigNotify2";
                case 7:
                    return "SPU_WrDec";
                case 8:
                    return "SPU_RdDec";
                case 11:
                    return "SPU_RdEventMask";
                case 13:
                    return "SPU_RdMachStat";
                case 14:
                    return "SPU_WrSRR0";
                case 15:
                    return "SPU_RdSRR0";
                case 28:
                    return "SPU_WrOutMbox";
                case 29:
                    return "SPU_RdInMbox";
                case 30:
                    return "SPU_WrOutIntrMbox";

                // MFC
                case 9:
                    return "MFC_WrMSSyncReq";
                case 12:
                    return "MFC_RdTagMask";
                case 16:
                    return "MFC_LSA";
                case 17:
                    return "MFC_EAH";
                case 18:
                    return "MFC_EAL";
                case 19:
                    return "MFC_Size";
                case 20:
                    return "MFC_TagID";
                case 21:
                    return "MFC_Cmd";
                case 22:
                    return "MFC_WrTagMask";
                case 23:
                    return "MFC_WrTagUpdate";
                case 24:
                    return "MFC_RdTagStat";
                case 25:
                    return "MFC_RdListStallStat";
                case 26:
                    return "MFC_WrListStallAck";
                case 27:
                    return "MFC_RdAtomicStat";
            }
            return "ch" + v;
        }

        public string getParameterString(string key, int pc)
        {
            string result = "";
            int idx = 0;
            string ra = "";
            string rt = "";
            switch (type)
            {
                case SPUOpcodeType.RR:
                    return mnemonic + " " + getRegisterString(ConversionUtil.binStringToInt(key.Substring(25, 7))) + ", " +
                        getRegisterString(ConversionUtil.binStringToInt(key.Substring(18, 7))) + ", " +
                        getRegisterString(ConversionUtil.binStringToInt(key.Substring(11, 7)));
                case SPUOpcodeType.RRR:
                    return mnemonic + " " + getRegisterString(ConversionUtil.binStringToInt(key.Substring(4, 7))) + ", " +
                        getRegisterString(ConversionUtil.binStringToInt(key.Substring(18, 7))) + ", " +
                        getRegisterString(ConversionUtil.binStringToInt(key.Substring(11, 7))) + ", " +
                        getRegisterString(ConversionUtil.binStringToInt(key.Substring(25, 7)));
                case SPUOpcodeType.RI7:
                    rt = getRegisterString(ConversionUtil.binStringToInt(key.Substring(25, 7)));
                    result = mnemonic + " " + rt + ", ";
                    ra = getRegisterString(ConversionUtil.binStringToInt(key.Substring(18, 7)));
                    idx = ConversionUtil.binStringToInt(key.Substring(11, 7));
                    if (signed)
                    {
                        idx <<= 32 - 7;
                        idx >>= 32 - 7;
                    }
                    idx <<= shift;
                    if (mnemonic[0] == 'b' && mnemonic[1] != 'r' && mnemonic[2] != 'a')
                        idx += pc;
                    else if (mnemonic[mnemonic.Length - 1] == 'd')
                        return result + ((idx <= 0) ? "" + idx : "0x" + idx.ToString("X")) + "(" + ra + ")";
                    if (mnemonic == "shlqbyi" && idx == 0)
                        return "lr " + rt + ", " + ra;
                    return result + ra + ", " + ((idx <= 0) ? ""+idx : "0x" + idx.ToString("X"));
                case SPUOpcodeType.RI8:
                    rt = getRegisterString(ConversionUtil.binStringToInt(key.Substring(25, 7)));
                    result = mnemonic + " " + rt + ", ";
                    ra = getRegisterString(ConversionUtil.binStringToInt(key.Substring(18, 7)));
                    idx = ConversionUtil.binStringToInt(key.Substring(10, 8));
                    if (signed)
                    {
                        idx <<= 32 - 8;
                        idx >>= 32 - 8;
                    }
                    idx <<= shift;
                    if (mnemonic[0] == 'b' && mnemonic[1] != 'r' && mnemonic[2] != 'a')
                        idx += pc;
                    else if (mnemonic[mnemonic.Length - 1] == 'd')
                        return result + ((idx <= 0) ? "" + idx : "0x" + idx.ToString("X")) + "(" + ra + ")";
                    return result + ra + ", " + ((idx <= 0) ? "" + idx : "0x" + idx.ToString("X"));
                case SPUOpcodeType.RI10:
                    rt = getRegisterString(ConversionUtil.binStringToInt(key.Substring(25, 7)));
                    result = mnemonic + " " + rt + ", ";
                    ra = getRegisterString(ConversionUtil.binStringToInt(key.Substring(18, 7)));
                    idx = ConversionUtil.binStringToInt(key.Substring(8, 10));
                    if (signed)
                    {
                        idx <<= 32 - 10;
                        idx >>= 32 - 10;
                    }
                    idx <<= shift;
                    if (mnemonic[0] == 'b' && mnemonic[1] != 'r' && mnemonic[2] != 'a')
                        idx += pc;
                    else if (mnemonic[mnemonic.Length - 1] == 'd')
                        return result + ((idx <= 0) ? "" + idx : "0x" + idx.ToString("X")) + "(" + ra + ")";
                    else if (mnemonic == "ori" && idx == 0)
                        return "lr " + rt + ", " + ra;
                    return result + ra + ", " + ((idx <= 0) ? "" + idx : "0x" + idx.ToString("X"));
                case SPUOpcodeType.RI16:
                    rt = getRegisterString(ConversionUtil.binStringToInt(key.Substring(25, 7)));
                    result = mnemonic + " " + rt + ", ";
                    idx = ConversionUtil.binStringToInt(key.Substring(9, 16));
                    if (signed)
                    {
                        idx <<= 32 - 16;
                        idx >>= 32 - 16;
                    }
                    idx <<= shift;
                    if (mnemonic[0] == 'b' && mnemonic[1] != 'r' && mnemonic[2] != 'a')
                        idx += pc;
                    return result + ((idx <= 0) ? "" + idx : "0x" + idx.ToString("X"));
                case SPUOpcodeType.RI18:
                    rt = getRegisterString(ConversionUtil.binStringToInt(key.Substring(25, 7)));
                    result = mnemonic + " " + rt + ", ";
                    idx = ConversionUtil.binStringToInt(key.Substring(7, 18));
                    if (signed)
                    {
                        idx <<= 32 - 16;
                        idx >>= 32 - 16;
                    }
                    idx <<= shift;
                    if (mnemonic[0] == 'b' && mnemonic[1] != 'r' && mnemonic[2] != 'a')
                        idx += pc;
                    return result + ((idx <= 0) ? "" + idx : "0x" + idx.ToString("X"));
                case SPUOpcodeType.Special:
                    return mnemonic;
            }
            return "";
        }

        public static SPUOpcodeType typeByString(string type)
        {
            switch (type)
            {
                case "rr":
                    return SPUOpcodeType.RR;
                case "rrr":
                    return SPUOpcodeType.RRR;
                case "ri7":
                    return SPUOpcodeType.RI7;
                case "ri8":
                    return SPUOpcodeType.RI8;
                case "ri10":
                    return SPUOpcodeType.RI10;
                case "ri16":
                    return SPUOpcodeType.RI16;
                case "ri18":
                    return SPUOpcodeType.RI18;
                case "special":
                    return SPUOpcodeType.Special;
            }
            return SPUOpcodeType.FAIL;
        }
    }
}
