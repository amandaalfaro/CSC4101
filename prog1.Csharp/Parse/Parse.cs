namespace Parse
{
    public class Parser
    {
        Scanner scanner;
        public Parser(Scanner s)
        {
            scanner = s;
        }

        // building the parse tree
        public Tree.Node parseExp()
        {
            Tokens.Token theToken = scanner.getNextToken();
            if(theToken == null)
            {
                return null;
            }

            Tree.Node expression = null;
            switch(theToken.getType())
            {
                case Tokens.TokenType.LPAREN:
                    {
                        expression = parseRest(true);
                        break;
                    }
                case Tokens.TokenType.FALSE:
                    {
                        expression = Tree.BoolLit.GetInstance(false);
                        break;
                    }
                case Tokens.TokenType.TRUE:
                    {
                        expression = Tree.BoolLit.GetInstance(true);
                        break;
                    }
                case Tokens.TokenType.QUOTE:
                    {
                        expression = new Tree.Cons(new Tree.Ident("'"), new Tree.Cons(parseExp(), null));
                        break;
                    }
                case Tokens.TokenType.INT:
                    {
                        expression = new Tree.IntLit(theToken.getIntVal());
                        break;
                    }
                case Tokens.TokenType.STRING:
                    {
                        expression = new Tree.StringLit(theToken.getStringVal());
                        break;
                    }
                case Tokens.TokenType.IDENT:
                    {
                        expression = new Tree.Ident(theToken.getName());
                        break;
                    }
                case Tokens.TokenType.DOT:
                    {
                        System.Console.WriteLine("Unexpected Expression: '.'");
                        expression = parseExp();
                        break;
                    }
                case Tokens.TokenType.RPAREN:
                    {
                        System.Console.WriteLine("Unexpected Expression: ')'");
                        expression = parseExp();
                        break;
                    }
            }
            return expression;
        }

        private Tree.Node parseRest(bool isFirst)
        {
            Tokens.Token theToken = scanner.getNextToken();
            if(theToken == null)
            {
                return null;
            }

            Tree.Node expression = null;
            switch(theToken.getType())
            {
                case Tokens.TokenType.RPAREN:
                    {
                        if (isFirst)
                        {
                            return Tree.Nil.GetInstance();
                        }
                        else
                        {
                            return null;
                        }
                    }
                case Tokens.TokenType.DOT:
                    {
                        theToken = scanner.getNextToken();
                        if(theToken.getType() != Tokens.TokenType.RPAREN)
                        {
                            scanner.backToInputStream(theToken); // need to reinsert this token so that it is parsed by the cons node
                            expression = new Tree.Cons(parseExp(), null);
                            if(scanner.getNextToken().getType() != Tokens.TokenType.RPAREN)
                            {
                                System.Console.WriteLine("Invalid use of '.'.");
                            }
                            else
                            {
                                ((Tree.Cons)expression).setVararg(); // implement method
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Unexpected symbol: ')'");
                            expression = parseExp();
                        }
                        break;
                    }
                default:
                    {
                        scanner.backToInputStream(theToken);
                        expression = new Tree.Cons(parseExp(), parseRest(false));
                        break;
                    }
            }

            return expression;
        }
    }
}
