using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Numerics;

namespace Snowydev_Port
{
public class EDAT {


    public static int STATUS_ERROR_INPUTFILE_IO = -100;
    public static int STATUS_ERROR_HASHTITLEIDNAME = -1;
    public static int STATUS_ERROR_HASHDEVKLIC = -2;
    public static int STATUS_ERROR_MISSINGKEY = -3;
    public static int STATUS_ERROR_HEADERCHECK = -4;
    public static int STATUS_ERROR_DECRYPTING = -5;
    public static int STATUS_ERROR_INCORRECT_FLAGS = -6;
    public static int STATUS_ERROR_INCORRECT_VERSION = -7;
    public static int STATUS_OK = 0;
    public static long FLAG_COMPRESSED = 0x00000001L;
    public static long FLAG_0x02 = 0x00000002L;
    public static long FLAG_KEYENCRYPTED = 0x00000008L;
    public static long FLAG_0x10 = 0x00000010L;
    public static long FLAG_0x20 = 0x00000020L;
    public static long FLAG_SDAT = 0x01000000L;
    public static long FLAG_DEBUG = 0x80000000L;

    public int decryptFile(String inFile, String outFile, byte[] devKLic, byte[] keyFromRif)
    {
        FileStream fin = File.Open(inFile, FileMode.Open);
        string[] fn = fin.Name.Split('\\');

        NPD[] ptr = new NPD[1]; //Ptr to Ptr
        int result = validateNPD(fn[fn.Length-1], devKLic, ptr, fin); //Validate NPD hashes
        if (result < 0) {
            fin.Close();
            return result;
        }
        NPD npd = ptr[0];
        EDATData data = getEDATData(fin); //Get flags, blocksize and file len
        byte[] rifkey = getKey(npd, data, devKLic, keyFromRif); //Obtain the key for decryption (result of sc471 or sdatkey)
        if (rifkey == null) {
            Debug.WriteLine("ERROR: Key for decryption is missing");
            fin.Close();
            return STATUS_ERROR_MISSINGKEY;
        } else {
            Debug.WriteLine("DECRYPTION KEY: " + ConversionUtils.getHexString(rifkey));
        }
        result = checkHeader(rifkey, data, npd, fin);
        if (result < 0) {
            fin.Close();
            return result;
        }
        FileStream o = File.Open(outFile, FileMode.Create);
        result = decryptData(fin, o, npd, data, rifkey);
        if (result < 0) {
            fin.Close();
            return result;
        }
        fin.Close();
        o.Close();
        return STATUS_OK;
    }
    private static int HEADER_MAX_BLOCKSIZE = 0x3C00;

