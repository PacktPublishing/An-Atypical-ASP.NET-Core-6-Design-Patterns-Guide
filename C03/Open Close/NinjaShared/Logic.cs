namespace NinjaShared
{
    public static class Logic
    {
        public static async Task ExecuteSequenceAsync(INinja ninja, INinja target, Func<string, Task> writeAsync)
        {
            // The Blue Phantom attacks The Unseen Mirage with a first attack
            var result = ninja.Attack(target);
            await PrintAttackResult(result);

            // The Unseen Mirage moves away from The Blue Phantom
            target.MoveTo(5, 5);

            // The Blue Phantom attacks The Unseen Mirage with a second attack
            var result2 = ninja.Attack(target);
            await PrintAttackResult(result2);

            // The Unseen Mirage moves further away from The Blue Phantom
            target.MoveTo(20, 20);

            // The Blue Phantom attacks The Unseen Mirage with a third attack
            var result3 = ninja.Attack(target);
            await PrintAttackResult(result3);

            // The Unseen Mirage strikes back at The Blue Phantom from a distance
            var result4 = target.Attack(ninja);
            await PrintAttackResult(result4);

            // Output
            async Task PrintAttackResult(AttackResult attackResult)
            {
                if (attackResult.Succeeded)
                {
                    await writeAsync($"{attackResult.Attacker} hits {attackResult.Target} using {attackResult.Weapon} at distance {attackResult.Distance}!{Environment.NewLine}");
                }
                else
                {
                    await writeAsync($"{attackResult.Attacker} misses {attackResult.Target} using {attackResult.Weapon} at distance {attackResult.Distance}...{Environment.NewLine}");
                }
            }
        }
    }
}
