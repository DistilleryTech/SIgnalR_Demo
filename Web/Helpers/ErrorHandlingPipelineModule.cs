namespace SignalRKit.Web.Helpers
{
    using Core.Models;
    using Core.Models.Exceptions;
    using Core.Utilities;
    using Microsoft.AspNet.SignalR.Hubs;
    using Models;

    /// <summary>
    /// Pipeline module for handling exceptions
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hubs.HubPipelineModule" />
    public class ErrorHandlingPipelineModule : HubPipelineModule
    {
        /// <summary>
        /// This is called when an uncaught exception is thrown by a server-side hub method or the incoming component of a
        /// module added later to the <see cref="T:Microsoft.AspNet.SignalR.Hubs.IHubPipeline" />. Observing the exception using this method will not prevent
        /// it from bubbling up to other modules.
        /// </summary>
        /// <param name="exceptionContext">Represents the exception that was thrown during the server-side invocation.
        /// It is possible to change the error or set a result using this context.</param>
        /// <param name="invokerContext">A description of the server-side hub method invocation.</param>
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            var errorFromContext = exceptionContext.Error;
            if (errorFromContext != null)
            {
                OutboundError error;

                if (errorFromContext is OutboundErrorException)
                    error = ((OutboundErrorException)errorFromContext).OutboundError;
                else
                    error = OutboundErrors.General.UNDEFINED_ERROR;

                exceptionContext.Result = new ErrorHubResponse(error);
            }

            base.OnIncomingError(exceptionContext, invokerContext);
        }
    }
}