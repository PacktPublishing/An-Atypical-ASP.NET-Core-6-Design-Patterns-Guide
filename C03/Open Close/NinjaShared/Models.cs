using System.Numerics;

namespace NinjaShared
{
    public interface IAttacker : IAttackable
    {
        AttackResult Attack(IAttackable target);
    }
    public interface IAttackable
    {
        public Vector2 Position { get; }
    }
    public interface INinja : IAttackable, IAttacker
    {
        string Name { get; }
        void MoveTo(float x, float y);
    }

    public abstract class Weapon
    {
        public abstract float MinRanged { get; }
        public abstract float MaxRanged { get; }
        public override string ToString()
            => $"{GetType().Name} (Min: {MinRanged}, Max: {MaxRanged})";

        public bool CanHit(float distance)
            => distance >= MinRanged && distance <= MaxRanged;
    }

    public class Sword : Weapon
    {
        public override float MinRanged { get; } = 0;
        public override float MaxRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
    }

    public class Shuriken : Weapon
    {
        public override float MinRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
        public override float MaxRanged { get; } = 20;
    }

    public class Pistol : Weapon
    {
        public override float MinRanged { get; } = Vector2.Distance(Vector2.Zero, Vector2.One);
        public override float MaxRanged { get; } = 50;
    }

    public class AttackResult
    {
        public Weapon Weapon { get; }
        public IAttacker Attacker { get; }
        public IAttackable Target { get; }
        public bool Succeeded { get; }
        public float Distance { get; }

        public AttackResult(Weapon weapon, IAttacker attacker, IAttackable target)
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