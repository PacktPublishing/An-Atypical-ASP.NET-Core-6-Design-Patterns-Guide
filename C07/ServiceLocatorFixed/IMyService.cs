using System;

namespace ServiceLocatorFixed
{
    public interface IMyService : IDisposable
    {
        void Execute();
    }
}
