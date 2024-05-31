using System;
using System.Collections.Generic;

namespace YouTubeTracker
{
    // Comment class to track the name and text of the comment
    public class Comment
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Comment(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }

    // Video class to track the title, author, length, and list of comments
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; } // Length in seconds
        private List<Comment> comments;

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            comments = new List<Comment>();
        }

        // Method to add a comment to the video
        public void AddComment(Comment comment)
        {
            comments.Add(comment);
        }

        // Method to get the number of comments
        public int GetNumberOfComments()
        {
            return comments.Count;
        }

        // Method to get all comments
        public List<Comment> GetComments()
        {
            return comments;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to store videos
            List<Video> videos = new List<Video>();

            // Create 3-4 video instances
            Video video1 = new Video("How to Cook Pasta", "Chef John", 600);
            Video video2 = new Video("Top 10 Programming Tips", "Tech Guru", 900);
            Video video3 = new Video("Travel Vlog: Paris", "Wanderlust", 1200);
            Video video4 = new Video("DIY Home Decor", "Craft Queen", 800);

            // Add comments to each video
            video1.AddComment(new Comment("Alice", "Great recipe!"));
            video1.AddComment(new Comment("Bob", "Tried it and loved it."));
            video1.AddComment(new Comment("Charlie", "Can't wait to try this."));

            video2.AddComment(new Comment("Dave", "Very informative."));
            video2.AddComment(new Comment("Eve", "Helped me a lot, thanks!"));
            video2.AddComment(new Comment("Frank", "Awesome tips!"));

            video3.AddComment(new Comment("Grace", "Beautiful video."));
            video3.AddComment(new Comment("Heidi", "Paris is amazing!"));
            video3.AddComment(new Comment("Ivan", "I want to go there too."));

            video4.AddComment(new Comment("Jack", "Such creative ideas!"));
            video4.AddComment(new Comment("Karen", "Can't wait to try these."));
            video4.AddComment(new Comment("Leo", "Very inspiring!"));

            // Add videos to the list
            videos.Add(video1);
            videos.Add(video2);
            videos.Add(video3);
            videos.Add(video4);

            // Iterate through the list of videos and display details
            foreach (Video video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.Length} seconds");
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($"Comment by {comment.Name}: {comment.Text}");
                }
                Console.WriteLine();
            }
        }
    }
}
