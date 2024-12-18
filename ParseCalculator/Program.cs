using ParseCalculator;
using System;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        //Console.WriteLine(UnaryMinusParser.Parse(input));
        var calc = new StringCalculator(input);
        //Console.WriteLine(ParseExpression.GetPostfixExpression(""));
        
        try
        {
            var result = calc.Calculate();
            Console.WriteLine($"Результат:{result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка:{ex.Message}");
        }
    }

    
}


    

