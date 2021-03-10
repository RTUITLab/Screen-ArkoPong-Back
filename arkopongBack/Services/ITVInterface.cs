using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace arkopongBack.Services
{
    public interface ITVInterface
    {
        public string tvConnectionId
        {
            get;
            set;
        }

        public int GetPlayerID(string ConnectionId);

        public void Connect(string ConnectionId);

        public void Disconnect(string ConnectionId);

        public bool CanConnect();
    }
}
