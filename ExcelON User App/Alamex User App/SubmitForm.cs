using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExcelON_User_App
{
    public partial class SubmitForm : Form
    {
        MainForm mf;
        FileType ft;
        SubmitType st;
        public SubmitForm(MainForm mf, FileType ft, SubmitType st)
        {
            InitializeComponent();
            this.mf = mf;
            this.ft = ft;
            this.st = st;
        }

        private void button_submit_Click(object sender, EventArgs e)
        {
            string inputName = textBoxName.Text.Substring(0, Math.Min(30, textBoxName.Text.Length)).TrimEnd('*');

            if (st == SubmitType.SaveNewFile)
                SaveAsFile(inputName);
            else RenameFile(inputName);
        }

        private void SaveAsFile(string inputName)
        {
            WV2Tab currWV2 = (WV2Tab)mf.tabCntrl.SelectedTab.Controls[0];

            int resId = Program.client.SaveAsFile(ft.ToString(), AuthForm.userId, inputName, currWV2.fileChngs);
            if (resId != int.MaxValue)
            {
                MessageBox.Show(inputName + AppMsg.SaveSucceeded, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                currWV2.fileId = resId;
                currWV2.fileName = inputName;
                currWV2.fileType = ft;
                mf.tabCntrl.SelectedTab.Text = inputName + "\\New " + ft;
                currWV2.status.isSynchronized = true;
                currWV2.fileChngs = "";
                Close();
            }
            else
                MessageBox.Show(inputName + AppMsg.SaveFailed, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RenameFile(string inputName)
        {
            WV2Tab currWV2 = (WV2Tab)mf.tabCntrl.SelectedTab.Controls[0];
            bool hasSucceeded = Program.client.RenameFile(ft.ToString(), currWV2.fileId, inputName);
            if (hasSucceeded)
            {
                MessageBox.Show(inputName + AppMsg.RenameSucceeded, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                currWV2.fileName = inputName;
                string t = mf.tabCntrl.SelectedTab.Text;
                mf.tabCntrl.SelectedTab.Text = t.Replace(t.Substring(0, t.IndexOf('\\')), inputName);

                Close();
            }
            else
                MessageBox.Show(inputName + AppMsg.RenameFailed, mf.clickedBtn.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox_inputName_Enter(object sender, EventArgs e)
        {
            textBoxName.ForeColor = Color.Black;
            textBoxName.Text = "";
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            button_submit.Enabled = textBoxName.Text.Length > 0;
        }

        private void SubmitForm_Load(object sender, EventArgs e)
        {
            mf = (MainForm)Owner;
        }

        private void textBox_inputName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_submit.Focus();
                button_submit.PerformClick();
            }
        }
    }
}
