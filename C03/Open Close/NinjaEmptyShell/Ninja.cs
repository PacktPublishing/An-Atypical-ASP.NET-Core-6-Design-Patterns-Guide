using System;

namespace NinjaEmptyShell
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
            throw new NotImplementedException();
        }

        public override string ToString() => Name;
    }

    public class Weapon { }

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
