using Eneter.Messaging.EndPoints.Rpc;
using Eneter.Messaging.MessagingSystems.NamedPipeMessagingSystem;
using System;

namespace InterprocessRPC.Eneter
{
    public class Client<TProxy> : IDisposable
        where TProxy : class
    {
        public Client()
        {
            IRpcFactory rcpFactory = new RpcFactory();
            RpcClient = rcpFactory.CreateClient<TProxy>();
        }

        public IRpcClient<TProxy> RpcClient { get; }

        public void Start(string serviceName)
        {
            Stop();
            var namedPipeMessagingFactory = new NamedPipeMessagingSystemFactory();
            var channel = namedPipeMessagingFactory.CreateDuplexOutputChannel($"net.pipe://localhost/{serviceName}");
            RpcClient.AttachDuplexOutputChannel(channel);
        }

        public void Stop()
        {
            if (RpcClient.IsDuplexOutputChannelAttached)
            {
                RpcClient.DetachDuplexOutputChannel();
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}