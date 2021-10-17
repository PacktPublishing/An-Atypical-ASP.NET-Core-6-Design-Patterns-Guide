using System;
using System.Text.Json.Serialization;

namespace OperationResult.StaticFactoryMethod
{
    public class OperationResultMessage
    {
        public OperationResultMessage(string message, OperationResultSeverity severity)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
        }

        public string Message { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OperationResultSeverity Severity { get; }
    }
}
