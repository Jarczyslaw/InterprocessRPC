using InterprocessRPC.Common;
using InterprocessRPC.Wrappers.ClientWrappers;
using System;
using System.Windows.Forms;

namespace InterprocessRPC.TestClient
{
    public partial class MainForm : Form
    {
        private readonly IClientWrapper clientWrapper;

        public MainForm(IClientWrapper clientWrapper)
        {
            this.clientWrapper = clientWrapper;

            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                using (var execTime = new ExecutionTime())
                {
                    await clientWrapper.Start();
                }
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
                var result = false;
                using (var execTime = new ExecutionTime())
                {
                    result = await clientWrapper.CheckConnection();
                }
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
                var message = string.Empty;
                using (var execTime = new ExecutionTime())
                {
                    message = await clientWrapper.GetHelloMessage(tbName.Text);
                }
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
                DateTime dateTime;
                using (var execTime = new ExecutionTime())
                {
                    dateTime = await clientWrapper.GetServerTime();
                }
                MessageBox.Show(dateTime.ToString());
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
                clientWrapper.Stop();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}