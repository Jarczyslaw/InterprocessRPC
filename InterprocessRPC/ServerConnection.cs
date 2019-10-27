using StreamJsonRpc;
using System;
using System.IO.Pipes;

namespace InterprocessRPC
{
    public class ServerConnection : IDisposable
    {
        public Guid Guid { get; internal set; }
        public NamedPipeServerStream Stream { get; internal set; }
        public JsonRpc Rpc { get; internal set; }

        public bool IsAlive => Stream.IsConnected;

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}