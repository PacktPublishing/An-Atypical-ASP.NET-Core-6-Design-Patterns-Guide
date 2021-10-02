namespace Singleton
{
    public class MySingletonWithLock
    {
        private readonly static object _myLock = new object();

        private static MySingletonWithLock _instance;
        private MySingletonWithLock() { }

        public static MySingletonWithLock Create()
        {
            lock (_myLock)
            {
                if (_instance == default(MySingletonWithLock))
                {
                    _instance = new MySingletonWithLock();
                }
            }
            return _instance;
        }
    }
}
