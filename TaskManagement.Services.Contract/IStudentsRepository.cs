using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTO;

namespace TaskManagement.Services.Contract
{
    public interface IStudentsRepository
    {
        List<Students> GetAllStudents();
        bool AddStudent(Students students);
        bool DeleteStudent(Guid Id);
        Students GetStudentById(Guid Id);
        bool UpdateStudent(Students students);
        void Save();
    }
}
