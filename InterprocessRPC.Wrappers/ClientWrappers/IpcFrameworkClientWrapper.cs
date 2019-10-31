using InterprocessRPC.Common;
using JKang.IpcServiceFramework;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ClientWrappers
{
    public class IpcFrameworkClientWrapper : IClientWrapper
    {
        private IpcServiceClient<IProxy> client;

        public Task<bool> CheckConnection()
        {
            return client.InvokeAsync(p => p.CheckConnection());
        }

        public Task<string> GetHelloMessage(string name)
        {
            return client.InvokeAsync(p => p.GetHelloMessage(name));
        }

        public Task<DateTime> GetServerTime()
        {
            return client.InvokeAsync(p => p.GetServerTime());
        }

        public Task Start()
        {
            client = new IpcServiceClientBuilder<IProxy>()
                .UseNamedPipe("ipcFrameworkPipeName")
                .Build();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            return Task.CompletedTask;
        }
    }
}