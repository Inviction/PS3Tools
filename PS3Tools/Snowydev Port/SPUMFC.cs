using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public class SPUMFC
    {
        public enum Commands
        {
            GET = 0x40
        }

        public uint TagStat, TagID, TagMask;
        public uint LSA, EAH, EAL, Size;
        public Dictionary<uint, Dictionary<uint, byte>> Memory;
        public SPU spu;

        public SPUMFC(SPU spu)
        {
            TagStat = TagID = TagMask = LSA = EAH = EAL = Size = 0;
            this.spu = spu;
            Memory = new Dictionary<uint, Dictionary<uint, byte>>();
        }

        public void WriteMemory(uint EAH, uint EAL, byte[] data)
        {
            if (!Memory.ContainsKey(EAH))
                Memory.Add(EAH, new Dictionary<uint, byte>());
            for (uint i = 0; i < data.Length; i++)
            {
                if (!Memory[EAH].ContainsKey(EAL + i))
                    Memory[EAH].Add(EAL + i, data[i]);
                else
                    Memory[EAH][EAL + i] = data[i];
            }
        }

        public void Command(uint cmd)
        {
            switch (cmd)
            {
                case (uint) Commands.GET:
                    if (Memory.ContainsKey(EAH))
                    {
                        for (uint i = 0; i < Size; i++)
                        {
                            if (Memory[EAH].ContainsKey(EAL + i))
                            {
                                spu.LocalStorage[LSA + i] = Memory[EAH][EAL + i];
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("MFC.GET: not mapped Memory Adress EAH: " + EAH + " (" + EAH.ToString("X") + "), EAL: " + EAL + " (" + EAL.ToString("X") + "), Size: " + Size + " -> LS: " + LSA + "(" + LSA.ToString("X") + ")");
                                return; // Prevent alot Messageboxes
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("MFC.GET: not mapped Memory Adress EAH: " + EAH + " (" + EAH.ToString("X") + "), EAL: " + EAL + " (" + EAL.ToString("X") + "), Size: " + Size + " -> LS: " + LSA + "(" + LSA.ToString("X") + ")");
                    }
                    break;
                default:
                 //   System.Windows.Forms.MessageBox.Show("MFC.CMD: unknown CMD: " + cmd + " (" + cmd.ToString("X") + ")");
                    break;
            }
        }

        public void TagUpdate(uint tag)
        {
            switch (tag)
            {
                case 0:
                    TagStat = TagMask;
                    break;
                case 2:
                    // $MFC_WrTagUpdate channel with a value of 2, it causes the $MFC_RdTagStat to not have a value until the operation is completed.
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("MFC.TagUpdate: Unknown tag for TagUpdate " + tag);
                    break;
            }
        }
    }
}
