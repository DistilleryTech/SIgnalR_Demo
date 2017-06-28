namespace SignalRKit.Core.Utilities
{
    using Models;

    /// <summary>
    /// A class with a set of error messages
    /// </summary>
    public static class OutboundErrors
    {
        /// <summary>
        /// A class with a set of general error messages.
        /// All error codes from this class start with 50.
        /// </summary>
        public static class General
        {
            // Basic 500 exception
            public static readonly OutboundError UNDEFINED_ERROR = new OutboundError(500, "Something went wrong. Please, try again later.");

            // Request errors
            public static readonly OutboundError REQUEST_IS_INVALID = new OutboundError(510, "The request is invalid.");
            public static readonly OutboundError REQUEST_IS_EMPTY = new OutboundError(511, "The request is empty.");
            public static readonly OutboundError REQUEST_IS_UNSUPPORTED_MEDIA_TYPE = new OutboundError(512, "The request is an unsupported type.");
        }
    }
}
