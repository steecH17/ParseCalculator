using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParseCalculator
{
    public class StringCalculator(string expression)
    {
        private string _expression = expression;


        public string Calculate()
        {
            string postfixString = ParseExpression.GetPostfixExpression(UnaryMinusParser.Parse(_expression));
            
            return CalculatePostfixString(postfixString).ToString();
        }

        private double CalculatePostfixString(string input)
        {
            double result = 0; 
            Stack<double> tempStorage = new Stack<double>(); 

            for (int i = 0; i < input.Length; i++) 
            {
                
                if (Char.IsDigit(input[i]))
                {
                    string currentNum = string.Empty;

                    while (!ParseExpression.IsDelimeter(input[i]) && !ParseExpression.IsOperator(input[i]) && input[i]!='~') 
                    {
                        currentNum += input[i];
                        i++;
                        if (i == input.Length) break;
                    }

                    tempStorage.Push(double.Parse(currentNum)); 
                    i--;
                }
                else if (ParseExpression.IsOperator(input[i])) 
                {
                    if (input[i] == '~')
                    {
                        result = (tempStorage.TryPop(out double element)) ? element * (-1) : throw new InvalidOperationException("Стэк переполнен!"); 
                    }
                    else
                    {
                        double num1 = (tempStorage.TryPop(out double element)) ? element : throw new InvalidOperationException("Стэк переполнен!");
                        double num2 = (tempStorage.TryPop(out element)) ? element : throw new InvalidOperationException("Стэк переполнен!");

                        result = CalculateSimpleExpression(num1, num2, input[i]);
                    }
                    tempStorage.Push(result); 
                }
            }

            return tempStorage.Peek(); 
        }

        private double CalculateSimpleExpression(double num1, double num2, char operation)
            => operation switch
            {
                '+' => num2 + num1,
                '-' => num2 - num1,
                '*' => num2 * num1,
                '/' => (num1 != 0) ? num2 / num1 : throw new DivideByZeroException("Деление на ноль!"),
                _ => throw new InvalidOperationException($"Неизвестный символ {operation}")
            };
        


    }
}

