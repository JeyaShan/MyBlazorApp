namespace MYBlazorAPP.Services
{
    public class HeaderMessageServices
    {
        public event Action<string>? onMessageReceived;

        public void SendMessage(string message)
        {
            onMessageReceived?.Invoke(message);
        }
    }
}
