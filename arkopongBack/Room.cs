namespace arkopongBack
{
    public class Room
    {
        private string[] players = new string[] { null, null };

        public bool Connect(string ConnectionId)
        {
            return ChangeConnectionState(null, ConnectionId);
        }

        public bool isUserConnected(string ConnectionId)
        {
            foreach (var player in players)
            {
                if (player == ConnectionId) return true;
            }

            return false;
        }

        private bool ChangeConnectionState(string from, string to)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i] == from)
                {
                    players[i] = to;
                    return true;
                }
            }
            return false;
        }

        public int GetPlayerID(string ConnectionId)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i] == ConnectionId) return i;
            }

            return -1;
        }

        public void RemoveUser(string ConnectionId)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i] == ConnectionId)
                {
                    players[i] = null;
                }
            }
        }

        public string[] GetPlayers()
        {
            return players;
        }
    }
}
