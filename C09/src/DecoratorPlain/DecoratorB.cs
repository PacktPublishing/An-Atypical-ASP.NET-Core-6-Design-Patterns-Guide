using System;

namespace DecoratorPlain
{
    public class DecoratorB : IComponent
    {
        private readonly IComponent _component;

        public DecoratorB(IComponent component)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public string Operation()
        {
            var result = _component.Operation();
            return $"<DecoratorB>{result}</DecoratorB>";
        }
    }
}
