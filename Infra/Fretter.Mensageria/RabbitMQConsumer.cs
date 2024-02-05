using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Fretter.Domain.Interfaces.Mensageria;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fretter.Mensageria
{
    public class RabbitMQConsumer<TEntity> : IRabbitMQConsumer<TEntity>, IDisposable
        where TEntity : class
    {
        public event EventHandler<TEntity> OnMessage;
        public event EventHandler<Exception> OnError;

        private readonly IRabbitMQPersistentConnection _persistentConnection;

        public bool _dispose { get; set; }

        private EventingBasicConsumer _consumer;
        private IModel _channel;


        public RabbitMQConsumer(IRabbitMQPersistentConnection persistentConnection)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        }

        public void Subscribe(string queueName)
        {
            try
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                if (_persistentConnection.IsConnected)
                {
                    _channel = (IModel)_persistentConnection.CreateModel();

                    var argsQueue = new Dictionary<string, object>();
                    argsQueue.Add("passive", "true");

                    var response = _channel.QueueDeclare(queueName, true, false, false, argsQueue);

                    _channel.BasicQos(0, 20, false);

                    _consumer = new EventingBasicConsumer(_channel);
                    _consumer.Received += (sender, args) =>
                        {
                            try
                            {
                                var body = args.Body.ToArray();
                                var message = Encoding.UTF8.GetString(body);
                                var objectEntity = JsonConvert.DeserializeObject<TEntity>(message);

                                OnMessage?.Invoke(this, objectEntity);

                                _channel.BasicAck(args.DeliveryTag, multiple: false);
                            }
                            catch (Exception ex)
                            {
                                OnError.Invoke(this, ex);
                            }
                        };

                    String consumerTag = _channel.BasicConsume(queueName, false, _consumer);

                }
            }
            catch (Exception ex)
            {
                OnError.Invoke(this, ex);
            }
        }

        public int Count(string queueName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = (IModel)_persistentConnection.CreateModel())
            {
                var response = channel.QueueDeclarePassive(queueName);
                return (int)response.MessageCount;
            }
        }

        public void Dispose()
        {
            if (!_dispose)
            {
                this._dispose = true;

                _channel?.Dispose();
            }
        }
    }
}
