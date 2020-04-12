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
            API.SetConvarReplicated("vmenu_enable_time_sync", "false");
            Tick += TimeSync;
        }

        private async Task TimeSync()
        {
            DateTime now = DateTime.UtcNow;

            int offset = API.GetConvarInt("rtsync_timezone_offset", 0);
            Debug.WriteLine($"Offset: {offset}");
            now = now.AddHours(offset);

            TriggerClientEvent("RTSync", now.Hour, now.Minute, now.Second);

            // Sync every second
            await Delay(1000);
        }
    }
}
