namespace ProjectMessageBoard.Commands
{
    public interface ICommandHandler
    {
        void Handle(PostMessageCommand command);
        void Handle(FollowProjectCommand command);
    }
}
