using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OperationResult.MultipleErrorsWithValue
{
    public class OperationResult
    {
        private readonly List<string> _errors;
        public OperationResult(params string[] errors)
        {
            _errors = new List<string>(errors ?? Enumerable.Empty<string>());
        }

        public bool Succeeded => !HasErrors();
        public int? Value { get; init; }

        public IEnumerable<string> Errors => new ReadOnlyCollection<string>(_errors);
        public bool HasErrors()
        {
            return Errors?.Count() > 0;
        }

        public void AddError(string message)
        {
            _errors.Add(message);
        }
    }
}
