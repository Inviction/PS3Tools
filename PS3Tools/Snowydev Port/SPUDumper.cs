using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SPU_simulation
{
    public class SPUDumper
    {
        public Dictionary<uint, List<SPUDumperCmd>> commands;

        private SPUDumper()
        {
            commands = new Dictionary<uint, List<SPUDumperCmd>>();
        }

        public void Add(uint addr, SPUDumperCmd cmd)
        {
            if (!commands.ContainsKey(addr))
                commands.Add(addr, new List<SPUDumperCmd>());
            commands[addr].Add(cmd);
        }

        public void Dump(SPU spu)
        {
            uint addr = (uint) spu.IP;
            if (commands.ContainsKey(addr))
            {
                foreach (SPUDumperCmd cmd in commands[addr])
                    cmd.execute(spu);
            }
        }

        public void CallStackDump(SPU spu, long oldIP, long newIP)
        {
            StreamWriter fs = new StreamWriter(new FileStream("spucallstack.txt", FileMode.Append));

            string newFunc = spu.LocalStorageCommands[(newIP) >> 2].functionName;
            string oldFunc = spu.LocalStorageCommands[(oldIP) >> 2].functionName;
            string oldIPStr = "00000000" + oldIP.ToString("X");
            string newIPStr = "00000000" + newIP.ToString("X");
            oldIPStr = oldIPStr.Substring(oldIPStr.Length - 8);
            newIPStr = newIPStr.Substring(newIPStr.Length - 8);
            string r3Str = "";
            for (int i = 0; i < 4; i++)
            {
                uint r3v = spu.Register[3, 1];
                string r3vStr = "00000000" + r3v.ToString("X");
                r3vStr = r3vStr.Substring(r3vStr.Length - 8);
                if (r3Str != "")
                    r3Str += ", ";
                r3Str += r3v + "(0x" + r3vStr + ")";
            }
            fs.Write("'" + oldFunc + "' 0x" + oldIPStr + " -> '" + newFunc + "' 0x" + newIPStr + " --- r3:" + r3Str + "\n");

            fs.Close();
        }

        public void FunctionFoundDump(int i, int end, string name)
        {
            StreamWriter fs = new StreamWriter(new FileStream("spufunctions.txt", FileMode.Append));
            string iStr = "00000000" + i.ToString("X");
            string endStr = "00000000" + end.ToString("X");
            iStr = iStr.Substring(iStr.Length - 8);
            endStr = endStr.Substring(endStr.Length - 8);

            fs.Write(name + ": " + i + " (0x" + iStr + ") - " + end + " (0x" + endStr + ")\n");

            fs.Close();
        }

        public void ChannelDump(int ch, int rt, uint val, bool write)
        {
            StreamWriter fs = new StreamWriter(new FileStream("spuchannel.txt", FileMode.Append));
            string channel = "" + ch;
            switch (ch)
            {
                case (int)SPUChannel.SPU_RdEventStat:
                    channel = "SPU_RdEventStat";
                    break;
                case (int)SPUChannel.SPU_WrEventMask:
                    channel = "SPU_WrEventMask";
                    break;
                case (int)SPUChannel.SPU_WrEventAck:
                    channel = "SPU_WrEventAck";
                    break;
                case (int)SPUChannel.SPU_RdSigNotify1:
                    channel = "SPU_RdSigNotify1";
                    break;
                case (int)SPUChannel.SPU_RdSigNotify2:
                    channel = "SPU_RdSigNotify2";
                    break;
                case (int)SPUChannel.SPU_WrDec:
                    channel = "SPU_WrDec";
                    break;
                case (int)SPUChannel.SPU_RdDec:
                    channel = "SPU_RdDec";
                    break;
                case (int)SPUChannel.MFC_WrMSSyncReq:
                    channel = "MFC_WrMSSyncReq";
                    break;
                case (int)SPUChannel.SPU_RdEventMask:
                    channel = "SPU_RdEventMask";
                    break;
                case (int)SPUChannel.MFC_RdTagMask:
                    channel = "MFC_RdTagMask";
                    break;
                case (int)SPUChannel.SPU_RdMachStat:
                    channel = "SPU_RdMachStat";
                    break;
                case (int)SPUChannel.SPU_WrSRR0:
                    channel = "SPU_WrSRR0";
                    break;
                case (int)SPUChannel.SPU_RdSRR0:
                    channel = "SPU_RdSRR0";
                    break;
                case (int)SPUChannel.MFC_LSA:
                    channel = "MFC_LSA";
                    break;
                case (int)SPUChannel.MFC_EAH:
                    channel = "MFC_EAH";
                    break;
                case (int)SPUChannel.MFC_EAL:
                    channel = "MFC_EAL";
                    break;
                case (int)SPUChannel.MFC_Size:
                    channel = "MFC_Size";
                    break;
                case (int)SPUChannel.MFC_TagID:
                    channel = "MFC_TagID";
                    break;
                case (int)SPUChannel.MFC_Cmd:
                    channel = "MFC_Cmd";
                    break;
                case (int)SPUChannel.MFC_WrTagMask:
                    channel = "MFC_WrTagMask";
                    break;
                case (int)SPUChannel.MFC_WrTagUpdate:
                    channel = "MFC_WrTagUpdate";
                    break;
                case (int)SPUChannel.MFC_RdTagStat:
                    channel = "MFC_RdTagStat";
                    break;
                case (int)SPUChannel.MFC_RdListStallStat:
                    channel = "MFC_RdListStallStat";
                    break;
                case (int)SPUChannel.MFC_WrListStallAck:
                    channel = "MFC_WrListStallAck";
                    break;
                case (int)SPUChannel.MFC_RdAtomicStat:
                    channel = "MFC_RdAtomicStat";
                    break;
                case (int)SPUChannel.SPU_WrOutMbox:
                    channel = "SPU_WrOutMbox";
                    break;
                case (int)SPUChannel.SPU_RdInMbox:
                    channel = "SPU_RdInMbox";
                    break;
                case (int)SPUChannel.SPU_WrOutIntrMbox:
                    channel = "SPU_WrOutIntrMbox";
                    break;
            }
            if (write)
            {
                fs.Write("r" + rt + "[ " + val + " (0x" + val.ToString("X") + ") ] -> " + channel + "\n");
            }
            else
            {
                fs.Write("r" + rt + " <- " + channel + "\n");
            }
            fs.Close();
        }

        private static SPUDumper _instance;

        public static SPUDumper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SPUDumper();
                return _instance;
            }
        }
    }
}
