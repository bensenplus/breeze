using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordProcessor.Command;

namespace WordProcessor.Control
{
    public partial class WToolBarButton : ToolStripButton
    {
        public WCommandType ComType;

        public WToolBarButton()
        {
            InitializeComponent();
        }

        protected override void OnClick(EventArgs e)
        {
            var command = WEditorView.GetCommand(ComType);
            if (command != null) command.Execute();
            base.OnClick(e);
        }
    }
}
