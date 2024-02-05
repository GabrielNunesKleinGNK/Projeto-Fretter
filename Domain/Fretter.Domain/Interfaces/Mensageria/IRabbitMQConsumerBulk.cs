using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Mensageria
{
    public interface IRabbitMQConsumerBulk
    {
        Task<List<TEntity>> GetMessages<TEntity>(string queueName, ushort listSize = 100);
        int Count(string queueName);

        void Commit();
        void Dispose();
    }
}
