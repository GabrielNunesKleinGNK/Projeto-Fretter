using Quartz;
using System;
using System.Threading.Tasks;
using Fretter.Repository.Contexts;
using Fretter.Domain.Interfaces.Mensageria;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Serilog;
using System.Linq;

namespace Fretter.Service.Tasks
{
    [DisallowConcurrentExecution]
    class LogRequestTask : IJob//, IJobTask
    {
        private readonly IGenericRepository<CommandContext> _repository;
        private readonly IRabbitMQConsumerBulk _rabbitMQConsumer;

        public LogRequestTask(IGenericRepository<CommandContext> repository,
                              IRabbitMQConsumerBulk rabbitMQConsumer)
        {
            _repository = repository;
            _rabbitMQConsumer = rabbitMQConsumer;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{context.JobDetail.Key.Name} - {DateTime.Now.ToString("HH:mm:ss")}");

            while (true)
            {
                try
                {
                    var logs = await _rabbitMQConsumer.GetMessages<LogRequest>("LogRequest", 10);

                    if (logs.Any())
                    {
                        foreach (var log in logs)
                        {
                            _repository.ExecuteStoredProcedure<LogRequest, LogRequest>(log, "SetLogRequest");
                        }

                        _rabbitMQConsumer.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Erro ao salvar log");
                }

                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }
        public bool CanExecute()
        {
            return true;
        }
    }
}
