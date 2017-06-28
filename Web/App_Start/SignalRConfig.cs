namespace SignalRKit.Web.App_Start
{
    using Core.IoC;
    using Helpers;
    using Helpers.HubMethodValidation;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.Owin.Cors;
    using Newtonsoft.Json;
    using Owin;
    using Utilities;

    /// <summary>
    /// Configures SignalR
    /// </summary>
    public class SignalRConfig
    {
        /// <summary>
        /// Registers the instance
        /// </summary>
        /// <param name="app">The application</param>
        public static void Register(IAppBuilder app)
        {
            // Use custom code for getting User Id. It's used by SignalR.
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new UserIdSignalRProvider());

            // Configure Json Serializer
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new SignalRContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            var serializer = JsonSerializer.Create(settings);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);

            // Register a Hub Activator for SignalR
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator());

            // Register the validation module
            GlobalHost.HubPipeline.AddModule(new ValidationHubPipelineModule());

            // Register the error handling module
            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());

            // Branch the pipeline here for requests that start with "/signalr"
            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some versions of IE) require JSONP to work cross domain
                    //EnableJSONP = true
                };

                // Run the SignalR pipeline. We're not using MapSignalR since this branch already runs under the "/signalr" path.
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}