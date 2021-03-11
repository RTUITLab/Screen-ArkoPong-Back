using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arkopongBack.Services
{
    public class TVInterface : ITVInterface
    {
        private Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        public void CreateRoom(string ConnectionId)
        {
            Room newRoom = new Room();
            rooms.Add(ConnectionId, newRoom);
        }

        public bool ConnectTo(string ClientConnectionId, string tvConnectionId)
        {
            if (rooms.ContainsKey(tvConnectionId) && rooms[tvConnectionId].Connect(ClientConnectionId))
            {
                return true;
            }
            return false;
        }

        public string Disconnect(string ConnectionId)
        {
            if (rooms.ContainsKey(ConnectionId))
            {
                rooms.Remove(ConnectionId);
                return ConnectionId;
            }
            else
            {
                foreach (var room in rooms)     //Если кто то отключается, то комната тоже удаляется.
                {
                    if (room.Value.isUserConnected(ConnectionId))
                    {
                        rooms.Remove(room.Key);
                        return room.Key;
                    }
                }
            }

            return null;
        }

        public int GetPlayerIDFrom(string ConnectionId, string tvConnectionId)
        {
            return rooms[tvConnectionId].GetPlayerID(ConnectionId);
        }

        public string WhereClient(string ConnectionId)
        {
            foreach (var room in rooms)
            {
                if (room.Value.isUserConnected(ConnectionId))
                {
                    return room.Key;
                }
            }

            return null;
        }

        public bool isRoomReady(string tvConnectionId)
        {
            return rooms[tvConnectionId].CanStart();
        }
    }
}
