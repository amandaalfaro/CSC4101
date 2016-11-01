// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
    	public If() { }

        // n is the number of spaces to indent
        //bool p: ndicating whether the opening parenthesis has been printed already or not
        public override void print(Node t, int n, bool p)
        {    
            //getting car
            //If
            Node car = t.getCar();

            //getting cdr
            Node cdr = t.getCdr();

            //getting cadr, main condition
            Node cadr = cdr.getCar();

            //getting cddr
            Node cddr = cdr.getCdr();

            //if(condition)
            Node caddr = cddr.getCar();

            //getting cdddr
            Node cdddr = cddr.getCdr();

            //if(!condition)
            Node cadddr = cdddr.getCar();

            //Ending parenthesis
            Node cddddr = cdddr.getCdr();

            //if there is no open parenthesis, print one
            if(!p)
            {
                Console.Write("(");
            }

            //printing if
            car.print(0, true);
            Console.Write(" ");

            //prinintg condition, cadr
            cadr.print(0, false);

            //updating number of spaces for indent
            n++;

            //indent
            SPP.Indent(n);

            //print the true condition inside the if
            caddr.print(0, false);


            //indent
            SPP.Indent(n);

            //print the false condition inside the if
            cadddr.print(0, false);
            
            //decrementing number of spaces for indent
            n--;

            //Print ending right parenthesis
            cddddr.print(n, true);
            Console.WriteLine();

        } 

    }
}

