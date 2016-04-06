using inventorySyctem.Services.Bus.Messages;

namespace inventorySyctem.Services.Bus.Subscribers
{
    /// <summary>
    /// Send email messages
    /// </summary>
    public class EmailSubscriber : Subscriber<EmailMessage>
    {
        protected override void ProcessMessage(EmailMessage message)
        {
            //todo: implement send emails
        }
    }
}
