using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Business;
using TaskManagement.DTO;
using TaskManagement.Notifier;
using TaskManagement.Services.Contract;

namespace TaskManagement.Host.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _ITaskRepository;
        private readonly ITaskManagmentNotifier _ITaskManagmentNotifier;

        public TaskController(ILogger<TaskController> logger, 
                              ITaskRepository ITaskRepository ,
                              ITaskManagmentNotifier ITaskManagmentNotifier)
        {
            _logger = logger;
            _ITaskRepository = ITaskRepository;
            _ITaskManagmentNotifier = ITaskManagmentNotifier;
        }


        [Route("GetAllTask")]
        [HttpGet]
        public List<Tasks> GetAllTask()
        {
            return _ITaskRepository.GetAllTask();
        }

        [Route("GetAllExecutionTask")]
        [HttpGet]
        public List<TaskExecution> GetAllExecutionTask()
        {
            return _ITaskRepository.GetAllExecutionTask();
        }

        [Route("AddTasks")]
        [HttpPost]
        public bool AddTasks([FromBody]  Tasks tasks)
        {
            return _ITaskRepository.AddTasks(tasks);
        }

        [Route("TaskExcution")]
        [HttpPost]
        public bool TaskExcution([FromBody] Tasks tasks)
        {
            return _ITaskRepository.ExcuteTask(tasks.Id);
        }

    }
}
