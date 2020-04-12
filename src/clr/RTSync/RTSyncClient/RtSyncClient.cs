using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RTSync.Client
{
    public class ClientHandler : BaseScript
    {
        public ClientHandler()
        {
            EventHandlers["RTSync"] += new Action<int, int, int>(RTSync);
        }

        private void RTSync(int hour, int minute, int second)
        {
            API.NetworkOverrideClockTime(hour, minute, second);
        }
    }
}
