using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string FN = Console.ReadLine();

        Console.Write("What is your last name? ");
        string LN = Console.ReadLine();

        Console.WriteLine($"Your name is {LN}, {FN} {LN}.");
    }
}