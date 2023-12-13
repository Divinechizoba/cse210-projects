
using System;
using System.Collections.Generic;

// Video class to manage video details and comments
class Video
{
    // Video attributes
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    // Constructor to initialize video details and create an empty list for comments
    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        LengthInSeconds = length;
        Comments = new List<Comment>();
    }

    // Method to add a comment to the video
    public void AddComment(string commenter, string text)
    {
        Comments.Add(new Comment(commenter, text));
    }

    // Method to get the number of comments
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Method to display video details and associated comments
    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");

        Console.WriteLine("\nComments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.Commenter}: {comment.Text}");
        }
    }
}

// Comment class to manage comment details
class Comment
{
    // Comment attributes
    public string Commenter { get; set; }
    public string Text { get; set; }

    // Constructor to initialize comment details
    public Comment(string commenter, string text)
    {
        Commenter = commenter;
        Text = text;
    }
}

class Program
{
    static void Main()
    {
        // Creating a video object and adding comments
        Video myVideo = new Video("Introduction to Programming", "John Doe", 300);
        myVideo.AddComment("User1", "Great video!");
        myVideo.AddComment("User2", "I learned a lot.");
        myVideo.AddComment("User3", "Could you cover more examples?");

        // Displaying video details and comments
        myVideo.DisplayVideoDetails();
    }
}
