namespace VTS.Services.BookingService
{
    /// <summary>
    /// The default chaining behavior.
    /// </summary>
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        /// <inheritdoc />
        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        /// <inheritdoc />
        public virtual object Handle(Core.DTO.Holiday request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
