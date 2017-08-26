using System;
using System.Threading.Tasks;

namespace RailwayLibrary.Result
{
    /// <summary>
    /// Static <see cref="Result{TSuccess,TError}"/> helper functions.
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Create and initializes 
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Result<TSuccess, TError> Ok<TSuccess, TError>(TSuccess content)
            => new Success<TSuccess, TError>(content);

        public static Result<TSuccess, TError> Fail<TSuccess, TError>(TError error)
            => new Failure<TError, TSuccess>(error);

        public static Result<TSuccess, Exception> Try<TSuccess>(Func<TSuccess> tryFunc)
        {
            try
            {
                var content = tryFunc();
                return Ok<TSuccess, Exception>(content);
            }
            catch (Exception ex)
            {
                return Fail<TSuccess, Exception>(ex);
            }
        }

        public static async Task<Result<TSuccess, Exception>> TryAsync<TSuccess>(Func<Task<TSuccess>> tryAsyncFunc)
        {
            try
            {
                var content = await tryAsyncFunc();
                return Ok<TSuccess, Exception>(content);
            }
            catch (Exception ex)
            {
                return Fail<TSuccess, Exception>(ex);
            }
        }
    }
}
