using System;
using System.Threading.Tasks;

namespace RailwayLibrary.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Executes the <paramref name="map"/> function only if the <paramref name="result"/> is an instance of <see cref="Success{TSuccess,TError}"/>;
        /// otherwise the <see cref="Failure{TError,TSuccess}"/> is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn">Success input type</typeparam>
        /// <typeparam name="TError">Error type</typeparam>
        /// <typeparam name="TSuccessOut">Success output type</typeparam>
        /// <param name="result">the <see cref="Result{TSuccess,TError}"/> instance</param>
        /// <param name="map">Function that is to execute in the case where the <paramref name="result"/> is a successful type.</param>
        public static Result<TSuccessOut, TError> Map<TSuccessIn, TSuccessOut, TError>(
            this Result<TSuccessIn, TError> result, Func<TSuccessIn, TSuccessOut> map)
        {
            if (result.IsSuccess)
            {
                var content = ((Success<TSuccessIn, TError>) result).Content;
                var mapped = map(content);
                return RailwayLibrary.Result.Result.Ok<TSuccessOut, TError>(mapped);
            }

            var error = ((Failure<TError, TSuccessIn>) result).Error;
            return RailwayLibrary.Result.Result.Fail<TSuccessOut, TError>(error);
        }

        /// <summary>
        /// Executes the <paramref name="map"/> function only if the result of the async <paramref name="task"/> is an instance of <see cref="Success{TSuccess,TError}"/>;
        /// otherwise the <see cref="Failure{TError,TSuccess}"/> is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn">Success input type</typeparam>
        /// <typeparam name="TError">Error type</typeparam>
        /// <typeparam name="TSuccessOut">Success output type</typeparam>
        /// <param name="task">the async task that results in an instance of <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="map">Function that is to execute in the case where the result is a successful type.</param>
        public static async Task<Result<TSuccessOut, TError>> Map<TSuccessIn, TError, TSuccessOut>(
                this Task<Result<TSuccessIn, TError>> task, Func<TSuccessIn, TSuccessOut> map)
            => (await task).Map(map);

        /// <summary>
        /// Executes the <paramref name="mapAsync"/> function only if the result of the async <paramref name="task"/> is an instance of <see cref="Success{TSuccess,TError}"/>;
        /// otherwise the <see cref="Failure{TError,TSuccess}"/> is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn">Success input type</typeparam>
        /// <typeparam name="TError">Error type</typeparam>
        /// <typeparam name="TSuccessOut">Success output type</typeparam>
        /// <param name="task">the async task that results in an instance of <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="mapAsync">Async function that is to execute in the case where the result is a successful type.</param>
        public static async Task<Result<TSuccessOut, TError>> MapAsync<TSuccessIn, TError, TSuccessOut>(
            this Task<Result<TSuccessIn, TError>> task, Func<TSuccessIn, Task<TSuccessOut>> mapAsync)
        {
            var result = await task;
            if (result.IsSuccess)
            {
                var content = ((Success<TSuccessIn, TError>) result).Content;
                var mapped = await mapAsync(content);
                return RailwayLibrary.Result.Result.Ok<TSuccessOut, TError>(mapped);
            }

            var error = ((Failure<TError, TSuccessIn>) result).Error;
            return RailwayLibrary.Result.Result.Fail<TSuccessOut, TError>(error);
        }
    }
}
