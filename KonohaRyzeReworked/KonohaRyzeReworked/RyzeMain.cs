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
                if (ObjectManager.Player == null)
                    Console.WriteLine("nullisimo");
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
            _modes.update(this);
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
                Circle.Draw(Color.AliceBlue, _spells.Q.Range, Player.Instance.Position);
            if (wSpell)
                Circle.Draw(Color.AliceBlue, _spells.W.Range, Player.Instance.Position);
            if (eSpell)
                Circle.Draw(Color.DarkGray, _spells.E.Range, Player.Instance.Position);

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

        private void OnLoad(EventArgs args)
        {
            Game.OnUpdate += Update;
            _modes = new Modes();
            _menus = new Menus();
            _spells = new Spells();
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpell;
            Drawing.OnDraw += Draw;
        }

        private void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
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
            }
        }
    }
}
