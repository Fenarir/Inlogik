using ProjectMessageBoard.Models;

namespace ProjectMessageBoard.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly Dictionary<string, List<Message>> _projectMessages;
        private readonly Dictionary<string, List<string>> _userFollows;

        public CommandHandler(Dictionary<string, List<Message>> projectMessages, Dictionary<string, List<string>> userFollows)
        {
            _projectMessages = projectMessages;
            _userFollows = userFollows;
        }

        public void Handle(PostMessageCommand command)
        {
            if (!_projectMessages.ContainsKey(command.ProjectName))
            {
                _projectMessages[command.ProjectName] = new List<Message>();
            }

            _projectMessages[command.ProjectName].Add(new Message
            {
                UserName = command.UserName,
                Text = command.MessageText,
                Timestamp = DateTime.Now
            });

            Console.WriteLine($"{command.UserName} posted to {command.ProjectName}: {command.MessageText}");
        }

        public void Handle(FollowProjectCommand command)
        {
            if (!_userFollows.ContainsKey(command.UserName))
            {
                _userFollows[command.UserName] = new List<string>();
            }

            if (!_userFollows[command.UserName].Contains(command.ProjectName))
            {
                _userFollows[command.UserName].Add(command.ProjectName);
                Console.WriteLine($"{command.UserName} now follows {command.ProjectName}");
            }
        }
    }
}
