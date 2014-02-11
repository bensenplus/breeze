using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordProcessor.WinForm
{
    public partial class TableDialog : Form
    {
        public int RowNum;
        public int ColNum;

        public TableDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RowNum = Convert.ToInt32(nudRowNum.Value);
            ColNum = Convert.ToInt32(nudColNum.Value);
            DialogResult = DialogResult.OK;
        }
    }
}
