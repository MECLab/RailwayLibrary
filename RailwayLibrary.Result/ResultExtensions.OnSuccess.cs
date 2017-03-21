using System;
using System.Threading.Tasks;

namespace RailwayLibrary.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Executes the <paramref name="onSuccess"/> function only if the <paramref name="result"/> is an instance of <see cref="Success{TSuccess,TError}"/>;
        /// otherwise the <see cref="Failure{TError,TSuccess}"/> is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn">Success input type</typeparam>
        /// <typeparam name="TError">Error type</typeparam>
        /// <typeparam name="TSuccessOut">Success output type</typeparam>
        /// <param name="result">the <see cref="Result{TSuccess,TError}"/> instance</param>
        /// <param name="onSuccess">Function that is to execute in the case where the <paramref name="result"/> is a successful type.</param>
        /// <remarks>Also known as FlatMap or Bind.</remarks>
        public static Result<TSuccessOut, TError> OnSuccess<TSuccessIn, TError, TSuccessOut>(
            this Result<TSuccessIn, TError> result, Func<TSuccessIn, Result<TSuccessOut, TError>> onSuccess) =>
                result.Reduce(onSuccess, RailwayLibrary.Result.Result.Fail<TSuccessOut, TError>);

        /// <summary>
        /// Executes the <paramref name="onSuccess"/> function only if the async <paramref name="task"/> results in an instance of <see cref="Success{TSuccess,TError}"/>;
        /// otherwise the <see cref="Failure{TError,TSuccess}"/> is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn">Success input type</typeparam>
        /// <typeparam name="TError">Error type</typeparam>
        /// <typeparam name="TSuccessOut">Success output type</typeparam>
        /// <param name="task">the <see cref="Result{TSuccess,TError}"/> instance</param>
        /// <param name="onSuccess">Function that is to execute in the case where the <paramref name="task"/> is a successful type.</param>
        /// <remarks>Also known as FlatMap or Bind.</remarks>
        public static async Task<Result<TSuccessOut, TError>> OnSuccess<TSuccessIn, TError, TSuccessOut>(
            this Task<Result<TSuccessIn, TError>> task, Func<TSuccessIn, Result<TSuccessOut, TError>> onSuccess) =>
                (await task).OnSuccess(onSuccess);

        /// <summary>
        /// Executes the <paramref name="onSuccessAsync"/> function only if the async <paramref name="task"/> results in an instance of <see cref="Success{TSuccess,TError}"/>;
        /// otherwise the <see cref="Failure{TError,TSuccess}"/> is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn">Success input type</typeparam>
        /// <typeparam name="TError">Error type</typeparam>
        /// <typeparam name="TSuccessOut">Success output type</typeparam>
        /// <param name="task">the <see cref="Result{TSuccess,TError}"/> instance</param>
        /// <param name="onSuccessAsync">Async function that is to execute in the case where the <paramref name="task"/> is a successful type.</param>
        /// <remarks>Also known as FlatMap or Bind.</remarks>
        public static Task<Result<TSuccessOut, TError>> OnSuccessAsync<TSuccessIn, TError, TSuccessOut>(
            this Task<Result<TSuccessIn, TError>> task, Func<TSuccessIn, Task<Result<TSuccessOut, TError>>> onSuccessAsync) =>
                task.ReduceAsync(onSuccessAsync, err => Task.FromResult(RailwayLibrary.Result.Result.Fail<TSuccessOut, TError>(err)));
    }
}
