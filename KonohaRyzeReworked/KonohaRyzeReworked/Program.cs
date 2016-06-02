using EloBuddy;
using EloBuddy.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonohaRyzeReworked
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoad;
     
        }

        private static void OnLoad(EventArgs args)
        {
            if (ObjectManager.Player.Hero != Champion.Ryze) return;
            var n = new RyzeMain();

        }
    }
}
