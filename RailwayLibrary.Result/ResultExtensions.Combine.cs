using System;
using System.Threading.Tasks;

namespace RailwayLibrary.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Combines this <paramref name="result"/> with the <paramref name="other"/> <see cref="Result{TSuccess,TError}"/> using the given <paramref name="combiner"/>.
        /// If either of the <see cref="Result{TSuccess,TError}"/>s resolves to a <see cref="Failure{TError,TSuccess}"/> then the <paramref name="combiner"/> is ignored
        /// and the error is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn1">The content type of the first incoming successful <see cref="Result{TSuccess,TError}"/>.</typeparam>
        /// <typeparam name="TSuccessIn2">The content type of the second incoming successful <see cref="Result{TSuccess,TError}"/>.</typeparam>
        /// <typeparam name="TCombined">The content type of the out-going combined success <see cref="Result{TSuccess,TError}"/></typeparam>
        /// <typeparam name="TError">The error type</typeparam>
        /// <param name="result">the first <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="other">the other <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="combiner">function that will execute to combine the two <see cref="Result{TSuccess,TError}"/>s.</param>
        public static Result<TCombined, TError> Combine<TSuccessIn1, TSuccessIn2, TCombined, TError>(
            this Result<TSuccessIn1, TError> result, Result<TSuccessIn2, TError> other, Func<TSuccessIn1, TSuccessIn2, TCombined> combiner) =>
                result.OnSuccess(res1 => other.Map(res2 => combiner(res1, res2)));

        /// <summary>
        /// Combines this async <paramref name="task"/> with the <paramref name="other"/> <see cref="Result{TSuccess,TError}"/> using the given <paramref name="combiner"/>.
        /// If either of the <see cref="Result{TSuccess,TError}"/>s resolves to a <see cref="Failure{TError,TSuccess}"/> then the <paramref name="combiner"/> is ignored
        /// and the error is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn1">The content type of the first incoming successful <see cref="Result{TSuccess,TError}"/>.</typeparam>
        /// <typeparam name="TSuccessIn2">The content type of the second incoming successful <see cref="Result{TSuccess,TError}"/>.</typeparam>
        /// <typeparam name="TCombined">The content type of the out-going combined success <see cref="Result{TSuccess,TError}"/></typeparam>
        /// <typeparam name="TError">The error type</typeparam>
        /// <param name="task">the first async <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="other">the other async <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="combiner">function that will execute to combine the two <see cref="Result{TSuccess,TError}"/>s.</param>
        public static async Task<Result<TCombined, TError>> Combine<TSuccessIn1, TSuccessIn2, TCombined, TError>(
            this Task<Result<TSuccessIn1, TError>> task, Task<Result<TSuccessIn2, TError>> other, Func<TSuccessIn1, TSuccessIn2, TCombined> combiner) =>
                (await task).Combine(await other, combiner);

        /// <summary>
        /// Combines this async <paramref name="task"/> with the <paramref name="other"/> <see cref="Result{TSuccess,TError}"/> using the given <paramref name="combiner"/>.
        /// If either of the <see cref="Result{TSuccess,TError}"/>s resolves to a <see cref="Failure{TError,TSuccess}"/> then the <paramref name="combiner"/> is ignored
        /// and the error is carried forward.
        /// </summary>
        /// <typeparam name="TSuccessIn1">The content type of the first incoming successful <see cref="Result{TSuccess,TError}"/>.</typeparam>
        /// <typeparam name="TSuccessIn2">The content type of the second incoming successful <see cref="Result{TSuccess,TError}"/>.</typeparam>
        /// <typeparam name="TCombined">The content type of the out-going combined success <see cref="Result{TSuccess,TError}"/></typeparam>
        /// <typeparam name="TError">The error type</typeparam>
        /// <param name="task">the first async <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="other">the other async <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="combiner">function that will execute to combine the two <see cref="Result{TSuccess,TError}"/>s.</param>
        public static async Task<Result<TCombined, TError>> CombineAsync<TSuccessIn1, TSuccessIn2, TError, TCombined>(
            this Task<Result<TSuccessIn1, TError>> task, Task<Result<TSuccessIn2, TError>> other, Func<TSuccessIn1, TSuccessIn2, Task<TCombined>> combiner) =>
                await task.OnSuccessAsync(res1 => other.MapAsync(async res2 => await combiner(res1, res2)));
    }
}
