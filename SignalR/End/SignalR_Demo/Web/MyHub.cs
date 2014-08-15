using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Web
{
    public class MyHub : Hub
    {
        public void ServerFunc(string serverParam)
        {
            Clients.All.ClientFunc(serverParam + ", back from the server.");
        }
    }
}