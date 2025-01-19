using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ExcelON_User_App
{
    internal class WV2Tab : WebView2
    {
        MainForm mf;

        internal FileType fileType;

        internal string fileName;

        internal TabStatus status = new TabStatus();

        internal string fileChngs = "";

        internal int fileId;

        int pageIndex;

        public WV2Tab(MainForm mf) : base()
        {
            Dock = DockStyle.Fill;
            this.mf = mf;
            CoreWebView2InitializationCompleted += WebViewInTab_CoreWebView2InitializationCompleted;
            pageIndex = mf.tabCntrl.TabCount - 1;
        }
        private void WebViewInTab_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
            CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged;

            CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

            CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
            CoreWebView2.WebMessageReceived += webView_WebMessageReceived;

        }
        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true; // let the default new window 

            TabPage tpage = new TabPage(); // boy

            tpage.Controls.Add(new WV2Tab(mf) { Source = new Uri(e.Uri) }); // toy

            mf.tabCntrl.TabPages.Add(tpage); // daddy
            mf.tabCntrl.SelectedTab = tpage; // user expectation
        }
        private void CoreWebView2_DocumentTitleChanged(object sender, object e)
        {
            string t = CoreWebView2.DocumentTitle;
            if (t.Contains('@'))
            {
                fileId = int.Parse(t.Substring(0, t.IndexOf('@')));
                t = t.Substring(t.IndexOf('@') + 1);
                fileName = t.Substring(0, t.IndexOf('\\'));
                fileType = t.Contains("Session") ? FileType.Session : FileType.CallBack;
            }
            //else { fileType = default; fileId = default(string); fileName = default(string); }

            mf.tabCntrl.TabPages[pageIndex].Text = t;
        }
        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
            {
                NavigateToString(@"<!DOCTYPE HTML>" +
                                  "<hr style = 'height:100px; visibility:hidden'/>" +
                                  "<center> " +
                                     "<h1> Important files are missing !!! - Please contact Admin </h1>" +
                                  "</center>");
            }
        }
        private void webView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            if (mf.clickedBtn != null)
                mf.clickedBtn.Enabled = false;

            switch (mf.action)
            {
                case ActionType.ReceiveReady:
                    status = JsonConvert.DeserializeObject<TabStatus>(e.WebMessageAsJson);

                    if (!status.isSynchronized && !mf.tabCntrl.SelectedTab.Text.EndsWith("*"))
                        mf.tabCntrl.SelectedTab.Text += "*";

                    break;

                case ActionType.SaveAsPdf:
                    string selectedSheetName = e.TryGetWebMessageAsString();
                    if (selectedSheetName == "FİYAT HESABI")
                        MessageBox.Show(selectedSheetName + AppMsg.SheetAsPdfStopped, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        string response = Program.client.SavePDF(fileType.ToString(), fileId, selectedSheetName, mf.isAdmin.ToString());
                        if (response.Contains(selectedSheetName))
                            System.Diagnostics.Process.Start(Program.serverUrl + response);
                        else MessageBox.Show(selectedSheetName + AppMsg.SheetAsPdfFailed, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case ActionType.SaveFile:

                    if (fileType == FileType.NewFile)
                    {
                        FileType supposedType = mf.clickedBtn.Text.Contains("Session") ? FileType.Session : FileType.CallBack;

                        fileChngs = accumulateJson(e.WebMessageAsJson);

                        new SubmitForm(mf, supposedType, SubmitType.SaveNewFile).ShowDialog();

                    }
                    else
                    {
                        fileChngs = accumulateJson(e.WebMessageAsJson);

                        bool hasSucceeded = Program.client.UpdateFile(fileType.ToString(), fileId, fileChngs);
                        if (hasSucceeded)
                        {
                            MessageBox.Show(fileName + AppMsg.UpdatesSucceeded, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            status.isSynchronized = true;
                            string t = mf.tabCntrl.SelectedTab.Text.TrimEnd('*');
                            mf.tabCntrl.SelectedTab.Text = t;
                            fileChngs = "";
                        }
                        else
                            MessageBox.Show(fileName + AppMsg.UpdatesFailed, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
            }

            mf.action = default;

            if (mf.clickedBtn != null)
            {
                mf.clickedBtn.Enabled = true;
                mf.clickedBtn = null;
            }
        }
        private string accumulateJson(string newJson)
        {
            string resJson = fileChngs;

            if (newJson == "")
                return resJson;

            if (fileChngs == "")
                resJson = newJson;
            else
            {

                resJson = fileChngs.Insert(fileChngs.Length - 2, ",");
                resJson = resJson.Insert(resJson.Length - 2, newJson.Substring(12, newJson.Length - 12 - 2));
            }

            return resJson;
        }
    }
}
class TabStatus
{
    public bool isSynchronized = true;
}