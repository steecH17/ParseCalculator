using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParseCalculator
{
    public class UnaryMinusParser
    {
        //-5 => ~5
        public static string Parse(string expression)
        {
            List<Token> tokens = Lex(expression);
            return ToExpression(tokens);

        }

        static List<Token> Lex(string expression)
        {
            List<Token> tokens = new List<Token>();
            string currentNumber = "";

            foreach (char c in expression)
            {
                if (char.IsDigit(c))
                {
                    currentNumber += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentNumber))
                    {
                        tokens.Add(new Token(TokenType.Number, currentNumber));
                        currentNumber = "";
                    }
                    tokens.Add(new Token(TokenType.Operator, c.ToString()));
                }
            }
            if (!string.IsNullOrEmpty(currentNumber))
            {
                tokens.Add(new Token(TokenType.Number, currentNumber));
            }
            return tokens;
        }


        static string ToExpression(List<Token> tokens)
        {
            StringBuilder result = new StringBuilder();
            bool unaryMinus = false;

            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (token.Type == TokenType.Operator && token.Value == "-")
                {
                    if (i == 0 || tokens[i - 1].Type == TokenType.Operator || tokens[i - 1].Value == "(") //Проверка на унарный минус в начале или после оператора
                    {
                        unaryMinus = true;
                    }
                    else
                    {
                        result.Append("-");
                        unaryMinus = false;
                    }
                }
                else if (token.Type == TokenType.Number && unaryMinus)
                {
                    result.Append("~");
                    result.Append(token.Value);
                    unaryMinus = false;
                }
                else if (token.Type == TokenType.Operator && token.Value == "(" && unaryMinus)
                {
                    result.Append("~(");
                    unaryMinus = false;
                }
                else
                {
                    result.Append(token.Value);
                }
            }
            return result.ToString();
        }

        enum TokenType { Number, Operator }
        class Token
        {
            public TokenType Type { get; set; }
            public string Value { get; set; }
            public Token(TokenType type, string value) { Type = type; Value = value; }
        }
    }
}
