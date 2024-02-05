using System;
using Microsoft.Extensions.Configuration;
using Quartz;
using static Fretter.Domain.Config.TaskDelayConfig;
using System.Collections.Generic;
using Fretter.Service.Infra.IoC;

namespace Fretter.Service.Infra.Host
{
    public class ServiceConfiguration
    {
        private readonly IServiceProvider _provider;
        private readonly IScheduler _scheduler;
        private readonly string _task;
        private readonly List<TaskDelayItemConfig> _tasksDelay;

        public ServiceConfiguration(IConfiguration config, IScheduler scheduler)
        {
            _provider = Container.Register(config);
            _scheduler = scheduler;
            _scheduler.JobFactory = new JobFactory(_provider);
            _task = config.GetSection("TaskConfig").Value;
            _tasksDelay = config.GetSection("TaskDelayConfig").Get<List<TaskDelayItemConfig>>();
        }

        public bool Start()
        {
            ServiceRunner runner = new ServiceRunner(_scheduler, _task, _tasksDelay);
            return runner.Start();
        }
    }
}
