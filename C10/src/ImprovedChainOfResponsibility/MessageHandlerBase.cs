namespace ImprovedChainOfResponsibility
{
    public abstract class MessageHandlerBase : IMessageHandler
    {
        private readonly IMessageHandler _next;
        public MessageHandlerBase(IMessageHandler next = null)
        {
            _next = next;
        }

        public void Handle(Message message)
        {
            if (CanHandle(message))
            {
                Process(message);
            }
            else if (HasNext())
            {
                _next.Handle(message);
            }
        }

        private bool HasNext()
        {
            return _next != null;
        }

        protected virtual bool CanHandle(Message message)
        {
            return message.Name == HandledMessageName;
        }

        protected abstract string HandledMessageName { get; }
        protected abstract void Process(Message message);
    }
}
