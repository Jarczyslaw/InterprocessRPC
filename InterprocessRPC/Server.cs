using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace InterprocessRPC
{
    public class Server<TProxy> : IDisposable
        where TProxy : class
    {
        public List<ServerConnections> Connections { get; } = new List<ServerConnections>();
        public string PipeName { get; private set; }
        public bool HasConnectedClients => Connections.Count != 0;

        public async Task Start(string pipeName, Func<TProxy> factoryFunc)
        {
            Dispose();
            await StartNew(pipeName, factoryFunc);
        }

        private async Task StartNew(string pipeName, Func<TProxy> factoryFunc)
        {
            PipeName = pipeName;
            while (true)
            {
                var stream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                await stream.WaitForConnectionAsync();
                var rpc = JsonRpc.Attach(stream, factoryFunc());
                var connection = new ServerConnections
                {
                    Stream = stream,
                    Rpc = rpc,
                };
                Connections.Add(connection);
                while (connection.IsAlive)
                {
                    await Task.Delay(1);
                }
                Connections.Remove(connection);
            }
        }

        public void Dispose()
        {
            foreach (var connection in Connections)
            {
                connection?.Dispose();
            }
            Connections.Clear();
        }
    }
}