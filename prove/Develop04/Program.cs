using System;
using System.Threading;

// Base class for all activities
public class Activity
{
    protected string Name { get; set; }
    protected string Description { get; set; }
    protected int Duration { get; set; }

    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    // Method to start the activity
    public void StartActivity()
    {
        Console.WriteLine($"Activity: {Name}");
        Console.WriteLine($"Description: {Description}");

        SetDuration();
        PrepareToStart();

        // Perform the specific activity
        PerformActivity();

        // Conclude the activity
        ConcludeActivity();
    }

    // Set the duration of the activity
    private void SetDuration()
    {
        Console.Write("Enter duration (in seconds): ");
        Duration = int.Parse(Console.ReadLine());
    }

    // Pause and display a countdown animation
    private void PrepareToStart()
    {
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(2000); // Pause for 2 seconds
    }

    // Perform the specific activity (to be overridden by subclasses)
    protected virtual void PerformActivity()
    {
        // Activity-specific implementation goes here
    }

    // Conclude the activity
    private void ConcludeActivity()
    {
        Console.WriteLine($"Good job! You have completed {Name} for {Duration} seconds.");
        Thread.Sleep(2000); // Pause for 2 seconds
    }
}

// Breathing activity class
public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "Relax by focusing on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Start breathing exercise...");
        int remainingTime = Duration;

        while (remainingTime > 0)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(1000); // Pause for 1 second
            Console.WriteLine("Breathe out...");
            Thread.Sleep(1000); // Pause for 1 second
            remainingTime -= 2; // Each cycle is 2 seconds
        }
    }
}

// Reflection activity class
public class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "Reflect on meaningful experiences.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pause for 3 seconds
        }
    }
}

// Listing activity class
public class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "List positive aspects of your life.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        Console.WriteLine("Start listing...");
        Thread.Sleep(3000); // Pause for 3 seconds

        Console.WriteLine("Enter items (press Enter after each item):");
        int itemCount = 0;

        while (Duration > 0)
        {
            Console.ReadLine(); // Wait for user input
            itemCount++;
            Duration--;
        }

        Console.WriteLine($"You listed {itemCount} items.");
    }
}

// Main program to interact with activities
public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an activity (1-4): ");

            string choice = Console.ReadLine();

            Activity activity;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.StartActivity();
            Console.WriteLine();
        }
    }
}
