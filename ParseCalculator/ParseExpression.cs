using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCalculator
{
    public class ParseExpression()
    {
        // - 4 + 5 => 4 ~ 5 +
        // -(7 + 8) => 7 8 + !
        //2 * (5 + 4) => 2 5 4 + *
        //-4+5-9 => ~4+5-9
        public static string GetPostfixExpression(string input)
        {
            if(input == string.Empty) throw new InvalidOperationException($"Пустое выражение!");
            if ("+-/*".Contains(input[0]) || "+-/*".Contains(input[^1])) throw new InvalidOperationException($"Некорректное выражение!");

            string output = string.Empty; //Строка для хранения выражения
            Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов
            char s;
            char previousToken = '\0';

            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {

                //Разделители пропускаем
                if (IsDelimeter(input[i]))
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считываем все число
                if (Char.IsDigit(input[i])) //Если цифра
                {
                    //Читаем до разделителя или оператора, что бы получить число
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i]; //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                }

                //Если символ - оператор
                if (IsOperator(input[i])) //Если оператор
                {
                    if (input[i] == '(')
                    {
                        if(previousToken == ')') throw new InvalidOperationException("Некорректное выражение!");
                        //Если символ - открывающая скобка
                        operStack.Push(input[i]); //Записываем её в стек
                    }
                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        if (previousToken == '(') throw new InvalidOperationException("Некорректное выражение!");
                        //Выписываем все операторы до открывающей скобки в строку
                        if (operStack.TryPop(out char symbol)) s = symbol;
                        else throw new InvalidOperationException("Некорректное выражение! Несбалансированные скобки!");

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            if (operStack.TryPop(out symbol)) s = symbol;
                            else throw new InvalidOperationException("Некорректное выражение! Несбалансированные скобки!");
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                        operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                    }
                }
                if(!IsDelimeter(input[i]) && !IsOperator(input[i]) && !Char.IsDigit(input[i]))
                {
                    throw new InvalidOperationException($"Неизвестный символ {input[i]}");
                }

                previousToken = input[i];
            }

            if(operStack.Contains('(')) throw new InvalidOperationException("Некорректное выражение! Несбалансированные скобки!");
            
            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }

        static public bool IsOperator(char с) => "+-/*()~".Contains(с);


        static public bool IsDelimeter(char c) => " =".Contains(c);
       

        static private byte GetPriority(char s)
        {
            return s switch
            {
                '(' => 0,
                ')' => 1,
                '+' => 2,
                '-' => 3,
                '*' => 4,
                '/' => 4,
                '~' => 5,
                _ => 6,
            };
        }
    }
}
