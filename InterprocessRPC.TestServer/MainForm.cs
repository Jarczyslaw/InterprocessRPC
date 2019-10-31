using InterprocessRPC.Wrappers.ServerWrappers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterprocessRPC.TestServer
{
    public partial class MainForm : Form
    {
        private readonly IServerWrapper serverWrapper;

        public MainForm(IServerWrapper serverWrapper)
        {
            this.serverWrapper = serverWrapper;
            InitializeComponent();

            serverWrapper.ListeningStart += () => AppendMessage("Server started");
            serverWrapper.ListeningStop += () => AppendMessage("Server stopped");
            serverWrapper.ClientConnected += () => AppendMessage("Client connected");
            serverWrapper.ClientDisconnected += () => AppendMessage("Client disconnected");
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Stop();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                await serverWrapper.Start();
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
                await serverWrapper.Stop();
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