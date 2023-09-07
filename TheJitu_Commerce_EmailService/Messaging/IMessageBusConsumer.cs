namespace TheJitu_Commerce_EmailService.Messaging
{
    public interface IMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
