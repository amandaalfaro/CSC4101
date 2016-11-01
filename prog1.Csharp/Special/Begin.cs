// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        // TODO: Add any fields needed.
 
        // TODO: Add an appropriate constructor.
	    public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            //getting car, begin
            Node car  = t.getCar();

            //getting cdr
            Node cdr  = t.getCdr();
            //getting cadr
            Node cadr = cdr.getCar();

            //getting cddr
            Node cddr = cdr.getCdr();

            if(!p)
            {
                Console.Write("(");
            }

            //printing begin
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
				Console.Write(")");
			}
			else 
            {
				t.getCdr().print(n, true);
			}

            //print ending parenthesis
            cdr.print(0, true);
            Console.WriteLine();


        }
    }
}

