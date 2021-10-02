using System;
using System.Diagnostics;

namespace ServiceLocator
{
    public class MyServiceImplementation : IMyService
    {
        public bool IsDisposed { get; private set; }
        public void Dispose() => IsDisposed = true;

        public void Execute()
        {
            if (IsDisposed)
            {
                throw new NullReferenceException("Some dependencies has been disposed.");
            }
        }
    }
}
