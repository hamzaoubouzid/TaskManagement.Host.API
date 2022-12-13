using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TaskManagement.DTO;
using TaskManagement.Services.Contract;

namespace TaskManagement.Host.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IMemoryCache _memoryCacheTeachers;
        private readonly ILogger<TeachersController> _logger;
        private readonly ITeachersRepository _ITeachersRepository;

        public TeachersController(ILogger<TeachersController> logger, 
                                    ITeachersRepository ITeachersRepository,
                                    IMemoryCache memoryCacheTeachers)
        {
            _logger = logger;
            _ITeachersRepository = ITeachersRepository;
            _memoryCacheTeachers = memoryCacheTeachers;
        }

        [Route("GetAllTeachers")]
        [HttpGet]
        public List<Teachers> GetAllTeachers()
        {
            return _ITeachersRepository.GetAllTeachers();
        }

        [HttpDelete("DeleteTeachers/{id}")]
        public bool DeleteTeachers(Guid Id)
        {
            return _ITeachersRepository.DeleteTeachers(Id);
        }

        [Route("UpdateTeachers")]
        [HttpPut]
        public bool UpdateTeachers([FromBody] Teachers teachers)
        {
            return _ITeachersRepository.UpdateTeachers(teachers);
        }

        [Route("AddTeachers")]
        [HttpPost]
        public bool AddTeachers([FromBody] Teachers teachers)
        {
            return _ITeachersRepository.AddTeachers(teachers);
        }

        [Route("GetTeachersById/{id}")]
        [HttpGet]
        public Teachers GetTeachersById(Guid Id)
        {
            return _ITeachersRepository.GetTeachersById(Id);
        }
    }
}
