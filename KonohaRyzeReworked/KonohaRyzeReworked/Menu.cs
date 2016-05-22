
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy;

namespace KonohaRyzeReworked
{
    class Menus
    {
        public  Menu menu, ComboMenu, DrawMenu, HarrashMenu, LaneClearMenu;
        public Menus()
        {
            Chat.Print("hEY BODDT");
            load();
        }
        private void load()
        {
   
               menu =  MainMenu.AddMenu("Ryze", "Ryze");

              ComboMenu = menu.AddSubMenu("Combo", "combo");
              HarrashMenu = menu.AddSubMenu("Harrash", "Harrash");
              HarrashMenu.Add("HMANA", new Slider("Min. mana for harrash :", 40, 0, 100));

              LaneClearMenu = menu.AddSubMenu("Laneclear", "Laneclear");
              LaneClearMenu.Add("LQ", new CheckBox("Use Q"));
              LaneClearMenu.Add("LW", new CheckBox("Use W"));
              LaneClearMenu.Add("LE", new CheckBox("Use E"));
              LaneClearMenu.Add("LR", new CheckBox("Use R"));
              LaneClearMenu.Add("LMANA", new Slider("Min. mana for laneclear :", 0, 0, 100));
              DrawMenu = menu.AddSubMenu("Draw", "draw");

              HarrashMenu.Add("HQ", new CheckBox("Use Q"));
              ComboMenu.Add("CQ", new CheckBox("Use Q"));
              ComboMenu.Add("CE", new CheckBox("Use E"));
              ComboMenu.Add("CW", new CheckBox("Use W"));
              ComboMenu.Add("CR", new CheckBox("Use R"));
              ComboMenu.Add("CRo", new CheckBox("Use R only on Root"));
              ComboMenu.Add("BlockAA", new CheckBox("Block AutoAttacks on combo"));
              DrawMenu.Add("DQ", new CheckBox("Draw Q"));
              DrawMenu.Add("DW", new CheckBox("Draw W"));
              DrawMenu.Add("DE", new CheckBox("Draw E"));
              DrawMenu.Add("DD", new CheckBox("Draw Damage"));


        }
    }
}
