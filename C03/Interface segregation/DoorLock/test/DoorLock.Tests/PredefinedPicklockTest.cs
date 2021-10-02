using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DoorLock
{
    public class PredefinedPicklockTest
    {
        public class CreateMatchingKeyFor : PredefinedPicklockTest
        {
            [Fact]
            public void Should_return_the_matching_key_when_provided()
            {
                // Arrange
                var sut = new PredefinedPicklock(new[] { "key1" });
                var @lock = new BasicLock("key1");

                // Act
                var key = sut.CreateMatchingKeyFor(@lock);

                // Assert
                Assert.NotNull(key);
                Assert.Equal("key1", key.Signature);
            }

            [Fact]
            public void Should_throw_an_ImpossibleToPickTheLockException_when_no_matching_key_can_be_generated()
            {
                // Arrange
                var sut = new PredefinedPicklock(new[] { "key2" });
                var @lock = new BasicLock("key1");

                // Act & Assert
                Assert.Throws<ImpossibleToPickTheLockException>(() => sut.CreateMatchingKeyFor(@lock));
            }
        }

        public class Pick : PredefinedPicklockTest
        {
            public static TheoryData<ILock> PickableLocks = new TheoryData<ILock>
            {
                new BasicLock("key1", isLocked: true),
                new MultiLock(
                    new BasicLock("key2", isLocked: true), 
                    new BasicLock("key3", isLocked: true)
                ),
                new MultiLock(
                    new BasicLock("key2", isLocked: true),
                    new MultiLock(
                        new BasicLock("key1", isLocked: true),
                        new BasicLock("key3", isLocked: true)
                    )
                )
            };

            [Theory]
            [MemberData(nameof(PickableLocks))]
            public void Should_unlock_the_specified_ILock(ILock @lock)
            {
                // Arrange
                Assert.True(@lock.IsLocked, "The lock should be locked.");
                var sut = new PredefinedPicklock(new[] { "key1", "key2", "key3" });

                // Act
                sut.Pick(@lock);

                // Assert
                Assert.False(@lock.IsLocked, "The lock should be unlocked.");
            }


            [Fact]
            public void Should_throw_an_ImpossibleToPickTheLockException_when_the_lock_cannot_be_opened()
            {
                // Arrange
                var sut = new PredefinedPicklock(new[] { "key2" });
                var @lock = new BasicLock("key1");

                // Act & Assert
                Assert.Throws<ImpossibleToPickTheLockException>(() => sut.Pick(@lock));
            }
        }
    }
}
