using System;

namespace OperationResult.SingleErrorWithValue
{
    public class OperationResult
    {
        public OperationResult() { }

        public OperationResult(string errorMessage)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }

        public bool Succeeded => string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; }

        public int? Value { get; init; }
    }
}
