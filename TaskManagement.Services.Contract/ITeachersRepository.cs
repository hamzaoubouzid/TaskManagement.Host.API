using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTO;

namespace TaskManagement.Services.Contract
{
    public interface ITeachersRepository
    {
        List<Teachers> GetAllTeachers();
        bool AddTeachers(Teachers Teachers);
        bool DeleteTeachers(Guid Id);
        bool UpdateTeachers(Teachers Teachers);
        Teachers GetTeachersById(Guid Id);
        void Save();
    }
}
