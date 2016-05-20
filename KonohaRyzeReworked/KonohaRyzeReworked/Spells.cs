using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonohaRyzeReworked
{
    class Spells
    {
        private static bool Enabled;
        private const int XOffset = 10;
        private const int YOffset = 0;
        private const int Width = 103;
        private const int Height = 8;
        private const int BarWidth = 104;
        public delegate float DamageToUnitDelegate(AIHeroClient hero);
        private static DamageToUnitDelegate DamageToUnit { get; set; }
        private static readonly Vector2 BarOffset = new Vector2(10, 25);
        private static Spell.Skillshot _q;
        private static Spell.Targeted _w, _e;
        private static Spell.Active _r;
        public Spell.Skillshot Q
        {
         
            get
            {
                return _q;
            }
        }
        public Spell.Targeted W
        {
         
            get
            {
                return _w;
            }
        }
        public Spell.Targeted E
        {
      
            get
            {
                return _e;
            }
        }
        public Spell.Active R
        {
            get { return _r; }

        }
        public Spells()
        {
            _q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1700, 100);
            _w = new Spell.Targeted(SpellSlot.W, 600);
            _e = new Spell.Targeted(SpellSlot.E, 600);
            _r = new Spell.Active(SpellSlot.R);
        }

        public float QDamage(Obj_AI_Base target)
        {
            return ObjectManager.Player.CalculateDamageOnUnit(
                target,
                DamageType.Magical,
                (float)
                (new double[] { 60, 85, 110, 135, 160 }[Q.Level - 1] + 0.55 * ObjectManager.Player.FlatMagicDamageMod
                 + new double[] { 2, 2.5, 3.0, 3.5, 4.0 }[Q.Level - 1] / 100 * ObjectManager.Player.MaxMana));
        }

        public  float WDamage(Obj_AI_Base target)
        {
            return ObjectManager.Player.CalculateDamageOnUnit(
                target,
                DamageType.Magical,
                new[] { 80, 100, 120, 140, 160 }[W.Level - 1] + 0.4f * ObjectManager.Player.FlatMagicDamageMod
                + 0.02f * ObjectManager.Player.MaxMana);
        }

        public  float EDamage(Obj_AI_Base target)
        {
            return ObjectManager.Player.CalculateDamageOnUnit(
                target,
                DamageType.Magical,
                new[] { 36, 52, 68, 84, 100 }[E.Level - 1] + 0.2f * ObjectManager.Player.FlatMagicDamageMod + 0.025f * ObjectManager.Player.MaxMana);
        }
        public  float GetComboDamage(Obj_AI_Base enemy)
        {
            float damage = 0;
            if (Q.IsReady() || Player.Instance.Mana <= Q.Handle.SData.Mana)
                damage += QDamage(enemy);

            if (E.IsReady() || Player.Instance.Mana <= E.Handle.SData.Mana)
                damage += EDamage(enemy);

            if (W.IsReady() || Player.Instance.Mana <= W.Handle.SData.Mana)
                damage += WDamage(enemy);
            return damage;
        }
        public static void Initialize(DamageToUnitDelegate damageToUnit)
        {
            // Apply needed field delegate for damage calculation
            DamageToUnit = damageToUnit;
            DrawingColor = SharpDX.Color.Green;
            Enabled = true;

            // Register event handlers
            Drawing.OnEndScene += DrawDamage;
        }
        public static void DrawDamage(EventArgs args)
        {

            foreach (var unit in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.IsHPBarRendered))
            {
                var damage = DamageToUnit(unit);

                // Continue on 0 damage
                //Chat.Print(""+damage);
                if (damage <= 0)
                    continue;

                // Get remaining HP after damage applied in percent and the current percent of health
                var damagePercentage = ((unit.Health - damage) > 0 ? (unit.Health - damage) : 0) / unit.MaxHealth;
                var currentHealthPercentage = unit.Health / unit.MaxHealth;

                // Calculate start and end point of the bar indicator
                var startPoint = new Vector2((int)(unit.HPBarPosition.X + BarOffset.X + damagePercentage * BarWidth), (int)(unit.HPBarPosition.Y + BarOffset.Y) - 5);
                var endPoint = new Vector2((int)(unit.HPBarPosition.X + BarOffset.X + currentHealthPercentage * BarWidth) + 1, (int)(unit.HPBarPosition.Y + BarOffset.Y) - 5);

                // Draw the line
                Drawing.DrawLine(startPoint, endPoint, 9, System.Drawing.Color.Red);
            }
        }

        public static Color DrawingColor { get; set; }
    }
}

