using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace OperationResult.MultipleErrorsWithValue
{
    public record class OperationResult
    {
        public OperationResult() { }
        public OperationResult(params string[] errors)
        {
            Errors = errors.ToImmutableList();
        }

        public bool Succeeded => !HasErrors();
        public int? Value { get; init; }

        public ImmutableList<string> Errors { get; init; }
        public bool HasErrors()
        {
            return Errors?.Count > 0;
        }
    }
}
