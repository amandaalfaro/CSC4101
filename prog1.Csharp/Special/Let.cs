// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
    	public Let() {}

        public override void print(Node t, int n, bool p)
        {
            //get car
            Node car = t.getCar();

            //get cdr
            Node cdr = t.getCdr();

            //getting cadr
            Node cadr = cdr.getCar();

            //getting cddr
            Node cddr = cdr.getCdr();

            if(!p)
            {
                Console.Write("(");
            }
            Console.Write("let");

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
        }
    }
}


