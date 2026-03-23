using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video v1 = new Video(" Basics", "Alice", 300);
        v1.AddComment(new Comment("John", "Great video!"));
        v1.AddComment(new Comment("Mary", "Very helpful."));
        v1.AddComment(new Comment("Luke", "Thanks for sharing."));
        videos.Add(v1);

        // Video 2
        Video v2 = new Video("OOP Explained", "Bob", 420);
        v2.AddComment(new Comment("Anna", "Now I understand classes."));
        v2.AddComment(new Comment("Tom", "Clear explanation."));
        v2.AddComment(new Comment("Sam", "Awesome!"));
        videos.Add(v2);

        // Video 3
        Video v3 = new Video("Advanced ", "Charlie", 600);
        v3.AddComment(new Comment("Mike", "This is deep."));
        v3.AddComment(new Comment("Sara", "Loved it."));
        v3.AddComment(new Comment("Chris", "Very informative."));
        videos.Add(v3);

        // Display all videos
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Comments: {video.GetCommentCount()}");

            Console.WriteLine("---- Comments ----");
            foreach (Comment c in video.GetComments())
            {
                Console.WriteLine($"{c.CommenterName}: {c.Text}");
            }

            Console.WriteLine();
        }
    }
}