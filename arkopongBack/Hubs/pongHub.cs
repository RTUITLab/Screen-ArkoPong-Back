using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using arkopongBack.Services;
using Microsoft.AspNetCore.Components;

namespace arkopongBack.Hubs
{
    public class pongHub : Hub
    {
        private readonly ITVInterface _tvInterface;

        public pongHub(ITVInterface tvInterface)
        {
            _tvInterface = tvInterface;
        }

        public Task ConnectTV()
        {
            if (_tvInterface.tvConnectionId != null)
            {
                return SendLogMsg("the TV is already connected", Clients.Caller);
            }
            _tvInterface.tvConnectionId = Context.ConnectionId;
            //Context.ConnectionAborted.Register(() => );
            return Groups.AddToGroupAsync(Context.ConnectionId, "tv");
        }

        public Task Disconnect()
        {
            _tvInterface.Disconnect(Context.ConnectionId);
            Context.Abort();
            return null;
        }

        public Task Connect()
        {
            Console.WriteLine(Context.ConnectionId);
            if (_tvInterface.CanConnect())
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "players");
                _tvInterface.Connect(Context.ConnectionId);
                return Clients.Caller.SendAsync("Connected");
            }
            return SendLogMsg("Can't connect", Clients.Caller);
        }

        public Task SendDirection(int direction)
        {
            int fromID = _tvInterface.GetPlayerID(Context.ConnectionId);
            SendMsg(fromID.ToString());
            if (fromID != -1)
            {
                return Clients.Client(_tvInterface.tvConnectionId).SendAsync("SetDirection", fromID, direction);
            }
            return null;
        }

        public Task SendMsg(string msg)
        {
            return SendLogMsg(msg, Clients.All);
        }

        private Task SendLogMsg(string msg, IClientProxy client)
        {
            return client.SendAsync("OutsideLog", msg);
        }
    }
}
