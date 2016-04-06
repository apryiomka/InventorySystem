using System;
using System.Collections.Generic;
using System.Linq;

namespace inventorySyctem.Services.Bus
{
    /// <summary>
    /// The bus router
    /// </summary>
    public interface  IRouter
    {
        /// <summary>
        /// Routes the message to the appropritae subscriber
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotSupportedException"></exception>
        void Route<TMessage>(TMessage message) where TMessage : class, IMessage;
    }

    /// <summary>
    /// The basic in memory router implementation
    /// </summary>
    public class SubscriberRouter : IRouter
    {
        private readonly Dictionary<Type, List<ISubscriber>> _regesteredSubscribers = new Dictionary<Type, List<ISubscriber>>();

        /// <summary>
        /// Creates the instance of <see cref="SubscriberRouter"/>
        /// </summary>
        /// <param name="subscribers"></param>
        public SubscriberRouter(IEnumerable<ISubscriber> subscribers)
        {
            foreach (var subscriber in subscribers)
            {
                Register(subscriber);
            }
        }

        private void Register(ISubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            var messageType = subscriber.GetType()
                                .GetInterface(typeof(ISubscriber<>).FullName)
                                .GetGenericArguments()
                                .Single();

            List<ISubscriber> listOfSubscribers;
            //check for existing subscribers
            if (_regesteredSubscribers.TryGetValue(messageType, out listOfSubscribers))
            {
                //if the type already have subscribers, add one more to the list
                listOfSubscribers.Add(subscriber);
            }
            else
            {
                //if no subscribers, add a new one
                _regesteredSubscribers.Add(messageType, new List<ISubscriber> { subscriber });
            }
        }

        void IRouter.Route<TMessage>(TMessage message)
        {
            List<ISubscriber> subscribers;
            if (!_regesteredSubscribers.TryGetValue(typeof(TMessage), out subscribers))
                throw new NotSupportedException("Unsupported entity type");

            //call each subscriber with a message
            foreach (var subscriber in subscribers)
            {
                ((ISubscriber<TMessage>)subscriber).Process(message);
            }
        }
    }
}
