using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationResult.StaticFactoryMethod
{
    public abstract class OperationResult
    {
        private OperationResult() { }

        public abstract bool Succeeded { get; }
        public virtual int? Value { get; init; }
        public abstract IEnumerable<OperationResultMessage> Messages { get; }

        public static OperationResult Success(int? value = null)
        {
            return new SuccessfulOperationResult { Value = value };
        }

        public static OperationResult Failure(params OperationResultMessage[] errors)
        {
            return new FailedOperationResult(errors);
        }

        public sealed class SuccessfulOperationResult : OperationResult
        {
            public override bool Succeeded => true;
            public override IEnumerable<OperationResultMessage> Messages 
                => Enumerable.Empty<OperationResultMessage>();
        }

        public sealed class FailedOperationResult : OperationResult
        {
            private readonly List<OperationResultMessage> _messages;
            public FailedOperationResult(params OperationResultMessage[] errors)
            {
                _messages = new List<OperationResultMessage>(errors ?? Enumerable.Empty<OperationResultMessage>());
            }

            public override bool Succeeded => false;

            public override IEnumerable<OperationResultMessage> Messages
                => new ReadOnlyCollection<OperationResultMessage>(_messages);
        }
    }
}
