using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MBtns = System.Windows.Forms.MessageBoxButtons;
using MIcn = System.Windows.Forms.MessageBoxIcon;



namespace ExcelON_User_App
{
    public partial class MainForm : Form
    {
        internal ActionType action = ActionType.ReceiveReady;
        internal ToolStripButton clickedBtn = null;
        string newEwaURL;
        internal bool isAdmin = false;


        public MainForm(List<string> ewaURLs)
        {
            InitializeComponent();

            for (byte i = 0; i < ewaURLs.Count; i++)
            {
                tabCntrl.TabPages.Add("");
                tabCntrl.TabPages[i].Controls.Add(new WV2Tab(this) { Source = new Uri(ewaURLs[i]) });
            }

            int last = tabCntrl.TabPages.Count - 1;
            
            newEwaURL = ewaURLs[last];
            isAdmin = newEwaURL.Contains("Admin");
        }
        private int checkSyncronization()
        {
            for (byte i = 0; i < tabCntrl.TabPages.Count; i++)
            {
                WV2Tab tab = (WV2Tab)tabCntrl.TabPages[i].Controls[0];

                if (!tab.status.isSynchronized)
                    return i;
            }

            return -1;
        }

        private void toolStripButton_SaveAsPdf_Click(object sender, EventArgs e)
        {
            if (tabCntrl.TabPages.Count == 0)
                return;

            clickedBtn = (ToolStripButton)sender;

            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];

            if (currWV2.fileType == FileType.NewFile)
            {
                MessageBox.Show(AppMsg.SheetAsPdfNewFile, clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!currWV2.status.isSynchronized)
            {
                if (MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.ChangesFound, clickedBtn.Text, MBtns.YesNo, MIcn.Exclamation) == DialogResult.Yes)
                {

                    clickedBtn = currWV2.fileType == FileType.Session ? tsb_SaveSession : tsb_SaveCallBack;
                    SaveFile();
                }
                else return;
            }
            else
            {
                action = ActionType.SaveAsPdf;
                currWV2.CoreWebView2.PostWebMessageAsString("GetSelectedSheet");
            }
        }
        private void toolStripButton_SaveSession_Click(object sender, EventArgs e)
        {
            if (tabCntrl.TabPages.Count == 0)
                return;

            clickedBtn = (ToolStripButton)sender;

            SaveFile();
        }

        private void SaveFile()
        {
            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];

