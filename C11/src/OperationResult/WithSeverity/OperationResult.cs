using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OperationResult.WithSeverity
{
    public class OperationResult
    {
        private readonly List<OperationResultMessage> _messages;
        public OperationResult(params OperationResultMessage[] errors)
        {
            _messages = new List<OperationResultMessage>(errors ?? Enumerable.Empty<OperationResultMessage>());
        }

        public bool Succeeded => !HasErrors();
        public int? Value { get; init; }

        public IEnumerable<OperationResultMessage> Messages
            => new ReadOnlyCollection<OperationResultMessage>(_messages);

        public bool HasErrors()
        {
            return FindErrors().Count() > 0;
        }

        public void AddMessage(OperationResultMessage message)
        {
            _messages.Add(message);
        }

        private IEnumerable<OperationResultMessage> FindErrors()
            => _messages.Where(x => x.Severity == OperationResultSeverity.Error);
    }
}
