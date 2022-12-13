using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTO;
using TaskManagement.Services.Contract;

namespace TaskManagement.Services.Mongo
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<Tasks> _ITaskRepository;
        private readonly IMongoCollection<TaskExecution> _ITaskExcurtionRepository;

        public TaskRepository(IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);
            _ITaskRepository = mongoDatabase.GetCollection<Tasks>("Tasks");
            _ITaskExcurtionRepository = mongoDatabase.GetCollection<TaskExecution>("TaskExecution");
        }
        public bool AddTasks(Tasks tasks)
        {
            try
            {
                Task task = _ITaskRepository.InsertOneAsync(tasks);
                if (task.IsCompleted)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public bool ExcuteTask(Guid id)
        {
            try
            {
                Tasks task = (Tasks)_ITaskRepository.Find(id.ToString());

                TaskExecution taskex = new TaskExecution()
                {
                    IdTask = Guid.NewGuid(),
                    StatusTask = "",
                    TaskStartDate = DateTime.Now,
                    TaskEndDate = null,
                    Task = new Tasks
                    {
                        Id = Guid.NewGuid(),
                        Name = task.Name,
                        ActionType = task.ActionType,
                        TableName = task.TableName
                    }
                };

                _ITaskExcurtionRepository.InsertOneAsync(taskex);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<TaskExecution> GetAllExecutionTask()
        {
            return _ITaskExcurtionRepository.Find(_ => true).ToList();
        }
        public List<Tasks> GetAllTask()
        {
            return _ITaskRepository.Find(_ => true).ToList();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
