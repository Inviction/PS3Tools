using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public class SPUCommand
    {
        public string mnemonics;
        public string offsets;
        public string functionName;
        public string fullCommand;
        public SPUOpcodeType type;
        public int rt, ra, rb, rc, idx;
        public SPUOpcodeTreeNode node;

        public SPUCommand(byte[] cmd, int offset)
        {
            string key = ConversionUtil.byteToBinString(cmd, offset);
            key += ConversionUtil.byteToBinString(cmd, offset + 1);
            key += ConversionUtil.byteToBinString(cmd, offset + 2);
            key += ConversionUtil.byteToBinString(cmd, offset + 3);
            node = SPUOpcodeTable.Instance.Opcodes.getTreeNodeFirstLeafByKey(key);
            rt = ra = rb = rc = idx = 0;
            functionName = "";

            try
            {
                fullCommand = node.data.getParameterString(key, offset);
                mnemonics = node.data.mnemonic;
                type = node.data.type;
                parseParameter(key);
            }
            catch (Exception)
            {
                mnemonics = ".byte";
                fullCommand = "";
                type = SPUOpcodeType.FAIL;
            }
        }

        public SPUCommand(string key)
        {
            node = SPUOpcodeTable.Instance.Opcodes.getTreeNodeFirstLeafByKey(key);
            rt = ra = rb = rc = idx = 0;
            try
            {
                mnemonics = node.data.mnemonic;
                type = node.data.type;
                parseParameter(key);
            }
            catch (Exception)
            {
                mnemonics = ".byte";
                type = SPUOpcodeType.Special;
            }
        }

        public void parseParameter(string key)
        {
            switch (type)
            {
                case SPUOpcodeType.RR:
                    rt = ConversionUtil.binStringToInt(key.Substring(25, 7));
                    ra = ConversionUtil.binStringToInt(key.Substring(18, 7));
                    rb = ConversionUtil.binStringToInt(key.Substring(11, 7));
                    break;
                case SPUOpcodeType.RRR:
                    rt = ConversionUtil.binStringToInt(key.Substring(4, 7));
                    ra = ConversionUtil.binStringToInt(key.Substring(18, 7));
                    rb = ConversionUtil.binStringToInt(key.Substring(11, 7));
                    rc = ConversionUtil.binStringToInt(key.Substring(25, 7));
                    break;
                case SPUOpcodeType.RI7:
                    rt = ConversionUtil.binStringToInt(key.Substring(25, 7));
                    ra = ConversionUtil.binStringToInt(key.Substring(18, 7));
                    idx = ConversionUtil.binStringToInt(key.Substring(11, 7));
                    if (node.data.signed)
                    {
                        idx <<= 32 - 7;
                        idx >>= 32 - 7;
                    }
                    idx <<= node.data.shift;
                    break;
                case SPUOpcodeType.RI8:
                    rt = ConversionUtil.binStringToInt(key.Substring(25, 7));
                    ra = ConversionUtil.binStringToInt(key.Substring(18, 7));
                    idx = ConversionUtil.binStringToInt(key.Substring(10, 8));
                    if (node.data.signed)
                    {
                        idx <<= 32 - 8;
                        idx >>= 32 - 8;
                    }
                    idx <<= node.data.shift;
                    break;
                case SPUOpcodeType.RI10:
                    rt = ConversionUtil.binStringToInt(key.Substring(25, 7));
                    ra = ConversionUtil.binStringToInt(key.Substring(18, 7));
                    idx = ConversionUtil.binStringToInt(key.Substring(8, 10));
                    if (node.data.signed)
                    {
                        idx <<= 32 - 10;
                        idx >>= 32 - 10;
                    }
                    idx <<= node.data.shift;
                    break;
                case SPUOpcodeType.RI16:
                    rt = ConversionUtil.binStringToInt(key.Substring(25, 7));
                    idx = ConversionUtil.binStringToInt(key.Substring(9, 16));
                    if (node.data.signed)
                    {
                        idx <<= 32 - 16;
                        idx >>= 32 - 16;
                    }
                    idx <<= node.data.shift;
                    break;
                case SPUOpcodeType.RI18:
                    rt = ConversionUtil.binStringToInt(key.Substring(25, 7));
                    idx = ConversionUtil.binStringToInt(key.Substring(7, 18));
                    if (node.data.signed)
                    {
                        idx <<= 32 - 16;
                        idx >>= 32 - 16;
                    }
                    idx <<= node.data.shift;
                    break;
                case SPUOpcodeType.Special:
                    break;
            }
        }

        public int execute(SPU spu)
        {
            spu.LastIP = spu.IP;
            SPUDumper.Instance.Dump(spu);
            uint addr = 0;
            int t;
            byte[] rab, rbb, rcb, rtb;
            ushort[] rah, rbh, rch, rth;
            uint s;
            int stop = 0;
            ulong r;
            long res;
            int se;
            int b;
            byte[] raB, rbB, rcB, rtB;
            float[] raf, rbf, rcf, rtf;

            if (type == SPUOpcodeType.Special)
            {
                /* 00000000000,special,stop,stop,trap
                {
                if ((opcode & 0xFF00) == 0x2100)
                {
                u32 sel = be32(ctx->ls + ctx->pc + 4);
                u32 arg = sel & 0xFFFFFF;
                sel >>= 24;

                printf("CELL SDK __send_to_ppe(0x%04x, 0x%02x, 0x%06x);\n", opcode & 0xFF, sel, arg);
                } else
                printf("####### stop instruction reached: %08x\n", opcode);
                }
                                 
                                 
                00101000000,rr,stopd,stop,trap
                {
                printf("####### stopd instruction reached\n");
                printf("ra: %08x %08x %08x %08x\n",
                raw[0],
                raw[1],
                raw[2],
                raw[3]);
                printf("rb: %08x %08x %08x %08x\n",
                rbw[0],
                rbw[1],
                rbw[2],
                rbw[3]);
                printf("rc: %08x %08x %08x %08x\n",
                rtw[0],
                rtw[1],
                rtw[2],
                rtw[3]);
                }
                                00000001100,rr,mfspr
                                {
                                printf("########## WARNING #################\n");
                                printf(" mfspr $%d, $%d not implemented!\n", rb, rt);
                                printf("####################################\n");
                                }

                                00100001100,rr,mtspr
                                {
                                printf("########## WARNING #################\n");
                                printf(" mtspr $%d, $%d not implemented!\n", rb, rt);
                                printf("####################################\n");
                                }

                 */
                if (mnemonics == "stop")
                    stop = 1;
                return stop;
            }
            switch (mnemonics)
            {
                case "lqd":
                    addr = (uint)(idx + spu.Register[ra, 0]);
                    SPUCommandHelper.ls2reg(spu, rt, addr);
                    break;
                case "lqx":
                    addr = (uint)(spu.Register[ra, 0] + spu.Register[rb, 0]);
                    SPUCommandHelper.ls2reg(spu, rt, addr);
                    break;
                case "lqa":
                    SPUCommandHelper.ls2reg(spu, rt, (uint) idx);
                    break;
                case "lqr":
                    SPUCommandHelper.ls2reg(spu, rt, (uint)(spu.IP + idx));
                    break;
                case "stqd":
                    SPUCommandHelper.reg2ls(spu, rt, (uint)(spu.Register[ra, 0] + idx));
                    break;
                case "stqx":
                    addr = spu.Register[ra, 0] + spu.Register[rb, 0];
                    SPUCommandHelper.reg2ls(spu, rt, addr);
                    break;
                case "stqa":
                    SPUCommandHelper.reg2ls(spu, rt, (uint) idx);
                    break;
                case "stqr":
                    SPUCommandHelper.reg2ls(spu, rt, (uint)(spu.IP + idx));
                    break;
                case "cbd":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    t = ((int)spu.Register[ra, 0] + idx) & 0xF;
                    for (int i = 0; i < 16; i++)
                        rtb[i] = (byte) ((i == t) ? 0x03 : (i | 0x10));
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "cbx":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rbb = SPUCommandHelper.reg_to_byte(spu, rb);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    t = (int) ((spu.Register[ra, 0] + spu.Register[rb, 0]) & 0xF);
                    for (int i = 0; i < 16; ++i)
                        rtb[i] = (byte) ((i == t) ? 0x03 : (i | 0x10));
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "chd":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    rah = SPUCommandHelper.reg_to_half(spu, ra);
                    t = (int) spu.Register[ra, 0] + idx;
                    t >>= 1;
                    t &= 7;

                    for (int i = 0; i < 8; ++i)
                        rth[i] = (ushort) ((i == t) ? 0x0203 : (i * 2 * 0x0101 + 0x1011));
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "chx":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    rah = SPUCommandHelper.reg_to_half(spu, ra);
                    t = (int)(spu.Register[ra, 0] + spu.Register[rb, 0]);
                    t >>= 1;
                    t &= 7;

                    for (int i = 0; i < 8; ++i)
                        rth[i] = (ushort)((i == t) ? 0x0203 : (i * 2 * 0x0101 + 0x1011));
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "cwd":
                    t = (int) (spu.Register[ra, 0] + idx);
                    t >>= 2;
                    t &= 3;
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint)((i == t) ? 0x00010203 : (0x01010101 * (i * 4) + 0x10111213));
                    break;
                case "cwx":
                    t = (int)(spu.Register[ra, 0] + spu.Register[rb, 0]);
                    t >>= 2;
                    t &= 3;
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint)((i == t) ? 0x00010203 : (0x01010101 * (i * 4) + 0x10111213));
                    break;
                case "cdd":
                    t = (int) (spu.Register[ra, 0] + idx);
                    t >>= 2;
                    t &= 2;
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint) ((i == t) ? 0x00010203 : (i == (t + 1)) ? 0x04050607 : (0x01010101 * (i * 4) + 0x10111213));
                    break;
                case "cdx":
                    t = (int)(spu.Register[ra, 0] + spu.Register[rb, 0]);
                    t >>= 2;
                    t &= 2;
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint)((i == t) ? 0x00010203 : (i == (t + 1)) ? 0x04050607 : (0x01010101 * (i * 4) + 0x10111213));
                    break;
                case "ilh":
                    s = (uint) ((idx << 16) | idx);
                    spu.Register[rt, 0] = s;
                    spu.Register[rt, 1] = s;
                    spu.Register[rt, 2] = s;
                    spu.Register[rt, 3] = s;
                    break;
                case "ilhu":
                    s = (uint)(idx << 16);
                    spu.Register[rt, 0] = s;
                    spu.Register[rt, 1] = s;
                    spu.Register[rt, 2] = s;
                    spu.Register[rt, 3] = s;
                    break;
                case "il":
                case "ila":
                    spu.Register[rt, 0] = (uint)idx;
                    spu.Register[rt, 1] = (uint)idx;
                    spu.Register[rt, 2] = (uint)idx;
                    spu.Register[rt, 3] = (uint)idx;
                    break;
                case "iohl":
                    spu.Register[rt, 0] |= (uint)idx;
                    spu.Register[rt, 1] |= (uint)idx;
                    spu.Register[rt, 2] |= (uint)idx;
                    spu.Register[rt, 3] |= (uint)idx;
                    break;
                case "fsmbi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    for (int i = 0; i < 16; ++i)
                        rtb[i] = (byte) (((idx & (1 << (15 - i))) != 0) ? 0xFF : 0x00);
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "hequi":
                    if (idx == spu.Register[ra, 0])
                        stop = 1;
                    break;
                case "bra":
                    spu.IP = idx - 4;
                    break;
                case "br":
                    spu.IP += idx - 4;
                    break;
                case "brz":
                    if (spu.Register[rt, 0] == 0)
                        spu.IP += idx - 4;
                    break;
                case "brnz":
                    if (spu.Register[rt, 0] != 0)
                        spu.IP += idx - 4;
                    break;
                case "bisl":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = 0;
                    spu.Register[rt, 0] = (uint) (spu.IP + 4);
                    spu.IP = spu.Register[ra, 0] - 4;
                    break;
                case "brsl":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = 0;
                    spu.Register[rt, 0] = (uint)(spu.IP + 4);
                    spu.IP += idx - 4;
                    break;
                case "brasl":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = 0;
                    spu.Register[rt, 0] = (uint)(spu.IP + 4);
                    spu.IP = idx - 4;
                    break;
                case "bihnz":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    if (rth[1] != 0)
                        spu.IP = (spu.Register[ra, 0] << 2) - 4;
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "bihz":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    if (rth[1] == 0)
                        spu.IP = (spu.Register[ra, 0] << 2) - 4;
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "brhnz":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    if (rth[1] != 0)
                        spu.IP += idx - 4;
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "brhz":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    if (rth[1] == 0)
                        spu.IP += idx - 4;
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "bi":
                    spu.IP = spu.Register[ra, 0] - 4;
                    break;
                case "biz":
                    if(spu.Register[rt, 0] == 0)
                        spu.IP = spu.Register[ra, 0] - 4;
                    break;
                case "binz":
                    if (spu.Register[rt, 0] != 0)
                        spu.IP = spu.Register[ra, 0] - 4;
                    break;
                case "ah":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    rah = SPUCommandHelper.reg_to_half(spu, ra);
                    rbh = SPUCommandHelper.reg_to_half(spu, rb);
                    for (int i = 0; i < 8; ++i)
                        rth[i] = (ushort) (rah[i] + rbh[i]);
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "ahi":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    rah = SPUCommandHelper.reg_to_half(spu, ra);
                    for (int i = 0; i < 8; ++i)
                        rth[i] = (ushort)(rah[i] + idx);
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "a":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = spu.Register[ra, i] + spu.Register[rb, i];
                    break;
                case "ai":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint) (spu.Register[ra, i] + idx);
                    break;
                case "sfh":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    rah = SPUCommandHelper.reg_to_half(spu, ra);
                    rbh = SPUCommandHelper.reg_to_half(spu, rb);
                    for (int i = 0; i < 8; ++i)
                        rth[i] = (ushort)(rbh[i] - rah[i]);
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "sfhi":
                    rth = SPUCommandHelper.reg_to_half(spu, rt);
                    rah = SPUCommandHelper.reg_to_half(spu, ra);
                    for (int i = 0; i < 8; ++i)
                        rth[i] = (ushort)(idx - rah[i]);
                    SPUCommandHelper.half_to_reg(spu, rt, rth);
                    break;
                case "sf":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = spu.Register[rb, i] - spu.Register[ra, i];
                    break;
                case "sfi":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint)(idx - spu.Register[ra, i]);
                    break;
                case "addx":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (spu.Register[rt, i] & 1) + spu.Register[ra, i] + spu.Register[rb, i];
                    break;
                case "cg":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint) (((spu.Register[ra, i] + spu.Register[rb, i]) < spu.Register[ra, i]) ? 1 : 0);
                    break;
                case "cgx":
                    for (int i = 0; i < 4; ++i)
                    {
                        r = spu.Register[ra, i] + (spu.Register[rt, i] & 1) + spu.Register[rb, i];
                        spu.Register[rt, i] = (uint)(r >> 32) & 1;
                    }
                    break;
                case "sfx":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint) (spu.Register[rb, i] - spu.Register[ra, i] - (((spu.Register[rt, i] & 1) != 0) ? 0 : 1));
                    break;
                case "bg":
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = (uint) ((spu.Register[ra, i] > spu.Register[rb, i]) ? 0 : 1);
                    break;
                case "bgx":
                    for (int i = 0; i < 4; ++i)
                    {
                        res = (long) (((ulong)spu.Register[ra, i]) - ((ulong)spu.Register[rb, i]) - (ulong)(spu.Register[rt, i] & 1));
                        spu.Register[rt, i] = (uint)((res < 0) ? 1 : 0);
                    }
                    break;
                case "mpyu":
                    for (int i = 0; i < 4; ++i)
                    {
                        spu.Register[rt, i] = (uint)((spu.Register[ra, i] & 0xFFFF) * (spu.Register[rb, i] & 0xFFFF));
                    }
                    break;
                case "mpyi":
                    for (int i = 0; i < 4; ++i)
                    {
                        spu.Register[rt, i] = (uint)((spu.Register[ra, i] & 0xFFFF) * idx);
                    }
                    break;
                case "mpyui":
                    idx &= 0xFFFF;
                    for (int i = 0; i < 4; ++i)
                    {
                        spu.Register[rt, i] = (uint)((spu.Register[ra, i] & 0xFFFF) * idx);
                    }
                    break;
                case "mpyh":
                    idx &= 0xFFFF;
                    for (int i = 0; i < 4; ++i)
                    {
                        spu.Register[rt, i] = (uint)(((spu.Register[ra, i] >> 16) * (spu.Register[rb, i] & 0xFFFF)) << 16);
                    }
                    break;
                case "mpys":
                    for (int i = 0; i < 4; ++i)
                    {
                        se = (int) (((spu.Register[ra, i] & 0xFFFF) * (spu.Register[rb, i] & 0xFFFF)) >> 16);
                        se <<= 32 - 16;
                        se >>= 32 - 16;
                        spu.Register[rt, i] = (uint) se;
                    }
                    break;
                case "mpyhh":
                    for (int i = 0; i < 4; ++i)
                    {
                        spu.Register[rt, i] = (uint)((spu.Register[ra, i] >> 16) * (spu.Register[rb, i] >> 16));
                    }
                    break;
                case "mpyhhu":
                    for (int i = 0; i < 4; ++i)
                    {
                        spu.Register[rt, i] = (uint)((spu.Register[ra, i] >> 16) * (spu.Register[rb, i] >> 16));
                    }
                    break;
                case "clz":
                    for (int i = 0; i < 4; ++i)
                    {
                        for (b = 0; b < 32; ++b)
                            if ((spu.Register[ra, i] & (1 << (31 - b))) != 0)
                                break;
                        spu.Register[rt, i] = (uint) b;
                    }
                    break;
                case "fsm":
                    for (int i = 0; i < 4; ++i)
                    {
                        spu.Register[rt, i] = ((spu.Register[ra, 0] & (8 >> i)) != 0) ? 0xFFFFFFFF : 0;
                    }
                    break;
                case "gbb":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    for (int i = 0; i < 16; ++i)
                        rtb[i] = 0;
                    for (int i = 0; i < 16; ++i)
                        rtb[2 + (i / 8)] |= (byte) ((rab[i]&1) << ((~i)&7));
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "gb":
                    spu.Register[rt, 0] = ((spu.Register[ra, 0] & 1) << 3) | ((spu.Register[ra, 1] & 1) << 2) | ((spu.Register[ra, 2] & 1) << 1) | (spu.Register[ra, 3] & 1);
                    spu.Register[rt, 1] = spu.Register[rt, 2] = spu.Register[rt, 3] = 0;
                    break;
                case "ori":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (uint) ((int) spu.Register[ra, i] | idx);
                    break;
                case "shlqbi":
                    rtB = SPUCommandHelper.reg_to_Bits(spu, rt);
                    raB = SPUCommandHelper.reg_to_Bits(spu, ra);
                    int shift_count = (int) spu.Register[rb, 0] & 7;
                    for(int i = 0; i < 128; i++)
                    {
                        if(i + shift_count < 128)
                            rtB[i] = raB[i + shift_count];
                        else
                            rtB[i] = 0;
                    }
                    SPUCommandHelper.Bits_to_reg(spu, rt, rtB);
                    break;
                case "shlqbyi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    idx &= 0x1F;

                    for (int i = 0; i < 16; i++)
                        rtb[i] = (byte) ((i + idx) >= 16 ? 0 : rab[i + idx]);
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "rotqby":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rbb = SPUCommandHelper.reg_to_byte(spu, rb);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    int shift = (int) (spu.Register[rb, 0] & 0xF);

                    for(int i = 0; i < 16; i++)
                        rtb[i] = rab[(i + shift) & 15];
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "ceqi":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (spu.Register[ra, i] == (uint) idx) ? 0xFFFFFFFF : 0x0;
                    break;
                case "ceq":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (spu.Register[ra, i] == spu.Register[rb, i]) ? 0xFFFFFFFF : 0x0;
                    break;
                case "ceqbi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    for(int i = 0; i < 16; i++)
                        rtb[i] = (byte) ((rab[i] == (idx & 0xFF)) ? 0xFF : 0x00);
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "xsbh":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    for (int i = 0; i < 16; i += 2)
                    {
                        rtb[i] = (byte) (((rab[i + 1] & 0x80) != 0) ? 0xFF : 0);
                        rtb[i + 1] = rab[i + 1];
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "hbra":
                case "hbrr":
                    break;
                case "shufb":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rcb = SPUCommandHelper.reg_to_byte(spu, rc);
                    rbb = SPUCommandHelper.reg_to_byte(spu, rb);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    for (int i = 0; i < 16; i++)
                    {
                        if ((rcb[i] & 0xC0) == 0x80)
                            rtb[i] = 0;
                        else if ((rcb[i] & 0xE0) == 0xC0)
                            rtb[i] = 0xFF;
                        else if ((rcb[i] & 0xE0) == 0xE0)
                            rtb[i] = 0x80;
                        else
                        {
                            b = rcb[i] & 0x1F;
                            if (b < 16)
                                rtb[i] = rab[b];
                            else
                                rtb[i] = rbb[b-16];
                        }
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "and":
                    for (int i = 0; i < 4; i++)
                        spu.Register[rt, i] = spu.Register[ra, i] & spu.Register[rb, i];
                    break;
                case "andi":
                    for (int i = 0; i < 4; i++)
                        spu.Register[rt, i] = spu.Register[ra, i] & (uint) idx;
                    break;
                case "rchcnt":
	                for (int i = 1; i < 4; ++i)
		                spu.Register[rt, i] = 0;

                    spu.Register[rt, 0] = spu.ReadChannelCount(ra);
                    break;
                case "rdch":
                    spu.ReadChannel(ra, rt);
                    break;
                case "wrch":
                    spu.WriteChannel(ra, rt);
                    break;
                case "or": //rotqmbyi rotqbyi
                    for (int i = 0; i < 4; ++i)
                        spu.Register[rt, i] = spu.Register[ra, i] | spu.Register[rb, i];
                    break;
                case "rotqbyi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    idx &= 0x1F;

                    for(int i = 0; i < 16; i++)
                        rtb[i] = rab[(i + idx) & 15];
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "rotqmbyi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    shift_count = (-idx) & 0x1F;

                    for (int i = 0; i < 16; i++)
                    {
                        if (i >= shift_count)
                            rtb[i] = rab[i - shift_count];
                        else
                            rtb[i] = 0;
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "rotqmbybi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rbb = SPUCommandHelper.reg_to_byte(spu, rb);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    shift_count = (0 - ((rbb[3]) >> 3)) & 0x1f;

                    for (int i = 0; i < 16; i++)
                    {
                        if (i >= shift_count)
                            rtb[i] = rab[i - shift_count];
                        else
                            rtb[i] = 0;
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "xswd":
                    spu.Register[rt, 0] = ((spu.Register[ra, 1] & 0x80000000) != 0) ? 0xFFFFFFFF : 0;
                    spu.Register[rt, 1] = spu.Register[ra, 1];
                    spu.Register[rt, 2] = ((spu.Register[ra, 3] & 0x80000000) != 0) ? 0xFFFFFFFF : 0;
                    spu.Register[rt, 3] = spu.Register[ra, 3];
                    break;
                case "cgti":
                    for (int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (((int)spu.Register[ra, i]) > ((int)idx)) ? 0xFFFFFFFF : 0;
                    break;
                case "clgti":
                    for (int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (spu.Register[ra, i] > idx) ? 0xFFFFFFFF : 0;
                    break;
                case "shlqby":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rbb = SPUCommandHelper.reg_to_byte(spu, rb);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    shift = (int) (spu.Register[rb, 0] & 0x1F);

                    for(int i = 0; i < 16; i++)
                        rtb[i] = ((i+shift) < 16) ? rab[i+shift] : (byte) 0;
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "selb":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (spu.Register[rc, i] & spu.Register[rb, i]) | ((~spu.Register[rc, i]) & spu.Register[ra, i]);
                    break;
                case "shli":
                    shift_count = idx & 0x3F;
                    for(int i = 0; i < 4; i++)
                    {
                        if (shift_count > 31)
                            spu.Register[rt, i] = 0;
                        else
                            spu.Register[rt, i] = spu.Register[ra, i] << shift_count;
                    }
                    break;
                case "clgt":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (spu.Register[ra, i] > spu.Register[rb, i]) ? 0xFFFFFFFF : 0;
                    break;
                case "rotm":
                    for(int i = 0; i < 4; i++)
                    {
                        shift_count = (-(int) spu.Register[rb, i]) & 63;
                        if (shift_count < 32)
                            spu.Register[rt, i] = spu.Register[ra, i] >> shift_count;
                        else
                            spu.Register[rt, i] = 0;
                    }
                    break;
                case "nor":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = ~(spu.Register[ra, i] | spu.Register[rb, i]);
                    break;
                case "shl":
                    for(int i = 0; i < 4; i++)
                    {
                        shift_count = (int) spu.Register[rb, i] & 0x3f;
                        if (shift_count > 31)
                            spu.Register[rt, i] = 0;
                        else
                            spu.Register[rt, i] = spu.Register[ra, i] << shift_count;
                    }
                    break;
                case "rotqmby":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rbb = SPUCommandHelper.reg_to_byte(spu, rb);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);
                    s = (uint) (-((int) spu.Register[rb, 0]) & 0x1F);

                    for(int i = 0; i < 16; i++)
                    {
                        if(i >= s)
                            rtb[i] = rab[i - s];
                        else
                            rtb[i] = 0;
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "andbi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);

                    for(int i = 0; i < 16; i++)
                    {
                        rtb[i] = (byte) (rab[i] & (byte)idx);
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "rotmi":
                    shift_count = (-idx) & 63;
                    for(int i = 0; i < 4; i++)
                    {
                        if(shift_count < 32)
                            spu.Register[rt, i] = spu.Register[ra, i] >> shift_count;
                        else
                            spu.Register[rt, i] = 0;
                    }
                    break;
                case "rotmai":
                    shift_count = (-idx) & 63;
                    for(int i = 0; i < 4; i++)
                    {
                        if(shift_count < 32)
                            spu.Register[rt, i] = (uint)((int)spu.Register[ra, i] >> shift_count);
                        else
                            spu.Register[rt, i] = (uint) ((int)spu.Register[ra, i] >> 31);
                    }
                    break;
                case "clgtbi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);

                    for(int i = 0; i < 16; i++)
                    {
                        rtb[i] = (rab[i] > (byte)idx) ? (byte) 0xFF : (byte) 0x00;
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "andc":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = spu.Register[ra, i] &~ spu.Register[rb, i];
                    break;
                case "xor":
                    for(int i = 0; i < 4; i++)
                    {
                        spu.Register[rt, i] = spu.Register[ra, i] ^ spu.Register[rb, i];
                    }
                    break;
                case "xorbi":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);

                    for(int i = 0; i < 16; i++)
                    {
                        rtb[i] = (byte) (rab[i] ^ (byte)idx);
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "shlqbii":
                    rtB = SPUCommandHelper.reg_to_Bits(spu, rt);
                    raB = SPUCommandHelper.reg_to_Bits(spu, ra);
                    shift_count = idx & 7;
                    for(int i = 0; i < 128; i++)
                    {
                        if(i + shift_count < 128)
                            rtB[i] = raB[i + shift_count];
                        else
                            rtB[i] = 0;
                    }
                    SPUCommandHelper.Bits_to_reg(spu, rt, rtB);
                    break;
                case "rotqmbii":
                    rtB = SPUCommandHelper.reg_to_Bits(spu, rt);
                    raB = SPUCommandHelper.reg_to_Bits(spu, ra);
                    shift_count = -idx & 7;
                    for(int i = 0; i < 128; i++)
                    {
                        if(i >= shift_count)
                            rtB[i] = raB[i - shift_count];
                        else
                            rtB[i] = 0;
                    }
                    SPUCommandHelper.Bits_to_reg(spu, rt, rtB);
                    break;
                case "rotqbii":
                    rtB = SPUCommandHelper.reg_to_Bits(spu, rt);
                    raB = SPUCommandHelper.reg_to_Bits(spu, ra);
                    shift_count = idx & 7;
                    for(int i = 0; i < 128; i++)
                    {
                        rtB[i] = raB[(i + shift_count) & 127];
                    }
                    SPUCommandHelper.Bits_to_reg(spu, rt, rtB);
                    break;
                case "fms":
                    rtf = SPUCommandHelper.reg_to_float(spu, rt);
                    rcf = SPUCommandHelper.reg_to_float(spu, rc);
                    rbf = SPUCommandHelper.reg_to_float(spu, rb);
                    raf = SPUCommandHelper.reg_to_float(spu, ra);

                    for(int i = 0; i < 4; i++)
                        rtf[i] = (raf[i] * rbf[i]) - rcf[i];

                    SPUCommandHelper.float_to_reg(spu, rt, rtf);
                    break;
                case "cgt":
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (((int)spu.Register[ra, i]) > ((int)spu.Register[rb, i])) ? 0xFFFFFFFF : 0x0;
                    break;
                case "ceqb":
                    rtb = SPUCommandHelper.reg_to_byte(spu, rt);
                    rbb = SPUCommandHelper.reg_to_byte(spu, rb);
                    rab = SPUCommandHelper.reg_to_byte(spu, ra);

                    for(int i = 0; i < 16; i++)
                    {
                        rtb[i] = (rab[i] == rbb[i]) ? (byte) 0xFF : (byte) 0x00;
                    }
                    SPUCommandHelper.byte_to_reg(spu, rt, rtb);
                    break;
                case "roti":
                    shift_count = idx & 0x1F;
                    for(int i = 0; i < 4; i++)
                        spu.Register[rt, i] = (spu.Register[ra, i] << shift_count) | (spu.Register[ra, i] >> (32 - shift_count));
                    break;
                case "rotqmbi":
                    rtB = SPUCommandHelper.reg_to_Bits(spu, rt);
                    raB = SPUCommandHelper.reg_to_Bits(spu, ra);
                    shift_count = (- (int) spu.Register[rb, 0]) & 7;
                    for(int i = 0; i < 128; i++)
                    {
                        if( i >= shift_count)
                            rtB[i] = raB[i - shift_count];
                        else
                            rtB[i] = 0;
                    }
                    SPUCommandHelper.Bits_to_reg(spu, rt, rtB);
                    break;

                /*
01010101110,rr,xshw,half
{
int i;
for (i = 0; i < 8; i += 2)
{
rth[i] = (rah[i + 1] & 0x8000) ? 0xFFFF : 0;
rth[i + 1] = rah[i + 1];
}
}

00010101,ri10,andhi,half,signed
{
int i;
for (i = 0; i < 8; ++i)
rth[i] &= i10;
}

01011001001,rr,orc
{
int i;
for (i = 0; i < 4; ++i)
rtw[i] = raw[i] |~ rbw[i];
}

01000100,ri10,xori,signed
{
int i;
for (i = 0; i < 4; ++i)
rtw[i] = raw[i] ^ i10;
}

01001001001,rr,eqv

# shift and rotate instruction

00111001111,rr,shlqbybi,byte
{
int i;
int shift_count = ((rbb[3])>>3) & 0x1f;

for (i = 0; i < 16; ++i)
{
if ((i + shift_count) < 16)
rtb[i] = rab[i + shift_count];
else
rtb[i] = 0;
}
}

00001111101,ri7,rothmi,half
{
int shift_count = (-i7) & 31;
int i;
for (i = 0; i < 8; ++i)
if (shift_count < 16)
rth[i] = rah[i] >> shift_count;
else
rth[i] = 0;
}

000001011010,rr,rotma
{
int i,shift_count;
for (i = 0; i < 4; ++i){
shift_count = (-rbw[i]) & 63;
if (shift_count < 32)
rtw[i] = ((s32)raw[i]) >> shift_count;
else
rtw[i] = ((s32)raw[i]) >> 31;
}
}

# compare, branch and halt instructions

01111101,ri10,ceqhi,signed,half
{
int i;

for (i = 0; i < 8; ++i)
rth[i] = -(rah[i] == i10);
}
01111001000,rr,ceqh,half
{
int i;

for (i = 0; i < 8; ++i)
rth[i] = -(rah[i] == rbh[i]);
}

01011010000,rr,clgtb,byte
{
int i;
for (i = 0; i < 16; ++i)
rtb[i] = -(rab[i] > rbb[i]);
}

01001101,ri10,cgthi,signed,half
{
int i;
for (i = 0; i < 8; ++i)
rth[i] = -(((s16)rah[i]) > ((s16)i10));
}

01011101,ri10,clgthi,signed,half
{
int i;
for (i = 0; i < 8; ++i)
rth[i] = -(rah[i] > i10);
}
01011001000,rr,clgth,half
{
int i;
for (i = 0; i < 8; ++i)
rth[i] = -(rah[i] > rbh[i]);
}

01011000100,rr,fa,float
{
int i;
for (i = 0; i < 4; ++i)
rtf[i] = raf[i] + rbf[i];
}
01011000101,rr,fs,float
{
int i;
for (i = 0; i < 4; ++i)
rtf[i] = raf[i] - rbf[i];
}
01011000110,rr,fm,float
{
int i;
for (i = 0; i < 4; ++i)
rtf[i] = raf[i] * rbf[i];

}
1110,rrr,fma,float
{
int i;
for (i = 0; i < 4; ++i)
rtf[i] = rcf[i] + (raf[i] * rbf[i]);
}

1101,rrr,fnms,float
{
int i;
for (i = 0; i < 4; ++i)
rtf[i] = rcf[i] - (raf[i] * rbf[i]);
}
01011000010,rr,fcgt,float
{
int itrue = 0xffffffff;
float ftrue = *(float*)&itrue;
int i;
for (i = 0; i < 4; ++i)
rtf[i] = raf[i]>rbf[i]?ftrue:0.0;

}
01111000010,rr,fceq,float
{
//note: floats are not accurately modeled!
int i;
int itrue = 0xffffffff;
float ftrue = *(float*)&itrue;
for (i = 0; i < 4; ++i)
rtf[i] = raf[i]==rbf[i]?ftrue:0.0;
}
00110111000,rr,frest,float
{
int i;
// not quite right, but assume this is followed by a 'fi'
for (i = 0; i < 4; ++i)
rtf[i] = 1/raf[1];
}
00110111001,rr,frsqest,float
{
int i;
// not quite right, but assume this is followed by a 'fi'
for (i = 0; i < 4; ++i)
rtf[i] = 1/sqrt(fabs(raf[i]));
}
01111010100,rr,fi,float
{
int i;
// not quite right, but assume this was preceeded by a 'fr*est'
for (i = 0; i < 4; ++i)
rtf[i] = rbf[i];
}
01110111000,rr,fesd,float
{
double result = raf[0];
float *tmp = (float*)&result;
rtf[0] = tmp[1];
rtf[1] = tmp[0];
result = raf[2];
rtf[2] = tmp[1];
rtf[3] = tmp[0];
}
01110111001,rr,frds,double
{
double dtmp = rad[0];
float ftmp = dtmp;
float *ptmp = (float*)&dtmp;
ptmp[1] = ftmp;
ptmp[0] = 0.0;
rtd[0]=dtmp;

dtmp = rad[1];
ftmp = dtmp;
ptmp[0] = ftmp;
ptmp[1] = 0.0;
rtd[1] = dtmp;
}

0111011010,ri8,csflt,float
{
int i;
for (i = 0; i < 4; ++i){
int val = raw[i];
// let's hope the x86s float HW does the right thing here...
rtf[i] = (float)val;
}
}
0111011000,ri8,cflts,float
{
int i;
for (i = 0; i < 4; ++i)
rtw[i] = (int)raf[i];
}
0111011001,ri8,cfltu,float
{
int i;
for (i = 0; i < 4; ++i)
rtw[i] = (u32)raf[i];
}
01011001110,rr,dfm,double
{
rtd[0] = rad[0]*rbd[0];
rtd[1] = rad[1]*rbd[1];
}
01011001101,rr,dfs,double
{
rtd[0] = rad[0]-rbd[0];
rtd[1] = rad[1]-rbd[1];
}
01011001100,rr,dfa,double
{
rtd[0] = rad[0]+rbd[0];
rtd[1] = rad[1]+rbd[1];
}
01101011101,rr,dfms,double
{
rtd[0] = (rad[0]*rbd[0])-rtd[0];
rtd[1] = (rad[1]*rbd[1])-rtd[1];
}
01101011110,rr,dfnms,double
{
// almost same as dfms. This is missing in SPU_ISA_1.2. See v.1.1
rtd[0] = (rad[0]*rbd[0])-rtd[0];
rtd[1] = (rad[1]*rbd[1])-rtd[1];
}
01101011100,rr,dfma,double
{
rtd[0] = (rad[0]*rbd[0])+rtd[0];
rtd[1] = (rad[1]*rbd[1])+rtd[1];
}

NOT IMPLEMENTED:
01111000100,rr,mpy
1100,rrr,mpya
01101000110,rr,mpyhha
01101001110,rr,mpyhhau
01010110100,rr,cntb
00110110110,rr,fsmb
00110110101,rr,fsmh
00110110001,rr,gbh
00011010011,rr,avgb
00001010011,rr,absdb
01001010011,rr,sumb
01000101,ri10,xorhi
00011001001,rr,nand
00111110000,rr,orx
00000110,ri10,orbi
00000101,ri10,orhi
                 */
                default:
                    System.Windows.Forms.MessageBox.Show(mnemonics + " not implemented yet!");
                    return 3;
            }
            if (spu.LastIP != spu.IP)
            {
                string newFunc = spu.LocalStorageCommands[(spu.IP + 4) >> 2].functionName;
                string oldFunc = spu.LocalStorageCommands[(spu.LastIP) >> 2].functionName;
                if (newFunc != oldFunc)
                {
                    SPUDumper.Instance.CallStackDump(spu, spu.LastIP, spu.IP + 4);
                }
            }
            return stop;
        }
    }
}
