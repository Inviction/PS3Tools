using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public enum SPUChannel
    {
        SPU_RdEventStat = 0,
        SPU_WrEventMask = 1,
        SPU_WrEventAck = 2,
        SPU_RdSigNotify1 = 3,
        SPU_RdSigNotify2 = 4,
        SPU_WrDec = 7,
        SPU_RdDec = 8,
        MFC_WrMSSyncReq = 9,
        SPU_RdEventMask = 11,
        MFC_RdTagMask = 12,
        SPU_RdMachStat = 13,
        SPU_WrSRR0 = 14,
        SPU_RdSRR0 = 15,
        MFC_LSA = 16,
        MFC_EAH = 17,
        MFC_EAL = 18,
        MFC_Size = 19,
        MFC_TagID = 20,
        MFC_Cmd = 21,
        MFC_WrTagMask = 22,
        MFC_WrTagUpdate = 23,
        MFC_RdTagStat = 24,
        MFC_RdListStallStat = 25,
        MFC_WrListStallAck = 26,
        MFC_RdAtomicStat = 27,
        SPU_WrOutMbox = 28,
        SPU_RdInMbox = 29,
        SPU_WrOutIntrMbox = 30
    }
}
