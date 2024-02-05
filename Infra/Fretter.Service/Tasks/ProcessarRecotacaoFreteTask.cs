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
    class ProcessarRecotacaoFreteTask : IJob, IJobTask
    {
        private readonly IRecotacaoFreteApplication<FretterContext> _application;

        public ProcessarRecotacaoFreteTask(IRecotacaoFreteApplication<FretterContext> application)
        {
            this._application = application;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Task - {context.JobDetail.Key.Name} Inicializado - {DateTime.Now.ToString("HH:mm:ss")}");
            int quantidade = _application.ProcessarArquivoRecotacaoAsync();
            Console.WriteLine($"Task - {context.JobDetail.Key.Name} Finalizadoc- {DateTime.Now.ToString("HH:mm:ss")} {quantidade} processados");
            return Task.FromResult(0);
        }
        public bool CanExecute()
        {
            return true;
        }
    }
}
