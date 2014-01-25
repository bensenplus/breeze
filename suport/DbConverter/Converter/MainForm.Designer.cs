namespace Converter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSQLitePath = new System.Windows.Forms.TextBox();
            this.btnBrowseSQLitePath = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxEncrypt = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserOracle = new System.Windows.Forms.TextBox();
            this.txtPassOracle = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cbxTriggers = new System.Windows.Forms.CheckBox();
            this.cbxCreateViews = new System.Windows.Forms.CheckBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtSid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbrProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "SQLite Database Path:";
            // 
            // txtSQLitePath
            // 
            this.txtSQLitePath.Location = new System.Drawing.Point(147, 14);
            this.txtSQLitePath.Name = "txtSQLitePath";
            this.txtSQLitePath.Size = new System.Drawing.Size(198, 21);
            this.txtSQLitePath.TabIndex = 11;
            this.txtSQLitePath.Text = "d:\\sqlite.db";
            this.txtSQLitePath.TextChanged += new System.EventHandler(this.txtSQLitePath_TextChanged);
            // 
            // btnBrowseSQLitePath
            // 
            this.btnBrowseSQLitePath.Location = new System.Drawing.Point(368, 14);
            this.btnBrowseSQLitePath.Name = "btnBrowseSQLitePath";
            this.btnBrowseSQLitePath.Size = new System.Drawing.Size(75, 21);
            this.btnBrowseSQLitePath.TabIndex = 12;
            this.btnBrowseSQLitePath.Text = "Browse...";
            this.btnBrowseSQLitePath.UseVisualStyleBackColor = true;
            this.btnBrowseSQLitePath.Click += new System.EventHandler(this.btnBrowseSQLitePath_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(602, 63);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 21);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "db";
            this.saveFileDialog1.Filter = "SQLite Files|*.db|All Files|*.*";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(626, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 21);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(721, 63);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 21);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxEncrypt
            // 
            this.cbxEncrypt.AutoSize = true;
            this.cbxEncrypt.Location = new System.Drawing.Point(9, 41);
            this.cbxEncrypt.Name = "cbxEncrypt";
            this.cbxEncrypt.Size = new System.Drawing.Size(144, 16);
            this.cbxEncrypt.TabIndex = 13;
            this.cbxEncrypt.Text = "Encryption password:";
            this.cbxEncrypt.UseVisualStyleBackColor = true;
            this.cbxEncrypt.CheckedChanged += new System.EventHandler(this.cbxEncrypt_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(148, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(197, 21);
            this.txtPassword.TabIndex = 14;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUserOracle
            // 
            this.txtUserOracle.Location = new System.Drawing.Point(426, 13);
            this.txtUserOracle.Name = "txtUserOracle";
            this.txtUserOracle.Size = new System.Drawing.Size(60, 21);
            this.txtUserOracle.TabIndex = 7;
            this.txtUserOracle.Text = "rhip";
            // 
            // txtPassOracle
            // 
            this.txtPassOracle.Location = new System.Drawing.Point(563, 13);
            this.txtPassOracle.Name = "txtPassOracle";
            this.txtPassOracle.PasswordChar = '*';
            this.txtPassOracle.Size = new System.Drawing.Size(54, 21);
            this.txtPassOracle.TabIndex = 9;
            this.txtPassOracle.Text = "rhip";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(382, 17);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(35, 12);
            this.lblUser.TabIndex = 6;
            this.lblUser.Text = "User:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(495, 17);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 12);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            // 
            // cbxTriggers
            // 
            this.cbxTriggers.AutoSize = true;
            this.cbxTriggers.Location = new System.Drawing.Point(9, 65);
            this.cbxTriggers.Name = "cbxTriggers";
            this.cbxTriggers.Size = new System.Drawing.Size(252, 16);
            this.cbxTriggers.TabIndex = 19;
            this.cbxTriggers.Text = "Create triggers enforcing foreign keys";
            this.cbxTriggers.UseVisualStyleBackColor = true;
            // 
            // cbxCreateViews
            // 
            this.cbxCreateViews.AutoSize = true;
            this.cbxCreateViews.Location = new System.Drawing.Point(280, 66);
            this.cbxCreateViews.Name = "cbxCreateViews";
            this.cbxCreateViews.Size = new System.Drawing.Size(312, 16);
            this.cbxCreateViews.TabIndex = 20;
            this.cbxCreateViews.Text = "Try to create views (works only in simple cases)";
            this.cbxCreateViews.UseVisualStyleBackColor = true;
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(90, 13);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(83, 21);
            this.txtIp.TabIndex = 21;
            this.txtIp.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "Oracle IP:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(226, 13);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(50, 21);
            this.txtPort.TabIndex = 24;
            this.txtPort.Text = "1521";
            // 
            // txtSid
            // 
            this.txtSid.Location = new System.Drawing.Point(323, 13);
            this.txtSid.Name = "txtSid";
            this.txtSid.Size = new System.Drawing.Size(50, 21);
            this.txtSid.TabIndex = 25;
            this.txtSid.Text = "orcl";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "SID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "Port:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column3,
            this.timestamp,
            this.Column5});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(861, 269);
            this.dataGridView1.TabIndex = 28;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage,
            this.pbrProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(861, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = false;
            this.lblMessage.AutoToolTip = true;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(400, 17);
            // 
            // pbrProgress
            // 
            this.pbrProgress.Name = "pbrProgress";
            this.pbrProgress.Size = new System.Drawing.Size(400, 16);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(89, 2);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(89, 21);
            this.btnDeselectAll.TabIndex = 31;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(8, 2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 21);
            this.btnSelectAll.TabIndex = 30;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblUser);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtUserOracle);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPassOracle);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtIp);
            this.groupBox1.Controls.Add(this.txtSid);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(861, 40);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtSQLitePath);
            this.groupBox3.Controls.Add(this.btnBrowseSQLitePath);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.cbxEncrypt);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.cbxCreateViews);
            this.groupBox3.Controls.Add(this.cbxTriggers);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 334);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(861, 90);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeselectAll);
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 309);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 25);
            this.panel1.TabIndex = 21;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "table name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "comment";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 200;
            // 
            // Column3
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column3.HeaderText = "rows";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // timestamp
            // 
            this.timestamp.HeaderText = "timestamp";
            this.timestamp.Name = "timestamp";
            this.timestamp.ReadOnly = true;
            this.timestamp.Width = 120;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "sqlite table";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 446);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "Oracle to SQLite DB Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtPassOracle;
        private System.Windows.Forms.TextBox txtUserOracle;

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSQLitePath;
        private System.Windows.Forms.Button btnBrowseSQLitePath;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbxEncrypt;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox cbxTriggers;
        private System.Windows.Forms.CheckBox cbxCreateViews;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtSid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripProgressBar pbrProgress;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}

