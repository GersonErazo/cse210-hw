using System;

// Address class to represent event address
public class Address
{
    private string street;
    private string city;
    private string state;
    private string zipCode;

    public Address(string street, string city, string state, string zipCode)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.zipCode = zipCode;
    }

    public override string ToString()
    {
        return $"{street}, {city}, {state} {zipCode}";
    }
}

// Base Event class
public class Event
{
    private string title;
    private string description;
    private DateTime dateTime;
    private Address address;

    public Event(string title, string description, DateTime dateTime, Address address)
    {
        this.title = title;
        this.description = description;
        this.dateTime = dateTime;
        this.address = address;
    }

    public string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {dateTime.ToShortDateString()}\nTime: {dateTime.ToShortTimeString()}\nAddress: {address}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public virtual string GetShortDescription()
    {
        return $"Type: Generic Event\nTitle: {title}\nDate: {dateTime.ToShortDateString()}";
    }
}

// Derived class for Lecture event
public class LectureEvent : Event
{
    private string speaker;
    private int capacity;

    public LectureEvent(string title, string description, DateTime dateTime, Address address, string speaker, int capacity)
        : base(title, description, dateTime, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
}

// Derived class for Reception event
public class ReceptionEvent : Event
{
    private string rsvpEmail;

    public ReceptionEvent(string title, string description, DateTime dateTime, Address address, string rsvpEmail)
        : base(title, description, dateTime, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }
}

// Derived class for Outdoor Gathering event
public class OutdoorGatheringEvent : Event
{
    private string weatherForecast;

    public OutdoorGatheringEvent(string title, string description, DateTime dateTime, Address address, string weatherForecast)
        : base(title, description, dateTime, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create event instances
        Address lectureAddress = new Address("123 Main St", "Anytown", "State", "12345");
        LectureEvent lecture = new LectureEvent("Tech Talk", "Exciting lecture about technology", new DateTime(2024, 6, 15, 10, 0, 0), lectureAddress, "John Doe", 50);

        Address receptionAddress = new Address("456 Elm St", "Othertown", "State", "54321");
        ReceptionEvent reception = new ReceptionEvent("Networking Mixer", "Come meet professionals in your industry", new DateTime(2024, 6, 20, 18, 0, 0), receptionAddress, "rsvp@example.com");

        Address outdoorAddress = new Address("789 Oak St", "Anothertown", "State", "67890");
        OutdoorGatheringEvent outdoor = new OutdoorGatheringEvent("Picnic in the Park", "Join us for a day of fun in the sun", new DateTime(2024, 7, 1, 12, 0, 0), outdoorAddress, "Sunny with a chance of clouds");

        // Generate and display marketing messages for each event
        Console.WriteLine("Lecture Event:");
        Console.WriteLine(lecture.GetStandardDetails());
        Console.WriteLine(lecture.GetFullDetails());
        Console.WriteLine(lecture.GetShortDescription());
        Console.WriteLine();

        Console.WriteLine("Reception Event:");
        Console.WriteLine(reception.GetStandardDetails());
        Console.WriteLine(reception.GetFullDetails());
        Console.WriteLine(reception.GetShortDescription());
        Console.WriteLine();

        Console.WriteLine("Outdoor Gathering Event:");
        Console.WriteLine(outdoor.GetStandardDetails());
        Console.WriteLine(outdoor.GetFullDetails());
        Console.WriteLine(outdoor.GetShortDescription());
        Console.WriteLine();
    }
}
