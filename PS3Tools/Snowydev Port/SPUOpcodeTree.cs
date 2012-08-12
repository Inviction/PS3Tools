using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public class SPUOpcodeTree
    {
        SPUOpcodeTreeNode Root;
        public SPUOpcodeTree()
        {
            Root = new SPUOpcodeTreeNode(-1);
        }


        public SPUOpcodeTreeNode getTreeNodeFirstLeafByKey(string key)
        {
            SPUOpcodeTreeNode current = Root;
            char[] nodes = key.ToCharArray();
            int depth = 0;
            foreach (char node in nodes)
            {
                if (current.leaf)
                    return current;
                if (node == '0')
                {
                    if (current.child[0] == null)
                        return null;
                    current = current.child[0];
                }
                else if (node == '1')
                {
                    if (current.child[1] == null)
                        return null;
                    current = current.child[1];
                }
                depth++;
            }
            return current;
        }

        public SPUOpcodeTreeNode getTreeNodeByKey(string key)
        {
            SPUOpcodeTreeNode current = Root;
            char[] nodes = key.ToCharArray();
            int depth = 0;
            foreach (char node in nodes)
            {
                if (node == '0')
                {
                    if (current.child[0] == null)
                        current.child[0] = new SPUOpcodeTreeNode(depth);
                    current = current.child[0];
                }
                else if (node == '1')
                {
                    if (current.child[1] == null)
                        current.child[1] = new SPUOpcodeTreeNode(depth);
                    current = current.child[1];
                }
                depth++;
            }
            return current;
        }

        public void setTreeNodeData(string key, string[] data)
        {
            SPUOpcodeTreeNode node = getTreeNodeByKey(key);
            SPUOpcodeTreeNodeData treeData = new SPUOpcodeTreeNodeData();
            treeData.opcode = data[0];
            treeData.mnemonic = data[2];
            treeData.shift = 0;
            treeData.signed = false;
            treeData.size = 128;
            treeData.stop = false;
            treeData.trap = false;
            treeData.type = SPUOpcodeTreeNodeData.typeByString(data[1]);

            for (int i = 3; i < data.Length; i++)
            {
                switch (data[i])
                {
                    case "signed":
                        treeData.signed = true;
                        break;
                    case "shift2":
                        treeData.shift = 2;
                        break;
                    case "shift4":
                        treeData.shift = 4;
                        break;
                    case "stop":
                        treeData.stop = true;
                        break;
                    case "trap":
                        treeData.trap = true;
                        break;
                    case "Bits":
                        treeData.size = 1;
                        break;
                    case "byte":
                        treeData.size = 8;
                        break;
                    case "half":
                        treeData.size = 16;
                        break;
                    case "float":
                        treeData.size = 32;
                        break;
                    case "double":
                        treeData.size = 64;
                        break;
                }
            }

            node.data = treeData;
            node.leaf = true;
        }

        public SPUOpcodeTreeNodeData getTreeNodeData(string key)
        {
            return (getTreeNodeByKey(key)).data;
        }
    }
}
