using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Designer;
using WordProcessor.Dom;

namespace WordProcessor.Dom
{
    public class DTextChar : DRowMember
    {
        public DTextChar()
        {
            MType = MemberType.TextChar;
        }

        public override void Paint(Graphics graphics)
        {
            graphics.DrawString(this.Value, this.WFont, new SolidBrush(this.FontColor), this.X,
                                         this.Y, EditorSetting.DefaultStringFormat);
            if (IsSelected)
                graphics.FillRectangle(SelectBrush, this.X, this.OwnerDocRow.Y, this.Width, this.OwnerDocRow.Height);
        }

        public override DMember Copy()
        {
            return new DTextChar()
                {
                    FontColor = this.FontColor,
                    WFont = this.WFont,
                    Value = this.Value,
                    IsBreakTail = this.IsBreakTail,
                    Width = this.Width,
                    Height = this.Height
                };
        }

        public override void MouseCapture()
        {
            OwnerDocRow.OwnerDocument.Context.ChangeCursor(MConstant.CursorType.Ibeam);
        }
    }
}
