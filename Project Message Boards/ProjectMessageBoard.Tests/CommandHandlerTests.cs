using ProjectMessageBoard.Commands;
using ProjectMessageBoard.Models;

namespace ProjectMessageBoard.Tests
{
    public class CommandHandlerTests
    {
        private readonly Dictionary<string, List<Message>> _projectMessages;
        private readonly Dictionary<string, List<string>> _userFollows;
        private readonly CommandHandler _commandHandler;

        public CommandHandlerTests()
        {
            _projectMessages = new Dictionary<string, List<Message>>();
            _userFollows = new Dictionary<string, List<string>>();
            _commandHandler = new CommandHandler(_projectMessages, _userFollows);

            // Setup initial data
            _commandHandler.Handle(new PostMessageCommand
            {
                UserName = "Alice",
                ProjectName = "Moonshot",
                MessageText = "I'm working on the log on screen"
            });

            _commandHandler.Handle(new PostMessageCommand
            {
                UserName = "Bob",
                ProjectName = "Moonshot",
                MessageText = "Awesome, I'll start on the forgotten password screen"
            });

            _commandHandler.Handle(new PostMessageCommand
            {
                UserName = "Bob",
                ProjectName = "Apollo",
                MessageText = "Has anyone thought about the next release demo?"
            });

            _commandHandler.Handle(new PostMessageCommand
            {
                UserName = "Bob",
                ProjectName = "Moonshot",
                MessageText = "I'll give you the link to put on the log on screen shortly Alice"
            });

            _commandHandler.Handle(new FollowProjectCommand
            {
                UserName = "Charlie",
                ProjectName = "Apollo"
            });

            _commandHandler.Handle(new FollowProjectCommand
            {
                UserName = "Charlie",
                ProjectName = "Moonshot"
            });
        }

        [Fact]
        public void TestPostMessage()
        {
            Assert.True(_projectMessages.ContainsKey("Moonshot"));
            Assert.True(_projectMessages.ContainsKey("Apollo"));

            var moonshotMessages = _projectMessages["Moonshot"];
            Assert.Equal(3, moonshotMessages.Count);
            Assert.Contains(moonshotMessages, m => m.Text == "I'm working on the log on screen");
            Assert.Contains(moonshotMessages, m => m.Text == "Awesome, I'll start on the forgotten password screen");
            Assert.Contains(moonshotMessages, m => m.Text == "I'll give you the link to put on the log on screen shortly Alice");
        }

        [Fact]
        public void TestFollowProject()
        {
            Assert.True(_userFollows.ContainsKey("Charlie"));
            var followedProjects = _userFollows["Charlie"];
            Assert.Contains("Apollo", followedProjects);
            Assert.Contains("Moonshot", followedProjects);
        }
    }
}
