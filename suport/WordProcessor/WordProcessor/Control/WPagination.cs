using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordProcessor.Designer;
using WordProcessor.Dom;
using WordProcessor.Interface;

namespace WordProcessor.Control
{
    /// <remarks>
    /// 页面视图
    /// </remarks>
    public partial class WPagination : UserControl
    {
        private readonly Pen _mpen = new Pen(Color.Gray);
        private readonly ColorBlend _colorBlend;
        private Bitmap _bitmap;

        public EditorDocument OwnerDocument;
        public MDocument CurrentDocument;

        public WPagination()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.UpdateStyles();

            InitializeComponent();

            _colorBlend = new ColorBlend(3)
                {
                    Colors = new Color[]
                        {
                            Color.Transparent, Color.FromArgb(255, Color.DimGray),
                            Color.FromArgb(255, Color.DimGray)
                        },
                    Positions = new float[] {0f, .1f, 1f}
                };
        }

        protected virtual void OnScroll(bool autoScroll)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            graphics.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

            if (OwnerDocument == null) return;

            foreach (var page in OwnerDocument.Pages)
            {
                PaintPage(graphics, page);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (OwnerDocument == null) return;

            foreach (var page in OwnerDocument.Pages)
            {
                if (this.Width > page.Width)
                {
                    page.X = (this.Width - page.Width - 10) / 2;
                    page.MarginLeft = (this.Width - page.Width - 10) / 2;
                }
                else
                {
                    page.X = 0;
                    page.MarginLeft = 0;
                }
                page.Resize();
            }

            Invalidate();
        }

        /// <summary>
        /// 绘制页面
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="page"></param>
        private void PaintPage(Graphics graphics, DPage page)
        {
            //graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var radius = (int)(page.Height * .02);

            var lwidth = page.Width + page.OffsetX * 2;
            var lheight = page.Height + page.OffsetY * 2;

            if (_bitmap != null && _bitmap.Width == lwidth && _bitmap.Height == lheight)
            {
                graphics.DrawImage(_bitmap, page.X, page.Y);
                return;
            }

            _bitmap = new Bitmap(lwidth, lheight);
            var bitGraphics = Graphics.FromImage(_bitmap); 

            try
            {
                using (var path = new GraphicsPath())
                {
                    path.AddLine(radius, 0, lwidth - (radius * 2), 0);
                    path.AddArc(lwidth - (radius * 2), 0, radius * 2, radius * 2, 270, 90);
                    path.AddLine(lwidth, radius, lwidth, lheight - (radius * 2));
                    path.AddArc(lwidth - (radius * 2), lheight - (radius * 2), radius * 2, radius * 2, 0, 90);
                    path.AddLine(lwidth - (radius * 2), lheight, radius, lheight);
                    path.AddArc(0, lheight - (radius * 2), radius * 2, radius * 2, 90, 90);
                    path.AddLine(0, lheight - (radius * 2), 0, radius);
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);

                    using (var brush = new PathGradientBrush(path))
                    {
                        brush.WrapMode = WrapMode.Clamp;
                        brush.InterpolationColors = _colorBlend;
                        bitGraphics.FillPath(brush, path);
                    }

                    using (var pen = new Pen(Color.White, 1f))
                    {
                        path.Reset();
                        path.AddRectangle(new Rectangle(page.OffsetX, page.OffsetY, page.Width, page.Height));
                        bitGraphics.FillPath(new SolidBrush(Color.White), path);
                    }
                }

                PaintPageEdgeLine(bitGraphics, page);

                graphics.DrawImage(_bitmap, page.X, page.Y);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(GetType().Name + ".PaintShadow() Error: " + ex.Message);
            }
        }

        /// <summary>
        /// 绘制页面边距线
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="page"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void PaintPageEdgeLine(Graphics graphics, DPage page)
        {
            //左上角(横、竖)
            graphics.DrawLine(_mpen, page.PaddingLeft - page.EdgeLineLength,
                              page.PaddingTop, page.PaddingLeft, page.PaddingTop);
            graphics.DrawLine(_mpen, page.PaddingLeft,
                              page.PaddingTop - page.EdgeLineLength, page.PaddingLeft,
                              page.PaddingTop);

            //右上角
            graphics.DrawLine(_mpen, page.Width - page.PaddingRight, page.PaddingTop,
                              page.Width - page.PaddingRight + page.EdgeLineLength, page.PaddingTop);
            graphics.DrawLine(_mpen, page.Width - page.PaddingRight,
                              page.PaddingTop - page.EdgeLineLength,
                              page.Width - page.PaddingRight, page.PaddingTop);

            //左下角
            graphics.DrawLine(_mpen, page.PaddingLeft - page.EdgeLineLength,
                              page.Height - page.PaddingBottom, page.PaddingLeft,
                              page.Height - page.PaddingBottom);
            graphics.DrawLine(_mpen, page.PaddingLeft, page.Height - page.PaddingBottom,
                              page.PaddingLeft, page.Height - page.PaddingBottom + page.EdgeLineLength);

            //右下角
            graphics.DrawLine(_mpen, page.Width - page.PaddingRight, page.Height - page.PaddingBottom,
                              page.Width - page.PaddingRight + page.EdgeLineLength,
                              page.Height - page.PaddingBottom);
            graphics.DrawLine(_mpen, page.Width - page.PaddingRight, page.Height - page.PaddingBottom,
                              page.Width - page.PaddingRight,
                              page.Height - page.PaddingBottom + page.EdgeLineLength);
        }
    }
}
