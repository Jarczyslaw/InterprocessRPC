using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterprocessRPC.TestServer
{
    public partial class MainForm : Form
    {
        private Server<IProxy> server;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Stop();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                server?.Stop();
                server = new Server<IProxy>();

                var proxy = new Proxy();
                proxy.GetServerInfoFunc += () => new ServerInfo
                {
                    ConnectionsCount = server.Connections.Count,
                    ServerTime = DateTime.Now
                };
                await server.Start(Proxy.ProxyPipeName, () => proxy);
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
    }
}