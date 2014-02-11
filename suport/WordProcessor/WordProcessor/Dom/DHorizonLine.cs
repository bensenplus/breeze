using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordProcessor.Designer;
using Timer = System.Threading.Timer;

namespace WordProcessor.Dom
{
    public class DHorizonLine : DRowMember
    {
        private const int LineThickness = 1;
        private static readonly Pen MPen = new Pen(Color.Black, LineThickness);
        private static readonly LinearGradientBrush LBrush =
            new LinearGradientBrush(new Rectangle(0, 0, RecLength, RecLength), Color.PowderBlue, Color.White,LinearGradientMode.Vertical);
        private static readonly Pen MPen2 = new Pen(Color.SteelBlue, 1);

        private const int TopMargin = 2;
        private const int BottomMargin = 2;
        private const int RecLength = TopMargin + LineThickness + BottomMargin;
        private int _dragWidth;
        private int _dragHeight;

        public DHorizonLine()
        {
            MType = MemberType.HorizonLine;
            this.Height = TopMargin + LineThickness + BottomMargin;
        }

        public override void AfterChangeRow()
        {
            if (this.X == 0) this.X = OwnerDocRow.X;
            if (this.Y == 0) this.Y = OwnerDocRow.Y;
            if (this.Width == 0) this.Width = OwnerDocRow.Width;
        }

        public override void Paint(Graphics graphics)
        {
            if (OwnerDocRow == null) return;

            graphics.DrawLine(MPen, this.X, this.Y + this.Height / 2, this.X + this.Width, this.Y + this.Height / 2);

            if (Focused)
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.FillEllipse(LBrush, this.X - 4.5f, this.Y - 4f, 9.0f, 9.0f);
                graphics.DrawEllipse(MPen2, this.X - 4.5f, this.Y - 4f, 9.0f, 9.0f);

                graphics.FillEllipse(LBrush, this.X + this.Width - 4.5f, this.Y - 4f, 9.0f, 9.0f);
                graphics.DrawEllipse(MPen2, this.X + this.Width - 4.5f, this.Y - 4f, 9.0f, 9.0f);
            }

            if (IsDrag)
            {
                graphics.DrawLine(MPen, this.X, this.Y + this.Height / 2, this.X + _dragWidth, _dragHeight);
            }
        }

        public override void MouseCapture()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            context.ChangeCursor(MConstant.CursorType.Default);

            var x = OwnerDocRow.OwnerDocument.Context.MouseCurrentPosition.X;
            var y = OwnerDocRow.OwnerDocument.Context.MouseCurrentPosition.Y;
            if (x > this.X + this.Width - 4.5f && y > this.Y - 4f)
            {
                context.ChangeCursor(MConstant.CursorType.Sizenwse);
            }
        }

        public override void MouseRelease()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            if (IsMouseEnter(context.MouseCurrentPosition.X, context.MouseCurrentPosition.Y)) return;
            context.ChangeCursor(MConstant.CursorType.Ibeam);
        }

        public override bool TouchMe(int x, int y)
        {
            IsTouch = x >= this.X && x <= this.X + this.Width;
            return IsTouch;
        }

        public override void MouseClick()
        {
            Focused = true;
            OwnerDocRow.OwnerDocument.Context.IsShowCursor = false;
            OwnerDocRow.OwnerDocument.Context.HideCursor();
            Invalidate(this.X,this.Y,this.Width,this.Height);
        }

        public override void MouseUnClick()
        {
            Focused = false;
            Invalidate(this.X, this.Y, this.Width, this.Height);
        }

        public override DMember Copy()
        {
            var horizonLine = new DHorizonLine()
            {
                FontColor = this.FontColor,
                WFont = this.WFont,
                Value = this.Value,
                IsBreakTail = this.IsBreakTail,
                Width = this.Width,
                Height = this.Height
            };

            return horizonLine;
        }

        public override void MouseDragBegin(int x, int y)
        {
            var mx = OwnerDocRow.OwnerDocument.Context.MouseCurrentPosition.X;
            var my = OwnerDocRow.OwnerDocument.Context.MouseCurrentPosition.Y;
            if (x > this.X + this.Width - 4.5f && y > this.Y - 4f)
            {
                _dragWidth = mx - this.X;
                _dragHeight = my;
                IsDrag = true;
                OwnerDocRow.OwnerDocument.Context.Invalidate();
            }
        }

        public override void MouseDragEnd()
        {
            IsDrag = false;
            this.Width = _dragWidth;
            OwnerDocRow.OwnerDocument.Context.Invalidate();
        }
    }
}
