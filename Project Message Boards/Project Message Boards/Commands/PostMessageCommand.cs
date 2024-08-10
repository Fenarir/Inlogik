namespace ProjectMessageBoard.Commands
{
    public class PostMessageCommand
    {
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public string MessageText { get; set; }
    }
}
