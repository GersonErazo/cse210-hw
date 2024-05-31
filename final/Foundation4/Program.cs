using System;
using System.Collections.Generic;

// Base Activity class
public class Activity
{
    private DateTime date;
    private int minutes;

    public Activity(DateTime date, int minutes)
    {
        this.date = date;
        this.minutes = minutes;
    }

    public virtual double GetDistance()
    {
        return 0; // Base class returns 0, overridden in derived classes
    }

    public virtual double GetSpeed()
    {
        return 0; // Base class returns 0, overridden in derived classes
    }

    public virtual double GetPace()
    {
        return 0; // Base class returns 0, overridden in derived classes
    }

    public virtual string GetSummary()
    {
        return $"{date.ToShortDateString()} ({GetType().Name} - {minutes} min): Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

// Derived class for Running activity
public class Running : Activity
{
    private double distance;

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return distance / (minutes / 60);
    }

    public override double GetPace()
    {
        return minutes / distance;
    }
}

// Derived class for Cycling activity
public class Cycling : Activity
{
    private double speed;

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        this.speed = speed;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetDistance()
    {
        return speed * (minutes / 60);
    }

    public override double GetPace()
    {
        return 60 / speed;
    }
}

// Derived class for Swimming activity
public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000 * 0.62; // Convert meters to miles
    }

    public override double GetSpeed()
    {
        return GetDistance() / (minutes / 60);
    }

    public override double GetPace()
    {
        return minutes / GetDistance();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of activities
        List<Activity> activities = new List<Activity>();

        // Add different activities to the list
        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 4.8));
        activities.Add(new Cycling(new DateTime(2022, 11, 3), 45, 15.0));
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 45, 20));

        // Display summary for each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
