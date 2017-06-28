namespace SignalRKit.Web.Utilities
{
    using System;
    using System.Reflection;
    using Microsoft.AspNet.SignalR.Infrastructure;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// SignalR contract resolver
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Serialization.IContractResolver" />
    public class SignalRContractResolver : IContractResolver
    {
        #region Initialization Members

        private readonly Assembly assembly;
        private readonly IContractResolver camelCaseContractResolver;
        private readonly IContractResolver defaultContractSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRContractResolver"/> class.
        /// </summary>
        public SignalRContractResolver()
        {
            defaultContractSerializer = new DefaultContractResolver();
            camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
            assembly = typeof(Connection).Assembly;
        }

        #endregion Initialization Members

        /// <summary>
        /// Resolves the contract for a given type.
        /// </summary>
        /// <param name="type">The type to resolve a contract for.</param>
        /// <returns>
        /// The contract for a given type.
        /// </returns>
        public JsonContract ResolveContract(Type type)
        {
            if (type.Assembly.Equals(assembly))
                return defaultContractSerializer.ResolveContract(type);

            return camelCaseContractResolver.ResolveContract(type);
        }
    }
}