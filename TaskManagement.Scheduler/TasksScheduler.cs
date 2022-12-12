using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Business;
using TaskManagement.DTO;

namespace TaskManagement.Scheduler
{
    public class TasksScheduler : ITaskScheduler
    { 
        public event EventHandler  TaskSchedulerCreated; 
        public event EventHandler TaskSchedulerCompleted;
        private readonly PeriodicTimer _timer;

        public async Task Run(CancellationToken stoppingToken,Action action)
        {

            while (_timer.Equals(stoppingToken)
            && !stoppingToken.IsCancellationRequested)
            {
                await Task.Run(action);

                Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        protected virtual void OnTaskSchedulerCreated(Tasks tasks)
        {
            if (TaskSchedulerCreated != null)
                TaskSchedulerCreated(this,null);
        }

        protected virtual void OnTaskSchedulerCompleted(Tasks tasks)
        {
            if (TaskSchedulerCompleted != null)
                TaskSchedulerCompleted(this, null);
        }

    }
}
