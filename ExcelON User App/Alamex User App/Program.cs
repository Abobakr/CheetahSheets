using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelON_User_App
{
    internal static class Program
    {
        public static RestClient client;
        public static string serverUrl = "";
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path = Directory.GetCurrentDirectory() + "\\serverIP.txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("Important files are missing !!!\n\n\nPlease contact admin and reinstall application", "Application Could not start successfully!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            serverUrl = "HTTP://" + File.ReadAllText(path) + "/";
            string RestAdd = serverUrl + "Service.svc";
            client = new RestClient(RestAdd);

            Application.Run(new AuthForm());
        }
    }
}
