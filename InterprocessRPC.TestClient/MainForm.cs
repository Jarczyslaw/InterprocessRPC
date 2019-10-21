using InterprocessRPC.Common;
using System;
using System.Windows.Forms;

namespace InterprocessRPC.TestClient
{
    public partial class MainForm : Form
    {
        private Client<IProxy> client;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                client?.Dispose();
                client = new Client<IProxy>();
                await client.Start(Proxy.ProxyPipeName);
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

        private async void btnCheckConnection_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await client.Proxy.CheckConnection();
                MessageBox.Show("Connection state: " + (result ? "connected" : "disconnected"));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnGetMessage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Please enter your name");
                return;
            }

            try
            {
                var message = await client.Proxy.GetHelloMessage(tbName.Text);
                MessageBox.Show(message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnGetServerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                var serverInfo = await client.Proxy.GetServerInfo();
                MessageBox.Show(serverInfo.ToString());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            try
            {
                client?.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}