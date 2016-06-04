using EloBuddy;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonohaRyzeReworked
{
    class Humanizer
    {
        public Random random = new Random(Environment.TickCount);
        public Humanizer()
        {

        }
        float timeElapsed;
        public void Humanize(RyzeMain ryze)
        {

          var activeh = ryze.Menu.HumanizerMenu["Active"].Cast<CheckBox>().CurrentValue;
            if ((activeh && (ryze.Time.ElapsedMilliseconds-ryze.oldtime) > ryze.time_wait) || (!activeh))
            {


                ryze.Modes.update(ryze);
            }

        }
    }
}
