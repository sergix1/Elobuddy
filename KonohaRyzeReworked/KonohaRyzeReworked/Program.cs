using EloBuddy;
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
            if (Player.Instance.Hero != Champion.Ryze) return;
            new RyzeMain();
        }
    }
}
