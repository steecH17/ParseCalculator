using ParseCalculator;
using System;

class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        var calc = new StringCalculator(input);
        Console.WriteLine(calc.Calculate());
    }
}