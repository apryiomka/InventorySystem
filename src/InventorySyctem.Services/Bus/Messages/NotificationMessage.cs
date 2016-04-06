namespace inventorySyctem.Services.Bus.Messages
{
    /// <summary>
    /// Notification message
    /// </summary>
    public abstract class NotificationMessage : IMessage{ }

    /// <summary>
    /// The email message
    /// </summary>
    public class EmailMessage : NotificationMessage
    {
        /// <summary>
        /// The email address
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// Sms message to send
    /// </summary>
    public class SmsMessage : NotificationMessage
    {
        /// <summary>
        /// The SMS number
        /// </summary>
        public string SmsNumber { get; set; }

    }
}
