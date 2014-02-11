
namespace WordProcessor.Control
{
    partial class Emre
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Emre));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.wToolBar = new WordProcessor.Control.WToolBar();
            this.wtbNewDoc = new WordProcessor.Control.WToolBarButton();
            this.wtbFont = new WordProcessor.Control.WToolBarButton();
            this.wtbColor = new WordProcessor.Control.WToolBarButton();
            this.tcbFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.wtbInputText = new WordProcessor.Control.WToolBarButton();
            this.wtbTable = new WordProcessor.Control.WToolBarButton();
            this.wtbRowLine = new WordProcessor.Control.WToolBarButton();
            this.wtbCheckBox = new WordProcessor.Control.WToolBarButton();
            this.wtbComboBox = new WordProcessor.Control.WToolBarButton();
            this.wtbHline = new WordProcessor.Control.WToolBarButton();
            this.wtbImage = new WordProcessor.Control.WToolBarButton();
            this.wtbRowIndex = new WordProcessor.Control.WToolBarButton();
            this.wtbXmlExport = new WordProcessor.Control.WToolBarButton();
            this.wtbXmlImport = new WordProcessor.Control.WToolBarButton();
            this.wtbLeft = new WordProcessor.Control.WToolBarButton();
            this.wtbMiddle = new WordProcessor.Control.WToolBarButton();
            this.wtbRight = new WordProcessor.Control.WToolBarButton();
            this.wStatusBar = new WordProcessor.Control.WStatusBar();
            this.sbpPage = new System.Windows.Forms.StatusBarPanel();
            this.sbpNumber = new System.Windows.Forms.StatusBarPanel();
            this.sbpCoordinate = new System.Windows.Forms.StatusBarPanel();
            this.sbpZoom = new System.Windows.Forms.StatusBarPanel();
            this.wEditView = new WordProcessor.Control.WEditorView();
            this.wEditorView1 = new WordProcessor.Control.WEditorView();
            this.tableLayoutPanel1.SuspendLayout();
            this.wToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbpPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpCoordinate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.wToolBar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.wStatusBar, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.wEditorView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(951, 453);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // wToolBar
            // 
            this.wToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wtbNewDoc,
            this.wtbFont,
            this.wtbColor,
            this.tcbFontSize,
            this.wtbInputText,
            this.wtbTable,
            this.wtbRowLine,
            this.wtbCheckBox,
            this.wtbComboBox,
            this.wtbHline,
            this.wtbImage,
            this.wtbRowIndex,
            this.wtbXmlExport,
            this.wtbXmlImport,
            this.wtbLeft,
            this.wtbMiddle,
            this.wtbRight});
            this.wToolBar.Location = new System.Drawing.Point(0, 0);
            this.wToolBar.Name = "wToolBar";
            this.wToolBar.Size = new System.Drawing.Size(951, 25);
            this.wToolBar.TabIndex = 2;
            this.wToolBar.Text = "wToolBar1";
            // 
            // wtbNewDoc
            // 
            this.wtbNewDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("wtbNewDoc.Image")));
            this.wtbNewDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbNewDoc.Name = "wtbNewDoc";
            this.wtbNewDoc.Size = new System.Drawing.Size(48, 22);
            this.wtbNewDoc.Text = "新文档";
            // 
            // wtbFont
            // 
            this.wtbFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbFont.Image = ((System.Drawing.Image)(resources.GetObject("wtbFont.Image")));
            this.wtbFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbFont.Name = "wtbFont";
            this.wtbFont.Size = new System.Drawing.Size(36, 22);
            this.wtbFont.Text = "字体";
            // 
            // wtbColor
            // 
            this.wtbColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbColor.Image = ((System.Drawing.Image)(resources.GetObject("wtbColor.Image")));
            this.wtbColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbColor.Name = "wtbColor";
            this.wtbColor.Size = new System.Drawing.Size(60, 22);
            this.wtbColor.Text = "字体颜色";
            // 
            // tcbFontSize
            // 
            this.tcbFontSize.Items.AddRange(new object[] {
            "9",
            "10",
            "11",
            "16",
            "25",
            "30",
            "35",
            "40",
            "50",
            "60",
            "70"});
            this.tcbFontSize.Name = "tcbFontSize";
            this.tcbFontSize.Size = new System.Drawing.Size(121, 25);
            this.tcbFontSize.Text = "字体大小";
            this.tcbFontSize.TextChanged += new System.EventHandler(this.tcbFontSize_TextChanged);
            // 
            // wtbInputText
            // 
            this.wtbInputText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbInputText.Image = ((System.Drawing.Image)(resources.GetObject("wtbInputText.Image")));
            this.wtbInputText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbInputText.Name = "wtbInputText";
            this.wtbInputText.Size = new System.Drawing.Size(48, 22);
            this.wtbInputText.Text = "文本域";
            // 
            // wtbTable
            // 
            this.wtbTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbTable.Image = ((System.Drawing.Image)(resources.GetObject("wtbTable.Image")));
            this.wtbTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbTable.Name = "wtbTable";
            this.wtbTable.Size = new System.Drawing.Size(36, 22);
            this.wtbTable.Text = "表格";
            // 
            // wtbRowLine
            // 
            this.wtbRowLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbRowLine.Image = ((System.Drawing.Image)(resources.GetObject("wtbRowLine.Image")));
            this.wtbRowLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbRowLine.Name = "wtbRowLine";
            this.wtbRowLine.Size = new System.Drawing.Size(48, 22);
            this.wtbRowLine.Text = "网格线";
            this.wtbRowLine.Click += new System.EventHandler(this.wtbRowLine_Click);
            // 
            // wtbCheckBox
            // 
            this.wtbCheckBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbCheckBox.Image = ((System.Drawing.Image)(resources.GetObject("wtbCheckBox.Image")));
            this.wtbCheckBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbCheckBox.Name = "wtbCheckBox";
            this.wtbCheckBox.Size = new System.Drawing.Size(48, 22);
            this.wtbCheckBox.Text = "复选框";
            // 
            // wtbComboBox
            // 
            this.wtbComboBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbComboBox.Image = ((System.Drawing.Image)(resources.GetObject("wtbComboBox.Image")));
            this.wtbComboBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbComboBox.Name = "wtbComboBox";
            this.wtbComboBox.Size = new System.Drawing.Size(48, 22);
            this.wtbComboBox.Text = "下拉框";
            // 
            // wtbHline
            // 
            this.wtbHline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbHline.Image = ((System.Drawing.Image)(resources.GetObject("wtbHline.Image")));
            this.wtbHline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbHline.Name = "wtbHline";
            this.wtbHline.Size = new System.Drawing.Size(48, 22);
            this.wtbHline.Text = "水平线";
            // 
            // wtbImage
            // 
            this.wtbImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbImage.Image = ((System.Drawing.Image)(resources.GetObject("wtbImage.Image")));
            this.wtbImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbImage.Name = "wtbImage";
            this.wtbImage.Size = new System.Drawing.Size(36, 22);
            this.wtbImage.Text = "图片";
            // 
            // wtbRowIndex
            // 
            this.wtbRowIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbRowIndex.Image = ((System.Drawing.Image)(resources.GetObject("wtbRowIndex.Image")));
            this.wtbRowIndex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbRowIndex.Name = "wtbRowIndex";
            this.wtbRowIndex.Size = new System.Drawing.Size(36, 22);
            this.wtbRowIndex.Text = "行号";
            this.wtbRowIndex.Click += new System.EventHandler(this.wtbRowIndex_Click);
            // 
            // wtbXmlExport
            // 
            this.wtbXmlExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbXmlExport.Image = ((System.Drawing.Image)(resources.GetObject("wtbXmlExport.Image")));
            this.wtbXmlExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbXmlExport.Name = "wtbXmlExport";
            this.wtbXmlExport.Size = new System.Drawing.Size(62, 22);
            this.wtbXmlExport.Text = "导出XML";
            // 
            // wtbXmlImport
            // 
            this.wtbXmlImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbXmlImport.Image = ((System.Drawing.Image)(resources.GetObject("wtbXmlImport.Image")));
            this.wtbXmlImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbXmlImport.Name = "wtbXmlImport";
            this.wtbXmlImport.Size = new System.Drawing.Size(62, 22);
            this.wtbXmlImport.Text = "导入XML";
            // 
            // wtbLeft
            // 
            this.wtbLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbLeft.Image = ((System.Drawing.Image)(resources.GetObject("wtbLeft.Image")));
            this.wtbLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbLeft.Name = "wtbLeft";
            this.wtbLeft.Size = new System.Drawing.Size(36, 22);
            this.wtbLeft.Text = "居左";
            // 
            // wtbMiddle
            // 
            this.wtbMiddle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbMiddle.Image = ((System.Drawing.Image)(resources.GetObject("wtbMiddle.Image")));
            this.wtbMiddle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbMiddle.Name = "wtbMiddle";
            this.wtbMiddle.Size = new System.Drawing.Size(36, 22);
            this.wtbMiddle.Text = "居中";
            // 
            // wtbRight
            // 
            this.wtbRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.wtbRight.Image = ((System.Drawing.Image)(resources.GetObject("wtbRight.Image")));
            this.wtbRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wtbRight.Name = "wtbRight";
            this.wtbRight.Size = new System.Drawing.Size(36, 22);
            this.wtbRight.Text = "居右";
            // 
            // wStatusBar
            // 
            this.wStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wStatusBar.Location = new System.Drawing.Point(0, 428);
            this.wStatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.wStatusBar.Name = "wStatusBar";
            this.wStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpPage,
            this.sbpNumber,
            this.sbpCoordinate,
            this.sbpZoom});
            this.wStatusBar.ShowPanels = true;
            this.wStatusBar.Size = new System.Drawing.Size(951, 25);
            this.wStatusBar.TabIndex = 3;
            this.wStatusBar.Text = "wStatusBar1";
            // 
            // sbpPage
            // 
            this.sbpPage.Name = "sbpPage";
            this.sbpPage.Text = "页面:1/1";
            this.sbpPage.Width = 80;
            // 
            // sbpNumber
            // 
            this.sbpNumber.Name = "sbpNumber";
            this.sbpNumber.Text = "字数:0";
            this.sbpNumber.Width = 70;
            // 
            // sbpCoordinate
            // 
            this.sbpCoordinate.Name = "sbpCoordinate";
            this.sbpCoordinate.Text = "坐标:X轴=0,Y轴=0";
            this.sbpCoordinate.Width = 150;
            // 
            // sbpZoom
            // 
            this.sbpZoom.Name = "sbpZoom";
            this.sbpZoom.Width = 300;
            // 
            // wEditView
            // 
            this.wEditView.AutoScroll = true;
            this.wEditView.AutoScrollMinSize = new System.Drawing.Size(860, 1045);
            this.wEditView.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.wEditView.BackColor = System.Drawing.Color.Silver;
            this.wEditView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.wEditView.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.wEditView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wEditView.Location = new System.Drawing.Point(0, 25);
            this.wEditView.Margin = new System.Windows.Forms.Padding(0);
            this.wEditView.Name = "wEditView";
            this.wEditView.Size = new System.Drawing.Size(951, 403);
            this.wEditView.TabIndex = 1;
            // 
            // wEditorView1
            // 
            this.wEditorView1.AutoScroll = true;
            this.wEditorView1.AutoScrollMinSize = new System.Drawing.Size(860, 1045);
            this.wEditorView1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.wEditorView1.BackColor = System.Drawing.Color.Transparent;
            this.wEditorView1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.wEditorView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wEditorView1.Location = new System.Drawing.Point(3, 28);
            this.wEditorView1.Name = "wEditorView1";
            this.wEditorView1.Size = new System.Drawing.Size(945, 397);
            this.wEditorView1.TabIndex = 4;
            // 
            // Emre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Emre";
            this.Size = new System.Drawing.Size(951, 453);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.wToolBar.ResumeLayout(false);
            this.wToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbpPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpCoordinate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WEditorView wEditView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private WToolBar wToolBar;
        private WStatusBar wStatusBar;
        private System.Windows.Forms.StatusBarPanel sbpPage;
        private System.Windows.Forms.StatusBarPanel sbpNumber;
        private System.Windows.Forms.StatusBarPanel sbpCoordinate;
        private System.Windows.Forms.StatusBarPanel sbpZoom;
        private System.Windows.Forms.ToolStripComboBox tcbFontSize;
        private WToolBarButton wtbInputText;
        private WToolBarButton wtbRowLine;
        private WToolBarButton wtbTable;
        private WToolBarButton wtbCheckBox;
        private WToolBarButton wtbComboBox;
        private WToolBarButton wtbHline;
        private WToolBarButton wtbImage;
        private WToolBarButton wtbRowIndex;
        private WToolBarButton wtbXmlExport;
        private WToolBarButton wtbXmlImport;
        private WToolBarButton wtbNewDoc;
        private WToolBarButton wtbFont;
        private WToolBarButton wtbColor;
        private WToolBarButton wtbLeft;
        private WToolBarButton wtbMiddle;
        private WToolBarButton wtbRight;
        private WEditorView wEditorView1;

    }
}
