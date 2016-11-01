// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {
	    public Quote()
        {

        }

        public override void print(Node t, int n, bool p)
        {
            SPP.Indent(n);
            t.getCar().print(n);
            Node cdr = t.getCdr();

            Console.Write("(");
            while (!cdr.isNull())
            {
                cdr.getCar().print(n);
                cdr = cdr.getCdr();
                if (!cdr.isNull())
                {
                    SPP.Indent(1);
                }
            }
            Nil.GetInstance().print(n, true);
        }
    }
}

