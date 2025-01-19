namespace ExcelON_User_App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip_webview = new System.Windows.Forms.ToolStrip();
            this.tsb_NewFile = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CloseTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Rename = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveAsPdf = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveSession = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveCallBack = new System.Windows.Forms.ToolStripButton();
            this.tsb_MakeCallBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_ConvertToNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_About = new System.Windows.Forms.ToolStripButton();
            this.tabCntrl = new System.Windows.Forms.TabControl();
            this.toolStrip_webview.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_webview
            // 
            this.toolStrip_webview.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip_webview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_webview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_NewFile,
            this.tsb_Refresh,
            this.toolStripButton_CloseTab,
            this.toolStripButton_Rename,
            this.tsb_SaveAsPdf,
            this.tsb_SaveSession,
            this.toolStripButton_Delete,
            this.tsb_SaveCallBack,
            this.tsb_MakeCallBack,
            this.toolStripButton_ConvertToNew,
            this.toolStripButton_About});
            this.toolStrip_webview.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_webview.Name = "toolStrip_webview";
            this.toolStrip_webview.Size = new System.Drawing.Size(1437, 32);
            this.toolStrip_webview.TabIndex = 1;
            // 
            // tsb_NewFile
            // 
            this.tsb_NewFile.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_NewFile.Image = global::ExcelON_User_App.Properties.Resources._new;
            this.tsb_NewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_NewFile.Name = "tsb_NewFile";
            this.tsb_NewFile.Size = new System.Drawing.Size(108, 29);
            this.tsb_NewFile.Text = "New File";
            this.tsb_NewFile.Click += new System.EventHandler(this.toolStripButton_NewFile_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.Image = global::ExcelON_User_App.Properties.Resources.refresh;
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(98, 29);
            this.tsb_Refresh.Text = "Refresh";
            this.tsb_Refresh.Click += new System.EventHandler(this.toolStripButton_Refresh_Click);
            // 
            // toolStripButton_CloseTab
            // 
            this.toolStripButton_CloseTab.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_CloseTab.Image = global::ExcelON_User_App.Properties.Resources.window_close;
            this.toolStripButton_CloseTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CloseTab.Name = "toolStripButton_CloseTab";
            this.toolStripButton_CloseTab.Size = new System.Drawing.Size(116, 29);
            this.toolStripButton_CloseTab.Text = "Close Tab";
            this.toolStripButton_CloseTab.Click += new System.EventHandler(this.toolStripButton_CloseTab_Click);
            // 
            // toolStripButton_Rename
            // 
            this.toolStripButton_Rename.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_Rename.Image = global::ExcelON_User_App.Properties.Resources.rename;
            this.toolStripButton_Rename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Rename.Name = "toolStripButton_Rename";
            this.toolStripButton_Rename.Size = new System.Drawing.Size(103, 29);
            this.toolStripButton_Rename.Text = "Rename";
            this.toolStripButton_Rename.Click += new System.EventHandler(this.toolStripButton_Rename_Click);
            // 
            // tsb_SaveAsPdf
            // 
            this.tsb_SaveAsPdf.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveAsPdf.Image = global::ExcelON_User_App.Properties.Resources.pdf_file_icon;
            this.tsb_SaveAsPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveAsPdf.Name = "tsb_SaveAsPdf";
            this.tsb_SaveAsPdf.Size = new System.Drawing.Size(138, 29);
            this.tsb_SaveAsPdf.Text = "Save As PDF";
            this.tsb_SaveAsPdf.Click += new System.EventHandler(this.toolStripButton_SaveAsPdf_Click);
            // 
            // tsb_SaveSession
            // 
            this.tsb_SaveSession.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveSession.Image = global::ExcelON_User_App.Properties.Resources.Microsoft_Excel_icon;
            this.tsb_SaveSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveSession.Name = "tsb_SaveSession";
            this.tsb_SaveSession.Size = new System.Drawing.Size(143, 29);
            this.tsb_SaveSession.Text = "Save Session";
            this.tsb_SaveSession.ToolTipText = "Save Session";
            this.tsb_SaveSession.Click += new System.EventHandler(this.toolStripButton_SaveSession_Click);
            // 
            // toolStripButton_Delete
            // 
            this.toolStripButton_Delete.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_Delete.Image = global::ExcelON_User_App.Properties.Resources.delete;
            this.toolStripButton_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Delete.Name = "toolStripButton_Delete";
            this.toolStripButton_Delete.Size = new System.Drawing.Size(124, 29);
            this.toolStripButton_Delete.Text = "Delete File";
            this.toolStripButton_Delete.Click += new System.EventHandler(this.toolStripButton_Delete_Click);
            // 
            // tsb_SaveCallBack
            // 
            this.tsb_SaveCallBack.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveCallBack.Image = global::ExcelON_User_App.Properties.Resources.Database_Upload_icon;
            this.tsb_SaveCallBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveCallBack.Name = "tsb_SaveCallBack";
            this.tsb_SaveCallBack.Size = new System.Drawing.Size(151, 29);
            this.tsb_SaveCallBack.Text = "Save CallBack";
            this.tsb_SaveCallBack.Click += new System.EventHandler(this.toolStripButton_SaveCallBack_Click);
            // 
            // tsb_MakeCallBack
            // 
            this.tsb_MakeCallBack.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_MakeCallBack.Image = global::ExcelON_User_App.Properties.Resources.excel_import;
            this.tsb_MakeCallBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MakeCallBack.Name = "tsb_MakeCallBack";
            this.tsb_MakeCallBack.Size = new System.Drawing.Size(158, 29);
            this.tsb_MakeCallBack.Text = "Make CallBack";
            this.tsb_MakeCallBack.Click += new System.EventHandler(this.toolStripButton_CallBack_Click);
            // 
            // toolStripButton_ConvertToNew
            // 
            this.toolStripButton_ConvertToNew.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton_ConvertToNew.Image = global::ExcelON_User_App.Properties.Resources.change_an_icon_13;
            this.toolStripButton_ConvertToNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ConvertToNew.Name = "toolStripButton_ConvertToNew";
            this.toolStripButton_ConvertToNew.Size = new System.Drawing.Size(169, 29);
            this.toolStripButton_ConvertToNew.Text = "Convert To New";
            this.toolStripButton_ConvertToNew.Click += new System.EventHandler(this.toolStripButton_ConvertToNewFile_Click);
            // 
            // toolStripButton_About
            // 
            this.toolStripButton_About.Image = global::ExcelON_User_App.Properties.Resources.about_us;
            this.toolStripButton_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_About.Name = "toolStripButton_About";
            this.toolStripButton_About.Size = new System.Drawing.Size(98, 29);
            this.toolStripButton_About.Text = "About Us";
            this.toolStripButton_About.Click += new System.EventHandler(this.toolStripButton_About_Click);
            // 
            // tabCntrl
            // 
            this.tabCntrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCntrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCntrl.Location = new System.Drawing.Point(0, 32);
            this.tabCntrl.Name = "tabCntrl";
            this.tabCntrl.SelectedIndex = 0;
            this.tabCntrl.Size = new System.Drawing.Size(1437, 510);
            this.tabCntrl.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1437, 542);
            this.Controls.Add(this.tabCntrl);
            this.Controls.Add(this.toolStrip_webview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cheeta Sheets User App";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip_webview.ResumeLayout(false);
            this.toolStrip_webview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip_webview;
        private System.Windows.Forms.ToolStripButton tsb_SaveAsPdf;
        private System.Windows.Forms.ToolStripButton tsb_SaveSession;
        internal System.Windows.Forms.TabControl tabCntrl;
        private System.Windows.Forms.ToolStripButton tsb_MakeCallBack;
        private System.Windows.Forms.ToolStripButton tsb_NewFile;
        private System.Windows.Forms.ToolStripButton toolStripButton_Delete;
        private System.Windows.Forms.ToolStripButton toolStripButton_CloseTab;
        private System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ToolStripButton toolStripButton_Rename;
        private System.Windows.Forms.ToolStripButton tsb_SaveCallBack;
        private System.Windows.Forms.ToolStripButton toolStripButton_ConvertToNew;
        private System.Windows.Forms.ToolStripButton toolStripButton_About;
    }
}

