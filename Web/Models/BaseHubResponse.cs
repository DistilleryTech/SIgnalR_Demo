namespace SignalRKit.Web.Models
{
    using System;
    using System.Collections.Generic;
    using SignalRKit.Core.Models;
    using SignalRKit.Core.Models.Exceptions;

    /// <summary>
    /// Base SignalR hub response
    /// </summary>
    public abstract class BaseHubResponse
    {
        #region Initialization Members

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHubResponse"/> class.
        /// </summary>
        /// <param name="successful">if set to <c>true</c> [successful].</param>
        public BaseHubResponse(Boolean successful)
        {
            Successful = successful;
        }

        #endregion Initialization Members

        /// <summary>
        /// Gets a value indicating whether this <see cref="BaseHubResponse"/> is successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if successful; otherwise, <c>false</c>.
        /// </value>
        public virtual Boolean Successful { get; private set; }
    }

    /// <summary>
    /// Successful SignalR hub response
    /// </summary>
    /// <seealso cref="Distillery.Shade.Web.Api.Models.BaseHubResponse" />
    public class OkHubResponse : BaseHubResponse
    {
        #region Initialization Members

        /// <summary>
        /// Initializes a new instance of the <see cref="OkHubResponse"/> class.
        /// </summary>
        public OkHubResponse()
            : base(true)
        { }

        #endregion Initialization Members
    }

    /// <summary>
    /// Successful SignalR hub response with data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Distillery.Shade.Web.Api.Models.BaseHubResponse" />
    public class OkHubResponse<T> : OkHubResponse
    {
        #region Initialization Members

        /// <summary>
        /// Initializes a new instance of the <see cref="OkHubResponse{T}"/> class.
        /// </summary>
        public OkHubResponse()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OkHubResponse{T}"/> class.
        /// </summary>
        /// <param name="data">Data</param>
        public OkHubResponse(T data)
            : base()
        {
            Data = data;
        }

        #endregion Initialization Members

        /// <summary>
        /// Data
        /// </summary>
        public virtual T Data { get; set; }
    }

    /// <summary>
    /// Error SignalR hub response
    /// </summary>
    /// <seealso cref="Distillery.Shade.Web.Api.Models.BaseHubResponse" />
    public class ErrorHubResponse : BaseHubResponse
    {
        #region Initialization Members

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHubResponse"/> class
        /// </summary>
        /// <param name="error">The error</param>
        public ErrorHubResponse(OutboundError error)
            : base(false)
        {
            ErrorCode = error.ErrorCode;
            ErrorMessage = error.ErrorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHubResponse" /> class
        /// </summary>
        /// <param name="errorException">The error exception</param>
        public ErrorHubResponse(OutboundErrorException errorException)
            : this(errorException.OutboundError)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHubResponse"/> class
        /// </summary>
        /// <param name="error">The outbound error</param>
        /// <param name="invalidFields">A list with additional invalid fields</param>
        public ErrorHubResponse(OutboundError error, IEnumerable<String> invalidFields = null)
            : this(error)
        {
            InvalidFields = invalidFields;
        }

        #endregion Initialization Members

        /// <summary>
        /// The error code
        /// </summary>
        public virtual Int32 ErrorCode { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public virtual String ErrorMessage { get; set; }

        /// <summary>
        /// A list with invalid fields
        /// </summary>
        public virtual IEnumerable<String> InvalidFields { get; set; }
    }
}