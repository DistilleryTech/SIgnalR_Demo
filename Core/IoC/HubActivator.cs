namespace SignalRKit.Core.IoC
{
    using Microsoft.AspNet.SignalR.Hubs;

    /// <summary>
    /// A SignalR hub activator using ComponentFactory container
    /// </summary>
    public class HubActivator : IHubActivator
    {
        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)ComponentFactory.GetInstance(descriptor.HubType);
        }
    }
}
