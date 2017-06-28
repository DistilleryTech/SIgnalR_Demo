using System.Web.Http;
using Microsoft.Owin;
using Owin;
using SignalRKit.Core.IoC;
using SignalRKit.Web.App_Start;

[assembly: OwinStartup(typeof(SignalRKit.Web.Startup))]

namespace SignalRKit.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Initialize DI container
            ComponentFactory.Initialize();

            var config = new HttpConfiguration();

            // Configures Authentication and Authorization
            var authConfig = new OwinAuthConfig();
            authConfig.ConfigureAuth(app, config);

            // Configures SignalR
            SignalRConfig.Register(app);
        }
    }
}
