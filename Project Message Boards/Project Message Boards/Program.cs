using ProjectMessageBoard.Commands;
using ProjectMessageBoard.Models;
using ProjectMessageBoard.Queries;

namespace ProjectMessageBoard
{
    class Program
    {
        // Data structures for storing messages and user follows
        static readonly Dictionary<string, List<Message>> projectMessages = new Dictionary<string, List<Message>>();
        static readonly Dictionary<string, List<string>> userFollows = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            ICommandHandler commandHandler = new CommandHandler(projectMessages, userFollows);
            IQueryHandler queryHandler = new QueryHandler(projectMessages, userFollows);

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                string commandType = DetermineCommandType(input);

                switch (commandType)
                {
                    case "PostMessage":
                        ProcessPostMessage(input, commandHandler);
                        break;

                    case "ReadProjectMessages":
                        ProcessReadProjectMessages(input, queryHandler);
                        break;

                    case "FollowProject":
                        ProcessFollowProject(input, commandHandler);
                        break;

                    case "DisplayWall":
                        ProcessDisplayWall(input, queryHandler);
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        static string DetermineCommandType(string input)
        {
            if (input.Contains("-> @"))
                return "PostMessage";

            if (!input.Contains(" "))
                return "ReadProjectMessages";

            if (input.Contains("follows"))
                return "FollowProject";

            if (input.EndsWith("wall"))
                return "DisplayWall";

            return "Unknown";
        }

        static void ProcessPostMessage(string input, ICommandHandler commandHandler)
        {
            string[] splitInput = input.Split(new[] { "-> @" }, 2, StringSplitOptions.None);
            string userName = splitInput[0].Trim();
            string[] projectAndMessage = splitInput[1].Split(new[] { ' ' }, 2);
            string projectName = projectAndMessage[0].Trim();
            string messageText = projectAndMessage[1].Trim();

            var command = new PostMessageCommand
            {
                UserName = userName,
                ProjectName = projectName,
                MessageText = messageText
            };

            commandHandler.Handle(command);
        }

        static void ProcessReadProjectMessages(string input, IQueryHandler queryHandler)
        {
            string projectName = input.Trim();
            var query = new ReadProjectMessagesQuery
            {
                ProjectName = projectName
            };
            queryHandler.Handle(query);
        }

        static void ProcessFollowProject(string input, ICommandHandler commandHandler)
        {
            string[] splitInput = input.Split("follows");
            string userName = splitInput[0].Trim();
            string projectName = splitInput[1].Trim();

            var command = new FollowProjectCommand
            {
                UserName = userName,
                ProjectName = projectName
            };

            commandHandler.Handle(command);
        }

        static void ProcessDisplayWall(string input, IQueryHandler queryHandler)
        {
            string userName = input.Replace("wall", "").Trim();
            var query = new DisplayWallQuery
            {
                UserName = userName
            };
            queryHandler.Handle(query);
        }
    }
}
