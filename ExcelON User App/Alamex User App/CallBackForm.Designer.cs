namespace ExcelON_User_App
{
    partial class CallBackForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallBackForm));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.button_Okay = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.textBox_Filter = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.comboBox_Cols = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.OrangeRed;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(0, 45);
            this.dgv.Margin = new System.Windows.Forms.Padding(2);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(500, 382);
            this.dgv.TabIndex = 0;
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_KeyDown);
            // 
            // button_Okay
            // 
            this.button_Okay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Okay.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Okay.Location = new System.Drawing.Point(80, 431);
            this.button_Okay.Margin = new System.Windows.Forms.Padding(2);
            this.button_Okay.Name = "button_Okay";
            this.button_Okay.Size = new System.Drawing.Size(135, 30);
            this.button_Okay.TabIndex = 1;
            this.button_Okay.Text = "Okay";
            this.button_Okay.UseVisualStyleBackColor = true;
            this.button_Okay.Click += new System.EventHandler(this.button_Okay_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Cancel.Location = new System.Drawing.Point(297, 431);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(135, 30);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // textBox_Filter
            // 
            this.textBox_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Filter.ForeColor = System.Drawing.Color.Gray;
            this.textBox_Filter.Location = new System.Drawing.Point(286, 9);
            this.textBox_Filter.Name = "textBox_Filter";
            this.textBox_Filter.Size = new System.Drawing.Size(204, 26);
            this.textBox_Filter.TabIndex = 3;
            this.textBox_Filter.Text = "Input value here";
            this.textBox_Filter.TextChanged += new System.EventHandler(this.textBox_Filter_TextChanged);
            this.textBox_Filter.Enter += new System.EventHandler(this.textBox_Filter_Enter);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(12, 12);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(134, 20);
            this.label.TabIndex = 4;
            this.label.Text = "Filter CallBack by:";
            // 
            // comboBox_Cols
            // 
            this.comboBox_Cols.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Cols.FormattingEnabled = true;
            this.comboBox_Cols.Location = new System.Drawing.Point(152, 9);
            this.comboBox_Cols.Name = "comboBox_Cols";
            this.comboBox_Cols.Size = new System.Drawing.Size(121, 28);
            this.comboBox_Cols.TabIndex = 5;
            // 
            // CallBackForm
            // 
            this.AcceptButton = this.button_Okay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(502, 472);
            this.Controls.Add(this.comboBox_Cols);
            this.Controls.Add(this.label);
            this.Controls.Add(this.textBox_Filter);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Okay);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "CallBackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Call Back Files";
            this.Load += new System.EventHandler(this.CallBackForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button button_Okay;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TextBox textBox_Filter;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox comboBox_Cols;
    }
}