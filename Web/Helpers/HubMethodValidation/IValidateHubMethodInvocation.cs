﻿namespace SignalRKit.Web.Helpers.HubMethodValidation
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR.Hubs;

    /// <summary>
    /// Interface to be implemented by <see cref="System.Attribute"/>s that can validate the parameters of an invocation of <see cref="IHub"/> methods.
    /// </summary>
    public interface IValidateHubMethodInvocation
    {
        /// <summary>
        /// Given a <see cref="IHubIncomingInvokerContext"/>, validate all the passed in parameters to determine if they are valid to invoke the <see cref="IHub"/> method
        /// </summary>
        /// <param name="hubIncomingInvokerContext">An <see cref="IHubIncomingInvokerContext"/> providing details regarding the <see cref="IHub"/> method invocation</param>
        /// <returns>List of invalid fields</returns>
        IEnumerable<String> ValidateHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext);
    }
}