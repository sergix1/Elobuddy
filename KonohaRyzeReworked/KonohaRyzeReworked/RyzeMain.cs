using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

using EloBuddy.SDK.Menu.Values;

using EloBuddy;
using SharpDX;
using EloBuddy.SDK.Rendering;

namespace KonohaRyzeReworked
{
    class RyzeMain
    {
       private Spells _spells;
        private Modes _modes;
       private Menus _menus;
        public AIHeroClient Hero
        {
            get
            {
                if(ObjectManager.Player==null)
                Console.WriteLine("nullisimo");
                return ObjectManager.Player;
            }
        }
        public Menus Menu
        {
            get { return _menus; }
        }
        public Spells SpellsObj {
            get{ return _spells;}
           
            }
        public RyzeMain()
        {
            Chat.Print("ryze loaded");
    
            Loading.OnLoadingComplete += OnLoad;
         
        }
        public void Update(EventArgs updateArgs)
        {
            _modes.update(this);
        }
        public void Draw(EventArgs drawingArgs)
        {
                 var qSpell = _menus.DrawMenu["DQ"].Cast<CheckBox>().CurrentValue;
               var wSpell = _menus.DrawMenu["DW"].Cast<CheckBox>().CurrentValue;
             var eSpell = _menus.DrawMenu["DE"].Cast<CheckBox>().CurrentValue;
             if (qSpell)
            Circle.Draw(Color.AliceBlue, _spells.Q.Range, Player.Instance.Position);
             if (wSpell) 
            Circle.Draw(Color.AliceBlue, _spells.W.Range, Player.Instance.Position);
            if (eSpell) 
            Circle.Draw(Color.DarkGray, _spells.E.Range, Player.Instance.Position);

        }

        public  int GetPassiveBuff
        {
            get
            {
                var data = ObjectManager.Player.Buffs.FirstOrDefault(b => b.DisplayName == "RyzePassiveStack");
                if (data != null)
                {
                    return data.Count == -1 ? 0 : data.Count == 0 ? 1 : data.Count;
                }
                return 0;
            }
        }

        private  void OnLoad(EventArgs args)
        {
            Game.OnUpdate += Update;       
            _modes = new Modes();
            _menus = new Menus();
            _spells = new Spells();
            Drawing.OnDraw += Draw;
        }
    }
}
