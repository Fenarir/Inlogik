namespace Project_Message_Boards.Models
{
    public class Project
    {
        public string Name { get; set; }
        public List<Message> Messages { get; } = new List<Message>();
    }
}
