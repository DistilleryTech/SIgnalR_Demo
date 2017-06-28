namespace SignalRKit.Web.Helpers.HubMethodValidation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.SignalR.Hubs;
    using Utilities;

    /// <summary>
    /// An attribute for validating requests to SignalR hubs.
    /// It checks FluentValidation validators
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="Distillery.Shade.Web.Api.Helpers.HubMethodValidation.IValidateHubMethodInvocation" />
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class ValidateHubMethodAttribute : Attribute, IValidateHubMethodInvocation
    {
        /// <summary>
        /// Given a <see cref="IHubIncomingInvokerContext"/>, validate all the passed in parameters to determine if they are valid to invoke the <see cref="IHub"/> method.
        /// It checks FluentValidation validators
        /// </summary>
        /// <param name="hubIncomingInvokerContext">An <see cref="IHubIncomingInvokerContext"/> providing details regarding the <see cref="IHub"/> method invocation</param>
        /// <returns>List of invalid fields</returns>
        public IEnumerable<String> ValidateHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext)
        {
            var errors = new List<String>();

            for (var i = 0; i < hubIncomingInvokerContext.Args.Count; i++)
            {
                var arg = hubIncomingInvokerContext.Args[i];

                var validatorInstance = FluentValidationHelper.GetValidator(arg);
                if (validatorInstance != null)
                {
                    var validationResult = validatorInstance.Validate(arg);
                    if (!validationResult.IsValid)
                    {
                        var paramName = hubIncomingInvokerContext.MethodDescriptor.Parameters[i].Name;
                        errors.AddRange(validationResult.Errors.Select(error => $"{paramName}.{error.PropertyName}"));
                    }
                }
            }

            return errors;
        }
    }
}