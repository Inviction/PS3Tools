using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPU_simulation
{
    public class SPUBreakpoints
    {
        public List<int> CodeBreakPoints;

        //used for StepOver
        public List<int> OneTimeCodeBreakPoints;

        private SPUBreakpoints()
        {
            OneTimeCodeBreakPoints = new List<int>();
            CodeBreakPoints = new List<int>();
        }

        public bool isBreakPoint(int pc)
        {
            return CodeBreakPoints.Contains(pc);
        }

        public bool isOneTimeBreakPoint(int pc, bool killIt)
        {
            if(OneTimeCodeBreakPoints.Contains(pc))
            {
                if(killIt)
                    OneTimeCodeBreakPoints.Remove(pc);
                return true;
            }
            return false;
        }

        private static SPUBreakpoints _instance;

        public static SPUBreakpoints Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SPUBreakpoints();
                return _instance;
            }
        }

    }
}
