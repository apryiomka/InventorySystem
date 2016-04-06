using System;
using System.Collections.Generic;

namespace inventorySyctem.Services.Bus
{
    /// <summary>
    /// A simple in memory service bus implementation
    /// </summary>
    public interface IBus
    {
        /// <summary>
        /// Publishes entity to the bus
        /// </summary>
        /// <param name="message">The instance of <see cref="IMessage"/></param>
        /// <exception cref="ArgumentNullException">Message should never be null</exception>
        void Publish(IMessage message);
        /// <summary>
        /// Commits the transaction onse the message is published
        /// </summary>
        void Commit();
    }

    /// <summary>
    /// In memory service bus implementation
    /// </summary>
    public class Bus : IBus
    {
        //siimple in memory queue
        private readonly Queue<IMessage> _messages = new Queue<IMessage>();
        private readonly IRouter _router;

        /// <summary>
        /// Creates instance of <see cref="Bus"/>
        /// </summary>
        /// <param name="router"></param>
        public Bus(IRouter router)
        {
            _router = router;
        }

        void IBus.Commit()
        {
            while (_messages.Count > 0)
            {
                var message = _messages.Dequeue();
                _router.Route(message);
            }
        }

        void IBus.Publish(IMessage message)
        {
            if(message == null) throw new ArgumentNullException(nameof(message));
            _messages.Enqueue(message);
        }
    }
}
