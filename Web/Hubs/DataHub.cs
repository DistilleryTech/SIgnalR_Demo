namespace SignalRKit.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Core.Services;
    using Helpers.HubMethodValidation;
    using Microsoft.AspNet.SignalR;
    using Models;
    using Models.Requests;
    using StructureMap.Attributes;

    public class DataHub : BaseHub
    {
        /// <summary>
        /// Data service
        /// </summary>
        [SetterProperty]
        public DataService DataService { get; set; }

        /// <summary>
        /// Says Hello to all users
        /// </summary>
        /// <returns>No content</returns>
        public virtual BaseHubResponse Hello()
        {
            Clients.All.hello();

            return OkResponse();
        }

        /// <summary>
        /// Marks the message as read
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>No content</returns>
        [ValidateHubMethod]
        public virtual BaseHubResponse MarkMessageAsRead(MessageMarkAsReadRequest request)
        {
            var message = DataService.MarkMessageAsRead(request.MessageId);

            Clients.All.MarkMessageAsRead(message);

            return OkResponse();
        }
    }
}