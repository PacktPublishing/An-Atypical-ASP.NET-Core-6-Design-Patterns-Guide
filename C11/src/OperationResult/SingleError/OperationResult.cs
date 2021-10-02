using System;

namespace OperationResult.SingleError
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
    }
}
