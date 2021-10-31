using System.Numerics;

namespace NinjaShared
{
    public interface IAttacker
    {
        AttackResult Attack(IAttackable target);
    }
    public interface IAttackable
    {
        public Vector2 Position { get; }
    }
    public interface IMoveable : IAttackable
    {
        void MoveTo(float x, float y);
    }
    public interface INinja : IAttackable, IMoveable, IAttacker
    {
    }

    public abstract class Weapon
    {
        public abstract WeaponType Type { get; }
        public abstract float MinRanged { get; }
        public abstract float MaxRanged { get; }
        public override string ToString()
            => $"{GetType().Name} (Min: {MinRanged}, Max: {MaxRanged}, Type: {Type})";

        public bool CanHit(float distance)
            => distance >= MinRanged && distance <= MaxRanged;
    }

    public enum WeaponType
    {
        Melee,
        Ranged,
    }

    public class Sword : Weapon
    {
        public override WeaponType Type => WeaponType.Melee;
        public override float MinRanged { get; } = 0;
        public override float MaxRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
    }

    public class Shuriken : Weapon
    {
        public override WeaponType Type => WeaponType.Ranged;
        public override float MinRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
        public override float MaxRanged { get; } = 20;
    }

    public class Pistol : Weapon
    {
        public override WeaponType Type => WeaponType.Ranged;
        public override float MinRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
        public override float MaxRanged { get; } = 50;
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
            Distance = attacker.DistanceFrom(target);
            Succeeded = Weapon.CanHit(Distance);
        }
    }

    public static class AttackableExtensions
    {
        public static float DistanceFrom(this IAttackable attacker, IAttackable target)
        {
            return Vector2.Distance(attacker.Position, target.Position);
        }
    }
}