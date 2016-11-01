// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Cond() {}

        public override void print(Node t, int n, bool p)
        { 
            //getting car
            Node car = t.getCar();

            //getting cdr
            Node cdr = t.getCdr();

            //getting cadr
            Node cadr = cdr.getCar();

            //getting cddr
            Node cddr = cdr.getCdr();

            if(!p)
            {
                Console.Write("(");
            }
            Console.Write("cond");
            car.print(n);
            Console.WriteLine();
            if(cdr.isPair())
            {
                cadr.print(0, false);
				Console.WriteLine();
                while ((cdr = cddr) != Nil.GetInstance())
				{
					cadr.print(n, false);
					Console.WriteLine();
				}
				Nil.GetInstance().print(n, true);
			}
			else 
            {
				t.getCdr().print(n, true);
			}
        }
    }
}


