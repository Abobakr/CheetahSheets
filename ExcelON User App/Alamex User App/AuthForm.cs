using DeviceId;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace ExcelON_User_App
{
    public partial class AuthForm : Form
    {
        public static string userId;
        public AuthForm()
        {
            InitializeComponent();
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            Show();
            userId = BuildCode();//"8PBZTBY5ZQ4KXF9D0W5EZ4KFBVR0SW6HX1GZX6GRHX5C0SSN49HTGEng Helmi21071906030537";//  "CS3ACW7F4F3QMSW4F57TM70K2AZ9V104QZMCE9S1QF9P00ESX890Lenovo00023303050722012720";//// // ;


            try
            {
                if (userId != null)
                {
                    List<string> ewaPaths = Program.client.GetSessions(userId);
                    Opacity = 100;
                    if (ewaPaths != null)
                    {
                        Hide();
                        for (int i = 0; i < ewaPaths.Count; i++)
                            ewaPaths[i] = Program.serverUrl + ewaPaths[i];

                        new MainForm(ewaPaths).ShowDialog();
                    }
                    else throw new Exception("Not Authorized User");
                }
                else throw new Exception("Not Authorized Access");
            }
            catch
            {
                MessageBox.Show("Not Authorized User", "Application is closing !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Close(); }

        }

        private string BuildCode()
        {
            string deviceId = new DeviceIdBuilder()
                .AddMachineName()
                .AddOsVersion()
                .OnWindows(windows => windows
                    .AddProcessorId()
                    .AddMotherboardSerialNumber()
                    .AddSystemDriveSerialNumber()
                    .AddMacAddressFromWmi(excludeWireless: true, excludeNonPhysical: true))
                .ToString();

            string userName = Environment.UserName;

            string driveSerialNO = "";
            ManagementObjectSearcher searcher;
            do
            {
                Thread.Sleep(100);
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
            } while (searcher.Get().Count == 0 && (MessageBox.Show("Dongle is not attached", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry));

            foreach (ManagementObject currObj in searcher.Get())
            {
                ManagementObject mngObj = new ManagementObject("Win32_PhysicalMedia.Tag='" + currObj["DeviceID"] + "'");
                driveSerialNO = mngObj["SerialNumber"].ToString();
                string builtCode = deviceId + userName + driveSerialNO;

                return builtCode;
            }

            return null;
        }
    }
}
