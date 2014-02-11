using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WordProcessor.Designer;
using WordProcessor.Dom;

namespace WordProcessor.Dom
{
    public class DCheckBox : DRowMember
    {
        public static string CheckMark = "√";
        private static readonly Pen MPen = new Pen(Color.Black);
        private static readonly Brush MBrush = new SolidBrush(Color.Black);
        private static readonly Font MFont = new Font("Arial", 11, FontStyle.Bold);
        public bool IsChecked = true;

        private const int LeftMargin = 2;
        private const int RightMargin = 2;
        private const int RectLength = 15;
        private const int Offset = 1;

        public override int Y
        {
            get
            {
                return OwnerDocRow == null ? 0 : OwnerDocRow.Y + (OwnerDocRow.Height - this.Height)/2;
            }
        }

        public DCheckBox()
        {
            MType = MemberType.CheckBox;
            Width = LeftMargin + RectLength + RightMargin;
            Height = Width;
        }

        public override void Paint(System.Drawing.Graphics graphics)
        {
            graphics.DrawRectangle(MPen, this.X + LeftMargin, this.Y, RectLength, RectLength);
            graphics.DrawString(IsChecked ? CheckMark : "", MFont, MBrush, this.X + LeftMargin + Offset, this.Y + Offset);
        }

        public override void MouseClick()
        {
            IsChecked = !IsChecked;
            Invalidate(this.X, this.Y, this.Width, this.Height);
        }

        public override bool TouchMe(int x, int y)
        {
            IsTouch = x >= this.X && x <= this.X + this.Width;
            return IsTouch;
        }

        public override void MouseCapture()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            context.ChangeCursor(MConstant.CursorType.Default);
        }

        public override void MouseRelease()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            if (IsMouseEnter(context.MouseCurrentPosition.X, context.MouseCurrentPosition.Y)) return;
            context.ChangeCursor(MConstant.CursorType.Ibeam);
        }

        public override DMember Copy()
        {
            return new DCheckBox()
            {
                FontColor = this.FontColor,
                WFont = this.WFont,
                Value = this.Value,
                IsBreakTail = this.IsBreakTail,
                IsChecked = this.IsChecked,
                Width = this.Width,
                Height = this.Height
            };
        }
    }
}
