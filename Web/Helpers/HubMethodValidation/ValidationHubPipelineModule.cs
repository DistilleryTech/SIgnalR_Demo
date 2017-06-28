namespace SignalRKit.Web.Helpers.HubMethodValidation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Hubs;
    using SignalRKit.Core.Utilities;
    using SignalRKit.Web.Models;

    /// <summary>
    /// Hub pipeline module for validating requests
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hubs.HubPipelineModule" />
    public class ValidationHubPipelineModule : HubPipelineModule
    {
        /// <summary>
        /// The method invocation cache
        /// </summary>
        private readonly ConcurrentDictionary<MethodDescriptor, IEnumerable<IValidateHubMethodInvocation>> _methodInvocationCache = new ConcurrentDictionary<MethodDescriptor, IEnumerable<IValidateHubMethodInvocation>>();

        /// <summary>
        /// Wraps a function that invokes a server-side hub method. Even if a client has not been authorized to connect
        /// to a hub, it will still be authorized to invoke server-side methods on that hub unless it is prevented in
        /// <see cref="M:Microsoft.AspNet.SignalR.Hubs.IHubPipelineModule.BuildIncoming(System.Func{Microsoft.AspNet.SignalR.Hubs.IHubIncomingInvokerContext,System.Threading.Tasks.Task{System.Object}})" /> by not executing the invoke parameter.
        /// </summary>
        /// <param name="invoke">A function that invokes a server-side hub method.</param>
        /// <returns>
        /// A wrapped function that invokes a server-side hub method.
        /// </returns>
        public override Func<IHubIncomingInvokerContext, Task<object>> BuildIncoming(Func<IHubIncomingInvokerContext, Task<object>> invoke)
        {
            return base.BuildIncoming(context =>
            {
                // Get method attributes implementing IValidateHubMethodInvocation from the cache
                // If the attributes do not exist in the cache, retrieve them from the MethodDescriptor and add them to the cache
                var methodLevelValidator = _methodInvocationCache.GetOrAdd(context.MethodDescriptor,
                    methodDescriptor => methodDescriptor.Attributes.OfType<IValidateHubMethodInvocation>()).FirstOrDefault();

                // no validator... keep going on with the rest of the pipeline
                if (methodLevelValidator == null)
                    return invoke(context);

                var invalidFields = methodLevelValidator.ValidateHubMethodInvocation(context);

                // no errors... keep going on with the rest of the pipeline
                if (!invalidFields.Any())
                    return invoke(context);

                var response = new ErrorHubResponse(OutboundErrors.General.REQUEST_IS_INVALID, invalidFields);

                // Send error model as a result back to the client
                return SetResult<Object>(response);
            });
        }

        /// <summary>
        /// Creates a TaskCompletionSource object and sets the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultModel">The result model</param>
        /// <returns>Created TaskCompletionSource object with the result</returns>
        private static Task<T> SetResult<T>(T resultModel)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(resultModel);
            return tcs.Task;
        }
    }
}