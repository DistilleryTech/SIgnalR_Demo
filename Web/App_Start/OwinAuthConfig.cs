namespace SignalRKit.Web.App_Start
{
    using System;
    using System.Web.Http;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using SignalRKit.Web.Utilities;

    /// <summary>
    /// Configuring Authentication/Authorization
    /// </summary>
    public class OwinAuthConfig
    {
        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="httpConfiguration">The HTTP configuration.</param>
        public void ConfigureAuth(IAppBuilder app, HttpConfiguration httpConfiguration)
        {
            // Configure the application for OAuth based flow
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                Provider = new OAuthAuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
#if DEBUG
                AllowInsecureHttp = true
#endif
            };

            app.UseOAuthAuthorizationServer(oAuthOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new ApplicationOAuthBearerProvider()
            });
        }
    }
}