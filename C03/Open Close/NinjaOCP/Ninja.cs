using System;

namespace NinjaOCP
{
    public interface IAttackable { }

    public class Ninja : IAttackable
    {
        public Weapon EquippedWeapon { get; set; }
        public string Name { get; }

        public Ninja(string name)
        {
            Name = name;
        }
        
        public AttackResult Attack(IAttackable target)
        {
            return new AttackResult(EquippedWeapon, this, target);
        }

        public override string ToString() => Name;
    }

    public class Weapon
    {
        public override string ToString() => this.GetType().Name;
    }

    public class Sword : Weapon { }

    public class Shuriken : Weapon { }

    public class AttackResult
    {
        public Weapon Weapon { get; }
        public IAttackable Attacker { get; }
        public IAttackable Target { get; }

        public AttackResult(Weapon weapon, IAttackable attacker, IAttackable target)
        {
            Weapon = weapon;
            Attacker = attacker;
            Target = target;
        }
    }
}
