namespace Project_Message_Boards.Models
{

    public class Message
    {
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"{User.Name}: {Text} ({TimeAgo()})";
        }

        public string TimeAgo()
        {
            var timeSpan = DateTime.Now - Timestamp;

            if (timeSpan.TotalMinutes < 1)
                return $"{timeSpan.Seconds} seconds ago";
            if (timeSpan.TotalHours < 1)
                return $"{timeSpan.Minutes} minutes ago";
            if (timeSpan.TotalDays < 1)
                return $"{timeSpan.Hours} hours ago";

            return $"{timeSpan.Days} days ago";
        }
    }
}
