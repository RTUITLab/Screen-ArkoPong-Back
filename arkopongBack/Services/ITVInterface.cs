namespace arkopongBack.Services
{
    public interface ITVInterface
    {

        public int GetPlayerIDFrom(string ConnectionId, string tvConnectionId);

        public bool ConnectTo(string ClientConnectionId, string tvConnectionId);

        public string Disconnect(string ConnectionId);

        public void CreateRoom(string ConnectionId);

        public string WhereClient(string ConnectionId);

    }
}
