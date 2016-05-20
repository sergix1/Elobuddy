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
        public void update(RyzeMain ryze)
        {
          
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
            {
                Combo(ryze);
            }
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass)
            {
                Harrash(ryze);
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Laneclear(ryze);
            }
           
        }
        public void Combo(RyzeMain ryze)
        {
            var target = TargetSelector.GetTarget(570, DamageType.Magical);
            
            if (target != null)
            {
                var qpred = ryze.SpellsObj.Q.GetPrediction(target);
                var q = qpred.CollisionObjects;
                var qSpell = ryze.Menu.ComboMenu["CQ"].Cast<CheckBox>().CurrentValue;
                var eSpell = ryze.Menu.ComboMenu["CE"].Cast<CheckBox>().CurrentValue;
                var wSpell = ryze.Menu.ComboMenu["CW"].Cast<CheckBox>().CurrentValue;
                var rSpell = ryze.Menu.ComboMenu["CR"].Cast<CheckBox>().CurrentValue;
                var rwwSpell = ryze.Menu.ComboMenu["CRo"].Cast<CheckBox>().CurrentValue;
                if (target.IsValidTarget(ryze.SpellsObj.Q.Range))
                {
                    if (ryze.GetPassiveBuff <= 2 || !ObjectManager.Player.HasBuff("RyzePassiveStack"))
                    {
                        if (target.IsValidTarget(ryze.SpellsObj.Q.Range) && qSpell && ryze.SpellsObj.Q.IsReady()) ryze.SpellsObj.Q.Cast(qpred.UnitPosition);

                        if (target.IsValidTarget(ryze.SpellsObj.W.Range) && wSpell && ryze.SpellsObj.W.IsReady()) ryze.SpellsObj.W.Cast(target);

                        if (target.IsValidTarget(ryze.SpellsObj.E.Range) && eSpell && ryze.SpellsObj.E.IsReady()) ryze.SpellsObj.E.Cast(target);

                        if (ryze.SpellsObj.R.IsReady() && rSpell)
                        {
                            if (target.IsValidTarget(ryze.SpellsObj.W.Range) && target.Health > (ryze.SpellsObj.QDamage(target) + ryze.SpellsObj.EDamage(target)))
                            {
                                
                                if (rwwSpell && target.HasBuff("RyzeW")) ryze.SpellsObj.R.Cast();
                                if (!rwwSpell) ryze.SpellsObj.R.Cast();
                            }
                        }
                    }


                    if (ryze.GetPassiveBuff == 3)
                    {
                        if (ryze.SpellsObj.Q.IsReady() && target.IsValidTarget(ryze.SpellsObj.Q.Range)) ryze.SpellsObj.Q.Cast(qpred.UnitPosition);

                        if (ryze.SpellsObj.E.IsReady() && target.IsValidTarget(ryze.SpellsObj.E.Range)) ryze.SpellsObj.E.Cast(target);

                        if (ryze.SpellsObj.W.IsReady() && target.IsValidTarget(ryze.SpellsObj.W.Range)) ryze.SpellsObj.W.Cast(target);

                        if (ryze.SpellsObj.R.IsReady() && rSpell)
                        {
                            if (target.IsValidTarget(ryze.SpellsObj.W.Range) && target.Health > (ryze.SpellsObj.QDamage(target) + ryze.SpellsObj.EDamage(target)))
                            {
                                if (rwwSpell && target.HasBuff("RyzeW")) ryze.SpellsObj.R.Cast();
                                if (!rwwSpell) ryze.SpellsObj.R.Cast();
                            }
                        }
                    }

                    if (ryze.GetPassiveBuff == 4)
                    {
                        if (target.IsValidTarget(ryze.SpellsObj.W.Range) && wSpell && ryze.SpellsObj.W.IsReady()) ryze.SpellsObj.W.Cast(target);

                        if (target.IsValidTarget(ryze.SpellsObj.Q.Range) && ryze.SpellsObj.Q.IsReady() && qSpell) ryze.SpellsObj.Q.Cast(qpred.UnitPosition);

                        if (target.IsValidTarget(ryze.SpellsObj.E.Range) && ryze.SpellsObj.E.IsReady() && eSpell) ryze.SpellsObj.E.Cast(target);

                        if (ryze.SpellsObj.R.IsReady() && rSpell)
                        {
                            if (target.IsValidTarget(ryze.SpellsObj.W.Range) && target.Health > (ryze.SpellsObj.QDamage(target) + ryze.SpellsObj.EDamage(target)))
                            {
                                if (rwwSpell && target.HasBuff("RyzeW")) ryze.SpellsObj.R.Cast();
                                if (!rwwSpell) ryze.SpellsObj.R.Cast();
                            }
                        }
                    }

                    if (ryze.Hero.HasBuff("ryzepassivecharged"))
                    {
                        if (wSpell && ryze.SpellsObj.W.IsReady() && target.IsValidTarget(ryze.SpellsObj.W.Range)) ryze.SpellsObj.W.Cast(target);

                        if (qSpell && ryze.SpellsObj.Q.IsReady() && target.IsValidTarget(ryze.SpellsObj.Q.Range)) ryze.SpellsObj.Q.Cast(qpred.UnitPosition);

                        if (eSpell && ryze.SpellsObj.E.IsReady() && target.IsValidTarget(ryze.SpellsObj.E.Range)) ryze.SpellsObj.E.Cast(target);

                        if (ryze.SpellsObj.R.IsReady() && rSpell)
                        {
                            if (target.IsValidTarget(ryze.SpellsObj.W.Range) && target.Health > (ryze.SpellsObj.QDamage(target)) + ryze.SpellsObj.EDamage(target))
                            {
                                if (rwwSpell && target.HasBuff("RyzeW")) ryze.SpellsObj.R.Cast();
                                if (!rwwSpell) ryze.SpellsObj.R.Cast();
                                if (!ryze.SpellsObj.E.IsReady() && !ryze.SpellsObj.Q.IsReady() && !ryze.SpellsObj.W.IsReady()) ryze.SpellsObj.R.Cast();
                            }
                        }
                    }
                }
                else
                {
                    if (wSpell && ryze.SpellsObj.W.IsReady() && target.IsValidTarget(ryze.SpellsObj.W.Range)) ryze.SpellsObj.W.Cast(target);

                    if (qSpell && ryze.SpellsObj.Q.IsReady() && target.IsValidTarget(ryze.SpellsObj.Q.Range)) ryze.SpellsObj.Q.Cast(qpred.UnitPosition);

                    if (eSpell && ryze.SpellsObj.E.IsReady() && target.IsValidTarget(ryze.SpellsObj.E.Range)) ryze.SpellsObj.E.Cast(target);
                }
                if (!ryze.SpellsObj.R.IsReady() || ryze.GetPassiveBuff != 4 || !rSpell) return;

                if (ryze.SpellsObj.Q.IsReady() || ryze.SpellsObj.W.IsReady() || ryze.SpellsObj.E.IsReady()) return;

                ryze.SpellsObj.R.Cast();

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
                    if (ryze.SpellsObj.Q.GetPrediction(target).HitChance == HitChance.High)
                    {
                        ryze.SpellsObj.Q.Cast(target);
                    }
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
