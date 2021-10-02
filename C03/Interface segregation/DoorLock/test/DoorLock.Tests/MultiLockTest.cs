using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DoorLock
{
    public class MultiLockTest
    {
        private readonly MultiLock sut;
        private readonly List<ILock> _locks;

        public MultiLockTest()
        {
            _locks = new List<ILock>();
            sut = new MultiLock(_locks);
        }

        public class DoesMatch : MultiLockTest
        {
            [Fact]
            public void Should_return_true_when_the_key_matches_at_least_one_internal_lock()
            {
                // Arrange
                _locks.Add(new BasicLock("key1"));
                _locks.Add(new BasicLock("key2"));

                // Act
                var result = sut.DoesMatch(new BasicKey("key2"));

                // Assert
                Assert.True(result, "The key should match a lock.");
            }

            [Fact]
            public void Should_return_false_when_the_key_does_not_match_any_internal_lock()
            {
                // Arrange
                _locks.Add(new BasicLock("key1"));
                _locks.Add(new BasicLock("key3"));

                // Act
                var result = sut.DoesMatch(new BasicKey("key2"));

                // Assert
                Assert.False(result, "The key should not match any lock.");
            }
        }

        public class Lock : MultiLockTest
        {
            public Lock()
            {
                Assert.False(sut.IsLocked, "The lock should be unlocked");
            }

            [Fact]
            public void Should_lock_the_lock_when_the_key_matches_any_internal_lock()
            {
                // Arrange
                _locks.Add(new BasicLock("key1"));
                _locks.Add(new BasicLock("key2"));

                // Act
                sut.Lock(new BasicKey("key2"));

                // Assert
                Assert.True(sut.IsLocked, "The lock should be locked");
            }

            [Fact]
            public void Should_throw_a_KeyDoesNotMatchException_when_the_key_does_not_match_any_internal_lock()
            {
                // Arrange
                _locks.Add(new BasicLock("key1"));
                _locks.Add(new BasicLock("key2"));

                // Act & Assert
                Assert.Throws<KeyDoesNotMatchException>(() => sut.Lock(new BasicKey("key3")));
            }
        }

        public class Unlock : MultiLockTest
        {
            [Fact]
            public void Should_unlock_the_matching_internal_lock_but_keep_the_lock_locked()
            {
                // Arrange
                var lock1 = CreateLockedLock("key1");
                var lock2 = CreateLockedLock("key2");
                Assert.True(sut.IsLocked, "The lock should be locked");

                // Act
                sut.Unlock(new BasicKey("key2"));

                // Assert
                Assert.False(lock2.IsLocked, "The internal lock should be unlocked");
                Assert.True(sut.IsLocked, "The lock should be locked");
            }

            [Fact]
            public void Should_unlock_the_lock_and_all_internal_locks_when_the_keys_matches_all_internal_locks()
            {
                // Arrange
                var lock1 = CreateLockedLock("key1");
                var lock2 = CreateLockedLock("key2");
                Assert.True(sut.IsLocked, "The lock should be locked");

                // Act
                sut.Unlock(new BasicKey("key1"));
                sut.Unlock(new BasicKey("key2"));

                // Assert
                Assert.False(lock1.IsLocked, "The lock1 should be unlocked");
                Assert.False(lock2.IsLocked, "The lock2 should be unlocked");
                Assert.False(sut.IsLocked, "The lock should be unlocked");
            }

            [Fact]
            public void Should_throw_a_KeyDoesNotMatchException_when_the_key_does_not_match_any_internal_lock()
            {
                // Arrange
                CreateLockedLock("key1");
                CreateLockedLock("key2");
                Assert.True(sut.IsLocked, "The lock should be locked");

                // Act & Assert
                Assert.Throws<KeyDoesNotMatchException>(() => sut.Unlock(new BasicKey("key3")));
            }

            private ILock CreateLockedLock(string expectedSignature)
            {
                var lockedLock = new BasicLock(expectedSignature);
                lockedLock.Lock(new BasicKey(expectedSignature));
                _locks.Add(lockedLock);
                return lockedLock;
            }
        }
    }
}
