using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTO;

namespace TaskManagement.Services.Contract
{
    public interface ITaskRepository
    {
        List<Tasks> GetAllTask();
        List<TaskExecution> GetAllExecutionTask();
        bool AddTasks(Tasks tasks);
        bool ExcuteTask(Guid id);
        void Save();
    }
}
