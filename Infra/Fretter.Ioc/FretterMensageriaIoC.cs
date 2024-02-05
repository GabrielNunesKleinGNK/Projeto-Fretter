
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Fretter.Domain.Entities.Configs;
using Microsoft.Extensions.Options;
using Fretter.Domain.Interfaces.Mensageria;
using Fretter.Mensageria;

namespace Fretter.IoC
{
    public static class FretterMensageriaIoc
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitConfig>(options => configuration.GetSection("RabbitConfig").Bind(options));

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var rabbitConfig = sp.GetRequiredService<IOptions<RabbitConfig>>();

                var retryCount = 5;

                return new RabbitMQPersistentConnection(rabbitConfig, retryCount);
            });

            services.AddSingleton(typeof(IRabbitMQPublisher), typeof(RabbitMQPublisher));

            services.AddTransient(typeof(IRabbitMQConsumerBulk), typeof(RabbitMQConsumerBulk));

            services.AddTransient(typeof(IRabbitMQConsumer<>), typeof(RabbitMQConsumer<>));
        }
    }
}