using System;
using System.Threading.Tasks;

namespace RailwayLibrary.Result
{
    /// <summary>
    /// <see cref="Result{TSuccess,TError}"/> extension helper functions
    /// </summary>
    /// <seealso cref="Result{TSuccess,TError}"/>
    /// <seealso cref="Result"/>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Get the content of the <see cref="Result{TSuccess,TError}"/> if and instance of <typeparamref name="TSuccess"/>;
        /// otherwise the <paramref name="defaultFunc"/> is executed.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the content in the case of a successful result.</typeparam>
        /// <typeparam name="TError">The type of the error in the case of a failed result.</typeparam>
        /// <param name="result">The <see cref="Result{TSuccess,TError}"/> of which is the subject.</param>
        /// <param name="defaultFunc">Function that is executed in the case of a failed <see cref="Result{TSuccess,TError}"/>.</param>
        public static TSuccess GetOrDefault<TSuccess, TError>(this Result<TSuccess, TError> result, Func<TSuccess> defaultFunc)
            => result.IsSuccess
                ? ((Success<TSuccess, TError>)result).Content
                : defaultFunc();

        /// <summary>
        /// Get the content of the <see cref="Result{TSuccess,TError}"/> if and instance of <typeparamref name="TSuccess"/>;
        /// otherwise the <paramref name="defaultFunc"/> is executed.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the content in the case of a successful result.</typeparam>
        /// <typeparam name="TError">The type of the error in the case of a failed result.</typeparam>
        /// <param name="task">The async <see cref="Result{TSuccess,TError}"/> of which is the subject.</param>
        /// <param name="defaultFunc">Function that is executed in the case of a failed <see cref="Result{TSuccess,TError}"/>.</param>
        public static async Task<TSuccess> GetOrDefault<TSuccess, TError>(this Task<Result<TSuccess, TError>> task, Func<TSuccess> defaultFunc)
            => (await task).GetOrDefault(defaultFunc);

        /// <summary>
        /// Get the content of the <see cref="Result{TSuccess,TError}"/> if and instance of <typeparamref name="TSuccess"/>;
        /// otherwise the value returned by the specified <paramref name="orElse"/> function is returned.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the content in the case of a successful result.</typeparam>
        /// <typeparam name="TError">The type of the error in the case of a failed result.</typeparam>
        /// <param name="result">The <see cref="Result{TSuccess,TError}"/> of which is the subject.</param>
        /// <param name="orElse">Function that is executed in the case of a failed <see cref="Result{TSuccess,TError}"/>.</param>
        public static TSuccess GetOrElse<TSuccess, TError>(this Result<TSuccess, TError> result, Func<TError, TSuccess> orElse) 
            => result.Reduce(_ => _, orElse);

        /// <summary>
        /// Get the content of the <see cref="Result{TSuccess,TError}"/> if and instance of <typeparamref name="TSuccess"/>;
        /// otherwise the value returned by the specified <paramref name="orElse"/> function is returned.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the content in the case of a successful result.</typeparam>
        /// <typeparam name="TError">The type of the error in the case of a failed result.</typeparam>
        /// <param name="task">The async <see cref="Result{TSuccess,TError}"/> of which is the subject.</param>
        /// <param name="orElse">Function that is executed in the case of a failed <see cref="Result{TSuccess,TError}"/>.</param>
        public static async Task<TSuccess> GetOrElse<TSuccess, TError>(this Task<Result<TSuccess, TError>> task, Func<TError, TSuccess> orElse)
            => (await task).GetOrElse(orElse);

        /// <summary>
        /// Get the content of the <see cref="Result{TSuccess,TError}"/> if and instance of <typeparamref name="TSuccess"/>;
        /// otherwise the value returned by the specified <paramref name="orElseAsync"/> function is returned.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the content in the case of a successful result.</typeparam>
        /// <typeparam name="TError">The type of the error in the case of a failed result.</typeparam>
        /// <param name="task">The async <see cref="Result{TSuccess,TError}"/> of which is the subject.</param>
        /// <param name="orElseAsync">Async function that is executed in the case of a failed <see cref="Result{TSuccess,TError}"/>.</param>
        public static Task<TSuccess> GetOrElseAsync<TSuccess, TError>(this Task<Result<TSuccess, TError>> task, Func<TError, Task<TSuccess>> orElseAsync)
            => task.ReduceAsync(Task.FromResult, orElseAsync);
    }
}
