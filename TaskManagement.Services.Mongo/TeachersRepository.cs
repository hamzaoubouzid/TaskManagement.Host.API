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
    public class TeachersRepository : ITeachersRepository
    {

        private readonly IMongoCollection<Teachers> _ITeachersRepository;

        public TeachersRepository(IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);

            _ITeachersRepository = mongoDatabase.GetCollection<Teachers>("Teachers");
        }
        public bool AddTeachers(Teachers Teachers)
        {
            try
            {
                Task task = _ITeachersRepository.InsertOneAsync(Teachers);
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
        public bool DeleteTeachers(Guid Id)
        {
            try
            {
                Task task = _ITeachersRepository.DeleteManyAsync(Id.ToString());
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
        public List<Teachers> GetAllTeachers()
        {
            return _ITeachersRepository.Find(_ => true).ToList();
        }
        public Teachers GetTeachersById(Guid Id)
        {
            return _ITeachersRepository.Find(x => x.Id == Id).FirstOrDefault();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
        public bool UpdateTeachers(Teachers Teachers)
        {
            try
            {
                _ITeachersRepository.ReplaceOne(x => x.Id == Teachers.Id, Teachers);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
