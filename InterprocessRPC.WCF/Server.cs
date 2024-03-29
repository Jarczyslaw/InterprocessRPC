﻿using System;
using System.ServiceModel;

namespace InterprocessRPC.WCF
{
    public class Server<TProxy> : IDisposable
        where TProxy : class
    {
        public Server()
        {
        }

        public Server(IAddressProvider addressProvider, Type serviceType)
            : this()
        {
            Start(addressProvider, serviceType);
        }

        public ServiceHost Host { get; private set; }
        public bool IsListening => Host?.State == CommunicationState.Opened;

        public void Start(IAddressProvider addressProvider, Type serviceType)
        {
            Stop();
            Host = new ServiceHost(serviceType, new Uri(addressProvider.BaseAddress));
            Host.AddServiceEndpoint(typeof(TProxy), new NetNamedPipeBinding(), addressProvider.ServiceName);
            Host.Open();
        }

        public void Start<TService>(IAddressProvider addressProvider)
            where TService : TProxy
        {
            Start(addressProvider, typeof(TService));
        }

        public void Stop()
        {
            if (Host != null)
            {
                try
                {
                    Host.Close();
                }
                catch
                {
                    Host.Abort();
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}