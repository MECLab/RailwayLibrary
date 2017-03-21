namespace RailwayLibrary.Result
{
    /// <summary>
    /// Either monad that represents a Result that can either be a <see cref="Success{TSuccess,TError}"/> or a <see cref="Failure{TError,TSuccess}"/>
    /// For more information see https://fsharpforfunandprofit.com/posts/recipe-part2/ 
    /// </summary>
    /// <typeparam name="TSuccess">The type of the success content.</typeparam>
    /// <typeparam name="TError">The type of the error content.</typeparam>
    /// <seealso cref="Success{TSuccess,TError}"/>
    /// <seealso cref="Failure{TError,TSuccess}"/>
    public abstract class Result<TSuccess, TError>
    {
        /// <summary>
        /// Gets a value that indicates if this is an instance of a <see cref="Success{TSuccess,TError}"/> <see cref="Result{TSuccess,TError}"/>.
        /// </summary>
        public bool IsSuccess => this is Success<TSuccess, TError>;
    }

    /// <summary>
    /// Represents a successful <see cref="Result{TSuccess,TError}"/>
    /// </summary>
    /// <typeparam name="TSuccess">The type of the success content.</typeparam>
    /// <typeparam name="TError">The type of the error content.</typeparam>
    public class Success<TSuccess, TError> : Result<TSuccess, TError>
    {
        /// <summary>
        /// Initializes an instance of a <see cref="Success{TSuccess,TError}"/>
        /// </summary>
        /// <param name="content">The content</param>
        public Success(TSuccess content)
        {
            Content = content;
        }

        /// <summary>
        /// Gets the content of this successful <see cref="Result{TSuccess,TError}"/>
        /// </summary>
        public TSuccess Content { get; }
    }

    /// <summary>
    /// Represents a failed <see cref="Result{TSuccess,TError}"/>
    /// </summary>
    /// <typeparam name="TError">the type of the error content</typeparam>
    /// <typeparam name="TSuccess">the type of the success content</typeparam>
    public class Failure<TError, TSuccess> : Result<TSuccess, TError>
    {
        /// <summary>
        /// Initializes an instance of a <see cref="Failure{TError,TSuccess}"/>
        /// </summary>
        /// <param name="error"></param>
        public Failure(TError error)
        {
            Error = error;
        }

        /// <summary>
        /// Get the content of this failed <see cref="Result{TSuccess,TError}"/>
        /// </summary>
        public TError Error { get; }
    }
}
