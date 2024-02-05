using Fretter.Domain.Enum;
using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Fretter.Domain.Helpers;
using Newtonsoft.Json;
using Fretter.Domain.Extentions;

namespace Fretter.Domain.Services
{
    public delegate void Error(Message message, string body, Exception ex);

    public class MessageBusService<TService> : IMessageBusService<TService>, IDisposable
        where TService : class
    {
        IMessageSender _sender;
        IMessageReceiver _receiver;
        public event Error OnError;

        public MessageBusService()
        {
        }
        public void InitReceiver(ReceiverType type, string serviceBusConnectionString, string queueOrTopic, string subscription = null, int prefectchCount = 100)
        {
            string entityPath = queueOrTopic;

            if (type == ReceiverType.Topic)
                entityPath = EntityNameHelper.FormatSubscriptionPath(queueOrTopic, subscription);
            else if (type == ReceiverType.DeadLetter)
                entityPath = EntityNameHelper.FormatDeadLetterPath(queueOrTopic);
            if (_receiver == null && !string.IsNullOrEmpty(queueOrTopic))
                _receiver = new MessageReceiver(serviceBusConnectionString, entityPath, ReceiveMode.PeekLock, RetryPolicy.Default, prefectchCount);
        }
        public void InitSender(string serviceBusConnectionString, string queueOrTopic)
        {
            if (_sender == null && !string.IsNullOrEmpty(queueOrTopic))
                _sender = new MessageSender(serviceBusConnectionString, queueOrTopic);
        }
        public async Task Send(object input)
        {
            Message message = input.AsMessage();
            await _sender.SendAsync(message);
        }
        public async Task SendRange<T>(List<T> list)
        {
            var taskList = new List<Task>();
            if (list.Any())
            {
                foreach (T item in list)
                    taskList.Add(_sender.SendAsync(item.AsMessage()));
            }
            Task.WhenAll(taskList).Wait();
            taskList.Clear();
        }
        public async Task<IList<MessageData<T>>> Receive<T>(int quantity, int secondsToTimeout, bool throwDisabled = true)
        {
            List<MessageData<T>> messageData = new List<MessageData<T>>();
            IList<Message> messages = new List<Message>();

            try
            {
                //messages = await _receiver.PeekAsync(quantity);
                messages = await _receiver.ReceiveAsync(quantity, TimeSpan.FromSeconds(secondsToTimeout));
            }
            catch (MessagingEntityDisabledException ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Error to receive messages from a topic : {ex.Message}");

                if (throwDisabled)
                    throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Error to receive messages from a topic : {ex.Message}");
            }

            if (messages.Any())
            {
                foreach (var message in messages)
                {
                    try
                    {
                        T body = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
                        messageData.Add(new MessageData<T>(message.SystemProperties.LockToken, message.SystemProperties.LockedUntilUtc, body));
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke(message, Encoding.UTF8.GetString(message.Body), ex);
                        await _receiver.CompleteAsync(message.SystemProperties.LockToken);
                    }
                }
            }

            return messageData;
        }

        public async Task<IList<MessageData<string>>> Receive(int quantity, int secondsToTimeout, bool throwDisabled = true)
        {
            List<MessageData<string>> messageData = new List<MessageData<string>>();
            IList<Message> messages = new List<Message>();

            try
            {
                //messages = await _receiver.PeekAsync(quantity);
                messages = await _receiver.ReceiveAsync(quantity, TimeSpan.FromSeconds(secondsToTimeout));
            }
            catch (MessagingEntityDisabledException ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Error to receive messages from a topic : {ex.Message}");

                if (throwDisabled)
                    throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Error to receive messages from a topic : {ex.Message}");
            }

            if (messages.Any())
            {
                foreach (var message in messages)
                {
                    try
                    {
                        messageData.Add(new MessageData<string>(message.SystemProperties.LockToken, message.SystemProperties.LockedUntilUtc, Encoding.UTF8.GetString(message.Body)));
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke(message, Encoding.UTF8.GetString(message.Body), ex);
                        await _receiver.CompleteAsync(message.SystemProperties.LockToken);
                    }
                }
            }

            return messageData;
        }
        public async Task Commit<T>(IList<MessageData<T>> messages)
        {
            var watch = new Stopwatch();
            watch.Start();
            List<string> tokens = new List<string>();

            foreach (var message in messages)
                tokens.Add(message.Token);

            await _receiver.CompleteAsync(tokens);
            //await _receiver.CompleteAsync(messages.Select(x => x.Token));

            Console.WriteLine($"{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Commintando ({messages.Count}) no MessageBus - : {watch.ElapsedMilliseconds}");
        }
        public async Task Abandon<T>(IList<MessageData<T>> messages)
        {
            var watch = new Stopwatch();
            watch.Start();
            List<Task> tasks = new List<Task>();

            foreach (var message in messages)
                tasks.Add(_receiver.AbandonAsync(message.Token));

            Task.WhenAll(tasks).Wait();
            tasks.Clear();

            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Abandonando ({messages.Count}) no MessageBus - : {watch.ElapsedMilliseconds}");
        }
        public async Task Close()
        {
            if (_sender != null && !_sender.IsClosedOrClosing)
                await _sender.CloseAsync();
            if (_receiver != null && !_receiver.IsClosedOrClosing)
                await _receiver.CloseAsync();
        }
        public void Dispose()
        {
            Close().Wait();
        }
    }
}

