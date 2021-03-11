namespace arkopongBack
{
    public class Room
    {
        private string[] players = new string[] { null, null };

        public bool Connect(string ConnectionId)
        {
            ChangeConnectionState(null, ConnectionId);
            return true;
        }

        public bool isUserConnected(string ConnectionId)
        {
            foreach (var player in players)
            {
                if (player == ConnectionId) return true;
            }

            return false;
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

        public bool CanStart()
        {
            foreach (var player in players)
            {
                if (player == null) return false;
            }
            return true;
        }
    }
}
