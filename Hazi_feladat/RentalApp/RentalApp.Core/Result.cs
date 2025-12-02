
using System.Diagnostics;

namespace RentalApp.Core
{
    internal class Result<TSuccess, TError>
    {
        public bool IsSuccess { get; }
        public bool IsError => !IsSuccess;

        TSuccess? Success { get; }
        TError? Error { get; }

        public Result(TSuccess success)
        {
            IsSuccess = true;
            Success = success; 
        }

        public Result(TError error)
        {
            IsSuccess = false;
            Error = error;
        }

        public void Visit(Action<TSuccess> success, Action<TError> error)
        {
            if(IsSuccess && Success is not null)
            {
                success.Invoke(Success);
                return;
            }
            if(IsError && Error is not null)
            {
                error.Invoke(Error);
                return;
            }
            throw new UnreachableException("Result is in an invalid state.");
        }


    }
}
