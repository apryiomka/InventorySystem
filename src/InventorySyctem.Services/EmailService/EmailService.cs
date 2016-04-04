using System;
using System.Threading.Tasks;

namespace inventorySyctem.Services.EmailService
{
    /// <summary>
    /// Email service implementation for <see cref="IEmailService"/>
    /// </summary>
    public class EmailService : IEmailService
    {
        async Task IEmailService.SendNotificationEmail(string itemLabel)
        {
            await Task.FromResult(0); //send email
        }
    }
}
