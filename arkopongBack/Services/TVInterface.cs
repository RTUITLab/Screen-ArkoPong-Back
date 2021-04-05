using System.Collections.Generic;

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
                foreach (var room in rooms)
                {
                    if (room.Value.isUserConnected(ConnectionId))
                    {
                        room.Value.RemoveUser(ConnectionId);
                        return room.Key;
                    }
                }
            }

            return null;
        }

        public bool isTV(string ConnectionId)
        {
            return rooms.ContainsKey(ConnectionId);
        }

        public int GetPlayerIDFrom(string ConnectionId, string tvConnectionId)
        {
            if(!rooms.ContainsKey(tvConnectionId)) return -1;
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

        public string[] GetPlayers(string tvConnectionId)
        {
            return rooms[tvConnectionId].GetPlayers();
        }

        public byte GetPlayersCount(string tvConnectionId)
        {
            byte res = 0;
            foreach (var player in rooms[tvConnectionId].GetPlayers())
            {
                if (player != null) ++res;
            }

            return res;
        }
    }
}
