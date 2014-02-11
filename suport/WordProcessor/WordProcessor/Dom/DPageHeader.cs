
using System.Drawing;
using WordProcessor.Designer;
using WordProcessor.Dom;
using WordProcessor.Painter;

namespace WordProcessor.Dom
{
    public class DPageHeader : DMember
    {
        public int OffsetX;
        public int OffSetY;
        private DPageHeader _pageHeader;
        public DPageHeader PrePageHeader
        {
            get { return _pageHeader; }
            set
            {
                _pageHeader = value;
                if (value == null) return;
                value.OwnerDocument.SynchronizeData(this);
            }
        }
        public DPageHeader NextPageHeader;
        public MDocument TopDocument;
        public PHeaderDocument OwnerDocument;
        public readonly PheaderPainter HeaderPainter;
        private const int LineThickness = 2;
        private static readonly Pen MPen1 = new Pen(Color.LightSkyBlue, 1);
        private static readonly Pen MPen2 = new Pen(Color.LightSkyBlue, LineThickness);
        private static readonly Pen MPen3 = new Pen(Color.Black);
        private static readonly Brush MBrush1 = new SolidBrush(Color.AliceBlue);
        private static readonly Brush MBrush2 = new SolidBrush(Color.DarkSlateBlue);
        private static readonly Font MFont = new Font("宋体",9);
        private Point[] _points;

        public DPageHeader(int width, int height)
        {
            Width = width;
            Height = height;
            PaddingLeft = 100;
            PaddingRight = 100;
            PaddingTop = 50;
            OwnerDocument = new PHeaderDocument(this);
            HeaderPainter = new PheaderPainter(OwnerDocument);
        }

        public override void Paint(Graphics graphics)
        {
            BorderPaint(graphics);
            HeaderPainter.Paint(graphics);
        }

        private void BorderPaint(Graphics graphics)
        {
            //横实线
            var height = OwnerDocument.SumRowHeight();
            graphics.DrawLine(MPen3, this.X + PaddingLeft, this.Y + PaddingTop + height,
                              this.X + this.Width - PaddingRight,
                              this.Y + PaddingTop + height);
            if (!IsEdit) return;
            MPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            MPen2.DashPattern = new float[] { 3, 2 };
            _points = new Point[]
                {
                    new Point(this.X + OffsetX + 10,this.Y + this.Height), 
                    new Point(this.X + OffsetX + 10,this.Y + this.Height + 17),
                    new Point(this.X + OffsetX + 13,this.Y + this.Height + 20),
                    new Point(this.X + OffsetX + 47,this.Y + this.Height + 20),
                    new Point(this.X + OffsetX + 50,this.Y + this.Height + 17),
                    new Point(this.X + OffsetX + 50,this.Y + this.Height)
                };
            graphics.DrawLine(MPen2, this.X + OffsetX, this.Y + this.Height, this.X + OffsetX + this.Width,
                              this.Y + this.Height);
            graphics.FillPolygon(MBrush1, _points);
            graphics.DrawPolygon(MPen1, _points);
            graphics.DrawString("页眉", MFont, MBrush2, this.X + OffsetX + 16, this.Y + this.Height + 3);
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
            OwnerDocument.FirstRow.Y = this.Y + this.PaddingTop;
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
            var tempHeader = GetTopPageHeader();
            while (tempHeader != null)
            {
                tempHeader.IsEdit = IsEdit;
                tempHeader = tempHeader.NextPageHeader;
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
            var tempHeader = GetTopPageHeader();
            while (tempHeader != null)
            {
                tempHeader.IsEdit = IsEdit;
                tempHeader.TopDocument = TopDocument;
                tempHeader.OwnerDocument.Context = TopDocument.Context;
                tempHeader = tempHeader.NextPageHeader;
            }
        }

        public DPageHeader GetTopPageHeader()
        {
            var tempHeader = PrePageHeader;
            while (tempHeader != null)
            {
                if (tempHeader.PrePageHeader == null) return tempHeader;
                tempHeader = tempHeader.PrePageHeader;
            }
            return this;
        }
    }
}
