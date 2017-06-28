namespace SignalRKit.Web.Models.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FluentValidation;
    using FluentValidation.Attributes;

    /// <summary>
    /// Request to update the message status
    /// </summary>
    [Serializable]
    [Validator(typeof(MessageMarkAsReadValidator))]
    public class MessageMarkAsReadRequest
    {
        /// <summary>
        /// The message identifier
        /// </summary>
        [Required]
        public virtual Int32 MessageId { get; set; }

        #region Validator

        /// <summary>
        /// Validator for the request
        /// </summary>
        protected internal class MessageMarkAsReadValidator : AbstractValidator<MessageMarkAsReadRequest>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MessageMarkAsReadValidator"/> class.
            /// </summary>
            public MessageMarkAsReadValidator()
            {
                RuleFor(p => p.MessageId)
                    .GreaterThan(0);
            }
        }

        #endregion Validator
    }
}