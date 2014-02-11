namespace WordProcessor.Control
{
    partial class WEditorView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WEditorView));
            this.imageList8 = new System.Windows.Forms.ImageList(this.components);
            this.TableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMergeCell = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSplitCell = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsertRowUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsertRowDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsertColumnLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsertColumnRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.TableMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList8
            // 
            this.imageList8.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList8.ImageStream")));
            this.imageList8.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList8.Images.SetKeyName(0, "enterArrow.jpg");
            // 
            // TableMenu
            // 
            this.TableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMergeCell,
            this.tsmiSplitCell,
            this.tsmiInsert,
            this.tsmiDelete});
            this.TableMenu.Name = "TableMenu";
            this.TableMenu.Size = new System.Drawing.Size(157, 92);
            // 
            // tsmiMergeCell
            // 
            this.tsmiMergeCell.Name = "tsmiMergeCell";
            this.tsmiMergeCell.Size = new System.Drawing.Size(156, 22);
            this.tsmiMergeCell.Text = "合并单元格(&M)";
            // 
            // tsmiSplitCell
            // 
            this.tsmiSplitCell.Name = "tsmiSplitCell";
            this.tsmiSplitCell.Size = new System.Drawing.Size(156, 22);
            this.tsmiSplitCell.Text = "拆分单元格";
            // 
            // tsmiInsert
            // 
            this.tsmiInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInsertRowUp,
            this.tsmiInsertRowDown,
            this.tsmiInsertColumnLeft,
            this.tsmiInsertColumnRight});
            this.tsmiInsert.Name = "tsmiInsert";
            this.tsmiInsert.Size = new System.Drawing.Size(156, 22);
            this.tsmiInsert.Text = "插入(&I)";
            // 
            // tsmiInsertRowUp
            // 
            this.tsmiInsertRowUp.Name = "tsmiInsertRowUp";
            this.tsmiInsertRowUp.Size = new System.Drawing.Size(165, 22);
            this.tsmiInsertRowUp.Text = "在上方插入行(&U)";
            // 
            // tsmiInsertRowDown
            // 
            this.tsmiInsertRowDown.Name = "tsmiInsertRowDown";
            this.tsmiInsertRowDown.Size = new System.Drawing.Size(165, 22);
            this.tsmiInsertRowDown.Text = "在下方插入行(&D)";
            // 
            // tsmiInsertColumnLeft
            // 
            this.tsmiInsertColumnLeft.Name = "tsmiInsertColumnLeft";
            this.tsmiInsertColumnLeft.Size = new System.Drawing.Size(165, 22);
            this.tsmiInsertColumnLeft.Text = "在左侧插入列(&L)";
            // 
            // tsmiInsertColumnRight
            // 
            this.tsmiInsertColumnRight.Name = "tsmiInsertColumnRight";
            this.tsmiInsertColumnRight.Size = new System.Drawing.Size(165, 22);
            this.tsmiInsertColumnRight.Text = "在右侧插入列(&R)";
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(156, 22);
            this.tsmiDelete.Text = "删除单元格...";
            // 
            // WEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Name = "WEditorView";
            this.Size = new System.Drawing.Size(146, 119);
            this.TableMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList8;
        private System.Windows.Forms.ContextMenuStrip TableMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiMergeCell;
        private System.Windows.Forms.ToolStripMenuItem tsmiSplitCell;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsert;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertRowUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertRowDown;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertColumnLeft;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertColumnRight;



    }
}
