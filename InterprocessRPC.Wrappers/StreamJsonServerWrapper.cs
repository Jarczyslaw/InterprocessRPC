﻿using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers
{
    public class StreamJsonServerWrapper : IServerWrapper
    {
        private Server<IProxy> server = new Server<IProxy>();

        public Action ListeningStart { get; set; }

        public Action ListeningStop { get; set; }

        public Action ClientConnected { get; set; }

        public Action ClientDisconnected { get; set; }

        public StreamJsonServerWrapper()
        {
            server.ClientConnected += _ => ClientConnected?.Invoke();
            server.ClientDisconnected += _ => ClientDisconnected?.Invoke();
            server.ListeningStart += () => ListeningStart?.Invoke();
            server.ListeningStop += _ => ListeningStop?.Invoke();
        }

        public async Task Start()
        {
            await server.Start(Proxy.ProxyPipeName, CreateProxy);
        }

        public async Task Stop()
        {
            await server.Stop();
        }

        private Proxy CreateProxy()
        {
            var proxy = new Proxy();
            proxy.GetServerInfoFunc += () => new ServerInfo
            {
                ConnectionsCount = server.Connections.Count,
                ServerTime = DateTime.Now
            };
            return proxy;
        }
    }
}