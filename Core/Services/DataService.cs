namespace SignalRKit.Core.Services
{
    using System;
    using Models;

    public class DataService
    {
        public ChatMessage MarkMessageAsRead(Int32 messageId)
        {
            return new ChatMessage { Id = messageId, IsRead = true, Text = "Message text" };
        }
    }
}
