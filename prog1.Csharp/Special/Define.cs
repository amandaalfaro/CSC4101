// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	    public Define() {}

        private bool isFunction = false;
 
        //n is the number of spaces to indent
        //bool p: indicating whether the opening parenthesis 
        //has been printed already or not
        //cdr second all subsequent items in a list

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

            //getting caddr
            Node caddr = cddr.getCar();

            //getting cdddr
            Node cdddr = cddr.getCdr();

            //if there is no open parenthesis, print one
            if(!p)
            {
                Console.Write("(");
            }

            //printing define
            Console.Write("define ");

            //indent
            SPP.Indent(n);

            //if cons node then set isFunc equal to true
            //define is a function
            if (cadr.isPair())
            {
                isFunction = true;
            }
            cadr.print(0, false);

            //if it's a function indent then print
            if(isFunction)
            {
                caddr.print(n+1, false);
            }
            else
            {
                SPP.Indent(1);
                caddr.print(0, false); //prinitng second parameter
            }

            //Print last right parenthesis
            cdddr.print(n, true);
            Console.WriteLine();
        } 
    }
}


