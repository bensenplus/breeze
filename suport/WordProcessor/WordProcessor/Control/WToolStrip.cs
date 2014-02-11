using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WordProcessor.API;

namespace WordProcessor.Control
{
    public sealed class WToolStrip : ToolStripDropDown
    {
        public Delegate AfterSelected;

        private readonly ToolStripControlHost _toolStripHostOfDataGrid;
        private readonly DataGridView _dataGrid = new DataGridView
        {
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeColumns = false,
            AllowUserToResizeRows = false,
            ColumnHeadersVisible = false,
            RowHeadersVisible = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            MultiSelect = false,
            CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        };

        public WToolStrip()
        {
            var form = new Form();
            form.Controls.Add(_dataGrid);
            form.SuspendLayout();
            _toolStripHostOfDataGrid = new ToolStripControlHost(_dataGrid);
            this.Items.Add(_toolStripHostOfDataGrid);
            _dataGrid.CellMouseDoubleClick += _dataGrid_CellMouseDoubleClick;
        }

        void _dataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (AfterSelected != null)
                AfterSelected.DynamicInvoke(Convert.ToString(_dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue));
            this.Close();
        }

        public void SetDataSource<T>(List<T> dataSource, Delegate method)
        {
            _dataGrid.DataSource = dataSource;
            AfterSelected = method;
        }

        protected override void OnResize(EventArgs e)
        {
            if (_dataGrid.Columns.Count > 0)
            {
                this.Width = _dataGrid.Columns[0].Width + 5;
                _toolStripHostOfDataGrid.Width = _dataGrid.Columns[0].Width + 5;
                _dataGrid.Width = _dataGrid.Columns[0].Width + 5;
            }
 	        base.OnResize(e);
        }

    }
}
