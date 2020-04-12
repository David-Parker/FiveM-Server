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
            EventHandlers["RTSyncPlayerConnected"] += new Action<Player>(PlayerConnected);
        }

        private async Task TimeSync()
        {
            TriggerClientEvent("RTSync", GetTime().ToString());

            // Sync every minute
            await Delay(1 * 60 * 1000);
        }

        private DateTimeOffset GetTime()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            int offset = API.GetConvarInt("rtsync_timezone_offset", 0);
            now = now.AddHours(offset);

            return now;
        }

        private void PlayerConnected([FromSource] Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            TriggerClientEvent(player, "RTSync", GetTime().ToString());
        }
    }
}