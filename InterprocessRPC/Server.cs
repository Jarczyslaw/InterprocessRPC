using StreamJsonRpc;
using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace InterprocessRPC
{
    public delegate void OnListeningStart();

    public delegate void OnListeningStop();

    public delegate void OnClientConnected(ServerConnection serverConnection);

    public delegate void OnClientDisconnected(ServerConnection serverConnection);

    public class Server<TProxy>
        where TProxy : class
    {
        private CancellationTokenSource cancellationTokenSource;
        private AutoResetEvent connectionTaskResetEvent = new AutoResetEvent(true);

        public event OnListeningStart ListeningStart;

        public event OnListeningStop ListeningStop;

        public event OnClientConnected ClientConnected;

        public event OnClientDisconnected ClientDisconnected;

        public ConcurrentList<ServerConnection> Connections { get; } = new ConcurrentList<ServerConnection>();
        public string PipeName { get; private set; }
        public bool HasConnectedClients => Connections.Count != 0;

        public async Task Start(string pipeName, Func<TProxy> factoryFunc)
        {
            await Stop();
            StartNew(pipeName, factoryFunc);
        }

        private void StartNew(string pipeName, Func<TProxy> factoryFunc)
        {
            PipeName = pipeName;
            cancellationTokenSource = new CancellationTokenSource();
            StartConnectionTask(factoryFunc(), cancellationTokenSource.Token);
        }

        private void StartConnectionTask(TProxy proxy, CancellationToken token)
        {
            Task.Run(async () =>
            {
                connectionTaskResetEvent.WaitOne();
                try
                {
                    ListeningStart?.Invoke();
                    while (!token.IsCancellationRequested)
                    {
                        var stream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                        try
                        {
                            await stream.WaitForConnectionAsync(token);
                            StartListeningTask(stream, proxy, token);
                        }
                        catch (OperationCanceledException) { }
                    }
                }
                finally
                {
                    ListeningStop?.Invoke();
                    connectionTaskResetEvent.Set();
                }
            }, token);
        }

        private void StartListeningTask(NamedPipeServerStream stream, TProxy proxy, CancellationToken token)
        {
            Task.Run(async () =>
            {
                var connection = new ServerConnection
                {
                    Rpc = JsonRpc.Attach(stream, proxy),
                    Stream = stream,
                    Guid = Guid.NewGuid()
                };

                try
                {
                    Connections.Add(connection);
                    ClientConnected?.Invoke(connection);
                    while (stream.IsConnected && !token.IsCancellationRequested)
                    {
                        await Task.Delay(1, token);
                    }
                }
                finally
                {
                    Connections.Remove(connection);
                    ClientDisconnected?.Invoke(connection);
                    connection.Dispose();
                }
            }, token);
        }

        public async Task Stop()
        {
            cancellationTokenSource?.Cancel();
            await Task.Run(async () =>
            {
                while (Connections.Count != 0)
                {
                    await Task.Delay(10);
                }
            });
        }
    }
}