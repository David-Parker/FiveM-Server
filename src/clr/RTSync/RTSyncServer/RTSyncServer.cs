using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RTSync.Server
{
    public class RTSyncServer : BaseScript
    {
        public RTSyncServer()
        {
            Tick += TimeSync;
        }

        private async Task TimeSync()
        {
            // Sync every 30s
            await Delay(30000);

            DateTime now = DateTime.UtcNow;
            int offset = API.GetConvarInt("rtsync_timezone_offset", 0);
            now.AddHours(offset);

            TriggerClientEvent("RTSync", now.Hour, now.Minute, now.Second);
        }
    }
}
