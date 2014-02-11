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
using WordProcessor.Designer;

namespace WordProcessor.Control
{
    public partial class Emre : UserControl
    {
        public Emre()
        {
            InitializeComponent();
            SetCommand();
        }

        private void SetCommand()
        {
            wEditView.NotifyChange = NotifyChange;
            wtbNewDoc.ComType = WCommandType.NewDocument;
            wtbInputText.ComType = WCommandType.NewTextInput;
            wtbRowLine.ComType = WCommandType.Null;
            wtbTable.ComType = WCommandType.NewTable;
            wtbCheckBox.ComType = WCommandType.NewCheckBox;
            wtbComboBox.ComType = WCommandType.NewComboBox;
            wtbHline.ComType = WCommandType.NewHorizonLine;
            wtbImage.ComType = WCommandType.NewImage;
            wtbXmlImport.ComType = WCommandType.XmlImport;
            wtbXmlExport.ComType = WCommandType.XmlExport;
            wtbFont.ComType = WCommandType.FontSet;
            wtbColor.ComType = WCommandType.FontColorSet;
            wtbLeft.ComType = WCommandType.ParagraphLeft;
            wtbMiddle.ComType = WCommandType.ParagraphMiddle;
            wtbRight.ComType = WCommandType.ParagraphRight;
            wtbRowIndex.ComType = WCommandType.Null;
        }

        private void NotifyChange(NotifyArags notifyArags)
        {
            sbpCoordinate.Text = @"鼠标坐标:X/Y=" + notifyArags.Coordinate.X + @"/" + notifyArags.Coordinate.Y;
            sbpPage.Text = @"页面:"+notifyArags.CurrentPageNumber+@"/"+notifyArags.PageCount;
            sbpNumber.Text = @"字数:"+notifyArags.CharCount;
            sbpZoom.Text = notifyArags.Flag ? "断行," : "";
            sbpZoom.Text += @"当前行坐标：X/Y=" + notifyArags.CurrentRowLocation.X + @"/" + notifyArags.CurrentRowLocation.Y;
        }

        private void tcbFontSize_TextChanged(object sender, EventArgs e)
        {
            var text = tcbFontSize.SelectedItem;
            try
            {
                var num = Convert.ToInt32(text);
                EditorSetting.CurrentFont = new Font("Arial", num);
            }
            catch
            {
                
            }
        }

        private void wtbRowLine_Click(object sender, EventArgs e)
        {
            EditorSetting.IsShowGridLine = !EditorSetting.IsShowGridLine;
        }

        private void wtbRowIndex_Click(object sender, EventArgs e)
        {
            EditorSetting.IsShowRowIndex = !EditorSetting.IsShowRowIndex;
        }

    }
}
