using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WordProcessor.API;
using WordProcessor.Control;
using WordProcessor.Designer;
using WordProcessor.Dom;

namespace WordProcessor.Painter
{
    /// <remarks>
    /// 页面绘制工具
    /// </remarks>
    public sealed class EditorPainter
    {
        private Bitmap _bitmap;
        private Graphics _tempGraphics;
        private Image _enterArrow = null;
        private Graphics _graphics = null;
        private WEditorView _view = null;
        private readonly Brush _brush1 = new SolidBrush(Color.FromArgb(100, Color.DodgerBlue));
        private readonly Brush _brush2 = new SolidBrush(Color.Black);
        private readonly Font _mFont = new Font("Arial",11);

        public void SetContext(WEditorView context)
        {
            _view = context;
            _view.OwnerDocument.ExecuteDocRowPaint = ExecuteDocRowPaint;
            _view.OwnerDocument.ExecuteMemberPaint = ExecuteMemberPaint;
        }

        public Image GetContentImage(Graphics graphics)
        {
            _bitmap = new Bitmap(_view.Width, _view.Height +  Math.Abs(_view.AutoScrollPosition.Y));
            var grap = Graphics.FromImage(_bitmap);
            grap.PageUnit = graphics.PageUnit;
            Paint(graphics);
            return _bitmap;
        }

        public void Paint(Graphics graphics)
        {
            _graphics = graphics;
            PaintMember();
            PaintPageHeader();
            PaintPageFooter();
        }

        public void PaintPageHeader()
        {
            var pages = _view.OwnerDocument.Pages;
            foreach (var page in pages)
            {
                page.PageHeader.Paint(_graphics);
            }
        }

        public void PaintPageFooter()
        {
            var pages = _view.OwnerDocument.Pages;
            foreach (var page in pages)
            {
                page.PageFooter.Paint(_graphics);
            }
        }

        public void PaintMember()
        {
            _view.OwnerDocument.Paint();
        }

        private void ExecuteDocRowPaint(DDocRow docRow)
        {
            docRow.Paint(_graphics);
            if (docRow.IsReadOnly) return;
            _enterArrow = _view.GetImageList8().Images[0];
            //网格线
            if (EditorSetting.IsShowGridLine)
            {
                _graphics.DrawLine(new Pen(Color.Gray), docRow.X, docRow.Y + docRow.Height, docRow.X + docRow.Width,
                   docRow.Y + docRow.Height);
            }

            //行号
            if (EditorSetting.IsShowRowIndex)
            {
                _graphics.DrawString(Convert.ToString(docRow.Index), _mFont, _brush2, docRow.X - 40,
                                 docRow.Y);
            }

            var member = docRow.GetShortestMember(MemberType.Unknown);
            var offset = member == null ? docRow.Height / 2 : (member.Height + _enterArrow.Height) / 2;

            //回车符
            if (docRow.FirstMember == null)
            {
                _graphics.DrawImage(_enterArrow, docRow.X,
                                    docRow.Y + docRow.Height - offset);
            }
            else if (docRow.LastMember == null)
            {
                _graphics.DrawImage(_enterArrow, docRow.FirstMember.X + docRow.FirstMember.Width,
                                    docRow.Y + docRow.Height - offset);
            }
            else if (!docRow.IsContinue)
            {
                _graphics.DrawImage(_enterArrow, docRow.LastMember.X + docRow.LastMember.Width,
                                    docRow.Y + docRow.Height - offset);
            }
            else if (docRow.LastMember.IsBreakTail)
            {
                _graphics.DrawImage(_enterArrow, docRow.LastMember.X + docRow.LastMember.Width,
                                    docRow.Y + docRow.Height - offset);
            }

            //空行选择区域
            if (docRow.IsSelected && docRow.FirstMember == null)
                _graphics.FillRectangle(_brush1, docRow.X, docRow.Y, _enterArrow.Width, docRow.Height);
        }

        private void ExecuteMemberPaint(DMember member)
        {
            member.Paint(_graphics);
        }
    }
}
