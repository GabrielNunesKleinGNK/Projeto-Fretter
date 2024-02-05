using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Service.Tasks
{
    [DisallowConcurrentExecution]
    class ProcessarOcorrenciaArquivoTask : IJob, IJobTask
    {
        private readonly IOcorrenciaArquivoApplication<FretterContext> _application;

        public ProcessarOcorrenciaArquivoTask(IOcorrenciaArquivoApplication<FretterContext> application)
        {
            this._application = application;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Task - {context.JobDetail.Key.Name} Inicializado - {DateTime.Now.ToString("HH:mm:ss")}");
            int quantidade = _application.ProcessarOcorrenciaArquivo();
            Console.WriteLine($"Task - {context.JobDetail.Key.Name} Finalizadoc- {DateTime.Now.ToString("HH:mm:ss")} {quantidade} processados");
            return Task.FromResult(0);
        }
        public bool CanExecute()
        {
            return true;
        }
    }
}
