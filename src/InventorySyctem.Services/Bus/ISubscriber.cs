using System;

namespace inventorySyctem.Services.Bus
{
    /// <summary>
    /// The main subscriber interface
    /// </summary>
    public interface ISubscriber{ }

    public interface ISubscriber<TMessage> : ISubscriber where TMessage : class, IMessage
    {
        /// <summary>
        /// Processes the message
        /// </summary>
        /// <param name="entity"></param>
        void Process(TMessage entity);

    }

    /// <summary>
    /// The base class for all subscribers
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class Subscriber<TMessage> : ISubscriber<TMessage> where TMessage : class, IMessage
    {
        protected abstract void ProcessMessage(TMessage message);

        void ISubscriber<TMessage>.Process(TMessage message)
        {
            if (message == null) throw new ArgumentException("Cannot be null", nameof(message));
            ProcessMessage(message);
        }
    }

}
