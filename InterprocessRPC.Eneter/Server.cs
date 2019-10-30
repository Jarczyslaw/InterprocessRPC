using Eneter.Messaging.EndPoints.Rpc;
using Eneter.Messaging.MessagingSystems.NamedPipeMessagingSystem;
using System;

namespace InterprocessRPC.Eneter
{
    public class Server<TProxy> : IDisposable
        where TProxy : class
    {
        private readonly IRpcFactory rcpFactory = new RpcFactory();

        public Server(Func<TProxy> proxyFactoryFunc)
        {
            RpcService = rcpFactory.CreatePerClientInstanceService(proxyFactoryFunc);
        }

        public IRpcService<TProxy> RpcService { get; }

        public void Start(string serviceName)
        {
            Stop();
            var namedPipeMessagingFactory = new NamedPipeMessagingSystemFactory();
            var channel = namedPipeMessagingFactory.CreateDuplexInputChannel($"net.pipe://localhost/{serviceName}");
            RpcService.AttachDuplexInputChannel(channel);
        }

        public void Stop()
        {
            if (RpcService.IsDuplexInputChannelAttached)
            {
                RpcService.DetachDuplexInputChannel();
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}