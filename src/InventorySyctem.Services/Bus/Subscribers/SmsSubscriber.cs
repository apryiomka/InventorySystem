using System;
using inventorySyctem.Services.Bus.Messages;

namespace inventorySyctem.Services.Bus.Subscribers
{
    /// <summary>
    /// Sends the SMS message
    /// </summary>
    public class SmsSubscriber : Subscriber<SmsMessage>
    {
        protected override void ProcessMessage(SmsMessage message)
        {
            //todo: send the SMS message here
        }
    }
}
