using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RTSync.Client
{
    public class ClientHandler : BaseScript
    {
        private DateTimeOffset serverTimeBase;
        private int gameTime;
        private bool start;
        private object sync;

        public ClientHandler()
        {
            this.start = false;
            this.gameTime = API.GetGameTimer();
            this.sync = new object();
            EventHandlers["RTSync"] += new Action<string>(RTSync);
            Tick += TimeUpdate;
        }

        private void RTSync(string time)
        {
            lock (this.sync)
            {
                Debug.WriteLine($"[RTSync] Time update: {time}");
                this.serverTimeBase = DateTimeOffset.Parse(time);
                this.gameTime = API.GetGameTimer();
                this.start = true;
            }
        }

        private async Task TimeUpdate()
        {
            lock (sync)
            {
                if (start)
                {
                    DateTimeOffset time = this.serverTimeBase.AddMilliseconds(API.GetGameTimer() - this.gameTime);
                    API.NetworkOverrideClockTime(time.Hour, time.Minute, time.Second);
                }
            }

            await Delay(100);
        }
    }
}