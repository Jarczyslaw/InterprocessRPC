using StreamJsonRpc;
using System;
using System.IO.Pipes;

namespace InterprocessRPC
{
    public class ServerConnection : IDisposable
    {
        public Guid Guid { get; set; }
        public NamedPipeServerStream Stream { get; set; }
        public JsonRpc Rpc { get; set; }

        public bool IsAlive => Stream.IsConnected;

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}