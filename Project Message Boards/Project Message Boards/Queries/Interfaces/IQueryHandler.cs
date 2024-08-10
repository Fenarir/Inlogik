namespace ProjectMessageBoard.Queries
{
    public interface IQueryHandler
    {
        void Handle(ReadProjectMessagesQuery query);
        void Handle(DisplayWallQuery query);
    }
}
