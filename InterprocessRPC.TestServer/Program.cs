using InterprocessRPC.Wrappers;
using System;
using System.Windows.Forms;

namespace InterprocessRPC.TestServer
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(ServerFactory.GetServerWrapper()));
        }
    }
}