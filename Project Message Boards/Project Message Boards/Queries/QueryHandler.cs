using ProjectMessageBoard.Models;

namespace ProjectMessageBoard.Queries
{
    public class QueryHandler
    {
        private readonly Dictionary<string, List<Message>> _projectMessages;
        private readonly Dictionary<string, List<string>> _userFollows;

        public QueryHandler(Dictionary<string, List<Message>> projectMessages, Dictionary<string, List<string>> userFollows)
        {
            _projectMessages = projectMessages;
            _userFollows = userFollows;
        }

        public void Handle(ReadProjectMessagesQuery query)
        {
            if (_projectMessages.ContainsKey(query.ProjectName))
            {
                foreach (Message message in _projectMessages[query.ProjectName])
                {
                    Console.WriteLine($"{message.UserName}: {message.Text} ({GetTimeAgo(message.Timestamp)})");
                }
            }
            else
            {
                Console.WriteLine($"No messages for project {query.ProjectName}");
            }
        }

        public void Handle(DisplayWallQuery query)
        {
            if (!_userFollows.ContainsKey(query.UserName))
            {
                Console.WriteLine($"{query.UserName} is not following any projects.");
                return;
            }

            List<string> followedProjects = _userFollows[query.UserName];
            List<Message> allMessages = new List<Message>();

            foreach (string project in followedProjects)
            {
                if (_projectMessages.ContainsKey(project))
                {
                    foreach (Message message in _projectMessages[project])
                    {
                        message.ProjectName = project;
                        allMessages.Add(message);
                    }
                }
            }

            IEnumerable<Message> orderedMessages = allMessages.OrderByDescending(m => m.Timestamp);

            foreach (Message message in orderedMessages)
            {
                Console.WriteLine($"{message.ProjectName} - {message.UserName}: {message.Text} ({GetTimeAgo(message.Timestamp)})");
            }
        }

        private string GetTimeAgo(DateTime timestamp)
        {
            TimeSpan timeSpan = DateTime.Now - timestamp;

            if (timeSpan.TotalMinutes < 1)
                return "just now";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minutes ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hours ago";
            return $"{(int)timeSpan.TotalDays} days ago";
        }
    }
}
