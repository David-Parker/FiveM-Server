using CitizenFX.Core;

namespace RTSync.Client
{
    public class ClientHandler : BaseScript
    {
        public ClientHandler()
        {
            TriggerEvent("chat:addMessage", new
            {
                color = new[] { 255, 0, 0 },
                args = new[] { "[RTSync]", $"Hello, World." }
            });
        }
    }
}
