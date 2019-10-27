using System;
using System.ServiceModel;

namespace InterprocessRPC.WCF
{
    public class Client<TProxy> : IDisposable
        where TProxy : class
    {
        public Client()
        {
        }

        public Client(IAddressProvider addressProvider)
            : this()
        {
            Start(addressProvider);
        }

        public TProxy Proxy { get; private set; }
        public ChannelFactory<TProxy> ChannelFactory { get; private set; }

        public void Start(IAddressProvider addressProvider)
        {
            ChannelFactory = new ChannelFactory<TProxy>(new NetNamedPipeBinding(), new EndpointAddress(addressProvider.ServiceAddress));
            Proxy = ChannelFactory.CreateChannel();
        }

        public void Stop()
        {
            if (ChannelFactory != null)
            {
                try
                {
                    ChannelFactory.Close();
                }
                catch
                {
                    ChannelFactory.Abort();
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}