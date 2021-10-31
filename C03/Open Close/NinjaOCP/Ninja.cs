using NinjaShared;
using System;
using System.Numerics;

namespace NinjaOCP
{
    public class Ninja : INinja
    {
        public Weapon MeleeWeapon { get; }
        public Weapon RangedWeapon { get; }

        public string Name { get; }
        public Vector2 Position { get; private set; }

        public Ninja(string name, Weapon meleeWeapon, Weapon rangedWeapon, Vector2? position = null)
        {
            Name = name;
            Position = position ?? Vector2.Zero;
            MeleeWeapon = meleeWeapon;
            RangedWeapon = rangedWeapon;
        }

        public AttackResult Attack(IAttackable target)
        {
            var distance = this.DistanceFrom(target);
            if (MeleeWeapon.CanHit(distance))
            {
                return new AttackResult(MeleeWeapon, this, target);
            }
            else
            {
                return new AttackResult(RangedWeapon, this, target);
            }
        }

        public void MoveTo(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        public override string ToString() => $"{Name} (Position: {Position})";
    }
}
