using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Helper;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Service.Tasks
{
    [DisallowConcurrentExecution]
    class ProcessarPedidoPendenteTransportadorTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IPedidoPendenteTransportadorApplication<FretterContext> _application;

        public ProcessarPedidoPendenteTransportadorTask(ILogHelper logHelper, IPedidoPendenteTransportadorApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            this._application = application;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                int quantidade = await _application.ProcessarPedidoPendenteTransportador();
                _logHelper.LogInfo(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds, ex);
            }

            watch.Stop();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
