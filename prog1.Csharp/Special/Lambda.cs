// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree
{
    public class Lambda : Special
    {
	public Lambda() { }

        public override void print(Node t, int n, bool p)
        {
             //getting car, lambda
            Node car = t.getCar();

            //getting cdr
            Node cdr = t.getCdr();

            //getting cadr, list1
            Node cadr = cdr.getCar();

            //getting cddr
            Node cddr = cdr.getCdr();

            //getting caddr, list2
            Node caddr = cddr.getCar();

            //getting cdddr, nil
            Node cdddr = cddr.getCdr();

            if(!p)
            {
                Console.Write("(");
            }
            Console.Write("lambda");

            // Printing list1
            cadr.print(0, false);
            n++;

            //Indent
            SPP.Indent(n);


            //Printing list2 
            caddr.print(n, false);
            n--;

            //Print last right parenthesis
            cdddr.print(n, true);           
            Console.WriteLine();

  	    }
    }
}

