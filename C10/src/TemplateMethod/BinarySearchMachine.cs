using System;

namespace TemplateMethod
{
    public class BinarySearchMachine : SearchMachine
    {
        public BinarySearchMachine(params int[] values) : base(values) { }

        public override int Find(int value)
        {
            return Array.BinarySearch(Values, value);
        }
    }
}
