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
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
        }

        private async Task TimeSync()
        {
            TriggerClientEvent("RTSync", GetTime().ToString());

            // Sync every 30s
            await Delay(30000);
        }

        private void OnPlayerConnecting([FromSource]Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            TriggerClientEvent(player, "RTSync", GetTime().ToString());
        }

        private DateTimeOffset GetTime()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            int offset = API.GetConvarInt("rtsync_timezone_offset", 0);
            now = now.AddHours(offset);

            return now;
        }
    }
}
