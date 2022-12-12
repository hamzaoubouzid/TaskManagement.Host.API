using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TaskManagement.DTO;
using TaskManagement.Services.Contract;

namespace TaskManagement.Host.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentsRepository _IStudentsRepository;
        public StudentsController(ILogger<StudentsController> logger, 
                                  IStudentsRepository IStudentsRepository)
        {
            _logger = logger;
            _IStudentsRepository = IStudentsRepository;
        }

        [Route("GetAllStudents")]
        [HttpGet]
        public List<Students> GetAllStudents()
        {
            return _IStudentsRepository.GetAllStudents();
        }

        [HttpDelete("DeleteStudients/{id}")]
        public bool DeleteStudients(Guid Id)
        {
            return _IStudentsRepository.DeleteStudent(Id);
        }

        [Route("UpDateStudients")]
        [HttpPut]
        public bool UpDateStudients([FromBody] Students students)
        {
            return _IStudentsRepository.UpdateStudent(students);
        }

        [Route("AddStudients")]
        [HttpPost]
        public bool AddStudients([FromBody] Students students)
        {
            return _IStudentsRepository.AddStudent(students);
        }

        [Route("DetailsStudents/{id}")]
        [HttpGet]
        public Students DetailsStudents(Guid Id)
        {
            return _IStudentsRepository.GetStudentById(Id);
        }
    }
}
