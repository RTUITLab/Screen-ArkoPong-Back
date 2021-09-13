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
            Console.WriteLine($"New TV connection. ID:[{Context.ConnectionId}];");
            _tvInterface.CreateRoom(Context.ConnectionId);
            return Clients.Caller.SendAsync("SetID", Context.ConnectionId);
        }

        public Task ConnectClient(string tvID)
        {
            if (!string.IsNullOrEmpty(tvID) && _tvInterface.ConnectTo(Context.ConnectionId, tvID))
            {
                Console.WriteLine($"New client connection. Client ID:[{Context.ConnectionId}]. TV ID:[{tvID}];");
                Clients.Client(tvID).SendAsync("PlayerJoin");
                if (_tvInterface.GetPlayersCount(tvID) == 2)
                {
                    Clients.Client(tvID).SendAsync("StartGame");
                }
                return Clients.Caller.SendAsync("Connected");
            }
            Console.WriteLine($"Сonnection rejected. Rejected ID:[{Context.ConnectionId}];");
            return Clients.Caller.SendLogMsg("Сonnection rejected;");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            if (_tvInterface.isTV(Context.ConnectionId))
            {
                foreach (var player in _tvInterface.GetPlayers(Context.ConnectionId))
                {
                    if (player != null)
                    {
                        await Clients.Client(player).SendAsync("Disconnect");
                    }
                }
                Console.WriteLine($"Disconnected TV ID:[{Context.ConnectionId}];");
            }
            else
            {
                byte playersCount = 0;
                string tvID = _tvInterface.WhereClient(Context.ConnectionId);
                foreach (var player in _tvInterface.GetPlayers(tvID))
                {
                    if (player != null) ++playersCount;
                }
                await Clients.Client(tvID).SendAsync((playersCount == 1) ? "StopGame" : "PauseGame");
                Console.WriteLine($"Disconnected client ID: {Context.ConnectionId};");
            }
            _tvInterface.Disconnect(Context.ConnectionId);
        }

        public Task SendDirection(float direction, string tvConnectionId)
        {
            int fromID = _tvInterface.GetPlayerIDFrom(Context.ConnectionId, tvConnectionId);
            Console.WriteLine($"Direction [{direction}] received from ID:[{fromID}];");
            if (fromID != -1)
            {
                return Clients.Client(tvConnectionId).SendAsync("SetDirection", fromID, direction);
            }
            return null;
        }
    }
}
