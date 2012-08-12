using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SPU_simulation
{
    public class SPUOpcodeTable
    {
        public SPUOpcodeTree Opcodes;

        public string getMnemonic(byte[] cmd)
        {
            string key = "";
            foreach (byte b in cmd)
            {
                string keyB = "";
                keyB = ((b & 128) != 0) ? "1" : "0";
                keyB += ((b & 64) != 0) ? "1" : "0";
                keyB += ((b & 32) != 0) ? "1" : "0";
                keyB += ((b & 16) != 0) ? "1" : "0";
                keyB += ((b & 8) != 0) ? "1" : "0";
                keyB += ((b & 4) != 0) ? "1" : "0";
                keyB += ((b & 2) != 0) ? "1" : "0";
                keyB += ((b & 1) != 0) ? "1" : "0";
                key += keyB;
            }
            SPUOpcodeTreeNode node = Opcodes.getTreeNodeFirstLeafByKey(key);
            try
            {
                return node.data.mnemonic;
            }
            catch (Exception)
            {
                return "UNKNOWN";
            }
        }

        public SPUCommand getCommand(byte[] cmd, int pc)
        {
            string key = "";
            foreach (byte b in cmd)
            {
                string keyB = "";
                keyB = ((b & 128) != 0) ? "1" : "0";
                keyB += ((b & 64) != 0) ? "1" : "0";
                keyB += ((b & 32) != 0) ? "1" : "0";
                keyB += ((b & 16) != 0) ? "1" : "0";
                keyB += ((b & 8) != 0) ? "1" : "0";
                keyB += ((b & 4) != 0) ? "1" : "0";
                keyB += ((b & 2) != 0) ? "1" : "0";
                keyB += ((b & 1) != 0) ? "1" : "0";
                key += keyB;
            }
            return new SPUCommand(key);
        }
       
        public string getCmdString(byte[] cmd, int pc)
        {
            string key = "";
            foreach (byte b in cmd)
            {
                string keyB = "";
                keyB = ((b & 128) != 0) ? "1" : "0";
                keyB += ((b & 64) != 0) ? "1" : "0";
                keyB += ((b & 32) != 0) ? "1" : "0";
                keyB += ((b & 16) != 0) ? "1" : "0";
                keyB += ((b & 8) != 0) ? "1" : "0";
                keyB += ((b & 4) != 0) ? "1" : "0";
                keyB += ((b & 2) != 0) ? "1" : "0";
                keyB += ((b & 1) != 0) ? "1" : "0";
                key += keyB;
            }
            SPUOpcodeTreeNode node = Opcodes.getTreeNodeFirstLeafByKey(key);
            try
            {
                return node.data.getParameterString(key, pc);
            }
            catch (Exception)
            {
                return "UNKNOWN: " + key;
            }
        }

        private SPUOpcodeTable()
        {
            Opcodes = new SPUOpcodeTree();
            StreamReader sr = new StreamReader(File.OpenRead("opcodes.txt"));
            string line;
            while((line = sr.ReadLine()) != null)
            {
                if (line != "" && (line[0] == '0' || line[0] == '1'))
                {
                    string[] cmdparts = line.Split((",").ToCharArray());
                    Opcodes.setTreeNodeData(cmdparts[0], cmdparts);
                }
            }
        }

        private static SPUOpcodeTable _instance;

        public static SPUOpcodeTable Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SPUOpcodeTable();
                return _instance;
            }
        }

    }
}
