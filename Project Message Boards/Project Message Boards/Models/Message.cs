namespace ProjectMessageBoard.Models
{
    public class Message
    {
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
