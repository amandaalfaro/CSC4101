// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        Cons cons;
        public Regular()
        {
            this.cons = cons;
        }

        public override void print(Node t, int n, bool p)
        {
            if (!p)
            {
                Console.Write("(");
            }

            if (cons.getCar().GetType() == typeof(Cons) || cons.getCar().GetType() == typeof(Nil)) 
            {
                cons.getCar().print(n, false);
            }
    	    else 
            {
                cons.getCar().print(n, true);
            }

            if (cons.getCdr() != null)
            {
                SPP.Indent(1);
                cons.getCdr().print(n, true);
            }
            else
            {
                Console.Write(")");
            }
        }
    }
}


