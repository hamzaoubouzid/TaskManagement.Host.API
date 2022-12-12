using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Scheduler
{
    public interface ITaskScheduler
    {
        public  Task Run(CancellationToken stoppingToken,Action action);
        
    }
}
