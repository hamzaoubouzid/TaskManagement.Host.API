using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Business
{
    public interface ITaskManagementService<T> where T : class
    {
        void AddRandomDataTask(T items);
        void DeleteAllDataTask(T items);
         
    }
}
