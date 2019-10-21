using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Common
{
    public class Proxy : IProxy
    {
        public static string ProxyPipeName => nameof(ProxyPipeName);

        public Func<ServerInfo> GetServerInfoFunc { get; set; }

        public Task<bool> CheckConnection()
        {
            return Task.FromResult(true);
        }

        public Task<string> GetHelloMessage(string name)
        {
            return Task.FromResult($"Hello {name}!");
        }

        public Task<ServerInfo> GetServerInfo()
        {
            return Task.FromResult(GetServerInfoFunc());
        }
    }
}