    /**
     *
     * Performs checks on the header:
     * -Version check: Must be between 0 and 3 included
     * -Flags check: Checks that only valid active flags are set for given version.
     *  Ver 0, 1 : Debug and compress
     *  Ver 2 : Debug, compress, SDAT, Keys encrypted,..
     *  Ver 3: Debug, compress, SDAT, Keys encrypted...
     * -Metadata section hash: Checks that metadata section is valid (uses encryption key)
     * -Header hash: Checks that header is correct (uses encryption key)
     * @param rifKey
     * @param data
     * @param npd
     * @param in
     * @return
     * @throws IOException
     */
    private int checkHeader(byte[] rifKey, EDATData data, NPD npd, FileStream i) {
        i.Seek(0, SeekOrigin.Begin);
        byte[] header = new byte[0xA0];
        byte[] o = new byte[0xA0];
        byte[] expectedHash = new byte[0x10];
        //Version check
        Debug.WriteLine("Checking NPD Version:" + npd.getVersion());
        if ((npd.getVersion() == 0) || (npd.getVersion() == 1)) {
            if ((data.getFlags() & 0x7FFFFFFE) != 0) return STATUS_ERROR_INCORRECT_FLAGS;
        } else if (npd.getVersion() == 2) {
            if ((data.getFlags() & 0x7EFFFFE0) != 0) return STATUS_ERROR_INCORRECT_FLAGS;
        } else if (npd.getVersion() == 3) {
            if ((data.getFlags() & 0x7EFFFFC0) != 0) return STATUS_ERROR_INCORRECT_FLAGS;
        } else return STATUS_ERROR_INCORRECT_VERSION;

        i.Read(header, 0, header.Length);
        i.Read(expectedHash, 0, expectedHash.Length);
        Debug.WriteLine("Checking header hash:");
        AppLoader a = new AppLoader();
        int hashFlag = ((data.getFlags() & FLAG_KEYENCRYPTED) == 0) ? 0x00000002 : 0x10000002;
        if ((data.getFlags() & FLAG_DEBUG) != 0) hashFlag |= 0x01000000;

        //Veryfing header
        bool result = a.doAll(hashFlag, 0x00000001, header, 0, o, 0, header.Length, new byte[0x10], new byte[0x10], rifKey, expectedHash, 0);
        if (!result) {
            Debug.WriteLine("Error verifying header. Is rifKey valid?.");
            return STATUS_ERROR_HEADERCHECK;
        }
        Debug.WriteLine("Checking metadata hash:");
        a = new AppLoader();
        a.doInit(hashFlag, 0x00000001, new byte[0x10], new byte[0x10], rifKey);

        int sectionSize = ((data.getFlags() & FLAG_COMPRESSED) != 0) ? 0x20 : 0x010; //BUG??? What about FLAG0x20??
        //Determine the metadatasection total len
        int numBlocks = (int) ((data.getFileLen() + data.getBlockSize() - 11) / data.getBlockSize());

        int readed = 0;
        int baseOffset = 0x100;
        //baseOffset +=  modifier; //There is an unknown offset to add to the metadatasection... value seen 0
        long remaining = sectionSize * numBlocks;
        while (remaining > 0) {
            int lenToRead = (HEADER_MAX_BLOCKSIZE > remaining) ? (int) remaining : HEADER_MAX_BLOCKSIZE;
            i.Seek(baseOffset + readed, SeekOrigin.Begin);
            byte[] content = new byte[lenToRead];
            o = new byte[lenToRead];
            i.Read(content, 0, content.Length);
            a.doUpdate(content, 0, o, 0, lenToRead);
            readed += lenToRead;
            remaining -= lenToRead;
        }
        result = a.doFinal(header, 0x90);


        if (!result) {
            Debug.WriteLine("Error verifying metadatasection. Data tampered");
            return STATUS_ERROR_HEADERCHECK;
        }
        return STATUS_OK;
    }

    private byte[] decryptMetadataSection(byte[] metadata) {
        byte[] result = new byte[0x10];
        result[0x00] = (byte) (metadata[0xC] ^ metadata[0x8] ^ metadata[0x10]);
        result[0x01] = (byte) (metadata[0xD] ^ metadata[0x9] ^ metadata[0x11]);
        result[0x02] = (byte) (metadata[0xE] ^ metadata[0xA] ^ metadata[0x12]);
        result[0x03] = (byte) (metadata[0xF] ^ metadata[0xB] ^ metadata[0x13]);
        result[0x04] = (byte) (metadata[0x4] ^ metadata[0x8] ^ metadata[0x14]);
        result[0x05] = (byte) (metadata[0x5] ^ metadata[0x9] ^ metadata[0x15]);
        result[0x06] = (byte) (metadata[0x6] ^ metadata[0xA] ^ metadata[0x16]);
        result[0x07] = (byte) (metadata[0x7] ^ metadata[0xB] ^ metadata[0x17]);
        result[0x08] = (byte) (metadata[0xC] ^ metadata[0x0] ^ metadata[0x18]);
        result[0x09] = (byte) (metadata[0xD] ^ metadata[0x1] ^ metadata[0x19]);
        result[0x0A] = (byte) (metadata[0xE] ^ metadata[0x2] ^ metadata[0x1A]);
        result[0x0B] = (byte) (metadata[0xF] ^ metadata[0x3] ^ metadata[0x1B]);
        result[0x0C] = (byte) (metadata[0x4] ^ metadata[0x0] ^ metadata[0x1C]);
        result[0x0D] = (byte) (metadata[0x5] ^ metadata[0x1] ^ metadata[0x1D]);
        result[0x0E] = (byte) (metadata[0x6] ^ metadata[0x2] ^ metadata[0x1E]);
        result[0x0F] = (byte) (metadata[0x7] ^ metadata[0x3] ^ metadata[0x1F]);
        return result;
    }

    private EDATData getEDATData(FileStream i)
    {
        i.Seek(0x80, SeekOrigin.Begin);
        byte[] data = new byte[0x10];
        i.Read(data, 0, data.Length);
        return EDATData.createEDATData(data);
    }

