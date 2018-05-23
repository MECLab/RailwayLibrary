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
        /// Create and initializes a <see cref="Success{TSuccess, TError}"/> instance with the <paramref name="content"/>
        /// </summary>
        /// <typeparam name="TSuccess">The Success type of the <paramref name="content"/></typeparam>
        /// <typeparam name="TError">The error type of the <see cref="Result{TSuccess, TError}"/></typeparam>
        /// <param name="content">The content that is to be set into the <see cref="Success{TSuccess, TError}"/> instance</param>
        public static Result<TSuccess, TError> Ok<TSuccess, TError>(TSuccess content)
            => new Success<TSuccess, TError>(content);

        /// <summary>
        /// Create and initializes a <see cref="Failure{TError, TSuccess}"/> instance with the <paramref name="error"/>
        /// </summary>
        /// <typeparam name="TSuccess">The success type of the <see cref="Result{TSuccess, TError}"/></typeparam>
        /// <typeparam name="TError">The error type of the <paramref name="error"/></typeparam>
        /// <param name="error">The error that is to be set into the <see cref="Failure{TError, TSuccess}"/> instance</param>
        public static Result<TSuccess, TError> Fail<TSuccess, TError>(TError error)
            => new Failure<TError, TSuccess>(error);

        /// <summary>
        /// Try and execute the <paramref name="tryFunc"/>, if an exception is thrown then a <see cref="Failure{TError, TSuccess}"/> <see cref="Result{TSuccess, TError}"/> is returned;
        /// otherwise a <see cref="Success{TSuccess, TError}"/> <see cref="Result{TSuccess, TError}"/> is returned.
        /// </summary>
        /// <typeparam name="TSuccess">The type that is returned by the <paramref name="tryFunc"/> that has not thrown an exceptoin.</typeparam>
        /// <param name="tryFunc">The function that is to be executed within a try-catch</param>
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

        /// <summary>
        /// Try and execute the asynchronous <paramref name="tryAsyncFunc"/>, if an exception is thrown then a <see cref="Failure{TError, TSuccess}"/> <see cref="Result{TSuccess, TError}"/> is returned;
        /// otherwise a <see cref="Success{TSuccess, TError}"/> <see cref="Result{TSuccess, TError}"/> is returned.
        /// </summary>
        /// <typeparam name="TSuccess">The type that is returned by the <paramref name="tryAsyncFunc"/> that has not thrown an exceptoin.</typeparam>
        /// <param name="tryAsyncFunc">The asynchronous function that is to be executed within a try-catch</param>
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
