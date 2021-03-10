using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arkopongBack.Services
{
    public class TVInterface : ITVInterface
    {
        private string[] players = new string[]{null, null};
        private string _tvConnectionId = null;
        public string tvConnectionId
        {
            get
            {
                return _tvConnectionId;
            }
            set
            {
                _tvConnectionId = value;
            }
        }

        public bool CanConnect()
        {
            if (_tvConnectionId == null) return false;
            foreach (var player in players)
            {
                if (player == null) return true;
            }
            return false;
        }

        public void Connect(string ConnectionId)
        {
            ChangeConnectionState(null, ConnectionId);
        }

        public void Disconnect(string ConnectionId)
        {
            if (tvConnectionId == ConnectionId)
            {
                tvConnectionId = null;
                return;
            }
            ChangeConnectionState(ConnectionId, null);
        }

        private void ChangeConnectionState(string from, string to)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i] == from)
                {
                    players[i] = to;
                    return;
                }
            }
        }

        public int GetPlayerID(string ConnectionId)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i] == ConnectionId) return i;
            }

            return -1;
        }

    }
}
