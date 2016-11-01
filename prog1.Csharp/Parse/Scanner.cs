// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.Collections.Generic;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i) { In = i; }

        private Queue<object> pushbackQueue = new Queue<object>();

        /// <summary>
        /// Reinserts a character back into the input stream.
        /// </summary>
        /// <param name="charToPush">The character to insert back into the input stream</param>
        public void backToInputStream(char charToPush)
        {
            pushbackQueue.Enqueue(charToPush);
        }

        /// <summary>
        /// Reinserts a token back into the input stream.
        /// </summary>
        /// <param name="tokenToPush">The token to insert back into the input stream.</param>
        public void backToInputStream(Token tokenToPush)
        {
            pushbackQueue.Enqueue(tokenToPush);
        }

        /// <summary>
        /// Returns whether a character is a valid beginning of an identifier.
        /// </summary>
        /// <param name="charToCheck">The character to check.</param>
        /// <returns>Returns true if the character meets the rules for forming identifiers in Scheme (http://people.csail.mit.edu/jaffer/r5rs_4.html). Otherwise returns false.</returns>
        bool isValidIdentifier(char charToCheck)
        {
            return !(charToCheck >= '0' && charToCheck <= 32) && ((charToCheck >= 'A' && charToCheck <= 'Z') || (charToCheck >= 'a' && charToCheck <= 'z') || (charToCheck >= '!' && charToCheck <= '/') || (charToCheck >= ':' && charToCheck <= '?') || charToCheck == '^' || charToCheck == '_' || charToCheck == '~');
        }

        /// <summary>
        /// Checks whether a character is a delimeter.
        /// </summary>
        /// <param name="charToCheck">The char to check.</param>
        /// <returns>Returns true if the char is a delimeter. False otherwise.</returns>
        bool isDelimiter(char charToCheck)
        {
            return charToCheck == ';' || char.IsWhiteSpace(charToCheck);
        }

        public Token getNextToken()
        {
            int ch;

            try
            {
                // It would be more efficient if we'd maintain our own
                // input buffer and read characters out of that
                // buffer, but reading individual characters from the
                // input stream is easier.

                if(pushbackQueue.Count > 0) // need to reinsert a character or token back into the scanner
                {
                    object nextChar = pushbackQueue.Dequeue();
                    if(nextChar.GetType() == typeof(char))
                    {
                        ch = (int)pushbackQueue.Dequeue();
                    }
                    else // it's a token
                    {
                        return (Token)nextChar;
                    }
                }
                else
                {
                    ch = In.Read();
                }

                while (char.IsWhiteSpace((char) ch) || ch == ';')
                {
                    // 1. discard white space (space, tab, newline, carriage-return, and form-feed characters)
                    if (char.IsWhiteSpace((char) ch))
                    {
                        ch = In.Read();
                    }

                    // 2. discard comments(everything from a semicolon to the end of the line)
                    if (ch == ';')
                    {
                        In.ReadLine();
                        ch = In.Read();
                    }
                }

                if (ch == -1)
                    return null;
                // Special characters
                else if (ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
                else if (ch == '.')
                    // We ignore the special identifier `...'.
                    return new Token(TokenType.DOT);
                
                // Boolean constants
                else if (ch == '#')
                {
                    ch = In.Read();

                    if (ch == 't')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'f')
                        return new Token(TokenType.FALSE);
                    else if (ch == -1)
                    {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants
                else if (ch == '"')
                {
                    // 6. recognize string constants (anything between double quotes)                    
                    int i;
                    for(i = 0; i < BUFSIZE; i++)
                    {
                        ch = In.Read();
                        if(ch == '"') // reached end of the string constant
                        {
                            break;
                        }
                        buf[i] = (char) ch;
                    }

                    return new StringToken(new String(buf, 0, i));
                }

    
                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {
                    int i = ch - '0';
                    // 5. recognize integer constants (for simplicity only decimal digits without a sign),                    ch = In.Read();                    while (ch >= '0' && ch <= '9')
                    {
                        i = i * 10 + (ch - '0');

                        ch = In.Read();
                        if(!(ch >= '0' && ch <= '9'))
                        {
                            backToInputStream((char)ch);
                        }
                    }

                    // make sure that the character following the integer
                    // is not removed from the input stream
                    return new IntToken(i);
                }
        
                // Identifiers
                else if (isValidIdentifier((char) ch))
                {
                    buf[0] = (char) ch;
                    // 7. recognize identifiers.
                    int i;
                    for(i = 1; i < BUFSIZE; i++)
                    {
                        ch = In.Read();
                        if(isDelimiter((char) ch))
                        {
                            backToInputStream((char)ch);
                            break;
                        }
                        else
                        {
                            buf[i] = (char)ch;
                        }
                    }

                    // make sure that the character following the identifier
                    // is not removed from the input stream

                    return new IdentToken(new String(buf, 0, i));
                }
    
                // Illegal character
                else
                {
                    Console.Error.WriteLine("Illegal input character '"
                                            + (char)ch + '\'');
                    return getNextToken();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }
    }

}

