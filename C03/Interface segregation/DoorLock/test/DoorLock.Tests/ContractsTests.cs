using System.Collections.Generic;
using Xunit;

namespace DoorLock
{
    public class ContractsTests
    {
        [Fact]
        public void A_single_key_should_fit_multiple_locks_expecting_the_same_signature()
        {
            IKey key = new BasicKey("key1");

            LockAndAssertResult(new BasicLock("key1"));
            LockAndAssertResult(new BasicLock("key1"));
            LockAndAssertResult(new MultiLock(new List<ILock> {
                new BasicLock("key1"),
                new BasicLock("key1")
            }));

            void LockAndAssertResult(ILock @lock)
            {
                var result = @lock.DoesMatch(key);
                Assert.True(result, $"The key '{key.Signature}' should fit the lock");
            }
        }

        [Fact]
        public void Multiple_keys_with_the_same_signature_should_fit_the_same_lock()
        {
            ILock @lock = new BasicLock("key1");

            var picklock = new PredefinedPicklock(new[] { "key1" });
            var fakeKey = picklock.CreateMatchingKeyFor(@lock);

            LockAndAssertResult(new BasicKey("key1"));
            LockAndAssertResult(new BasicKey("key1"));
            LockAndAssertResult(fakeKey);

            void LockAndAssertResult(IKey key)
            {
                var result = @lock.DoesMatch(key);
                Assert.True(result, $"The key '{key.Signature}' should fit the lock");
            }
        }
    }
}
