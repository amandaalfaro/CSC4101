// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
        public Set() {}
	
        public override void print(Node t, int n, bool p)
        {
            //getting car, begin
            Node car = t.getCar();

            //getting cdr
            Node cdr = t.getCdr();

            SPP.Indent(n);
            //if there is no open parenthesis, print one
            if(!p)
            {
                Console.Write("(");
            }
            //
            Console.Write("set");

            if(car.isPair())
            {
                car.print(n + 4, true);
            }
           
            //print ending parenthesis
            cdr.print(0, true);          
            Console.WriteLine();


        }
    }
}