            if (!clickedBtn.Text.Contains(currWV2.fileType.ToString()) && currWV2.fileType != FileType.NewFile)
            {
                MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.SaveNotValid, clickedBtn.Text, MBtns.OK, MIcn.Exclamation);
                return;
            }

            if (currWV2.status.isSynchronized)
            {
                MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.ChangesNotFound, clickedBtn.Text, MBtns.OK, MIcn.Exclamation);
                return;
            }

            action = ActionType.SaveFile;
            currWV2.CoreWebView2.PostWebMessageAsString("GetLastChanges");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int index = checkSyncronization();
            if (index >= 0)
            {
                tabCntrl.SelectedIndex = index;
                if (MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.ChangesFound, e.CloseReason.ToString(), MBtns.YesNo, MIcn.Question) == DialogResult.Yes)
                {
                    WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];
                    if (currWV2.fileType != FileType.NewFile)
                    {
                        clickedBtn = currWV2.fileType == FileType.Session ? tsb_SaveSession : tsb_SaveCallBack;
                        SaveFile();
                    }
                    e.Cancel = true;
                }
            }
        }

        private void toolStripButton_CallBack_Click(object sender, EventArgs e)
        {
            new CallBackForm(this).ShowDialog();
        }

        private void toolStripButton_NewFile_Click(object sender, EventArgs e)
        {
            tabCntrl.TabPages.Add("");
            int cnt = tabCntrl.TabPages.Count;
            tabCntrl.TabPages[cnt - 1].Controls.Add(new WV2Tab(this) { Source = new Uri(newEwaURL) });
            tabCntrl.SelectedIndex = cnt - 1;            
            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            if (tabCntrl.TabPages.Count == 0)
                return;
            clickedBtn = (ToolStripButton)sender;
            
            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];
            if (currWV2.fileType == FileType.Session || currWV2.fileType == FileType.CallBack)
                MessageBox.Show(AppMsg.RefreshStopped, clickedBtn.Text, MBtns.OK, MIcn.Exclamation);
            else
            {
                currWV2.Reload();
                currWV2.status.isSynchronized = true;
                tabCntrl.SelectedTab.Text = (isAdmin ? "Admin" : "User") + " New File";
            }
        }

        private void toolStripButton_CloseTab_Click(object sender, EventArgs e)
        {

            if (tabCntrl.TabPages.Count == 0)
                return;

            clickedBtn = (ToolStripButton)sender;

            int index = tabCntrl.SelectedIndex;
            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];
            if (!currWV2.status.isSynchronized)
                if (MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.ChangesFound, clickedBtn.Text, MBtns.YesNo, MIcn.Exclamation) == DialogResult.Yes)
                {
                    if (currWV2.fileType != FileType.NewFile)
                    {
                        clickedBtn = currWV2.fileType == FileType.Session ? tsb_SaveSession : tsb_SaveCallBack;
                        SaveFile();
                    }
                    return;
                }
                    
            tabCntrl.TabPages.RemoveAt(index);
            tabCntrl.SelectedIndex = index > 1 ? index - 1 : 0;
        }

        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            if (tabCntrl.TabPages.Count == 0)
                return;

            clickedBtn = (ToolStripButton)sender;
            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];
            if (currWV2.fileType == FileType.NewFile)
            {
                MessageBox.Show(AppMsg.DeleteNewFile, clickedBtn.Text, MBtns.OK, MIcn.Error);
                return;
            }

            if (MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.DeleteWarning, clickedBtn.Text, MBtns.YesNo, MIcn.Warning) == DialogResult.Yes)
            {
                if (Program.client.DeleteFile(currWV2.fileType.ToString(), currWV2.fileId))
                {
                    MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.DeleteSucceeded, clickedBtn.Text, MBtns.OK, MIcn.Information);
                    toolStripButton_CloseTab.PerformClick();
                }
                else
                    MessageBox.Show(tabCntrl.SelectedTab.Text + AppMsg.DeleteFailed, clickedBtn.Text, MBtns.OK, MIcn.Error);
            }
        }

        private void toolStripButton_SaveCallBack_Click(object sender, EventArgs e)
        {
            if (tabCntrl.TabPages.Count == 0)
                return;

            clickedBtn = (ToolStripButton)sender;
            SaveFile();
        }

        private void toolStripButton_ConvertToNewFile_Click(object sender, EventArgs e)
        {
            if (tabCntrl.TabPages.Count == 0)
                return;

            clickedBtn = (ToolStripButton)sender;
            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];
            if (currWV2.fileType == default) //New File
            {
                MessageBox.Show(AppMsg.CopyToNewStopped, clickedBtn.Text, MBtns.OK, MIcn.Exclamation);
                return;
            }

            if (currWV2.fileType == FileType.Session || currWV2.fileType == FileType.CallBack)
            {
                currWV2.fileChngs = Program.client.GetFileChanges(currWV2.fileType.ToString(), currWV2.fileId.ToString());
                tabCntrl.SelectedTab.Text = currWV2.fileName + " Copy of " + currWV2.fileType;
                currWV2.fileType = FileType.NewFile;
            }
        }

        private void toolStripButton_Rename_Click(object sender, EventArgs e)
        {
            if (tabCntrl.TabPages.Count == 0)
                return;

            clickedBtn = (ToolStripButton)sender;

            WV2Tab currWV2 = (WV2Tab)tabCntrl.SelectedTab.Controls[0];
            if (currWV2.fileType == default)
            {
                MessageBox.Show(AppMsg.RenameNotValid, clickedBtn.Text, MBtns.OK, MIcn.Exclamation);
                return;
            }
            new SubmitForm(this, currWV2.fileType, SubmitType.RenameFile).ShowDialog();
        }

        private void toolStripButton_About_Click(object sender, EventArgs e)
        {
            clickedBtn = (ToolStripButton)sender;
            MessageBox.Show(AppMsg.AboutUs, clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
    }
    public enum SubmitType
    {
        SaveNewFile,
        RenameFile
    }
    public enum ActionType
    {
        ReceiveReady,
        SaveFile,
        SaveAsPdf
    }
    public enum FileType
    {
        NewFile,
        Session,
        CallBack
    }

    public struct AppMsg
    {

        public static string ChangesNotFound = " file is synchronized and has NO unsaved changes.";
        public static string ChangesFound = " file has changes which are NOT saved yet!\n\nDo you want to save them?";

        public static string SheetAsPdfNewFile = "New files must be saved first.\n\nPlease save as a session or a callback file.";
        public static string SheetAsPdfStopped = " sheet can NOT be saved as PDF file.\n\nPlease select another sheet.";
        public static string SheetAsPdfFailed = " sheet could NOT be saved as a PDF file.  :(\n\nPlease try again later.";

        public static string SaveNotValid = " file type can NOT be saved! directly\n\nPlease copy top a new file of it first.";
        public static string SaveSucceeded = " file was successfully saved on the server.  (:";
        public static string SaveFailed = " file is already existed on the server. :(\n\nPlease enter another name.";

        public static string RenameNotValid = "New files type can NOT be renamed !\n\nPlease save it first as a session or a callback";
        public static string RenameSucceeded = " file was successfully renamed on the server.  (:";
        public static string RenameFailed = " file name could NOT be renamed on the server :(\n\nPlease try again later.";

        public static string UpdatesSucceeded = " file was successfully updated on the server  (:";
        public static string UpdatesFailed = " file could NOT be updated on the server.  :(\n\nPlease try again later.";

        public static string DeleteNewFile = "New files can NOT be deleted.\n\nPlease use close tab button instead.";
        public static string DeleteWarning = " file is about to be deleted permanently.\n\nDo you want to continue?";
        public static string DeleteSucceeded = " file was successfully deleted on the server.  (:";
        public static string DeleteFailed = " file could NOT be deleted on the server.  :(\n\nIt may be have been DELETED by other user.";

        public static string CopyToNewStopped = "New files can NOT be changed!\n\nPlease save it as a session or as a call back first.";

        public static string RefreshStopped = "Saved files can NOT be reloaded!\n\nPlease make a callback or close & open for sessions";

        public static string AboutUs = "جميع الحقوق محفوظة\n\nشركة أبوبكر للذكاء الإصطناعي قونيا-تركيا\n\nALL RIGHTS ARE RESERVED\n\nEbubekir Yapay Zeka Company Konya-Türkiye\n\n";
    }

}
