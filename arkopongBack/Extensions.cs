using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace arkopongBack
{
    public static class Extensions
    {
        public static Task SendLogMsg(this IClientProxy client, string msg)
        {
            return client.SendAsync("OutsideLog", msg);
        }
    }
}
