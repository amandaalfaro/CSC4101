// Nil -- Parse tree node class for representing the empty list

using System;

namespace Tree
{
    public class Nil : Node
    {
        private static Nil NilInstance;
        private Nil() {}

        public static Nil GetInstance()
        {
            if(NilInstance == null)
            {
                NilInstance = new Nil();
            }
            return NilInstance;
        }
  
        public override void print(int n)
        {
            print(n, false);
        }

        public override void print(int n, bool p)
        {
            SPP.Indent(n);

            if (p)
                Console.WriteLine(")");
            else
                Console.WriteLine("()");
        }

        public override bool isNull()
        {
            return true;
        }
    }
}
