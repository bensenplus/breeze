namespace WordProcessor.WinForm
{
    partial class TableDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudRowNum = new System.Windows.Forms.NumericUpDown();
            this.nudColNum = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "行数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "列数";
            // 
            // nudRowNum
            // 
            this.nudRowNum.Location = new System.Drawing.Point(56, 17);
            this.nudRowNum.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudRowNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRowNum.Name = "nudRowNum";
            this.nudRowNum.Size = new System.Drawing.Size(56, 21);
            this.nudRowNum.TabIndex = 1;
            this.nudRowNum.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // nudColNum
            // 
            this.nudColNum.Location = new System.Drawing.Point(56, 44);
            this.nudColNum.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudColNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColNum.Name = "nudColNum";
            this.nudColNum.Size = new System.Drawing.Size(56, 21);
            this.nudColNum.TabIndex = 1;
            this.nudColNum.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(37, 85);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定(&E)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // TableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(143, 118);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudColNum);
            this.Controls.Add(this.nudRowNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableDialog";
            this.Text = "表格属性";
            ((System.ComponentModel.ISupportInitialize)(this.nudRowNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudRowNum;
        private System.Windows.Forms.NumericUpDown nudColNum;
        private System.Windows.Forms.Button btnOK;
    }
}