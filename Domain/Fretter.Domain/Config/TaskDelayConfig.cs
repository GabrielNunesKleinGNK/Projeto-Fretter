using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Config
{
    public class TaskDelayConfig
    {
        public TaskDelayConfig()
        {

        }
        public List<TaskDelayItemConfig> TasksExecution { get; set; }
        public class TaskDelayItemConfig
        {
            public string TaskName { get; set; }
            public int TaskDelay { get; set; } //Minutes
        }
    }
}
