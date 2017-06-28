namespace SignalRKit.Web.Utilities
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// Custom implementation of Bearer token handling to make SignalR work
    /// </summary>
    /// <seealso cref="Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationProvider" />
    public class ApplicationOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        /// <summary>
        /// Handles processing OAuth bearer token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var queryToken = context.OwinContext.Request.Query["access_token"];
            if (!String.IsNullOrEmpty(queryToken))
                context.Token = queryToken;

            return Task.FromResult<Object>(null);
        }

    }
}