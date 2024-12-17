using ParseCalculator;
using System;

class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        var calc = new StringCalculator(input);
        try
        {
            var result = calc.Calculate();
            Console.WriteLine($"Результат:{result}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Ошибка:{ex.Message}");
        }
    }
}