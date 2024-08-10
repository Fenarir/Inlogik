namespace ProjectMessageBoard
{
    class Program
    {
        // Data structures to store the messages, followers, and users.
        static Dictionary<string, List<Message>> projectMessages = new Dictionary<string, List<Message>>();
        static Dictionary<string, List<string>> userFollows = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                // Handle posting, reading, following, and wall commands
                HandleCommand(input);
            }
        }

        static void HandleCommand(string input)
        {
            // Posting command: <user name> -> @<project name> <message>
            if (input.Contains("-> @"))
            {
                string[] splitInput = input.Split(new[] { "-> @" }, 2, StringSplitOptions.None);
                string userName = splitInput[0].Trim();
                string[] projectAndMessage = splitInput[1].Split(new[] { ' ' }, 2);
                string projectName = projectAndMessage[0].Trim();
                string messageText = projectAndMessage[1].Trim();

                PostMessage(userName, projectName, messageText);
            }
            // Reading command: <project name>
            else if (!input.Contains(" "))
            {
                string projectName = input.Trim();
                ReadProjectMessages(projectName);
            }
            // Following command: <user name> follows <project name>
            else if (input.Contains("follows"))
            {
                string[] splitInput = input.Split("follows");
                string userName = splitInput[0].Trim();
                string projectName = splitInput[1].Trim();

                FollowProject(userName, projectName);
            }
            // Wall command: <user name> wall
            else if (input.EndsWith("wall"))
            {
                string userName = input.Replace("wall", "").Trim();
                DisplayWall(userName);
            }
        }

        static void PostMessage(string userName, string projectName, string messageText)
        {
            if (!projectMessages.ContainsKey(projectName))
            {
                projectMessages[projectName] = new List<Message>();
            }

            projectMessages[projectName].Add(new Message
            {
                UserName = userName,
                Text = messageText,
                Timestamp = DateTime.Now
            });

            Console.WriteLine($"{userName} posted to {projectName}: {messageText}");
        }

        static void ReadProjectMessages(string projectName)
        {
            if (projectMessages.ContainsKey(projectName))
            {
                foreach (Message message in projectMessages[projectName])
                {
                    Console.WriteLine($"{message.UserName}: {message.Text} ({GetTimeAgo(message.Timestamp)})");
                }
            }
            else
            {
                Console.WriteLine($"No messages for project {projectName}");
            }
        }

        static void FollowProject(string userName, string projectName)
        {
            if (!userFollows.ContainsKey(userName))
            {
                userFollows[userName] = new List<string>();
            }

            if (!userFollows[userName].Contains(projectName))
            {
                userFollows[userName].Add(projectName);
                Console.WriteLine($"{userName} now follows {projectName}");
            }
        }

        static void DisplayWall(string userName)
        {
            if (!userFollows.ContainsKey(userName))
            {
                Console.WriteLine($"{userName} is not following any projects.");
                return;
            }

            List<string> followedProjects = userFollows[userName];
            List<Message> allMessages = new List<Message>();

            foreach (string project in followedProjects)
            {
                if (projectMessages.ContainsKey(project))
                {
                    foreach (Message message in projectMessages[project])
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

        static string GetTimeAgo(DateTime timestamp)
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

    class Message
    {
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
