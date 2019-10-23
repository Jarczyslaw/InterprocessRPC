using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterprocessRPC.TestServer
{
    public partial class MainForm : Form
    {
        private Server<IProxy> server = new Server<IProxy>();

        public MainForm()
        {
            InitializeComponent();

            server.ListeningStart += () => AppendMessage("Server started");
            server.ListeningStop += () => AppendMessage("Server stopped");
            server.ClientConnected += _ => AppendMessage($"Client connected. Clients count: {server.Connections.Count}");
            server.ClientDisconnected += _ => AppendMessage($"Client disconnected. Clients count: {server.Connections.Count}");
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Stop();
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

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                var proxy = new Proxy();
                proxy.GetServerInfoFunc += () => new ServerInfo
                {
                    ConnectionsCount = server.Connections.Count,
                    ServerTime = DateTime.Now
                };
                await server.Start(Proxy.ProxyPipeName, CreateProxy);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            await Stop();
        }

        private async Task Stop()
        {
            try
            {
                if (server != null)
                {
                    await server.Stop();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AppendMessage(string message)
        {
            this.SafeInvoke(() =>
            {
                tbMessages.Text = $"[{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss.fff")}] - {message}"
                    + Environment.NewLine + tbMessages.Text;
            });
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbMessages.Text = string.Empty;
        }
    }
}