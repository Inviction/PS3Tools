using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public class SPUOpcodeTreeNode
    {
        public SPUOpcodeTreeNode[] child;
        public SPUOpcodeTreeNodeData data;
        public int depth;
        public bool leaf;

        public SPUOpcodeTreeNode(int depth)
        {
            child = new SPUOpcodeTreeNode[2];
            this.depth = depth;
            leaf = false;
        }
    }
}
