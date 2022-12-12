using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace TaskManagement.Notifier
{
    public class TaskManagementNotifier :Hub,ITaskManagmentNotifier
    {
        public async Task SendMessageToUser(string user, string message)
        {
            await Clients.All.SendToSpecificUser(user, message);
        }
    }
}
