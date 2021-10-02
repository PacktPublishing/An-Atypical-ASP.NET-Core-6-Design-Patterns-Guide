using System;

namespace Adapter
{
    public class ExternalGreeterAdapter : IGreeter
    {
        private readonly ExternalGreeter _adaptee;

        public ExternalGreeterAdapter(ExternalGreeter adaptee)
        {
            _adaptee = adaptee ?? throw new ArgumentNullException(nameof(adaptee));
        }

        public string Greeting()
        {
            return _adaptee.GreetByName("System");
        }
    }
}
