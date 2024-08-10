namespace Project_Message_Boards.Models
{
    public class User
    {
        public string Name { get; set; }
        public List<Project> FollowingProjects { get; } = new List<Project>();
    }
}
