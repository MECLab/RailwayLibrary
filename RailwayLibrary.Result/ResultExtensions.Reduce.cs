using System;
using System.Threading.Tasks;

namespace RailwayLibrary.Result
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Reduces the given <paramref name="result"/> down using the given <paramref name="onSuccess"/> and <paramref name="onError"/>
        /// functions
        /// </summary>
        /// <typeparam name="TSuccess">The type of the success side.</typeparam>
        /// <typeparam name="TError">The type of the failure side.</typeparam>
        /// <typeparam name="TReduced">The return type</typeparam>
        /// <param name="result">the <see cref="Result"/> instance</param>
        /// <param name="onSuccess">
        /// The function that is to be executed in the case that the <paramref name="result"/> is an instance of a <see cref="Success{TSuccess,TError}"/>
        /// </param>
        /// <param name="onError">
        /// The function that is to be executed in the case that the <paramref name="result"/> is an instance of a <see cref="Failure{TError,TSuccess}"/>
        /// </param>
        public static TReduced Reduce<TSuccess, TError, TReduced>(this Result<TSuccess, TError> result,
            Func<TSuccess, TReduced> onSuccess, Func<TError, TReduced> onError)
        {
            if (result.IsSuccess)
            {
                var content = ((Success<TSuccess, TError>)result).Content;
                return onSuccess(content);
            }

            var error = ((Failure<TError, TSuccess>)result).Error;
            return onError(error);
        }

        /// <summary>
        /// Reduces the <see cref="Result{TSuccess,TError}"/> of the async <paramref name="task"/> down using the given <paramref name="onSuccess"/> and <paramref name="onError"/>
        /// functions
        /// </summary>
        /// <typeparam name="TSuccess">The type of the success side.</typeparam>
        /// <typeparam name="TError">The type of the failure side.</typeparam>
        /// <typeparam name="TReduced">The return type</typeparam>
        /// <param name="task">the async <see cref="Task{TResult}"/> the result in an instace of the base <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="onSuccess">
        /// The function that is to be executed in the case that the result of the <paramref name="task"/> is an instance of a <see cref="Success{TSuccess,TError}"/>
        /// </param>
        /// <param name="onError">
        /// The function that is to be executed in the case that the result of the <paramref name="task"/> is an instance of a <see cref="Failure{TError,TSuccess}"/>
        /// </param>
        public static async Task<TReduced> Reduce<TSuccess, TError, TReduced>(this Task<Result<TSuccess, TError>> task,
            Func<TSuccess, TReduced> onSuccess, Func<TError, TReduced> onError) => 
                (await task).Reduce(onSuccess, onError);

        /// <summary>
        /// Reduces the <see cref="Result{TSuccess,TError}"/> of the async <paramref name="task"/> down using the given <paramref name="onSuccessAsync"/> and <paramref name="onErrorAsync"/>
        /// functions
        /// </summary>
        /// <typeparam name="TSuccess">The type of the success side.</typeparam>
        /// <typeparam name="TError">The type of the failure side.</typeparam>
        /// <typeparam name="TReduced">The return type</typeparam>
        /// <param name="task">the async <see cref="Task{TResult}"/> the result in an instace of the base <see cref="Result{TSuccess,TError}"/></param>
        /// <param name="onSuccessAsync">
        /// The async function that is to be executed in the case that the result of the <paramref name="task"/> is an instance of a <see cref="Success{TSuccess,TError}"/>
        /// </param>
        /// <param name="onErrorAsync">
        /// The async function that is to be executed in the case that the result of the <paramref name="task"/> is an instance of a <see cref="Failure{TError,TSuccess}"/>
        /// </param>
        public static async Task<TReduced> ReduceAsync<TSuccess, TError, TReduced>(
            this Task<Result<TSuccess, TError>> task,
            Func<TSuccess, Task<TReduced>> onSuccessAsync, Func<TError, Task<TReduced>> onErrorAsync)
        {
            var result = await task;
            if (result.IsSuccess)
            {
                var reduction = await onSuccessAsync(result.GetOrDefault(() => default(TSuccess)));
                return reduction;
            }

            var error = ((Failure<TError, TSuccess>) result).Error;
            return await onErrorAsync(error);
        }
    }
}
