namespace SignalRKit.Core.Models
{
    using System;

    /// <summary>
    /// This class represents an error with a code and a message
    /// </summary>
    public class OutboundError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundError"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public OutboundError(Int32 errorCode, String errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// A code of an error
        /// </summary>
        public Int32 ErrorCode { get; set; }

        /// <summary>
        /// An error message
        /// </summary>
        public String ErrorMessage { get; set; }

        /// <summary>
        /// Any additional information
        /// </summary>
        public String AdditionalInfo { get; set; }
    }
}
