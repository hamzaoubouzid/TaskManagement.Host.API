using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTO;

namespace TaskManagement.Services.EF.Context
{
    public  class TaskManagmentAppContext:DbContext
    {

        private readonly DbContextOptions<TaskManagmentAppContext> _options;

        public TaskManagmentAppContext(DbContextOptions<TaskManagmentAppContext> options) 
            : base(options)
        {
            _options = options;
        }


        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TaskExecution> TaskExecution { get; set; }
        public DbSet<Students> Students { get; set; }
    }
    
}
