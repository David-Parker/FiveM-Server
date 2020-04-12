using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RTSync.Client
{
    public class ClientHandler : BaseScript
    {
        private DateTimeOffset serverTimeBase;
        private Stopwatch stopwatch;
        private bool start = false;
        private object sync;

        public ClientHandler()
        {
            this.stopwatch = new Stopwatch();
            this.sync = new object();
            EventHandlers["RTSync"] += new Action<string>(RTSync);
            Tick += TimeUpdate;
        }

        private void RTSync(string time)
        {
            lock (this.sync)
            {
                this.serverTimeBase = DateTimeOffset.Parse(time);
                this.stopwatch.Restart();
                this.start = true;
            }
        }

        private async Task TimeUpdate()
        {
            lock (sync)
            {
                if (start)
                {
                    DateTimeOffset time = this.serverTimeBase.AddMilliseconds(this.stopwatch.ElapsedMilliseconds);
                    API.NetworkOverrideClockTime(time.Hour, time.Minute, time.Second);
                }
            }

            await Delay(100);
        }
    }
}