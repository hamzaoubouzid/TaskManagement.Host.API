using Microsoft.EntityFrameworkCore;
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
    public class StudentsRepository : IStudentsRepository
    {
        private readonly TaskManagmentAppContext _TaskManagmentAppContext;

        public StudentsRepository(TaskManagmentAppContext TaskManagmentAppContext)
        {
            _TaskManagmentAppContext = TaskManagmentAppContext;
        }
        public bool AddStudent(Students students)
        {
            try
            {
                var CheckExistStudents = _TaskManagmentAppContext.Students.FirstOrDefault(m => m.Id == students.Id);

                if (CheckExistStudents == null)
                {
                    _TaskManagmentAppContext.Students.Add(students);
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
        public bool DeleteStudent(Guid Id)
        {
            try
            {
                var Student = _TaskManagmentAppContext.Students.Find(Id);

                if (Student != null)
                {
                    _TaskManagmentAppContext.Students.Remove(Student);
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
        public List<Students> GetAllStudents()
        {
            return _TaskManagmentAppContext.Students.ToList();
        }
        public bool UpdateStudent(Students students)
        {
            try
            {
                var Student = _TaskManagmentAppContext.Students.Find(students.Id);

                if (!string.IsNullOrEmpty(Student.Name) || Student.YearOfStudy != 0)
                {
                    Student.Id = students.Id;
                    Student.Name = students.Name;
                    Student.BirthDate = students.BirthDate;
                    Student.YearOfStudy = students.YearOfStudy;
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
        public Students GetStudentById(Guid Id)
        {
            try
            {
                var StudentsDetails = _TaskManagmentAppContext.Students.Find(Id);

                if (StudentsDetails != null)
                    return StudentsDetails;

                return new Students();

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
