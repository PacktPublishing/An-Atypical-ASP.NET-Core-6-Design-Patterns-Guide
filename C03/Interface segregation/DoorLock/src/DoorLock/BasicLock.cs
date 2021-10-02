using System;
using System.Collections.Generic;
using System.Linq;

namespace DoorLock
{
    public class BasicLock : ILock
    {
        private readonly string _expectedSignature;

        public BasicLock(string expectedSignature, bool isLocked = false)
        {
            _expectedSignature = expectedSignature ?? throw new ArgumentNullException(nameof(expectedSignature));
            IsLocked = isLocked;
        }

        public bool IsLocked { get; private set; }

        public bool DoesMatch(IKey key)
        {
            return key.Signature.Equals(_expectedSignature);
        }

        public void Lock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            IsLocked = true;
        }

        public void Unlock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            IsLocked = false;
        }
    }

    public class BasicKey : IKey
    {
        public BasicKey(string signature)
        {
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        }

        public string Signature { get; }
    }

    /// <summary>
    /// Represent a tool that can be used to pick a lock.
    /// </summary>
    public interface IPicklock
    {
        /// <summary>
        /// Create a key that fits the specified <see cref="ILock"/>.
        /// </summary>
        /// <param name="lock">The lock to pick.</param>
        /// <returns>The key that fits the specified <see cref="ILock"/>.</returns>
        /// <exception cref="ImpossibleToPickTheLockException">
        /// The <see cref="Exception"/> that is thrown when a lock cannot be picked using the current <see cref="IPicklock"/>.
        /// </exception>
        IKey CreateMatchingKeyFor(ILock @lock);

        /// <summary>
        /// Unlock the specified <see cref="ILock"/>.
        /// </summary>
        /// <param name="lock">The lock to pick.</param>
        /// <exception cref="ImpossibleToPickTheLockException">
        /// The <see cref="Exception"/> that is thrown when a lock cannot be picked using the current <see cref="IPicklock"/>.
        /// </exception>
        void Pick(ILock @lock);
    }

    public class PredefinedPicklock : IPicklock
    {
        private readonly string[] _signatures;

        public PredefinedPicklock(string[] signatures)
        {
            _signatures = signatures ?? throw new ArgumentNullException(nameof(signatures));
        }

        public IKey CreateMatchingKeyFor(ILock @lock)
        {
            var key = new FakeKey();
            foreach (var signature in _signatures)
            {
                key.Signature = signature;
                if (@lock.DoesMatch(key))
                {
                    return key;
                }
            }
            throw new ImpossibleToPickTheLockException(@lock);
        }

        public void Pick(ILock @lock)
        {
            var key = new FakeKey();
            foreach (var signature in _signatures)
            {
                key.Signature = signature;
                if (@lock.DoesMatch(key))
                {
                    @lock.Unlock(key);
                    if (!@lock.IsLocked)
                    {
                        return;
                    }
                }
            }
            throw new ImpossibleToPickTheLockException(@lock);
        }

        private class FakeKey : IKey
        {
            public string Signature { get; set; }
        }
    }

    public class MultiLock : ILock
    {
        private readonly List<ILock> _locks;
        public MultiLock(List<ILock> locks)
        {
            _locks = locks ?? throw new ArgumentNullException(nameof(locks));
        }
        public MultiLock(params ILock[] locks)
            : this(new List<ILock>(locks))
        {
            if (locks == null) { throw new ArgumentNullException(nameof(locks)); }
        }

        public bool IsLocked => _locks.Any(@lock => @lock.IsLocked);

        public bool DoesMatch(IKey key)
        {
            return _locks.Any(@lock => @lock.DoesMatch(key));
        }

        public void Lock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            _locks
                .Where(@lock => @lock.DoesMatch(key))
                .ToList()
                .ForEach(@lock => @lock.Lock(key));
        }

        public void Unlock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            _locks
                .Where(@lock => @lock.DoesMatch(key))
                .ToList()
                .ForEach(@lock => @lock.Unlock(key));
        }
    }

    /// <summary>
    /// Represents a lock than can be opened by zero or more keys.
    /// </summary>
    public interface ILock
    {
        /// <summary>
        /// Gets if the lock is locked or not.
        /// </summary>
        bool IsLocked { get; }

        /// <summary>
        /// Locks the lock using the specified <see cref="IKey"/>.
        /// </summary>
        /// <param name="key">The <see cref="IKey"/> used to lock the lock.</param>
        /// <exception cref="KeyDoesNotMatchException">The <see cref="Exception"/> that is thrown when the specified <see cref="IKey"/> does not match the <see cref="ILock"/>.</exception>
        void Lock(IKey key);

        /// <summary>
        /// Unlocks the lock using the specified <see cref="IKey"/>.
        /// </summary>
        /// <param name="key">The <see cref="IKey"/> used to unlock the lock.</param>
        /// <exception cref="KeyDoesNotMatchException">The <see cref="Exception"/> that is thrown when the specified <see cref="IKey"/> does not match the <see cref="ILock"/>.</exception>
        void Unlock(IKey key);

        /// <summary>
        /// Validate that the key's <see cref="IKey.Signature"/> match the lock.
        /// </summary>
        /// <param name="key">The <see cref="IKey"/> to validate.</param>
        /// <returns><c>true</c> if the key's <see cref="IKey.Signature"/> match the lock; otherwise <c>false</c>.</returns>
        bool DoesMatch(IKey key);
    }

    /// <summary>
    /// Represents a key that can open zero or more locks.
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// Gets the key's signature.
        /// This should be used by <see cref="ILock"/> to decide whether or not the key matches the lock.
        /// </summary>
        string Signature { get; }
    }

    public class KeyDoesNotMatchException : Exception
    {
        public KeyDoesNotMatchException(IKey key)
            : base($"The key {key.Signature} does not match the lock's lock.")
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public IKey Key { get; }
    }

    public class ImpossibleToPickTheLockException : Exception
    {
        public ImpossibleToPickTheLockException(ILock @lock)
            :base("Impossible to pick the lock.")
        {
            Lock = @lock ?? throw new ArgumentNullException(nameof(@lock));
        }

        public ILock Lock { get; }
    }
}
