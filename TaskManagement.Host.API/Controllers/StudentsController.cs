using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using TaskManagement.DTO;
using TaskManagement.Services.Contract;

namespace TaskManagement.Host.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMemoryCache _memoryCacheStudents;
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentsRepository _IStudentsRepository;
        private readonly string StudentCollectionKey = "studentCollectionKey";
        public StudentsController(ILogger<StudentsController> logger, 
                                  IStudentsRepository IStudentsRepository , IMemoryCache memoryCacheStudents)
        {
            _logger = logger;
            _IStudentsRepository = IStudentsRepository;
            _memoryCacheStudents = memoryCacheStudents;
        }

        [Route("GetAllStudents")]
        [HttpGet]
        public List<Students> GetAllStudents()
        {
            List<Students> liststudent = null;

            if (_memoryCacheStudents.TryGetValue(StudentCollectionKey, out liststudent))
            {
                return liststudent;
            }

            liststudent  = _IStudentsRepository.GetAllStudents();
            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
            _memoryCacheStudents.Set(StudentCollectionKey, liststudent, cacheOptions);
            return liststudent;
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
