using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Designer;

namespace WordProcessor.Dom
{
    public class DPage : DMember
    {
        public readonly DPageHeader PageHeader;
        public readonly DPageFooter PageFooter;
        public readonly DPageBody PageBody;
        private int _index;
        public int Index
        {
            get
            {
                if (_index > 0 || PrePage == null) return _index;
                return PrePage.Index + 1;
            }

            set { _index = value; }
        }

        private int _x = 30;
        public override int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public override int Y
        {
            get
            {
                if (PrePage == null) return 20;
                return PrePage.Y + PrePage.Height + Spacing;
            }
        }

        public int BodyWidth = EditorSetting.DefaultBodyWidth;
        public int BodyHeight = EditorSetting.DefaultBodyHeight;
        public int HeaderWidth = EditorSetting.DefaultHeaderWidth;
        public int HeaderHeight = EditorSetting.DefaultHeaderHeight;
        public int FooterWidth = EditorSetting.DefaultFooterWidth;
        public int FooterHeight = EditorSetting.DefaultFooterHeight;
        public int EdgeLineLength = EditorSetting.DefaultEdgeLineLength;

        public int OffsetX = 12;
        public int OffsetY = 10;
        public int Spacing = 15;

        public static Color BackColor = Color.White;
        public static Brush Brush = new SolidBrush(Color.White);

        public int RealWidth;
        public int RealHeight;

        private DPage _prePage;
        public DPage PrePage
        {
            get { return _prePage; }
            set
            {
                _prePage = value;
                if (value == null) return;
                PageHeader.PrePageHeader = value.PageHeader;
                PageFooter.PrePageFooter = value.PageFooter;
                PageBody.PrePageBody = value.PageBody;
            }
        }

        private DPage _nextPage;
        public DPage NextPage
        {
            get { return _nextPage; }
            set
            {
                _nextPage = value;
                if (value == null) return;
                PageHeader.NextPageHeader = value.PageHeader;
                PageFooter.NextPageFooter = value.PageFooter;
                PageBody.NextPageBody = value.PageBody;
            }
        }

        public DPage()
        {
            PageHeader = new DPageHeader(EditorSetting.DefaultHeaderWidth, this.PaddingTop);
            PageFooter = new DPageFooter(EditorSetting.DefaultFooterWidth, this.PaddingBottom);
            PageBody = new DPageBody(EditorSetting.DefaultBodyWidth - this.PaddingLeft - this.PaddingRight,
                                     EditorSetting.DefaultPageHeight - PageHeader.Height - PageFooter.Height);
        }

        public void Initialize()
        {
            PaddingLeft = EditorSetting.DefaultPaddingLeft;
            PaddingRight = EditorSetting.DefaultPaddingRight;
            PaddingTop = EditorSetting.DefaultPaddingTop;
            PaddingBottom = EditorSetting.DefaultPaddingBottom;

            Width = 800;
            Height = 1000;
            MarginLeft = 30;
            MarginRight = 30;
            MarginTop = 20;
            MarginBottom = 20;

            if (PageHeader != null)
            {
                PageHeader.OffsetX = this.OffsetX;
                PageHeader.OffSetY = this.OffsetY;
                PageHeader.X = this.X;
                PageHeader.Y = this.Y;
                PageHeader.Width = this.Width;
                PageHeader.Height = this.PaddingTop;
            }

            if (PageFooter != null)
            {
                PageFooter.OffsetX = this.OffsetX;
                PageFooter.OffSetY = this.OffsetY;
                PageFooter.X = this.X;
                PageFooter.Y = this.Y + this.PaddingTop + this.BodyHeight;
                PageFooter.Width = this.Width;
                PageFooter.Height = this.PaddingBottom;
            }

            if (PageBody != null)
            {
                PageBody.X = this.X + this.PaddingLeft;
                PageBody.Width = this.Width - this.PaddingLeft - this.PaddingRight;
                if (PageHeader != null && PageFooter != null)
                {
                    PageBody.Y = this.Y + PageHeader.Height;
                    PageBody.Height = this.Height - PageHeader.Height - PageFooter.Height;
                }
                else
                {
                    PageBody.Y = this.Y + this.PaddingTop;
                    PageBody.Height = this.Height - this.PaddingTop - this.PaddingBottom;
                }
            }
        }

        public void Resize()
        {
            Initialize();
            PageBody.Resize(this.X + this.PaddingLeft, this.Y + this.PaddingTop);
            PageHeader.Resize(this.X, this.Y);
            PageFooter.Resize(this.X, this.Y);
        }

        public void Locate(int x, int y)
        {
            if (y >= PageBody.Y && y <= PageBody.Y + PageBody.Height)
            {
                PageBody.Locate(x, y);
            }
            else if (x >= PageHeader.X && x <= PageHeader.X + PageHeader.Width &&
                     y >= PageHeader.Y && y <= PageHeader.Y + PageHeader.Height)
            {
                PageHeader.Locate(x, y);
            }
            else if (x >= PageFooter.X && x <= PageFooter.X + PageFooter.Width &&
                     y >= PageFooter.Y && y <= PageFooter.Y + PageFooter.Height)
            {
                PageFooter.Locate(x, y);
            }
        }

        public bool IsEdit(int x, int y)
        {
            if (PageBody.IsInRange(x, y))
            {
                return PageBody.IsEdit;
            }
            else if (PageHeader.IsInRange(x, y))
            {
                return PageHeader.IsEdit;
            }
            else if (PageFooter.IsInRange(x, y))
            {
                return PageFooter.IsEdit;
            }
            return false;
        }

        public void SetDocument(MDocument document)
        {
            PageHeader.TopDocument = document;
            PageHeader.OwnerDocument.Context = document.Context;
            PageFooter.TopDocument = document;
            PageFooter.OwnerDocument.Context = document.Context;
        }

        public MDocument GetDocument()
        {
            return PageHeader.TopDocument;
        }
    }
}
