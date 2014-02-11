using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Designer;

namespace WordProcessor.Dom
{
    public class DImage : DRowMember
    {
        private static readonly Brush MBrush1 = new SolidBrush(Color.Blue);
        private static readonly Brush MBrush2 = new SolidBrush(Color.FromArgb(32, Color.Blue));

        private static readonly LinearGradientBrush LBrush =
            new LinearGradientBrush(new Rectangle(0, 0, RecLength, RecLength), Color.PowderBlue, Color.White,LinearGradientMode.Vertical);
        private static readonly Pen MPen = new Pen(Color.SteelBlue, 1);
        private static readonly Pen MPen2 = new Pen(Color.Black, 1);

        private const int RecLength = 5;
        private const int Offset = 4;
        private const int Offset2 = 2;
        private int _dragWidth;
        private int _dragHeight;

        public Image MImage;

        public DImage(Image image)
        {
            MType = MemberType.Image;
            MImage = image;
            this.Width = image.Width;
            this.Height = image.Height;
            LBrush.SetBlendTriangularShape(0.5f);
            MPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            MPen2.DashPattern = new float[] { 3, 2 };
        }

        public override void Paint(Graphics graphics)
        {
            if (MImage == null) return;

            graphics.DrawImage(MImage, this.X, this.Y, this.Width, this.Height);

            if (Focused)
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawRectangle(MPen, this.X, this.Y, this.Width, this.Height);

                var length = RecLength + Offset;

                graphics.FillEllipse(LBrush, this.X - Offset, this.Y - Offset, length, length);
                graphics.DrawEllipse(MPen, this.X - Offset, this.Y - Offset, length, length);

                graphics.FillEllipse(LBrush, this.X + this.Width - RecLength, this.Y - Offset, length, length);
                graphics.DrawEllipse(MPen, this.X + this.Width - RecLength, this.Y - Offset, length, length);

                graphics.FillEllipse(LBrush, this.X - Offset, this.Y + this.Height - RecLength, length, length);
                graphics.DrawEllipse(MPen, this.X - Offset, this.Y + this.Height - RecLength, length, length);

                graphics.FillEllipse(LBrush, this.X + this.Width - RecLength, this.Y + this.Height - RecLength, length, length);
                graphics.DrawEllipse(MPen, this.X + this.Width - RecLength, this.Y + this.Height - RecLength, length, length);


                length = RecLength + Offset2;

                graphics.FillRectangle(LBrush, this.X - length / 2, this.Y + (this.Height - length) / 2, length, length);
                graphics.DrawRectangle(MPen, this.X - length / 2, this.Y + (this.Height - length) / 2, length, length);

                graphics.FillRectangle(LBrush, this.X + (this.Width - length) / 2, this.Y - length / 2, length, length);
                graphics.DrawRectangle(MPen, this.X + (this.Width - length) / 2, this.Y - length / 2, length, length);

                graphics.FillRectangle(LBrush, this.X + this.Width - length / 2, this.Y + (this.Height - length) / 2, length, length);
                graphics.DrawRectangle(MPen, this.X + this.Width - length / 2, this.Y + (this.Height - length) / 2, length, length);

                graphics.FillRectangle(LBrush, this.X + (this.Width - length) / 2, this.Y + this.Height - length / 2, length, length);
                graphics.DrawRectangle(MPen, this.X + (this.Width - length) / 2, this.Y + this.Height - length / 2, length, length);
            }

            if (IsDrag)
            {
                graphics.DrawRectangle(MPen2, this.X, this.Y, _dragWidth, _dragHeight);
            }
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
            Invalidate(this.X, this.Y, this.Width, this.Height);
        }

        public override void MouseUnClick()
        {
            Focused = false;
            Invalidate(this.X, this.Y, this.Width, this.Height);
        }

        public override DMember Copy()
        {
            return new DImage(this.MImage)
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
            var context = OwnerDocRow.OwnerDocument.Context;
            var x = context.MouseCurrentPosition.X;
            var y = context.MouseCurrentPosition.Y;
            //右下角
            if (x > this.X + this.Width - 5 &&
                y > this.Y + this.Height - 5)
            {
                context.ChangeCursor(MConstant.CursorType.Sizenwse);
            }
            //右中间
            else if (x > this.X + this.Width - (RecLength + Offset2)/2 &&
                y > this.Y + (this.Height - (RecLength + Offset2))/2 &&
                y < this.Y + (this.Height + (RecLength + Offset2)) / 2)
            {
                context.ChangeCursor(MConstant.CursorType.Sizewe);
            }
            //下中间
            else if (x > this.X + (this.Width - (RecLength + Offset2)) / 2 &&
                x < this.X + (this.Width + (RecLength + Offset2)) / 2 &&
                y > this.Y + this.Height - (RecLength + Offset2) / 2)
            {
                context.ChangeCursor(MConstant.CursorType.Sizens);
            }
            else
            {
                context.ChangeCursor(MConstant.CursorType.Default);
            }
        }

        public override void MouseDragBegin(int x, int y)
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            //右下角
            if (x > this.X + this.Width - 5 &&
                y > this.Y + this.Height - 5)
            {
                _dragWidth = context.MouseCurrentPosition.X - this.X;
                _dragHeight = context.MouseCurrentPosition.Y - this.Y;
                IsDrag = true;
                OwnerDocRow.OwnerDocument.Context.Invalidate();
            }
            //右中间
            else if (x > this.X + this.Width - (RecLength + Offset2) / 2 &&
               y > this.Y + (this.Height - (RecLength + Offset2)) / 2 &&
               y < this.Y + (this.Height + (RecLength + Offset2)) / 2)
            {
                _dragWidth = context.MouseCurrentPosition.X - this.X;
                _dragHeight = this.Height;
                IsDrag = true;
                OwnerDocRow.OwnerDocument.Context.Invalidate();
            }
            //下中间
            else if (x > this.X + (this.Width - (RecLength + Offset2)) / 2 &&
                x < this.X + (this.Width + (RecLength + Offset2)) / 2 &&
                y > this.Y + this.Height - (RecLength + Offset2) / 2)
            {
                _dragWidth = this.Width;
                _dragHeight = context.MouseCurrentPosition.Y - this.Y;
                IsDrag = true;
                OwnerDocRow.OwnerDocument.Context.Invalidate();
            }
        }

        public override void MouseDragEnd()
        {
            if (!IsDrag) return;
            IsDrag = false;
            this.Width = _dragWidth;
            this.Height = _dragHeight;
            OwnerDocRow.OwnerDocument.Context.Invalidate();
        }
    }
}
