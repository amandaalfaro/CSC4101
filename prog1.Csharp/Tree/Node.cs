// Node -- Base class for parse tree node objects

namespace Tree
{
    public class Node
    {
        // The argument of print(int) is the number of characters to indent.
        // Every subclass of Node must implement print(int).
        public virtual void print(int n) { }

        // The first argument of print(int, bool) is the number of characters
        // to indent.  It is interpreted the same as for print(int).
        // The second argument is only useful for lists (nodes of classes
        // Cons or Nil).  For all other subclasses of Node, the boolean
        // argument is ignored.  Therefore, print(n,p) defaults to print(n)
        // for all classes other than Cons and Nil.
        // For classes Cons and Nil, print(n,TRUE) means that the open
        // parenthesis was printed already by the caller.
        // Only classes Cons and Nil override print(int, bool).
        // For correctly indenting special forms, you might need to pass
        // additional information to print.  What additional information
        // you pass is up to you.  If you only need one more bit, you can
        // encode that in the sign bit of n. If you need additional parameters,
        // make sure that you define the method print in all the appropriate
        // subclasses of Node as well.
        public virtual void print(int n, bool p)
        {
            print(n);
        }

        // For parsing Cons nodes, for printing trees, and later for
        // evaluating them, we need some helper functions that test
        // the type of a node and that extract some information.

        // TODO: implement these in the appropriate subclasses to return true.
        public virtual bool isBool()   { return false; }  // BoolLit **DONE**
        public virtual bool isNumber() { return false; }  // IntLit **DONE**
        public virtual bool isString() { return false; }  // StringLit **DONE**
        public virtual bool isSymbol() { return false; }  // Ident **DONE**
        public virtual bool isNull()   { return false; }  // Nil **DONE**
        public virtual bool isPair()   { return false; }  // Cons **DONE**

        // TODO: Report an error in these default methods and implement them
        // in class Cons.  After setCar, a Cons cell needs to be `parsed' again
        // using parseList.
        public virtual Node getCar() // **DONE**
        {
            System.Console.Error.WriteLine("Error: argument of 'car' must be a pair.");
            return null;
        } 
        public virtual Node getCdr() // **DONE**
        {
            System.Console.Error.WriteLine("Error: argument of 'cdr' must be a pair.");
            return null;
        } 
        public virtual void setCar(Node a) // **DONE**
        {
            System.Console.Error.WriteLine("Error: argument of 'set-car!' must be a pair.");
        } 
        public virtual void setCdr(Node d) // **DONE**
        {
            System.Console.Error.WriteLine("Error: argument of 'set-cdr!' must be a pair.");
        }
    }
}

