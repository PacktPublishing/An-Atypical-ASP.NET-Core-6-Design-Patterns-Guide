using System;

namespace TemplateMethod
{
    public abstract class SearchMachine
    {
        protected int[] Values { get; }

        protected SearchMachine(params int[] values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public int? IndexOf(int value)
        {
            var result = Find(value);
            if (result < 0) { return null; }
            return result;
        }
        public abstract int Find(int value);
    }
}
