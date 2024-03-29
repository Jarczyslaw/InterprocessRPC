﻿using StreamJsonRpc;
using System;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace InterprocessRPC
{
    public class Client<TProxy> : IDisposable
        where TProxy : class
    {
        public TProxy Proxy { get; private set; }
        public NamedPipeClientStream Stream { get; private set; }
        public string PipeName { get; private set; }

        public bool IsConnected
        {
            get
            {
                if (Stream == null)
                {
                    return false;
                }
                return Stream.IsConnected && Proxy != null;
            }
        }

        public async Task Start(string pipeName)
        {
            Stop();
            await StartNew(pipeName);
        }

        public static async Task<Client<T>> StartNew<T>(string pipeName)
            where T : class
        {
            var client = new Client<T>();
            await client.Start(pipeName);
            return client;
        }

        private async Task StartNew(string pipeName)
        {
            PipeName = pipeName;
            Stream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            await Stream.ConnectAsync();
            Proxy = JsonRpc.Attach<TProxy>(Stream);
        }

        public void Stop()
        {
            ((IDisposable)Proxy)?.Dispose();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}