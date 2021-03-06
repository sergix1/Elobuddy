﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using System.Diagnostics;
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
        Humanizer humanizer;
        System.Diagnostics.Stopwatch _stopWatch;
        public Stopwatch Time
        {
           get
            {
                return _stopWatch;
            }
        }
        public Modes Modes
        {
            get
            {
                return _modes;
            }
        }
        private Menus _menus;
        public AIHeroClient Hero
        {
            get
            {
             
                return ObjectManager.Player;
            }
        }
        public Menus Menu
        {
            get { return _menus; }
        }
        public Spells SpellsObj
        {
            get { return _spells; }

        }
        public RyzeMain()
        {


            Loading.OnLoadingComplete += OnLoad;

        }
        public void OnProcessSpell()
        {

        }
        public void Update(EventArgs updateArgs)
        {
            humanizer.Humanize(this);
        }
        public void onprocess()
        {

        }
        public void Draw(EventArgs drawingArgs)
        {
            var qSpell = _menus.DrawMenu["DQ"].Cast<CheckBox>().CurrentValue;
            var wSpell = _menus.DrawMenu["DW"].Cast<CheckBox>().CurrentValue;
            var eSpell = _menus.DrawMenu["DE"].Cast<CheckBox>().CurrentValue;
          
            if (qSpell)
                if(_spells.Q.IsReady())
                Circle.Draw(Color.DeepSkyBlue, _spells.Q.Range, Player.Instance.Position);
            if (wSpell)
                if (_spells.W.IsReady())
                    Circle.Draw(Color.DeepSkyBlue, _spells.W.Range, Player.Instance.Position);
            if (eSpell)
                if (_spells.E.IsReady())
                    Circle.Draw(Color.DeepSkyBlue, _spells.E.Range, Player.Instance.Position);

        }

        public int GetPassiveBuff
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
        Humanizer h;
        private void OnLoad(EventArgs args)
        {
            if (ObjectManager.Player.Hero != Champion.Ryze) return;
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
          humanizer=new Humanizer();
   
            _modes = new Modes();
            _menus = new Menus();
            _spells = new Spells();
            Game.OnUpdate += Update;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpell;
            Drawing.OnDraw += Draw;
        ;
         //   Drawing.OnEndScene += _spells.DrawDamage;
        }

        public float oldtime;
      public  float time_wait;
        public Random random = new Random(Environment.TickCount);
        private void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
         
            if (sender.IsMe)
            {
                //   if ((Game.Time - oldtime) > 0.4)
                //         {

                if (_modes.Functions != null)
                    {
                        if (_modes.Functions[_modes.I] == "Q")
                            if (args.Slot == SpellSlot.Q)
                            {
                                _modes.Rev = true;
                            }
                        if (_modes.Functions[_modes.I] == "W")
                            if (args.Slot == SpellSlot.W)
                            {
                                _modes.Rev = true;
                            }
                        if (_modes.Functions[_modes.I] == "E")
                            if (args.Slot == SpellSlot.E)
                            {
                                _modes.Rev = true;
                            }
                        if (_modes.Functions[_modes.I] == "R")
                            if (args.Slot == SpellSlot.R)
                            {
                                _modes.Rev = true;
                            }

                    }
                    else
                    {
                        if (args.Slot == SpellSlot.Q)
                        {
                            _modes.Qcast = false;
                        }
                        if (args.Slot == SpellSlot.W)
                        {
                            _modes.Qcast = true;
                        }
                        if (args.Slot == SpellSlot.E)
                        {
                            _modes.Qcast = true;
                        }
                        if (args.Slot == SpellSlot.R)
                        {
                            _modes.Qcast = true;
                        }
                    }
                var maxCast = Menu.HumanizerMenu["SliderH"].Cast<Slider>().CurrentValue;
                var minCast = Menu.HumanizerMenu["SliderHM"].Cast<Slider>().CurrentValue;
                try
                {
                    time_wait = random.Next(minCast, maxCast);
                }
                catch {
                    Console.WriteLine("The big is the other man!");
                }
                oldtime =_stopWatch.ElapsedMilliseconds;
            }
           
         //   }
        }
    }
}
