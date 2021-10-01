using System;
namespace Lab01_PabloCuevas_1239719
{
    class Parser
    {
        Scanner _scanner;
        Token _token;

        #region Variables
        private double E()
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.Num:
                case TokenType.LParen:
                    double _T = T();
                    result = EP(_T);
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double EP(double num)
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Plus:
                    Match(TokenType.Plus);
                    double _T = T();
                    double sum = num + _T;
                    result = EP(sum);
                    break;
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    double __T = T();
                    double sub = num - __T;
                    result = EP(sub);
                    break;
                case TokenType.RParen:
                case TokenType.EOF:
                    result = num;
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double T()
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.Num:
                case TokenType.LParen:
                    double _F = F();
                    result = TP(_F);
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double TP(double num)
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Multiplication:
                    Match(TokenType.Multiplication);
                    double _F = F();
                    double mul = num * _F;
                    result = TP(mul);
                    break;
                case TokenType.Division:
                    Match(TokenType.Division);
                    double __F = F();
                    double div = num / __F;
                    result = TP(div);
                    break;
                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.RParen:
                case TokenType.EOF:
                    result = num;
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double F()
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    result = N() * -1;
                    break;
                case TokenType.Num:
                case TokenType.LParen:
                    result = P();
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double P()
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Num:
                    double num = double.Parse(_token.Value);
                    Match(TokenType.Num);
                    result = S(num);
                    break;
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    result = E();
                    Match(TokenType.RParen);
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double N()
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.Num:
                    result = U();
                    break;
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    result = E();
                    Match(TokenType.RParen);
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double U()
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    result = U() * -1;
                    break;
                case TokenType.Num:
                    double num = double.Parse(_token.Value);
                    Match(TokenType.Num);
                    result = S(num);
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }

        private double S(double num)
        {
            double result;
            switch (_token.Tag)
            {
                case TokenType.Num:
                    string first = num.ToString();
                    string second = _token.Value;
                    double append = double.Parse(first + second);
                    Match(TokenType.Num);
                    result = S(append);
                    break;
                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.Multiplication:
                case TokenType.Division:
                case TokenType.RParen:
                case TokenType.EOF:
                    result = num;
                    break;
                default:
                    throw new Exception("Syntax error");
            }
            return result;
        }
        #endregion

        private void Match(TokenType tag)
        {
            if (_token.Tag == tag)
            {
                _token = _scanner.GetToken();
            }
            else
            {
                throw new Exception("Syntax error");
            }
        }
        public double Parse(string regexp)
        {
            _scanner = new Scanner(regexp + (char)TokenType.EOF);
            _token = _scanner.GetToken();
            double result;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.LParen:
                case TokenType.Num:
                    result = E();
                    break;
                default:
                    result = 0;
                    break;
            }
            Match(TokenType.EOF);
            return result;
        }
    }
}