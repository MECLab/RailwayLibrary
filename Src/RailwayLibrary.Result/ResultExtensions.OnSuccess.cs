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
                result.Reduce(onSuccess, Result.Fail<TSuccessOut, TError>);

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
                task.ReduceAsync(onSuccessAsync, err => Task.FromResult(Result.Fail<TSuccessOut, TError>(err)));

        /// <summary>
        /// Try and execute the <paramref name="tryFunc"/> if the <paramref name="result"/> is an instance of <see cref="Success{TSuccess, TError}"/>
        /// </summary>
        /// <typeparam name="TSuccessIn">The input Success type</typeparam>
        /// <typeparam name="TSuccessOut">The output success type</typeparam>
        /// <param name="result">the <see cref="Result{TSuccess, TError}"/> instance</param>
        /// <param name="tryFunc">The function that will be executed and may throw an <see cref="Exception"/></param>
        public static Result<TSuccessOut, Exception> OnSuccessThenTry<TSuccessIn, TSuccessOut>(this Result<TSuccessIn, Exception> result, Func<TSuccessIn, TSuccessOut> tryFunc) =>
            result.OnSuccess(param => Result.Try(() => tryFunc(param)));

        /// <summary>
        /// Try and execute the <paramref name="tryFunc"/> if the asynchronous <paramref name="resultAsync"/> is an instance of <see cref="Success{TSuccess, TError}"/>
        /// </summary>
        /// <typeparam name="TSuccessIn">The input Success type</typeparam>
        /// <typeparam name="TSuccessOut">The output success type</typeparam>
        /// <param name="resultAsync">the asynchronous <see cref="Result{TSuccess, TError}"/></param>
        /// <param name="tryFunc">The function that will be executed and may throw an <see cref="Exception"/></param>
        public static Task<Result<TSuccessOut, Exception>> OnSuccessThenTry<TSuccessIn, TSuccessOut>(this Task<Result<TSuccessIn, Exception>> resultAsync, Func<TSuccessIn, TSuccessOut> tryFunc) =>
            resultAsync.OnSuccess(param => Result.Try(() => tryFunc(param)));

        /// <summary>
        /// Try and execute the <paramref name="tryAsyncFunc"/> if the asynchronous <paramref name="resultAsync"/> resolves as an instance of <see cref="Success{TSuccess, TError}"/>
        /// </summary>
        /// <typeparam name="TSuccessIn">The input Success type</typeparam>
        /// <typeparam name="TSuccessOut">The output success type</typeparam>
        /// <param name="resultAsync">the asynchronous <see cref="Result{TSuccess, TError}"/></param>
        /// <param name="tryAsyncFunc">The asynchronous function that will be executed and may throw an <see cref="Exception"/></param>
        public static Task<Result<TSuccessOut, Exception>> OnSuccessThenTryAsync<TSuccessIn, TSuccessOut>(this Task<Result<TSuccessIn, Exception>> resultAsync, Func<TSuccessIn, Task<TSuccessOut>> tryAsyncFunc) =>
            resultAsync.OnSuccessAsync(param => Result.TryAsync(() => tryAsyncFunc(param)));
    }
}
