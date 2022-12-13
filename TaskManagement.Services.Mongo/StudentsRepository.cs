using Microsoft.Extensions.Options;
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
    public class StudentsRepository : IStudentsRepository
    {
        private readonly IMongoCollection<Students> _IStudentsRepository;

        public StudentsRepository(IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);
            _IStudentsRepository = mongoDatabase.GetCollection<Students>("Students");
        }
        public bool AddStudent(Students students)
        {
            try
            {
                Task task = _IStudentsRepository.InsertOneAsync(students);
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
        public bool DeleteStudent(Guid Id)
        {
            try
            {
                Task task = _IStudentsRepository.DeleteManyAsync(Id.ToString());
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
        public List<Students> GetAllStudents()
        {
           return   _IStudentsRepository.Find(_ => true).ToList();
        } 
        public Students GetStudentById(Guid Id)
        {
             return  _IStudentsRepository.Find(x => x.Id == Id).FirstOrDefault();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
        public bool UpdateStudent(Students students)
        {
            try
            {
                _IStudentsRepository.ReplaceOne(x => x.Id == students.Id, students);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
    }
}
