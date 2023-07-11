namespace MelonBookshelfApi.Services.Contracts
{
    public interface IMessageSender
    {
        Task SendMessage(string to, string content);
    }
}
