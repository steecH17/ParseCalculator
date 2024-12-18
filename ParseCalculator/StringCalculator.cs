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

        public double CalculatePostfixString(string input)
        {
            double result = 0; //Результат
            Stack<double> temp = new Stack<double>(); //Временный стек для решения

            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!ParseExpression.IsDelimeter(input[i]) && !ParseExpression.IsOperator(input[i]) && input[i]!='~') //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a)); //Записываем в стек
                    i--;
                }
                else if (ParseExpression.IsOperator(input[i])) //Если символ - оператор
                {
                    if (input[i] == '~')
                    {
                        double a = temp.Pop();
                        result = a * (-1);
                    }
                    else
                    {
                        //Берем два последних значения из стека
                        double a = temp.Pop();
                        double b = temp.Pop();

                        switch (input[i]) //И производим над ними действие, согласно оператору
                        {
                            case '+': result = b + a; break;
                            case '-': result = b - a; break;
                            case '*': result = b * a; break;
                            case '/': result = (a != 0) ? b / a : throw new DivideByZeroException("Деление на ноль!"); break;
                            case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                            
                        }
                    }
                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }

        public static string Evalute(string input)
        {
            //char previousToken = '\0';

            //for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            //{
            //    if (ParseExpression.IsDelimeter(input[i]))
            //        continue;

            //    if (char.IsDigit(input[i]) || input[i] == '(')
            //    {
            //        if(previousToken == '-')
            //        {

            //            input = input.Insert(i, "~");
            //            //input = input.Remove(i);
            //        }
            //    }
            //    previousToken = input[i];
            //}
            //return input;
            string pattern = @"-(?<number>[0-9]+)";

            // Замените каждый найденный унарный минус на "~"
            string output = Regex.Replace(input, pattern, "~${number}");
            return output;

        }

        public static string Evalute2(string input)
        {
            return Regex.Replace(input, @"(?<!\w)-(?=\s*\()", "~($1", RegexOptions.None);
        }

    }
}

