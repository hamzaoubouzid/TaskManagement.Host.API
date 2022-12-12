
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Notifier
{
    public interface ITaskManagmentNotifier
    {
        Task SendMessageToUser(string user, string message);
    }
     
}
