using System;
namespace Lab01_PabloCuevas_1239719
{
    public class Scanner
    {
        private string _regexp = "";
        private int _index = 0;

        public Scanner(string regexp)
        {
            _regexp = regexp + (char)TokenType.EOF;
            _index = 0;
        }

        public Token GetToken()
        {
            Token result = new Token() { Value = ((char)0).ToString() };
            bool tokenFound = false;
            while (!tokenFound)
            {
                char peek = _regexp[_index];

                // Whitespace removal
                while (char.IsWhiteSpace(peek))
                {
                    _index++;
                    peek = _regexp[_index];
                }

                switch (peek)
                {
                    case (char)TokenType.Plus:
                    case (char)TokenType.Minus:
                    case (char)TokenType.Multiplication:
                    case (char)TokenType.Division:
                    case (char)TokenType.LParen:
                    case (char)TokenType.RParen:
                    case (char)TokenType.EOF:
                        tokenFound = true;
                        result.Tag = (TokenType)peek;
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        tokenFound = true;
                        result.Tag = TokenType.Num;
                        result.Value = peek.ToString();
                        break;
                    default:
                        throw new Exception("Lex Error");
                } // SWITCH - peek

                _index++;
            } // WHILE - tokenFound

            return result;
        } // GetToken
    }
}