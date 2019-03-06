using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluator
{
    /// <summary>
    /// MathEvaluator
    /// </summary>
    public class MathEvaluator
    {
        private Scanner _scanner;
        private Token _token;

        public double Eval(string expressionString)
        {
            _scanner = new Scanner(expressionString);
            NextToken();
            double result = 0;
            Exp1(ref result);
            return result;
        }

        private void Exp1(ref double result)
        {
            if (_token.Kind == TokenKind.OpenBracket)
            {
                NextToken();
                Exp1(ref result);
                if (_token.Kind == TokenKind.CloseBracket) NextToken();
                else throw new MathEvaluatorException();
            }
            Exp2(ref result);
            if (_token.Kind == TokenKind.Add || _token.Kind == TokenKind.Sub)
            {
                var op = _token;
                NextToken();
                double right = 0;
                Exp1(ref right);
                if (op.Kind == TokenKind.Add) result += right;
                else if (op.Kind == TokenKind.Sub) result -= right;
            }
        }

        private void Exp2(ref double result)
        {
            Atom(ref result);
            if (_token.Kind == TokenKind.Mul || _token.Kind == TokenKind.Div)
            {
                var op = _token;
                NextToken();
                double right = 0;
                Exp1(ref right);
                if (op.Kind == TokenKind.Mul) result *= right;
                else if (op.Kind == TokenKind.Div) result /= right;
            }
        }

        private void Atom(ref double result)
        {
            if (_token.Kind == TokenKind.Number)
            {
                result = _token.Value;
                NextToken();
            }
        }

        private void NextToken() { _token = _scanner.GetNextToken(); }
    }

    /// <summary>
    /// Scanner
    /// </summary>
    public class Scanner
    {
        private readonly string _expressionString;
        private int _currentIndex;

        public Scanner(string expressionString)
        {
            _expressionString = expressionString ?? throw new ArgumentNullException(nameof(expressionString));
            if (String.IsNullOrWhiteSpace(expressionString)) throw new ArgumentException("expressionString is empty");
            _currentIndex = 0;
        }

        public IEnumerable<Token> GetAllTokens()
        {
            var token = GetNextToken();
            while (token.Kind != TokenKind.EOF)
            {
                yield return token;
                token = GetNextToken();
            }
        }

        public Token GetNextToken()
        {
            char ch;
            switch (1)
            {
                case 1:
                    if (_currentIndex == _expressionString.Length) return new Token(TokenKind.EOF);
                    ch = _expressionString[_currentIndex++];
                    if (ch == ' ') goto case 1;
                    else if (ch == '(') return new Token(TokenKind.OpenBracket);
                    else if (ch == ')') return new Token(TokenKind.CloseBracket);
                    else if (ch == '+') return new Token(TokenKind.Add);
                    else if (ch == '-') return new Token(TokenKind.Sub);
                    else if (ch == '*') return new Token(TokenKind.Mul);
                    else if (ch == '/') return new Token(TokenKind.Div);
                    else if (Char.IsDigit(ch)) { _currentIndex--; goto case 2; }
                    else throw new MathEvaluatorException();
                // number
                case 2:
                    var token = new Token(TokenKind.Number);
                    int _dotIndex = _currentIndex + 1;
                    while (_dotIndex < _expressionString.Length && Char.IsDigit(_expressionString[_dotIndex])) _dotIndex++;
                    while (_currentIndex < _expressionString.Length && Char.IsDigit(ch = _expressionString[_currentIndex]))
                    {
                        var digit = ch - 48;
                        if (digit > 0) token.Value += _m1[digit - 1, _dotIndex - _currentIndex - 1];
                        _currentIndex++;
                    }
                    if (ch == ',' || ch == '.')
                    {
                        _currentIndex++;
                        while (_currentIndex < _expressionString.Length && Char.IsDigit(ch = _expressionString[_currentIndex]))
                        {
                            var digit = ch - 48;
                            if (digit > 0) token.Value += _m2[digit - 1, _currentIndex - _dotIndex - 1];
                            _currentIndex++;
                        }
                    }
                    return token;
            }
        }

        private double[,] _m1 = new double[,]
        {
            {1,10,100,1000,10000,100000,1000000},
            {2,20,200,2000,20000,200000,2000000},
            {3,30,300,3000,30000,300000,3000000},
            {4,40,400,4000,40000,400000,4000000},
            {5,50,500,5000,50000,500000,5000000},
            {6,60,600,6000,60000,600000,6000000},
            {7,70,700,7000,70000,700000,7000000},
            {8,80,800,8000,80000,800000,8000000},
            {9,90,900,9000,90000,900000,9000000},
        };

        private double[,] _m2 = new double[,]
        {
            {.1,.01,.001,.0001,.00001,.000001,.0000001},
            {.2,.02,.002,.0002,.00002,.000002,.0000002},
            {.3,.03,.003,.0003,.00003,.000003,.0000003},
            {.4,.04,.004,.0004,.00004,.000004,.0000004},
            {.5,.05,.005,.0005,.00005,.000005,.0000005},
            {.6,.06,.006,.0006,.00006,.000006,.0000006},
            {.7,.07,.007,.0007,.00007,.000007,.0000007},
            {.8,.08,.008,.0008,.00008,.000008,.0000008},
            {.9,.09,.009,.0009,.00009,.000009,.0000009},
        };
    }

    public struct Token
    {
        public TokenKind Kind;
        public double Value;

        public Token(TokenKind kind)
        {
            Kind = kind;
            Value = 0;
        }

        public Token(TokenKind kind, double value)
        {
            Kind = kind;
            Value = value;
        }

        public override string ToString()
        {
            if (Kind == TokenKind.Number) return Value.ToString();
            else return Kind.ToString();
        }
    }

    public enum TokenKind
    {
        _Unknown,
        OpenBracket,
        CloseBracket,
        Add,
        Sub,
        Mul,
        Div,
        Number,
        EOF
    }

    public class MathEvaluatorException : Exception
    {
        public MathEvaluatorException() : base("incorrect math expression") { }
    }
}
