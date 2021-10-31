using System.Numerics;
using System.Security.Cryptography;

namespace NinjaBeforeOCP
{
    public interface IAttackable
    {
        public Vector2 Position { get; }
    }

    public class Ninja : IAttackable
    {
        public string Name { get; }
        public Vector2 Position { get; private set; }

        public Ninja(string name, Vector2? position = null)
        {
            Name = name;
            Position = position ?? Vector2.Zero;
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

        public void MoveTo(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        private bool IsCloseRange(IAttackable target)
            => RandomNumberGenerator.GetInt32(10_000) % 2 == 0;

        public override string ToString() => Name;
    }

    public abstract class Weapon
    {
        public abstract WeaponType Type { get; }
        public override string ToString() => GetType().Name;

        public bool CanHit(float distance)
        {
            return !IsCloseRange(distance) && Type == WeaponType.Ranged
                 || IsCloseRange(distance) && Type == WeaponType.Melee;
        }

        private bool IsCloseRange(float distance) => distance <= _meleeMaxDistance;
        private readonly float _meleeMaxDistance = Vector2.Distance(Vector2.Zero, Vector2.One);
    }

    public enum WeaponType
    {
        Melee,
        Ranged,
    }

    public class Sword : Weapon
    {
        public override WeaponType Type => WeaponType.Melee;
    }

    public class Shuriken : Weapon
    {
        public override WeaponType Type => WeaponType.Ranged;
    }

    public class AttackResult
    {
        public Weapon Weapon { get; }
        public IAttackable Attacker { get; }
        public IAttackable Target { get; }
        public bool Succeeded { get; }
        public float Distance { get; }

        public AttackResult(Weapon weapon, IAttackable attacker, IAttackable target)
        {
            Weapon = weapon;
            Attacker = attacker;
            Target = target;
            Distance = Vector2.Distance(attacker.Position, target.Position);
            Succeeded = Weapon.CanHit(Distance);
        }
    }
}
