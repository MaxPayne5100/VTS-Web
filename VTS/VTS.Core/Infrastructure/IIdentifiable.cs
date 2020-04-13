namespace VTS.Core.Infrastructure
{
    /// <summary>
    /// Interface for get the identifier.
    /// </summary>
    /// <typeparam name="TKey">A type of the identifier.</typeparam>
    public interface IIdentifiable<TKey>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        TKey Id { get; }
    }
}