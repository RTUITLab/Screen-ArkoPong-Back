using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using arkopongBack.Services;

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
            Console.WriteLine($"TV id: {Context.ConnectionId}");
            _tvInterface.CreateRoom(Context.ConnectionId);
            Groups.AddToGroupAsync(Context.ConnectionId, "tv");
            return Clients.Caller.SendAsync("SetID", Context.ConnectionId);
        }

        public Task Disconnect()
        {
            string connectedTv = _tvInterface.Disconnect(Context.ConnectionId);
            if (connectedTv != null)
            {
                Clients.Client(connectedTv).SendAsync("StopGame");
            }
            else
            {
                Clients.Caller.SendLogMsg("No room with this user or ID found");
            }
            return null;
        }

        public Task Connect(string tvID)
        {
            if (tvID != "" && _tvInterface.ConnectTo(Context.ConnectionId, tvID))
            {
                Console.WriteLine($"Client id: {Context.ConnectionId}, connect to tv id: {tvID}");
                Groups.AddToGroupAsync(Context.ConnectionId, "players");
                if (_tvInterface.isRoomReady(tvID))
                {
                    Clients.Client(tvID).SendAsync("StartGame");
                }
                return Clients.Caller.SendAsync("Connected");
            }
            Console.WriteLine("connection attempt without tvID");
            return Clients.Caller.SendLogMsg("Сonnection rejected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"Disconnected: {Context.ConnectionId}");
            string tvId = _tvInterface.WhereClient(Context.ConnectionId);
            if (tvId != null)
            {
                await Clients.Client(_tvInterface.WhereClient(Context.ConnectionId)).SendAsync("StopGame");
                _tvInterface.Disconnect(Context.ConnectionId);
            }
        }

        public Task SendDirection(float direction, string tvConnectionId)
        {
            int fromID = _tvInterface.GetPlayerIDFrom(Context.ConnectionId, tvConnectionId);
            if (fromID != -1)
            {
                return Clients.Client(tvConnectionId).SendAsync("SetDirection", fromID, direction);
            }
            return null;
        }
    }
}
