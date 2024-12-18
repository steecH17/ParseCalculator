using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCalculator
{
    public class ParseExpression()
    {
        
        public static string GetPostfixExpression(string input)
        {
            //Проверка начальных ошибок
            if(input == string.Empty) throw new InvalidOperationException($"Пустое выражение!");
            if ("+-/*".Contains(input[0]) || "+-/*".Contains(input[^1])) throw new InvalidOperationException($"Некорректное выражение!");

            string output = string.Empty;
            Stack<char> operationStack = new Stack<char>(); 
            char currentToken;
            char previousToken = '\0';

            for (int i = 0; i < input.Length; i++) 
            {
                if (IsDelimeter(input[i]))
                    continue; 

                
                if (Char.IsDigit(input[i])) 
                {
                    
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++; 

                        if (i == input.Length) break; 
                    }

                    output += " "; 
                    i--; 
                }

                
                if (IsOperator(input[i])) 
                {
                    if (input[i] == '(')
                    {
                        if(previousToken == ')') throw new InvalidOperationException("Некорректное выражение!");
                        
                        operationStack.Push(input[i]); 
                    }
                    else if (input[i] == ')') 
                    {
                        if (previousToken == '(') throw new InvalidOperationException("Некорректное выражение!");

                        currentToken = (operationStack.TryPop(out char symbol)) ? symbol : throw new InvalidOperationException("Некорректное выражение! Несбалансированные скобки!");
                        
                        while (currentToken != '(')
                        {
                            output += currentToken.ToString() + ' ';
                            currentToken = (operationStack.TryPop(out symbol)) ? symbol : throw new InvalidOperationException("Некорректное выражение! Несбалансированные скобки!"); ;
                        }
                    }
                    else 
                    {
                        if (operationStack.Count > 0 && GetPriority(input[i]) <= GetPriority(operationStack.Peek())) 
                                output += operationStack.Pop().ToString() + " "; 

                        operationStack.Push(char.Parse(input[i].ToString())); 

                    }
                }
                if(!IsDelimeter(input[i]) && !IsOperator(input[i]) && !Char.IsDigit(input[i]))
                {
                    throw new InvalidOperationException($"Неизвестный символ {input[i]}");
                }

                previousToken = input[i];
            }

            if(operationStack.Contains('(')) throw new InvalidOperationException("Некорректное выражение! Несбалансированные скобки!");
            
            
            while (operationStack.Count > 0)
                output += operationStack.Pop() + " ";

            return output; 
        }

        static public bool IsOperator(char token) => "+-/*()~".Contains(token);


        static public bool IsDelimeter(char token) => " =".Contains(token);
       
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
