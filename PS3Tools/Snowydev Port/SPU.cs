using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public class SPU
    {
        public byte[] LocalStorage;
        public uint[,] Register;
        public long LastIP, IP;
        public uint[] SA;
        public SPUCommand[] LocalStorageCommands;
        public Stack<uint> mBox;
        public SPUMFC MFC;
        public bool versionCheck;

        public SPU()
        {
            mBox = new Stack<uint>();
            LocalStorage = new byte[1024 * 256];
            LocalStorageCommands = new SPUCommand[1024 * 256 / 4];
            Register = new uint[128, 4];
            SA = new uint[4];
            MFC = new SPUMFC(this);
            IP = 0;
            versionCheck = false;
        }

        public void buildLocalStorageCommands()
        {
            LoadingScreen ls = new LoadingScreen();
            ls.progressBar1.Maximum = LocalStorage.Length;
            ls.progressBar1.Value = 0;
            ls.label1.Text = "Building SPU Commands out of LocalStorage...";
            ls.Show();
            for (int i = 0; i < LocalStorage.Length - 3; i += 4)
            {
                LocalStorageCommands[i >> 2] = new SPUCommand(LocalStorage, i);
                if ((i & 0xFF) == 0)
                {
                    ls.progressBar1.Value = i;
                    ls.Refresh();
                }
            }
            ls.Hide();
        }

        public uint ReadChannelCount(int ch)
        {
            switch (ch)
            {
                case (int)SPUChannel.SPU_RdInMbox:
                    return (uint) mBox.Count;
                case (int)SPUChannel.MFC_WrTagUpdate:
                case (int)SPUChannel.MFC_RdTagStat:
                case (int)SPUChannel.MFC_RdAtomicStat:
                case (int)SPUChannel.SPU_WrOutMbox: // maybe return 0
                case (int)SPUChannel.SPU_WrOutIntrMbox:
                    return 1;
                default:
                    System.Windows.Forms.MessageBox.Show("rchcnt: Channel unknown " + ch);
                    return 0;
            }
        }

        public void ReadChannel(int ch, int rt)
        {
            uint r = 0;
            SPUDumper.Instance.ChannelDump(ch, rt, 0, false);
            switch (ch)
            {
                case (int)SPUChannel.MFC_RdTagStat:
                    r = MFC.TagStat;
                    break;
                case (int)SPUChannel.SPU_RdInMbox:
                    r = mBox.Pop();
                    break;
                case 73:
                    if (!versionCheck)
                        r = 0x30055;
                    else
                        r = 0x0;
                    versionCheck = !versionCheck;
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("rdch: Channel unknown " + ch);
                    break;
            }
            Register[rt, 0] = r;
            Register[rt, 1] = 0;
            Register[rt, 2] = 0;
            Register[rt, 3] = 0;
        }

        public void WriteChannel(int ch, int ra)
        {
            uint r = Register[ra, 0];
            SPUDumper.Instance.ChannelDump(ch, ra, r, true);
            switch (ch)
            {
                case (int)SPUChannel.MFC_LSA:
                    MFC.LSA = r;
                    break;
                case (int)SPUChannel.MFC_EAH:
                    MFC.EAH = r;
                    break;
                case (int)SPUChannel.MFC_EAL:
                    MFC.EAL = r;
                    break;
                case (int)SPUChannel.MFC_Size:
                    MFC.Size = r;
                    break;
                case (int)SPUChannel.MFC_TagID:
                    MFC.TagID = r;
                    break;
                case (int)SPUChannel.MFC_Cmd:
                    MFC.Command(r);
                    break;
                case (int)SPUChannel.MFC_WrTagMask:
                    MFC.TagMask = r;
                    break;
                case (int)SPUChannel.MFC_WrTagUpdate:
                    MFC.TagUpdate(r);
                    break;
                case (int)SPUChannel.SPU_WrOutMbox:
                    mBox.Push(r);
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("wrch: Channel unknown " + ch);
                    break;
            }
        }
    }
}
