using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string answer = Console.ReadLine();
        int p = int.Parse(answer);

        string letter = "";

        if (p >= 90)
        {
            letter = "A";
        }
        else if (p >= 80)
        {
            letter = "B";
        }
        else if (p >= 70)
        {
            letter = "C";
        }
        else if (p >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is: {letter}");
        
        if (p >= 70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("You don't passed. Better luck next time!");
        }
    }
}