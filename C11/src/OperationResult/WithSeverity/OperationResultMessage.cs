using System;

namespace OperationResult.WithSeverity
{
    public class OperationResultMessage
    {
        public OperationResultMessage(string message, OperationResultSeverity severity)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
        }

        public string Message { get; }
        public OperationResultSeverity Severity { get; }
    }
}
