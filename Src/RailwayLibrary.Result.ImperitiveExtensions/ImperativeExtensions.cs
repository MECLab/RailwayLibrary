using System;

namespace RailwayLibrary.Result.ImperitiveExtensions
{
    public static class ImperativeExtensions
    {
        /// <summary>
        /// Try and retrieve the content if the <see cref="Result{TSuccess, TError}"/> is an instance of a <see cref="Success{TSuccess, TError}"/>.
        /// <c>True</c> is returned and the <paramref name="content"/> is set to the content of the <see cref="Success{TSuccess, TError}"/> instance; 
        /// otherwise <c>false</c> is returned and the <paramref name="content"/> out parameter is set to its default value.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the Success instance</typeparam>
        /// <typeparam name="TError">The type of the error instance</typeparam>
        /// <param name="result">The <see cref="Result{TSuccess, TError}"/> that is the target of this retrieval operation.</param>
        /// <param name="content">The out parameter that the content is retrieved from</param>
        /// <returns>
        /// <c>True</c> if the <see cref="Result{TSuccess, TError}"/> is an instance of a <see cref="Success{TSuccess, TError}"/>; 
        /// otherwise <c>false</c>
        /// </returns>
        public static bool TryRetrieve<TSuccess, TError>(this Result<TSuccess, TError> result, out TSuccess content)
        {
            switch (result)
            {
                case Success<TSuccess, TError> success:
                    content = success.Content;
                    return true;
                default:
                    content = default(TSuccess);
                    return false;
            }
        }

        /// <summary>
        /// Get the <typeparamref name="TError"/> from the specified <paramref name="result"/>.
        /// otherwise an <see cref="InvalidOperationException"/> is thrown if the <paramref name="result"/> is not an instance of <see cref="Failure{TError, TSuccess}"/>
        /// </summary>
        /// <typeparam name="TSuccess">The success type</typeparam>
        /// <typeparam name="TError">The error type</typeparam>
        /// <param name="result">The <see cref="Result{TSuccess, TError}"/> that is the target of this operation.</param>
        /// <returns></returns>
        public static TError GetError<TSuccess, TError>(this Result<TSuccess, TError> result)
        {
            switch(result)
            {
                case Failure<TError, TSuccess> failure:
                    return failure.Error;
                default:
                    throw new InvalidOperationException("Cannot get an error from a Result that is a Success instance.");
            }
        }
    }
}
