using System.Threading.Tasks;

namespace inventorySyctem.Services.EmailService
{
    /// <summary>
    /// Email service to send email notifications
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email that the item with specific label was removed
        /// </summary>
        /// <param name="itemLabel">The actual item label</param>
        /// <returns></returns>
        Task SendNotificationEmail(string itemLabel);
    }
}