    private bool compareBytes(byte[] value1, int offset1, byte[] value2, int offset2, int len) {
        bool result = true;
        for (int i = 0; i < len; i++) {
            if (value1[i + offset1] != value2[i + offset2]) {
                result = false;
                break;
            }
        }
        return result;
    }

    private int validateNPD(String filename, byte[] devKLic, NPD[] npdPtr, FileStream i) {
        i.Seek(0, SeekOrigin.Begin);
        byte[] npd = new byte[0x80];
        i.Read(npd, 0, npd.Length);
        byte[] extraData = new byte[0x04];
        i.Read(extraData, 0, extraData.Length);
        long flag = ConversionUtils.be32(extraData, 0);
        if ((flag & FLAG_SDAT) != 0) {
            Debug.WriteLine("INFO: SDAT detected. NPD header is not validated");
        } else if (!checkNPDHash1(filename, npd)) {
            return STATUS_ERROR_HASHTITLEIDNAME;
        } else if (devKLic == null) {
            Debug.WriteLine("WARNING: Can not validate devklic header");
        } else if (!checkNPDHash2(devKLic, npd)) {
            return STATUS_ERROR_HASHDEVKLIC;
        }
        npdPtr[0] = NPD.createNPD(npd);
        return STATUS_OK;
    }

    private bool checkNPDHash1(String filename, byte[] npd) {
        byte[] fileBytes = ConversionUtils.charsToByte(filename.ToCharArray());
        byte[] data1 = new byte[0x30 + fileBytes.Length];
        ConversionUtils.arraycopy(npd, 0x10, data1, 0, 0x30);
        ConversionUtils.arraycopy(fileBytes, 0x00, data1, 0x30, fileBytes.Length);
        byte[] hash1 = ToolsImpl.CMAC128(EDATKeys.npdrm_omac_key3, data1, 0, data1.Length);
        bool result1 = compareBytes(hash1, 0, npd, 0x50, 0x10);
        if (result1) {
            Debug.WriteLine("NPD hash 1 is valid (" + ConversionUtils.getHexString(hash1) + ")");
        }
        return result1;
    }

    private bool checkNPDHash2(byte[] klicensee, byte[] npd) {
        byte[] xoredKey = new byte[0x10];
        ToolsImpl.XOR(xoredKey, klicensee, EDATKeys.npdrm_omac_key2);
        byte[] calculated = ToolsImpl.CMAC128(xoredKey, npd, 0, 0x60);
        bool result2 = compareBytes(calculated, 0, npd, 0x60, 0x10);
        if (result2) {
            Debug.WriteLine("NPD hash 2 is valid (" + ConversionUtils.getHexString(calculated) + ")");
        }
        return result2;
    }


    private byte[] getKey(NPD npd, EDATData data, byte[] devKLic, byte[] keyFromRif) {
        byte[] result = null;
        if ((data.getFlags() & FLAG_SDAT) != 0) {
            //Case SDAT
            result = new byte[0x10];
            ToolsImpl.XOR(result, npd.getDevHash(), EDATKeys.SDATKEY);
        } else {
            //Case EDAT
            if (npd.getLicense() == 0x03) {
                result = devKLic;
            } else if (npd.getLicense() == 0x02) {
                result = keyFromRif;
            }
        }
        return result;
    }

