using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Mensageria
{
    public interface IRabbitMQPersistentConnection
     : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();
        object CreateModel();
    }
}
