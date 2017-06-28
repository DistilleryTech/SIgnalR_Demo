namespace SignalRKit.Core.Models.Exceptions
{
    using System;

    /// <summary>
    /// An exception class for outbound errors
    /// </summary>
    public class OutboundErrorException : Exception
    {
        #region Initialization Members

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundErrorException"/> class.
        /// </summary>
        protected OutboundErrorException()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboundErrorException"/> class.
        /// </summary>
        /// <param name="outboundError">The outbound error.</param>
        /// <param name="additionalMessage">The additional message.</param>
        public OutboundErrorException(OutboundError outboundError, String additionalMessage = null)
            : base(outboundError.ErrorMessage)
        {
            OutboundError = outboundError;
            OutboundError.AdditionalInfo = additionalMessage;
        }

        #endregion Initialization Members

        /// <summary>
        /// The outbound error
        /// </summary>
        public OutboundError OutboundError { get; set; }
    }
}
