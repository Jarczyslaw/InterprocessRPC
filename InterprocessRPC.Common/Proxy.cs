using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace InterprocessRPC.Common
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Proxy : IProxy
    {
        public static string ProxyPipeName => nameof(ProxyPipeName);

        public Task<bool> CheckConnection()
        {
            return Task.FromResult(true);
        }

        public Task<string> GetHelloMessage(string name)
        {
            return Task.FromResult($"Hello {name}!");
        }

        public Task<DateTime> GetServerTime()
        {
            return Task.FromResult(DateTime.Now);
        }
    }
}