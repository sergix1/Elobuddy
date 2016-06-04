
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
        public  Menu menu, ComboMenu, DrawMenu, HarrashMenu, LaneClearMenu,JungleclearMenu,HumanizerMenu;
        public Menus()
        {
            load();
        }
        private void load()
        {
   
               menu =  MainMenu.AddMenu("Ryze", "Ryze");
              HarrashMenu = menu.AddSubMenu("Harrash", "Harrash");
            HarrashMenu.Add("HMANA", new Slider("Min. mana for harrash :", 40, 0, 100));
              LaneClearMenu = menu.AddSubMenu("Laneclear", "Laneclear");
              LaneClearMenu.Add("LQ", new CheckBox("Use Q"));
              LaneClearMenu.Add("LW", new CheckBox("Use W"));
              LaneClearMenu.Add("LE", new CheckBox("Use E"));
              LaneClearMenu.Add("LR", new CheckBox("Use R"));
              LaneClearMenu.Add("LMANA", new Slider("Min. mana for laneclear :", 0, 0, 100));
              DrawMenu = menu.AddSubMenu("Draw", "draw"); 
              DrawMenu.Add("DQ", new CheckBox("Draw Q"));
              DrawMenu.Add("DW", new CheckBox("Draw W"));
              DrawMenu.Add("DE", new CheckBox("Draw E"));
            DrawMenu.Add("DD", new CheckBox("Draw Damage"));
            JungleclearMenu = menu.AddSubMenu("Jungleclear", "Jungleclear");
            JungleclearMenu.Add("JQ", new CheckBox("Use Q"));
            JungleclearMenu.Add("JW", new CheckBox("Use W"));
            JungleclearMenu.Add("JE", new CheckBox("Use E"));
            JungleclearMenu.Add("JR", new CheckBox("Use R"));
            HumanizerMenu = menu.AddSubMenu("Humanizer", "Humanizer");
            HumanizerMenu.Add("Active", new CheckBox("Active"));
            HumanizerMenu.Add("SliderH", new Slider("Max ticks/s for cast :", 500, 0, 1000));
            HumanizerMenu.Add("SliderHM", new Slider("Min ticks/s for cast :", 250, 0, 1000));
        }
    }
}
