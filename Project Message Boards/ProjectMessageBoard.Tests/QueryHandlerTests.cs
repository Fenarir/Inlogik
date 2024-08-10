using ProjectMessageBoard.Models;
using ProjectMessageBoard.Queries;

namespace ProjectMessageBoard.Tests
{
    public class QueryHandlerTests
    {
        private readonly Dictionary<string, List<Message>> _projectMessages;
        private readonly Dictionary<string, List<string>> _userFollows;
        private readonly QueryHandler _queryHandler;

        public QueryHandlerTests()
        {
            _projectMessages = new Dictionary<string, List<Message>>();
            _userFollows = new Dictionary<string, List<string>>();
            _queryHandler = new QueryHandler(_projectMessages, _userFollows);

            // Setup initial data
            _projectMessages["Moonshot"] = new List<Message>
            {
                new Message { UserName = "Alice", Text = "I'm working on the log on screen", Timestamp = DateTime.Now.AddMinutes(-10) },
                new Message { UserName = "Bob", Text = "Awesome, I'll start on the forgotten password screen", Timestamp = DateTime.Now.AddMinutes(-5) },
                new Message { UserName = "Bob", Text = "I'll give you the link to put on the log on screen shortly Alice", Timestamp = DateTime.Now.AddMinutes(-2) }
            };

            _projectMessages["Apollo"] = new List<Message>
            {
                new Message { UserName = "Bob", Text = "Has anyone thought about the next release demo?", Timestamp = DateTime.Now.AddMinutes(-7) }
            };

            _userFollows["Charlie"] = new List<string>
            {
                "Apollo",
                "Moonshot"
            };
        }

        [Fact]
        public void TestCharlieWallMessages()
        {
            var query = new DisplayWallQuery { UserName = "Charlie" };

            var stringWriter = new StringWriter();
            var originalConsoleOut = Console.Out;
            Console.SetOut(stringWriter);

            _queryHandler.Handle(query);

            var output = stringWriter.ToString();
            Console.SetOut(originalConsoleOut);

            Assert.Contains("Moonshot - Alice: I'm working on the log on screen", output);
            Assert.Contains("Moonshot - Bob: Awesome, I'll start on the forgotten password screen", output);
            Assert.Contains("Moonshot - Bob: I'll give you the link to put on the log on screen shortly Alice", output);
            Assert.Contains("Apollo - Bob: Has anyone thought about the next release demo?", output);
        }

        [Fact]
        public void TestReadProjectMessages()
        {
            var query = new ReadProjectMessagesQuery { ProjectName = "Moonshot" };

            var stringWriter = new StringWriter();
            var originalConsoleOut = Console.Out;
            Console.SetOut(stringWriter);

            _queryHandler.Handle(query);

            var output = stringWriter.ToString();
            Console.SetOut(originalConsoleOut);

            Assert.Contains("Alice: I'm working on the log on screen", output);
            Assert.Contains("Bob: Awesome, I'll start on the forgotten password screen", output);
            Assert.Contains("Bob: I'll give you the link to put on the log on screen shortly Alice", output);
        }
    }
}
