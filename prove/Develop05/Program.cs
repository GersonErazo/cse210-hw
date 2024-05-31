using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class for all goals
    abstract class Goal
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public bool IsCompleted { get; set; }

        public Goal(string name, int points)
        {
            Name = name;
            Points = points;
            IsCompleted = false;
        }

        public abstract int RecordEvent();
        public abstract void Display();
    }

    // Simple goal class
    class SimpleGoal : Goal
    {
        public SimpleGoal(string name, int points) : base(name, points) { }

        public override int RecordEvent()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                return Points;
            }
            return 0;
        }

        public override void Display()
        {
            Console.WriteLine($"[ {(IsCompleted ? "X" : " ")} ] {Name} - {Points} points");
        }
    }

    // Eternal goal class
    class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points) { }

        public override int RecordEvent()
        {
            return Points;
        }

        public override void Display()
        {
            Console.WriteLine($"[ ∞ ] {Name} - {Points} points each time");
        }
    }

    // Checklist goal class
    class ChecklistGoal : Goal
    {
        public int RequiredCount { get; set; }
        public int CurrentCount { get; set; }
        public int BonusPoints { get; set; }

        public ChecklistGoal(string name, int points, int requiredCount, int bonusPoints) 
            : base(name, points)
        {
            RequiredCount = requiredCount;
            BonusPoints = bonusPoints;
            CurrentCount = 0;
        }

        public override int RecordEvent()
        {
            if (CurrentCount < RequiredCount)
            {
                CurrentCount++;
                if (CurrentCount == RequiredCount)
                {
                    IsCompleted = true;
                    return Points + BonusPoints;
                }
                return Points;
            }
            return 0;
        }

        public override void Display()
        {
            Console.WriteLine($"[ {(IsCompleted ? "X" : " ")} ] {Name} - {Points} points each time, {CurrentCount}/{RequiredCount} times completed");
        }
    }

    class Program
    {
        static List<Goal> goals = new List<Goal>();
        static int totalPoints = 0;

        static void Main(string[] args)
        {
            LoadGoals();

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Eternal Quest");
                Console.WriteLine("1. Create a new goal");
                Console.WriteLine("2. Record an event");
                Console.WriteLine("3. Show goals");
                Console.WriteLine("4. Show score");
                Console.WriteLine("5. Save and exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        RecordEvent();
                        break;
                    case "3":
                        ShowGoals();
                        break;
                    case "4":
                        ShowScore();
                        break;
                    case "5":
                        SaveGoals();
                        running = false;
                        break;
                }
            }
        }

        static void CreateGoal()
        {
            Console.Clear();
            Console.WriteLine("Create a New Goal");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choose a goal type: ");
            string choice = Console.ReadLine();

            Console.Write("Enter the name of the goal: ");
            string name = Console.ReadLine();
            Console.Write("Enter the points for completing the goal: ");
            int points = int.Parse(Console.ReadLine());

            Goal newGoal = null;
            switch (choice)
            {
                case "1":
                    newGoal = new SimpleGoal(name, points);
                    break;
                case "2":
                    newGoal = new EternalGoal(name, points);
                    break;
                case "3":
                    Console.Write("Enter the number of times to complete the goal: ");
                    int count = int.Parse(Console.ReadLine());
                    Console.Write("Enter the bonus points for completing the goal: ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    newGoal = new ChecklistGoal(name, points, count, bonusPoints);
                    break;
            }

            if (newGoal != null)
            {
                goals.Add(newGoal);
                Console.WriteLine("Goal created successfully!");
            }

            Console.ReadKey();
        }

        static void RecordEvent()
        {
            Console.Clear();
            Console.WriteLine("Record an Event");
            ShowGoals();
            Console.Write("Enter the number of the goal: ");
            int goalNumber = int.Parse(Console.ReadLine());

            if (goalNumber >= 1 && goalNumber <= goals.Count)
            {
                int pointsEarned = goals[goalNumber - 1].RecordEvent();
                totalPoints += pointsEarned;
                Console.WriteLine($"Event recorded! You earned {pointsEarned} points.");
            }
            else
            {
                Console.WriteLine("Invalid goal number.");
            }

            Console.ReadKey();
        }

        static void ShowGoals()
        {
            Console.Clear();
            Console.WriteLine("Your Goals");
            for (int i = 0; i < goals.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                goals[i].Display();
            }
            Console.ReadKey();
        }

        static void ShowScore()
        {
            Console.Clear();
            Console.WriteLine($"Total Points: {totalPoints}");
            Console.ReadKey();
        }

        static void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                writer.WriteLine(totalPoints);
                foreach (Goal goal in goals)
                {
                    string goalType = goal.GetType().Name;
                    writer.WriteLine($"{goalType}|{goal.Name}|{goal.Points}|{goal.IsCompleted}");

                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine($"{checklistGoal.RequiredCount}|{checklistGoal.CurrentCount}|{checklistGoal.BonusPoints}");
                    }
                }
            }
        }

        static void LoadGoals()
        {
            if (File.Exists("goals.txt"))
            {
                using (StreamReader reader = new StreamReader("goals.txt"))
                {
                    totalPoints = int.Parse(reader.ReadLine());

                    while (!reader.EndOfStream)
                    {
                        string[] goalData = reader.ReadLine().Split('|');
                        string goalType = goalData[0];
                        string name = goalData[1];
                        int points = int.Parse(goalData[2]);
                        bool isCompleted = bool.Parse(goalData[3]);

                        Goal goal = null;
                        switch (goalType)
                        {
                            case "SimpleGoal":
                                goal = new SimpleGoal(name, points);
                                break;
                            case "EternalGoal":
                                goal = new EternalGoal(name, points);
                                break;
                            case "ChecklistGoal":
                                int requiredCount = int.Parse(reader.ReadLine());
                                int currentCount = int.Parse(reader.ReadLine());
                                int bonusPoints = int.Parse(reader.ReadLine());
                                goal = new ChecklistGoal(name, points, requiredCount, bonusPoints);
                                break;
                        }

                        if (goal != null)
                        {
                            goal.IsCompleted = isCompleted;
                            goals.Add(goal);
                        }
                    }
                }
            }
        }
    }
}
