using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;
using System;
using System.Runtime.CompilerServices;

namespace Fretter.Service.Tasks
{
    class JobTaskFactory : SimpleJobFactory
    {
        private ConditionalWeakTable<IJob, IServiceScope> scopes;
        private readonly IServiceProvider _container;

        public JobTaskFactory(IServiceProvider container)
        {
            _container = container;
            scopes = new ConditionalWeakTable<IJob, IServiceScope>();
        }

        //public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) => (IJob)_container.GetService(bundle.JobDetail.JobType);

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = _container.GetService<IServiceScopeFactory>().CreateScope();
            var job = (IJob)scope.ServiceProvider.GetService(bundle.JobDetail.JobType);

            scopes.Add(job, scope);
            return job;
        }

        public override void ReturnJob(IJob job)
        {
            //destroy job
            if (scopes.TryGetValue(job, out var scope))
            {
                try
                {
                    scope.Dispose();
                }
                catch
                {
                    // ignorar ou logar erros no dispose
                }
            }

            base.ReturnJob(job);
        }
    }
}
