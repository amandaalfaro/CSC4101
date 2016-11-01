// BoolLit -- Parse tree node class for representing boolean literals

using System;

namespace Tree
{
    public class BoolLit : Node
    {
        private static BoolLit TrueSingleton, FalseSingleton;
        private bool boolVal;
  
        private BoolLit(bool b)
        {
            boolVal = b;
        }

        public static BoolLit GetInstance(bool b)
        {
            if(b)
            {
                if(TrueSingleton == null)
                {
                    TrueSingleton = new BoolLit(true);
                }
                return TrueSingleton;
            }
            else
            {
                if (FalseSingleton == null)
                {
                    FalseSingleton = new BoolLit(false);
                }
                return FalseSingleton;
            }
        }
  
        public override void print(int n)
        {
            SPP.Indent(n);

            if (boolVal)
                Console.WriteLine("#t");
            else
                Console.WriteLine("#f");
        }

        public override bool isBool()
        {
            return true;
        }
    }
}
