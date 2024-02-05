using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Mensageria
{
    public interface IRabbitMQConsumer<TEntity>
        where TEntity : class
    {
        event EventHandler<TEntity> OnMessage;
        event EventHandler<Exception> OnError;

        void Subscribe(string queueName);
    }
}
