using System;
using System.Threading.Tasks;

namespace RailwayLibrary.Result
{
    public partial class ResultExtensions
    {
        /// <summary>
        /// Helper function that will allow access or mapping of the <see cref="Failure{TError, TSuccess}"/> instannce.
        /// </summary>
        /// <typeparam name="TSuccess">
        /// The type of the success content in the case that the <paramref name="result"/> is a <see cref="Success{TSuccess, TError}"/>
        /// </typeparam>
        /// <typeparam name="TErrorIn">
        /// The type of the error content of the <paramref name="result"/> instance
        /// </typeparam>
        /// <typeparam name="TErrorOut">
        /// The mapped error type
        /// </typeparam>
        /// <param name="result">The <see cref="Result{TSuccess, TError}"/> that is to be mapped</param>
        /// <param name="mapError">
        /// The mapping function that will execute in the case that the <paramref name="result"/> is a <see cref="Failure{TError, TSuccess}"/>
        /// </param>
        public static Result<TSuccess, TErrorOut> OnError<TSuccess, TErrorIn, TErrorOut>(this Result<TSuccess, TErrorIn> result, Func<TErrorIn, TErrorOut> mapError)
        {
            switch (result)
            {
                case Success<TSuccess, TErrorIn> succ:
                    return Result.Ok<TSuccess, TErrorOut>(succ.Content);
                case Failure<TErrorIn, TSuccess> err:
                    return Result.Fail<TSuccess, TErrorOut>(mapError(err.Error));
                default:
                    throw new InvalidOperationException($"Type {result.GetType().Name} is not supported by the default {typeof(Result).Name}");
            }
        }

        /// <summary>
        /// Helper function that will allow access or mapping of the <see cref="Failure{TError, TSuccess}"/> instannce.
        /// </summary>
        /// <typeparam name="TSuccess">
        /// The type of the success content in the case that the async <paramref name="task"/> resolves as a <see cref="Success{TSuccess, TError}"/>
        /// </typeparam>
        /// <typeparam name="TErrorIn">
        /// The type of the error content that the async <paramref name="task"/> resolves with
        /// </typeparam>
        /// <typeparam name="TErrorOut">
        /// The mapped error type
        /// </typeparam>
        /// <param name="task">The async <see cref="Result{TSuccess, TError}"/> that is to be mapped</param>
        /// <param name="mapError">
        /// The mapping function that will execute in the case that the async <paramref name="task"/> resolves as a <see cref="Failure{TError, TSuccess}"/>
        /// </param>
        public static async Task<Result<TSuccess, TErrorOut>> OnError<TSuccess, TErrorIn, TErrorOut>(this Task<Result<TSuccess, TErrorIn>> task, Func<TErrorIn, TErrorOut> mapError) =>
            (await task).OnError(mapError);

        /// <summary>
        /// Helper function that will allow access or mapping of the <see cref="Failure{TError, TSuccess}"/> instannce.
        /// </summary>
        /// <typeparam name="TSuccess">
        /// The type of the success content in the case that the async <paramref name="task"/> resolves as a <see cref="Success{TSuccess, TError}"/>
        /// </typeparam>
        /// <typeparam name="TErrorIn">
        /// The type of the error content that the async <paramref name="task"/> resolves with
        /// </typeparam>
        /// <typeparam name="TErrorOut">
        /// The mapped error type
        /// </typeparam>
        /// <param name="task">The async <see cref="Result{TSuccess, TError}"/> that is to be mapped</param>
        /// <param name="mapError">
        /// The mapping function that will execute in the case that the async <paramref name="task"/> resolves as a <see cref="Failure{TError, TSuccess}"/>
        /// </param>
        public static async Task<Result<TSuccess, TErrorOut>> OnErrorAsync<TSuccess, TErrorIn, TErrorOut>(
            this Task<Result<TSuccess, TErrorIn>> task,
            Func<TErrorIn, Task<TErrorOut>> mapError)
        {
            var result = await task;
            switch (result)
            {
                case Success<TSuccess, TErrorIn> succ:
                    return Result.Ok<TSuccess, TErrorOut>(succ.Content);
                case Failure<TErrorIn, TSuccess> err:
                    return Result.Fail<TSuccess, TErrorOut>(await mapError(err.Error));
                default:
                    throw new InvalidOperationException($"Type {result.GetType().Name} is not supported by the default {typeof(Result).Name}");
            }
        }
    }
}
