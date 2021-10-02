using System;

namespace OperationResult.SingleErrorWithValue
{
    public class Executor
    {
        public OperationResult Operation()
        {
            // Randomize the success indicator
            // This should be real logic
            var randomNumber = new Random().Next(100);
            var success = randomNumber % 2 == 0;

            // Return the operation result
            return success
                ? new OperationResult { Value = randomNumber }
                : new OperationResult($"Something went wrong with the number '{randomNumber}'.")
                {
                    Value = randomNumber
                };
        }
    }
}
