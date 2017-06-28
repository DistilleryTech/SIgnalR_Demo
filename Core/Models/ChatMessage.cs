namespace SignalRKit.Core.Models
{
    using System;

    public class ChatMessage
    {
        public virtual Int32 Id { get; set; }
        public virtual String Text { get; set; }
        public virtual Boolean IsRead { get; set; }
    }
}
