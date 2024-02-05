
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IMessageBusService<TService> where TService : class
    {
        void InitReceiver(ReceiverType type, string serviceBusConnectionString, string queueOrTopic, string subscription = null, int prefectchCount = 100);
        void InitSender(string serviceBusConnectionString, string queueOrTopic);
        Task Send(object input);
        Task SendRange<T>(List<T> list);
        Task<IList<MessageData<T>>> Receive<T>(int quantity, int secondsToTimeout, bool throwDisabled = true);
        Task<IList<MessageData<string>>> Receive(int quantity, int secondsToTimeout, bool throwDisabled = true);
        Task Commit<T>(IList<MessageData<T>> messages);
        Task Abandon<T>(IList<MessageData<T>> messages);
        Task Close();
    }
}
