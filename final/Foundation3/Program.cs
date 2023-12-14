using System;

// Base class for all event types
class Event
{
    // Common event attributes
    public string EventTitle { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public string Address { get; set; }

    // Method to generate standard event message
    public virtual void GenerateStandardMessage()
    {
        Console.WriteLine($"Standard Event Details:");
        Console.WriteLine($"Title: {EventTitle}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
        Console.WriteLine($"Time: {Time}");
        Console.WriteLine($"Address: {Address}");
    }

    // Method to generate full event message
    public virtual void GenerateFullMessage()
    {
        GenerateStandardMessage();
    }

    // Method to generate short event message
    public virtual void GenerateShortMessage()
    {
        Console.WriteLine($"Short Event Details:");
        Console.WriteLine($"Type: {GetType().Name}");
        Console.WriteLine($"Title: {EventTitle}");
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
    }
}

// Derived class for Lecture events
class Lecture : Event
{
    // Additional attributes for lectures
    public string Speaker { get; set; }
    public int Capacity { get; set; }

    // Override method to generate full event message for lectures
    public override void GenerateFullMessage()
    {
        GenerateStandardMessage();
        Console.WriteLine($"Event Type: Lecture");
        Console.WriteLine($"Speaker: {Speaker}");
        Console.WriteLine($"Capacity: {Capacity}");
    }
}

// Derived class for Reception events
class Reception : Event
{
    // Additional attribute for receptions
    public string RSVPEmail { get; set; }

    // Override method to generate full event message for receptions
    public override void GenerateFullMessage()
    {
        GenerateStandardMessage();
        Console.WriteLine($"Event Type: Reception");
        Console.WriteLine($"RSVP Email: {RSVPEmail}");
    }
}

// Derived class for Outdoor Gathering events
class OutdoorGathering : Event
{
    // Additional attribute for outdoor gatherings
    public string WeatherForecast { get; set; }

    // Override method to generate full event message for outdoor gatherings
    public override void GenerateFullMessage()
    {
        GenerateStandardMessage();
        Console.WriteLine($"Event Type: Outdoor Gathering");
        Console.WriteLine($"Weather Forecast: {WeatherForecast}");
    }
}

class Program
{
    static void Main()
    {
        // Creating instances of different event types
        Lecture lectureEvent = new Lecture
        {
            EventTitle = "Programming Workshop",
            Description = "A workshop on programming concepts",
            Date = DateTime.Now.AddDays(7),
            Time = "10:00 AM",
            Address = "123 Main Street",
            Speaker = "Dr. Smith",
            Capacity = 50
        };

        Reception receptionEvent = new Reception
        {
            EventTitle = "Networking Event",
            Description = "Networking event for professionals",
            Date = DateTime.Now.AddDays(14),
            Time = "6:00 PM",
            Address = "456 Oak Avenue",
            RSVPEmail = "info@example.com"
        };

        OutdoorGathering outdoorEvent = new OutdoorGathering
        {
            EventTitle = "Summer Picnic",
            Description = "Picnic for employees and families",
            Date = DateTime.Now.AddDays(21),
            Time = "12:00 PM",
            Address = "789 Park Lane",
            WeatherForecast = "Sunny"
        };

        // Displaying event details
        Console.WriteLine("Event Details:");
        lectureEvent.GenerateFullMessage();
        Console.WriteLine();
        receptionEvent.GenerateFullMessage();
        Console.WriteLine();
        outdoorEvent.GenerateFullMessage();
    }
}
