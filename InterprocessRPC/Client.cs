using StreamJsonRpc;
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
            Dispose();
            await StartNew(pipeName);
        }

        private async Task StartNew(string pipeName)
        {
            PipeName = pipeName;
            Stream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            await Stream.ConnectAsync();
            Proxy = JsonRpc.Attach<TProxy>(Stream);
        }

        public void Dispose()
        {
            ((IDisposable)Proxy)?.Dispose();
        }
    }
}