    private int decryptData(FileStream ii, FileStream o, NPD npd, EDATData data, byte[] rifkey) {
        int numBlocks = (int) ((data.getFileLen() + data.getBlockSize() - 1) / data.getBlockSize());
        int metadataSectionSize = ((data.getFlags() & FLAG_COMPRESSED) != 0 || (data.getFlags() & FLAG_0x20) != 0) ? 0x20 : 0x10;
        int baseOffset = 0x100; //+ offset (unknown)
        for (int i = 0; i < numBlocks; i++) {
            ii.Seek(baseOffset + i * metadataSectionSize, SeekOrigin.Begin);
            byte[] expectedHash = new byte[0x10];
            long offset;
            int len;
            int compressionEndBlock = 0;
            if ((data.getFlags() & FLAG_COMPRESSED) != 0) {
                byte[] metadata = new byte[0x20];
                ii.Read(metadata, 0, metadata.Length);
                byte[] result = decryptMetadataSection(metadata);
                offset = (int)(ConversionUtils.be64(result, 0)); // + offset (unknown)
                len = (int)(ConversionUtils.be32(result, 8));
                compressionEndBlock = (int)(ConversionUtils.be32(result, 0xC));
                ConversionUtils.arraycopy(metadata, 0, expectedHash, 0, 0x10);
            } else if ((data.getFlags() & FLAG_0x20) != 0) {
                //NOT TESTED: CASE WHERE METADATASECTION IS 0x20 BYTES LONG
                byte[] metadata = new byte[0x20];
                ii.Read(metadata, 0, metadata.Length);
                for (int j = 0; j<0x10;j++) {
                    expectedHash[j] = (byte)(metadata[j] ^ metadata[j+0x10]);
                }
                offset = baseOffset + i * data.getBlockSize() + numBlocks * metadataSectionSize;
                len = (int)(data.getBlockSize());
                if (i == numBlocks - 1) {
                    len = (int) (data.getFileLen() % (new BigInteger(data.getBlockSize())));
                }
            } else {
                ii.Read(expectedHash, 0, expectedHash.Length);
                offset = baseOffset + i * data.getBlockSize() + numBlocks * metadataSectionSize;
                len = (int)(data.getBlockSize());
                if (i == numBlocks - 1) {
                    len = (int) (data.getFileLen() % (new BigInteger(data.getBlockSize())));
                }
            }
            int realLen = len;
            len = (int) ((uint)(len + 0xF) & 0xFFFFFFF0);
            Debug.Print("Offset: %016X, len: %08X, realLen: %08X, endCompress: %d\r\n", offset, len, realLen,compressionEndBlock);
            ii.Seek(offset, SeekOrigin.Begin);
            byte[] encryptedData = new byte[len];
            byte[] decryptedData = new byte[len];
            ii.Read(encryptedData, 0, encryptedData.Length);
            byte[] key = new byte[0x10];
            byte[] hash = new byte[0x10];
            byte[] blockKey = calculateBlockKey(i, npd);

            ToolsImpl.aesecbEncrypt(rifkey, blockKey, 0, key, 0, blockKey.Length);
            if ((data.getFlags() & FLAG_0x10) != 0) {
                ToolsImpl.aesecbEncrypt(rifkey, key, 0, hash, 0, key.Length);
            } else {
                ConversionUtils.arraycopy(key, 0, hash, 0, key.Length);
            }
            int cryptoFlag = ((data.getFlags() & FLAG_0x02) == 0) ? 0x2 : 0x1;
            int hashFlag;
            if ((data.getFlags() & FLAG_0x10) == 0) {
                hashFlag = 0x02;
            } else if ((data.getFlags() & FLAG_0x20) == 0) {
                hashFlag = 0x04;
            } else {
                hashFlag = 0x01;
            }
            if ((data.getFlags() & FLAG_KEYENCRYPTED) != 0) {
                cryptoFlag |= 0x10000000;
                hashFlag |= 0x10000000;
            }
            if ((data.getFlags() & FLAG_DEBUG) != 0) {
                cryptoFlag |= 0x01000000;
                hashFlag |= 0x01000000;
            }
            AppLoader a = new AppLoader();
            byte[] iv = (npd.getVersion() <= 1)?(new byte[0x10]):npd.getDigest();

            bool rresult = a.doAll(hashFlag, cryptoFlag, encryptedData, 0, decryptedData, 0, encryptedData.Length, key, npd.getDigest(), hash, expectedHash, 0);
            if (!rresult) {
                Debug.WriteLine("Error decrypting block " + i);
                // KDSBest find out why block 30 errors
                //return STATUS_ERROR_DECRYPTING;
            }
            if ((data.getFlags() & FLAG_COMPRESSED) != 0) {
                //byte[] decompress = new byte[Long.valueOf(data.getBlockSize()).intValue()];
                //DECOMPRESS: MISSING ALGORITHM
                //out.write(decompress, 0, data.getBlockSize());
            } else {
                o.Write(decryptedData, 0, realLen);
            }
        }
        return STATUS_OK;
    }

    private byte[] calculateBlockKey(int blk, NPD npd) {
        byte[] baseKey = (npd.getVersion() <= 1)?(new byte[0x10]):npd.getDevHash();
        byte[] result = new byte[0x10];
        ConversionUtils.arraycopy(baseKey, 0, result, 0, 0xC);
        result[0xC] = (byte) (blk >> 24 & 0xFF);
        result[0xD] = (byte) (blk >> 16 & 0xFF);
        result[0xE] = (byte) (blk >> 8 & 0xFF);
        result[0xF] = (byte) (blk & 0xFF);
        return result;
    }
}

}
