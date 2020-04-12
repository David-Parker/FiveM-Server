using System;
using System.Threading.Tasks;
using CitizenFX.Core;

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

            DateTime now = DateTime.Now;

            TriggerClientEvent("RTSync", now.Hour, now.Minute, now.Second);
        }
    }
}
