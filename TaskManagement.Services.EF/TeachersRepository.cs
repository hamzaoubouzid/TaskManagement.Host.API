using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTO;
using TaskManagement.Services.Contract;
using TaskManagement.Services.EF.Context;

namespace TaskManagement.Services.EF
{
    public class TeachersRepository : ITeachersRepository
    {

        private readonly TaskManagmentAppContext _TaskManagmentAppContext;

        public TeachersRepository(TaskManagmentAppContext TaskManagmentAppContext)
        {
            _TaskManagmentAppContext = TaskManagmentAppContext;
        }

        public bool AddTeachers(Teachers Teachers)
        {
            try
            {
                var CheckExistTeachers = _TaskManagmentAppContext.Teachers.FirstOrDefault(m => m.Id == Teachers.Id);

                if (CheckExistTeachers == null)
                {
                    _TaskManagmentAppContext.Teachers.Add(Teachers);
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

        public bool DeleteTeachers(Guid Id)
        {
            try
            {
                var Teachers = _TaskManagmentAppContext.Teachers.Find(Id);

                if (Teachers != null)
                {
                    _TaskManagmentAppContext.Teachers.Remove(Teachers);
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

        public List<Teachers> GetAllTeachers()
        {
            return _TaskManagmentAppContext.Teachers.ToList();
        }

        public Teachers GetTeachersById(Guid Id)
        {
            try
            {
                var TeachersDetails = _TaskManagmentAppContext.Teachers.Find(Id);

                if (TeachersDetails != null)
                    return TeachersDetails;

                return new Teachers();

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

        public bool UpdateTeachers(Teachers Teachers)
        {
            try
            {
                var teacher = _TaskManagmentAppContext.Teachers.Find(Teachers.Id);

                if (!string.IsNullOrEmpty(teacher.Name) || 
                    !string.IsNullOrEmpty(teacher.MainSubjectTeaching))
                {
                    teacher.Id = Teachers.Id;
                    teacher.Name = Teachers.Name;
                    teacher.BirthDate = Teachers.BirthDate;
                    teacher.MainSubjectTeaching = Teachers.MainSubjectTeaching;

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
    }
}
