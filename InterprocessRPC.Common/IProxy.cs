using System.Threading.Tasks;

namespace InterprocessRPC.Common
{
    public interface IProxy
    {
        Task<bool> CheckConnection();

        Task<string> GetHelloMessage(string name);

        Task<ServerInfo> GetServerInfo();
    }
}