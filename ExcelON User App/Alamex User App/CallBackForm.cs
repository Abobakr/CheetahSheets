using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ExcelON_User_App
{
    public partial class CallBackForm : Form
    {
        static string[] CBHeaders = { "Id", "Name", "Author", "Updated Date" };
        MainForm mf;
        public CallBackForm(MainForm mf)
        {
            InitializeComponent();
            this.mf = mf;
        }

        private void CallBackForm_Load(object sender, EventArgs e)
        {
            List<CallBack> callBacks = Program.client.GetAllCallBacks(AuthForm.userId);
            dgv.DataSource = callBackListToDataTable(callBacks);
            dgv.Columns[0].Visible = false;

            comboBox_Cols.Items.AddRange(CBHeaders);
            comboBox_Cols.Items.Remove(CBHeaders[0]);
            comboBox_Cols.SelectedIndex = 0;
        }

        static DataTable callBackListToDataTable(List<CallBack> callBacksList)
        {
            DataTable dt = new DataTable();

            foreach (var item in CBHeaders)
            {
                dt.Columns.Add(item);
            }

            object[] values = new object[4];
            foreach (var item in callBacksList)
            {
                values[0] = item.fileId;
                values[1] = item.callBackName;
                values[2] = item.authorName;
                values[3] = item.updatedDate;
                dt.Rows.Add(values);
            }
            return dt;
        }

        private void button_Okay_Click(object sender, EventArgs e)
        {
            int cnt = dgv.SelectedRows.Count;
            if (cnt < 1)
                return;

            string idList = dgv.SelectedRows[0].Cells[0].Value.ToString();

            for (int i = 1; i < cnt; i++)
                idList += "," + dgv.SelectedRows[i].Cells[0].Value;

            List<string> ewaURLs = Program.client.GetCallBacks(idList, mf.isAdmin.ToString());

            if (ewaURLs.Count == 0)
                return;

            for (int i = 0; i < ewaURLs.Count; i++)
                ewaURLs[i] = Program.serverUrl + ewaURLs[i];

            TabControl tc = mf.tabCntrl;

            for (byte i = 0; i < ewaURLs.Count; i++)
            {
                tc.TabPages.Add("");
                tc.TabPages[tc.TabPages.Count - 1].Controls.Add(new WV2Tab(mf) { Source = new Uri(ewaURLs[i]) });
            }
            int last = tc.TabPages.Count - 1;

            tc.SelectedIndex = last;
        }

        private void textBox_Filter_TextChanged(object sender, EventArgs e)
        {
            (dgv.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", comboBox_Cols.SelectedItem, textBox_Filter.Text);
        }

        private void textBox_Filter_Enter(object sender, EventArgs e)
        {
            textBox_Filter.ForeColor = Color.Black;
            textBox_Filter.Text = "";
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Okay.Focus();
                button_Okay.PerformClick();
            }
        }
    }
}


