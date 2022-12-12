
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Services.EF.Context;

namespace TaskManagement.Business
{
    public class TaskManagementService<T> : ITaskManagementService<T> where T : class
    {
        private readonly TaskManagmentAppContext _TaskManagmentAppContext;
        public TaskManagementService(TaskManagmentAppContext TaskManagmentAppContext)
        {
            _TaskManagmentAppContext = TaskManagmentAppContext;
        }
        public void AddRandomDataTask(T item)
        {
            // faker data 
            var repository = Builder<T>.CreateListOfSize(500).Build();
            _TaskManagmentAppContext.Set<T>().AddRange(repository.ToArray());
        }

        public void DeleteAllDataTask(T item)
        {
            DbSet<T> dbSet = _TaskManagmentAppContext.Set<T>();
            dbSet.RemoveRange(dbSet.ToList());
        }
    }
}
