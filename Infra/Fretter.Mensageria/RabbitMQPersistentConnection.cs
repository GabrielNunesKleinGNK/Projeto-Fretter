using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Fretter.Domain.Entities.Configs;
using Fretter.Domain.Interfaces.Mensageria;
using System;
using System.IO;
using System.Net.Sockets;

namespace Fretter.Mensageria
{
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        // private readonly ILogger _logger;
        private readonly int _retryCount;
        IConnection _connection;
        bool _disposed;

        object sync_root = new object();

        public RabbitMQPersistentConnection(IOptions<RabbitConfig> rabbitConfig, int retryCount = 5)
        {
            _retryCount = retryCount;

            _connectionFactory = new ConnectionFactory()
            {
                UserName = rabbitConfig.Value.Usuario,
                Password = rabbitConfig.Value.Senha,
                VirtualHost = rabbitConfig.Value.VirtualHost,
                HostName = rabbitConfig.Value.Host,
                AutomaticRecoveryEnabled = false
            };
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public object CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException)
            {
                //  _logger.Error("DefaultRabbitMQPersistentConnection - Dispose", ex);
            }
        }

        public bool TryConnect()
        {
            //  _logger.Info("RabbitMQ Client is trying to connect");

            lock (sync_root)
            {


                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                            //  _logger.Error("RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage}) {time.TotalSeconds:n1}", ex);
                        }
                );

                policy.Execute(() =>
                {
                    if (!IsConnected)
                    {
                        _connection = _connectionFactory
                              .CreateConnection();
                    }
                });

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    //  _logger.Info("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events " + _connection.Endpoint.HostName);

                    return true;
                }
                else
                {
                    //   _logger.Info("FATAL ERROR: RabbitMQ connections could not be created and opened");

                    return false;
                }

            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            //   _logger.Info("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            //_logger.Info("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            //  _logger.Info("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }
    }
}
