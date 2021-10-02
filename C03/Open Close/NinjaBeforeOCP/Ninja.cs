using System;

namespace NinjaBeforeOCP
{
    public interface IAttackable { }

    public class Ninja : IAttackable
    {
        public string Name { get; }

        public Ninja(string name)
        {
            Name = name;
        }

        public AttackResult Attack(IAttackable target)
        {
            if (IsCloseRange(target))
            {
                return new AttackResult(new Sword(), this, target);
            }
            else
            {
                return new AttackResult(new Shuriken(), this, target);
            }
        }

        private bool IsCloseRange(IAttackable target) => DateTime.Now.Ticks % 2 == 0;

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
