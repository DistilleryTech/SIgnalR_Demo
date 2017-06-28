namespace SignalRKit.Web.Hubs
{
    using System;
    using Microsoft.AspNet.SignalR;
    using SignalRKit.Core.Models;
    using SignalRKit.Core.Models.Exceptions;
    using SignalRKit.Web.Helpers;
    using SignalRKit.Web.Models;

    /// <summary>
    /// Base SignalR hub
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    public class BaseHub : Hub
    {
        #region Auth Members

        /// <summary>
        /// Id of the API user
        /// </summary>
        protected Int64 ApiUserId { get { return Context.User.GetUserIdValue(); } }

        /// <summary>
        /// UserName of the API user
        /// </summary>
        protected String ApiUserName { get { return Context.User.GetUserName(); } }

        #endregion Auth Members

        /// <summary>
        /// Ok response
        /// </summary>
        /// <returns></returns>
        protected BaseHubResponse OkResponse()
        {
            return new OkHubResponse();
        }

        /// <summary>
        /// Ok response with data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Data</param>
        /// <returns></returns>
        protected BaseHubResponse OkResponse<T>(T data)
        {
            return new OkHubResponse<T>(data);
        }

        /// <summary>
        /// Error response
        /// </summary>
        /// <param name="error">The error</param>
        /// <returns></returns>
        protected BaseHubResponse ErrorResponse(OutboundError error)
        {
            return new ErrorHubResponse(error);
        }

        /// <summary>
        /// Error response
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns></returns>
        protected BaseHubResponse ErrorResponse(OutboundErrorException exception)
        {
            return new ErrorHubResponse(exception.OutboundError);
        }
    }
}