using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTO;
using TaskManagement.Notifier;
using TaskManagement.Services.Contract;
using TaskManagement.Services.EF.Context;

namespace TaskManagement.Services.EF
{
    public class TaskRepository : ITaskRepository
    {

        private readonly TaskManagmentAppContext _TaskManagmentAppContext;
        private readonly ITaskManagmentNotifier _ITaskManagmentNotifier;
        public string MessageTaskExpried = "TASK COMPLETED";
        public string MessageTaskExcuted = "TASK IN PROGRESS";

        public TaskRepository(TaskManagmentAppContext TaskManagmentAppContext, ITaskManagmentNotifier ITaskManagmentNotifier)
        {
            _TaskManagmentAppContext = TaskManagmentAppContext;
            _ITaskManagmentNotifier = ITaskManagmentNotifier;
        }
        public bool AddTasks(Tasks tasks)
        {
            try
            {
                var CheckExistStudents = _TaskManagmentAppContext.Tasks.FirstOrDefault(m => m.Id == tasks.Id);

                if (CheckExistStudents == null)
                {
                    _TaskManagmentAppContext.Tasks.Add(tasks);
                    Save();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<TaskExecution> GetAllExecutionTask()
        {
            try
            {
                List<TaskExecution> TaskExecution = new List<TaskExecution>();

                var Resulte = _TaskManagmentAppContext.TaskExecution.Join(_TaskManagmentAppContext.Tasks,
                              ts => ts.Task.Id,
                              tse => tse.Id,
                              (ts, tse) => new { ts.IdTask, ts.TaskStartDate, ts.TaskEndDate, ts.Task.Name });

                foreach (var item in Resulte)
                {
                    TaskExecution.Add(new TaskExecution()
                    {
                        IdTask = item.IdTask,
                        TaskStartDate = item.TaskStartDate,
                        TaskEndDate = item.TaskEndDate,
                        StatusTask = item.TaskEndDate == null ? "TASK IN PROGRESS " : "TASK COMPLETED",
                        Task = new Tasks
                        {
                            Name = item.Name
                        }
                    });
                }

                return TaskExecution;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Tasks> GetAllTask()
        {
            try
            {
                return _TaskManagmentAppContext.Tasks.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public   bool ExcuteTask(Guid id)
        {

            try
            {
 
                TaskExecution taskex = new TaskExecution();
                var CheckTaskExOrNot = _TaskManagmentAppContext.TaskExecution.Where(x => x.Task.Id == id).Count();

                var FindTask = _TaskManagmentAppContext.Tasks.Find(id);

                if (CheckTaskExOrNot  == 0)
                {
                    taskex.IdTask = Guid.NewGuid();
                    taskex.TaskStartDate= DateTime.Now;
                    taskex.TaskEndDate =  null;

                    taskex.Task = new Tasks {
                        Id= Guid.NewGuid(),
                        Name = FindTask.Name,
                        ActionType = FindTask.ActionType,
                        TableName = FindTask.TableName
                    };
                    taskex.StatusTask = null;
                    _TaskManagmentAppContext.TaskExecution.Add(taskex);
                    Save();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Save()
        {
            _TaskManagmentAppContext.SaveChanges();
        }
    }
}
