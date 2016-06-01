using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonohaRyzeReworked
{
    class Modes
    {
        public Modes()
        {

        }
      private  List<String> functions = new List<String>();
        public List<String>  Functions
        {
            get
            {
                return functions;
            }
        }
       private int i;
        public int I
        {
            get
            {
                return i;
            }
        }
      private  bool rev;
        public bool Rev
        {
           get
        {
                return rev;
              
            }
            set
            {
                rev = value;
            }
                }
       private bool qcast;
        public bool Qcast
        {
            get
            {
                return qcast;
            }
            set
            {
                qcast = value;
            }
        }
        public void update(RyzeMain ryze)
        {
            //  if(ryze.GetPassiveBuff==0)
          //  {
                //     i = 0;
                //     functions = null;
                //    rev = false;
                //    }
                if (functions != null)
                {
                    if (i < functions.Count)
                    {

                        sendSpell(functions[i], ryze);
                        if (rev)
                        {

                            i++;
                            rev = false;
                        }
                    }
                    else
                    {

                        i = 0;
                        functions = null;
                        rev = false;
                    }

                }
                if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                {
                    Combo(ryze);
                }
                else
                {

                    i = 0;
                    rev = false;
                    functions = null;
                }
                if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass)
                {
                    Harrash(ryze);
                }
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                {
                    Laneclear(ryze);
                }
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                {
                    JungleClear(ryze);
                }

            }
        //}
        private static void JungleClear(RyzeMain ryze)
        {
            var jungleclearQ = ryze.Menu.JungleclearMenu["JQ"].Cast<CheckBox>().CurrentValue;
            var jungleclearW = ryze.Menu.JungleclearMenu["JW"].Cast<CheckBox>().CurrentValue;
            var jungleclearE = ryze.Menu.JungleclearMenu["JE"].Cast<CheckBox>().CurrentValue;
            var jungleclearR = ryze.Menu.JungleclearMenu["JR"].Cast<CheckBox>().CurrentValue;
            Obj_AI_Base minion =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(

                    ObjectManager.Player.Position,
                    600,
                    true).FirstOrDefault();
            if (minion != null)
            {
                if (jungleclearQ && ryze.SpellsObj.Q.IsReady())
                {
                    var Qpred = ryze.SpellsObj.Q.GetPrediction(minion);
                    ryze.SpellsObj.Q.Cast(Qpred.UnitPosition);
                }
                if (jungleclearE && ryze.SpellsObj.E.IsReady())
                {
                    ryze.SpellsObj.E.Cast(minion);
                }
                if (jungleclearW && ryze.SpellsObj.W.IsReady())
                {
                    ryze.SpellsObj.W.Cast(minion);
                }
                if (jungleclearR && ryze.SpellsObj.R.IsReady() && (ryze.GetPassiveBuff >= 4 || ryze.Hero.HasBuff("ryzepassivecharged")))
                {
                    ryze.SpellsObj.R.Cast();
                }
            }
        }

        public void ComboAuto(RyzeMain ryze)
        {

        }
    public bool sendSpell(string s,RyzeMain ryze)
        {
            switch(s)
            {
                case "Q":
                  
                    return ryze.SpellsObj.Qcast();
                  
                case "W":
              
                    return ryze.SpellsObj.Wcast();
           
                case "E":
                
                    return ryze.SpellsObj.Ecast();
                case "R":
                    return ryze.SpellsObj.Rcast();

            }
            return false;
        }
        public void Combo(RyzeMain ryze)
        {
  
            var target = TargetSelector.GetTarget(570, DamageType.Magical);

            if (target != null)
            {
                if (functions == null)
                {
                    if (ryze.SpellsObj.Q.IsReady() && ryze.SpellsObj.W.IsReady() && ryze.SpellsObj.E.IsReady() && ryze.SpellsObj.R.IsReady()&&ryze.GetPassiveBuff>0)
                    {
                        switch (ryze.GetPassiveBuff)
                        {

                            case 1:
                                functions = new List<String> { "R", "E", "Q", "W", "Q", "E", "Q", "W", "Q", "E", "Q" };
                                break;
                            case 2:
                                functions = new List<String> { "R", "Q", "W", "Q", "E", "Q", "W", "Q", "E", "Q" };
                                break;
                            case 3:
                                functions = new List<String> { "R", "W", "Q", "E", "Q", "W", "Q", "E", "Q", "W", "Q" };
                                break;
                            case 4:
                                functions = new List<String> { "R", "W", "Q", "E", "Q", "W", "Q", "E" };
                                break;
                        }
                    }

                    else if (ryze.SpellsObj.Q.IsReady() && ryze.SpellsObj.W.IsReady() && ryze.SpellsObj.E.IsReady() && !ryze.SpellsObj.R.IsReady() && ryze.GetPassiveBuff > 1)

                    {
                        switch (ryze.GetPassiveBuff)
                        {

                            case 2:
                                functions = new List<String> { "Q", "E", "W", "Q", "E", "Q", "W", "Q", "E" };
                                break;
                            case 3:
                                functions = new List<String> { "Q", "W", "Q", "E", "Q", "W", "Q", "E" };
                                break;
                            case 4:
                                functions = new List<String> { "W", "Q", "E", "Q", "W", "Q", "E", "Q", "W", "Q", "E", "Q" };
                                break;
                        }
                    }
                    else
                {

                        if (ryze.Hero.HasBuff("ryzepassivecharged"))
                        {
                       Console.WriteLine("Hey tengo 5 cargas");
                            if (qcast)
                            {
                                if (ryze.SpellsObj.Q.IsReady())
                                    ryze.SpellsObj.Qcast();
                                else if (ryze.SpellsObj.R.IsReady())
                                {
                                    ryze.SpellsObj.Rcast();
                               
                                }

                            }
                            else
                            {
                                if (ryze.SpellsObj.W.IsReady())
                                {

                                    ryze.SpellsObj.Wcast();
                                }
                                else if (ryze.SpellsObj.R.IsReady())
                                {
                                   
                                    ryze.SpellsObj.Rcast();
                                }
                                else if (ryze.SpellsObj.E.IsReady())
                                {
                                    ryze.SpellsObj.Ecast();
                                }



                            }
                         
                        }
                        else
                        {
                    
                            if (ryze.SpellsObj.Q.IsReady())
                            {
                                ryze.SpellsObj.Qcast();
                            }
                            else if(ryze.SpellsObj.W.IsReady())
                            {
                                ryze.SpellsObj.Wcast();
                            }
                            else if (ryze.SpellsObj.E.IsReady())
                            {
                                ryze.SpellsObj.Ecast();
                            }
                        }

                        


                        
                    }
                 
                }
            }
            else
            {
               

            }
        }
        public void Harrash(RyzeMain ryze)
        {
            var HarrashMinMana = ryze.Menu.HarrashMenu["hMANA"].Cast<Slider>().CurrentValue;
            var target = TargetSelector.GetTarget(900, DamageType.Magical);
            var qSpell = ryze.Menu.HarrashMenu["HQ"].Cast<CheckBox>().CurrentValue;
            if (target != null && Player.Instance.ManaPercent > HarrashMinMana)
            {
                var qpred = ryze.SpellsObj.Q.GetPrediction(target);
                if (qSpell)
                {
                    if (ryze.SpellsObj.Q.GetPrediction(target).HitChance >= HitChance.High)
                    {
                        ryze.SpellsObj.Q.Cast(target);
                    }
                }
            }
        }
        public void onProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {

                if (functions != null)
                {
                    if (functions[i] == "Q")
                        if (args.Slot == SpellSlot.Q)
                        {
                            rev = true;
                        }
                    if (functions[i] == "W")
                        if (args.Slot == SpellSlot.E)
                        {
                            rev = true;
                        }
                    if (functions[i] == "E")
                        if (args.Slot == SpellSlot.E)
                        {
                            rev = true;
                        }
                    if (functions[i] == "R")
                        if (args.Slot == SpellSlot.R)
                        {
                            rev = true;
                        }
                }

        }
        public void Laneclear(RyzeMain ryze)
        {
            var laneclearQ = ryze.Menu.LaneClearMenu["LQ"].Cast<CheckBox>().CurrentValue;
            var laneclearW = ryze.Menu.LaneClearMenu["LW"].Cast<CheckBox>().CurrentValue;
            var laneclearE = ryze.Menu.LaneClearMenu["LE"].Cast<CheckBox>().CurrentValue;
            var laneclearR = ryze.Menu.LaneClearMenu["LR"].Cast<CheckBox>().CurrentValue;
            var laneclearMinMana = ryze.Menu.LaneClearMenu["LMANA"].Cast<Slider>().CurrentValue;
            Obj_AI_Base minion =
                EntityManager.MinionsAndMonsters.GetLaneMinions(
                    EntityManager.UnitTeam.Enemy,
                    ObjectManager.Player.Position,
                    600,
                    true).FirstOrDefault();
            if (minion != null && Player.Instance.ManaPercent > laneclearMinMana)
            {
                if (laneclearQ && ryze.SpellsObj.Q.IsReady())
                {
                    var Qpred = ryze.SpellsObj.Q.GetPrediction(minion);
                    ryze.SpellsObj.Q.Cast(Qpred.UnitPosition);
                }
                if (laneclearE && ryze.SpellsObj.E.IsReady())
                {
                    ryze.SpellsObj.E.Cast(minion);
                }
                if (laneclearW && ryze.SpellsObj.W.IsReady())
                {
                    ryze.SpellsObj.W.Cast(minion);
                }
                if (laneclearR && ryze.SpellsObj.R.IsReady() && (ryze.GetPassiveBuff >= 4 || ryze.Hero.HasBuff("ryzepassivecharged")))
                {
                    ryze.SpellsObj.R.Cast();
                }
            }
        }
      
    }
}
