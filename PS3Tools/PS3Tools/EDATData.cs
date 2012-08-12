using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Snowydev_Port
{
    public class EDATData
    {
        private long flags;
        private long blockSize;
        private BigInteger fileLen;

        private EDATData()
        {
        }

        public static EDATData createEDATData(byte[] data)
        {
            EDATData result = new EDATData();
            result.flags = ConversionUtils.be32(data, 0);
            result.blockSize = ConversionUtils.be32(data, 4);
            result.fileLen = ConversionUtils.be64(data, 0x8);
            return result;
        }

        public long getBlockSize()
        {
            return blockSize;
        }

        public BigInteger getFileLen()
        {
            return fileLen;
        }

        public long getFlags()
        {
            return flags;
        }
    }
}
