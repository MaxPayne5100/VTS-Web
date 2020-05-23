namespace VTS.Services.BookingService
{
    /// <summary>
    /// The Handler interface declares a method for building the chain of
    /// handlers. It also declares a method for executing a request.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Method for building the chain of handlers.
        /// </summary>
        /// <param name="handler">handler.</param>
        /// <returns>handler or null.</returns>
        IHandler SetNext(IHandler handler);

        /// <summary>
        /// Method for checking whether your holiday request is suitable with company policy.
        /// </summary>
        /// <param name="request">Holiday DTO.</param>
        /// <returns>Exception or null.</returns>
        object Handle(Core.DTO.Holiday request);
    }
}
