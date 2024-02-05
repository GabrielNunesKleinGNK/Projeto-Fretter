using Newtonsoft.Json;
using RabbitMQ.Client;
using Fretter.Domain.Interfaces.Mensageria;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Mensageria
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;

        public bool _dispose { get; set; }

        public RabbitMQPublisher(IRabbitMQPersistentConnection persistentConnection)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        }

        public void PublishMessage<TEntity>(string queueName, TEntity message)
        {
            try
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                if (_persistentConnection.IsConnected)
                {
                    using (var channel = (IModel)_persistentConnection.CreateModel())
                    {
                        var durable = true;
                        var args = new Dictionary<string, object>();
                        // args.Add("x-queue-mode", "lazy");
                        args.Add("passive", "true");
                        channel.QueueDeclare(queueName, durable, false, false, args);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                        channel.BasicPublish("", queueName, properties, bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao publicar mensagem na fila {queueName} do RabbitMQ", ex.Message);
            }
        }


        public void PublishList<TEntity>(string queueName, IEnumerable<TEntity> messages)
        {
            try
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                if (_persistentConnection.IsConnected)
                {
                    using (var channel = (IModel)_persistentConnection.CreateModel())
                    {
                        var durable = true;
                        var args = new Dictionary<string, object>();
                        // args.Add("x-queue-mode", "lazy");
                        args.Add("passive", "true");
                        channel.QueueDeclare(queueName, durable, false, false, args);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        foreach (var message in messages)
                        {
                            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                            channel.BasicPublish("", queueName, properties, bytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao publicar mensagem na fila {queueName} do RabbitMQ", ex.Message);
            }
        }
    }
}
