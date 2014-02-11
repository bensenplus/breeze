using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Designer;
using WordProcessor.Dom;
using WordProcessor.Painter;

namespace WordProcessor.Dom
{
    public class DPageFooter : DMember
    {
        public int OffsetX;
        public int OffSetY;
        private DPageFooter _prePageFooter;
        public DPageFooter PrePageFooter
        {
            get { return _prePageFooter; }
            set
            {
                _prePageFooter = value;
                if (value == null) return;
                value.OwnerDocument.SynchronizeData(this);
            }
        }
        public DPageFooter NextPageFooter;
        public MDocument TopDocument;
        public readonly PFooterDocument OwnerDocument;
        public readonly PfooterPainter FooterPainter;
        private const int LineThickness = 2;
        private static readonly Pen MPen1 = new Pen(Color.LightSkyBlue, 1);
        private static readonly Pen MPen2 = new Pen(Color.LightSkyBlue, LineThickness);
        private static readonly Pen MPen3 = new Pen(Color.Black);
        private static readonly Brush MBrush1 = new SolidBrush(Color.AliceBlue);
        private static readonly Brush MBrush2 = new SolidBrush(Color.DarkSlateBlue);
        private static readonly Font MFont = new Font("宋体",9);
        private Point[] _points;

        public DPageFooter(int width, int height)
        {
            Width = width;
            Height = height;
            PaddingLeft = 100;
            PaddingRight = 100;
            OwnerDocument = new PFooterDocument(this);
            FooterPainter = new PfooterPainter(OwnerDocument);
        }

        public override void Paint(Graphics graphics)
        {
            BorderPaint(graphics);
            FooterPainter.Paint(graphics);
        }

        private void BorderPaint(Graphics graphics)
        {
            if (!IsEdit) return;
            MPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            MPen2.DashPattern = new float[] { 3, 2 };
            _points = new Point[]
                {
                    new Point(this.X + OffsetX + 10,this.Y - LineThickness), 
                    new Point(this.X + OffsetX + 10,this.Y - LineThickness - 17),
                    new Point(this.X + OffsetX + 13,this.Y - LineThickness - 20),
                    new Point(this.X + OffsetX + 47,this.Y - LineThickness - 20),
                    new Point(this.X + OffsetX + 50,this.Y - LineThickness - 17),
                    new Point(this.X + OffsetX + 50,this.Y - LineThickness)
                };
            graphics.DrawLine(MPen2, this.X + OffsetX, this.Y, this.X + OffsetX + this.Width, this.Y);
            graphics.FillPolygon(MBrush1, _points);
            graphics.DrawPolygon(MPen1, _points);
            var size = graphics.MeasureString("页脚", MFont);
            graphics.DrawString("页脚", MFont, MBrush2, this.X + OffsetX + 16, this.Y - size.Height - 3);
        }

        public void Locate(int x, int y)
        {
            if (!IsEdit || !IsInRange(x, y)) return;
            ChangeContextCurrentDoc(false);
            OwnerDocument.LocateCurrentRow(x, y);
        }

        public bool IsInRange(int x, int y)
        {
            return x >= this.X && x <= this.X + this.Width
                   && y >= this.Y && y <= this.Y + this.Height;
        }

        public void Resize(int x, int y)
        {
            if (OwnerDocument.FirstRow == null) return;
            OwnerDocument.FirstRow.X = this.X + this.PaddingLeft + 2;
            OwnerDocument.FirstRow.Y = this.Y;
        }

        public override void MouseDbClick()
        {
            IsEdit = true;
            ChangeContextCurrentDoc(true);
            OwnerDocument.Context.Invalidate(new Rectangle(this.X, this.Y, this.Width, this.Height));
        }

        public override void MouseUnDbClick()
        {
            IsEdit = false;
            var tempFooter = GetTopPageFooter();
            while (tempFooter != null)
            {
                tempFooter.IsEdit = IsEdit;
                tempFooter = tempFooter.NextPageFooter;
            }
            OwnerDocument.Context.Invalidate(new Rectangle(this.X, this.Y, this.Width, this.Height));
        }

        private void ChangeContextCurrentDoc(bool isChangeAll)
        {
            if (TopDocument == null) return;
            OwnerDocument.Context = TopDocument.Context;
            TopDocument.ChangeContextCurrentDoc(OwnerDocument);
            if (!isChangeAll) return;
            //改变所有页眉的上下文环境和顶级文档以及编辑状态
            var tempFooter = GetTopPageFooter();
            while (tempFooter != null)
            {
                tempFooter.IsEdit = IsEdit;
                tempFooter.TopDocument = TopDocument;
                tempFooter.OwnerDocument.Context = TopDocument.Context;
                tempFooter = tempFooter.NextPageFooter;
            }
        }

        public DPageFooter GetTopPageFooter()
        {
            var tempFooter = PrePageFooter;
            while (tempFooter != null)
            {
                if (tempFooter.PrePageFooter == null) return tempFooter;
                tempFooter = tempFooter.PrePageFooter;
            }
            return this;
        }
    }
}
