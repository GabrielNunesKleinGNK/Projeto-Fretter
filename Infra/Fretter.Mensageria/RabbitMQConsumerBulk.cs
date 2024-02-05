using Fretter.Domain.Interfaces.Mensageria;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Fretter.Mensageria
{
    public class RabbitMQConsumerBulk : IRabbitMQConsumerBulk, IDisposable
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;

        private bool _dispose { get; set; }
        private IModel _channel;

        public List<ulong> ListOjects { get; protected set; }

        public RabbitMQConsumerBulk(IRabbitMQPersistentConnection persistentConnection)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            this.ListOjects = new List<ulong>();
            CreateChannel();
        }

        private void CreateChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            if (_channel == null || _channel != null && _channel.IsClosed)
                _channel = (IModel)_persistentConnection.CreateModel();
        }

        public async Task<List<TEntity>> GetMessages<TEntity>(string queueName, ushort listSize = 100)
        {
            bool valid = true;

            var list = new List<TEntity>();

            var timeOutToken = new CancellationTokenSource();

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(30), timeOutToken.Token).ContinueWith(p =>
                {
                    if (p.IsCompleted)
                    {
                        valid = false;
                    }
                });

                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                if (_persistentConnection.IsConnected)
                {
                    CreateChannel();

                    var argsQueue = new Dictionary<string, object>();
                    argsQueue.Add("passive", "true");

                    var response = _channel.QueueDeclare(queueName, true, false, false, argsQueue);

                    if (response.MessageCount > 0)
                    {
                        var size = response.MessageCount > listSize ? listSize : response.MessageCount;

                        _channel.BasicQos(0, (ushort)size, false);

                        await Task.Run(() =>
                        {
                            var consumer = new EventingBasicConsumer(_channel);

                            consumer.Received += (sender, args) =>
                            {
                                if (list.Count < size && valid)
                                {
                                    try
                                    {
                                        var body = args.Body.ToArray();
                                        var message = Encoding.UTF8.GetString(body);
                                        var objectEntity = JsonConvert.DeserializeObject<TEntity>(message);

                                        list.Add(objectEntity);
                                        this.ListOjects.Add(args.DeliveryTag);
                                    }
                                    finally
                                    {
                                        //  _channel.BasicAck(args.DeliveryTag, multiple: false);
                                    }
                                }
                            };

                            String consumerTag = _channel.BasicConsume(queueName, false, consumer);
                        });

                        await Task.Run(async () =>
                        {
                            while (list.Count < size && valid)
                            {
                                await Task.Delay(TimeSpan.FromMilliseconds(100), timeOutToken.Token);
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter mensagem do rabbirmq", ex.Message);
            }
            finally
            {
                timeOutToken?.Cancel();
            }

            return list;
        }

        private void CloseChannel()
        {
            if (!_channel.IsClosed)
                _channel.Dispose();
        }


        public void Commit()
        {
            foreach (var item in ListOjects)
            {
                _channel.BasicAck(item, multiple: false);
            }

            this.ListOjects = new List<ulong>();

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

                CloseChannel();
            }
        }
    }
}


