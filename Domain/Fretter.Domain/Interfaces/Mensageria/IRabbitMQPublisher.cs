using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Mensageria
{
    public interface IRabbitMQPublisher
    {
        void PublishMessage<TEntity>(string queueName, TEntity message);
        void PublishList<TEntity>(string queueName, IEnumerable<TEntity> messages);
    }
}
