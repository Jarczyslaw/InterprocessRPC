using InterprocessRPC.Common;
using System;
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                server?.Dispose();
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            try
            {
                server?.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}