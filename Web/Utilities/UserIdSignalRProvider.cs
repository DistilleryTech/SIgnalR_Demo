namespace SignalRKit.Web.Utilities
{
    using System;
    using Helpers;
    using Microsoft.AspNet.SignalR;

    /// <summary>
    /// Custom provider for getting user Id.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.IUserIdProvider" />
    public class UserIdSignalRProvider : IUserIdProvider
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The user id.</returns>
        public String GetUserId(IRequest request)
        {
            var userId = request.User.GetUserId();
            return userId.HasValue ? userId.Value.ToString() : null;
        }
    }
}