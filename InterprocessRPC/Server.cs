using StreamJsonRpc;
using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace InterprocessRPC
{
    public delegate void OnListeningStart();

    public delegate void OnListeningStop(ListeningInfo listeningInfo);

    public delegate void OnClientConnected(ServerConnection serverConnection);

    public delegate void OnClientDisconnected(ClientInfo clientInfo);

    public class Server<TProxy>
        where TProxy : class
    {
        private CancellationTokenSource cancellationTokenSource;
        private readonly AutoResetEvent connectionTaskResetEvent = new AutoResetEvent(true);

        public event OnListeningStart ListeningStart;

        public event OnListeningStop ListeningStop;

        public event OnClientConnected ClientConnected;

        public event OnClientDisconnected ClientDisconnected;

        public ConcurrentList<ServerConnection> Connections { get; } = new ConcurrentList<ServerConnection>();
        public string PipeName { get; private set; }
        public bool HasConnectedClients => Connections.Count != 0;

        public bool Listening { get; private set; }

        private void InvokeListeningStart()
        {
            Listening = true;
            ListeningStart?.Invoke();
        }

        private void InvokeListeningStop(ListeningInfo listeningInfo)
        {
            Listening = false;
            ListeningStop?.Invoke(listeningInfo);
        }

        public async Task Start(string pipeName, Func<TProxy> factoryFunc)
        {
            await Stop();
            StartNew(pipeName, factoryFunc);
        }

        private void StartNew(string pipeName, Func<TProxy> factoryFunc)
        {
            PipeName = pipeName;
            cancellationTokenSource = new CancellationTokenSource();
            StartListeningTask(factoryFunc(), cancellationTokenSource.Token);
        }

        private void StartListeningTask(TProxy proxy, CancellationToken token)
        {
            Task.Run(async () =>
            {
                connectionTaskResetEvent.WaitOne();
                var info = new ListeningInfo
                {
                    StartTime = DateTime.Now
                };
                try
                {
                    InvokeListeningStart();
                    await StartListening(proxy, token);
                }
                catch (OperationCanceledException) { }
                catch (Exception exc)
                {
                    info.Exception = exc;
                }
                finally
                {
                    info.EndTime = DateTime.Now;
                    InvokeListeningStop(info);
                    connectionTaskResetEvent.Set();
                }
            }, token);
        }

        private async Task StartListening(TProxy proxy, CancellationToken token)
        {
            NamedPipeServerStream stream = null;
            try
            {
                while (!token.IsCancellationRequested)
                {
                    stream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                    await stream.WaitForConnectionAsync(token);
                    StartConnectionTask(stream, proxy, token);
                }
            }
            finally
            {
                stream?.Dispose();
            }
        }

        private void StartConnectionTask(NamedPipeServerStream stream, TProxy proxy, CancellationToken token)
        {
            Task.Run(async () =>
            {
                var id = Guid.NewGuid();
                var info = new ClientInfo
                {
                    ConnectedTime = DateTime.Now,
                    Guid = id,
                };
                var connection = new ServerConnection
                {
                    Rpc = JsonRpc.Attach(stream, proxy),
                    Stream = stream,
                    Guid = id
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
                catch (OperationCanceledException) { }
                catch (Exception exc)
                {
                    info.Exception = exc;
                }
                finally
                {
                    info.DisconnectedTime = DateTime.Now;
                    Connections.Remove(connection);
                    connection.Dispose();
                    ClientDisconnected?.Invoke(info);
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