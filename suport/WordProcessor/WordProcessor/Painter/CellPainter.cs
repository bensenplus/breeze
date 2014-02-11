using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Control;
using WordProcessor.Designer;
using WordProcessor.Dom;

namespace WordProcessor.Painter
{
    public sealed class CellPainter
    {
        private CellDocument _cellDocument;
        private Bitmap _bitmap;
        private Graphics _tempGraphics;
        private readonly Image _enterArrow = MImage.EnterArrow;
        private Graphics _graphics = null;
        private readonly Brush _brush1 = new SolidBrush(Color.FromArgb(100, Color.DodgerBlue));
        private readonly Brush _brush2 = new SolidBrush(Color.Black);
        private readonly Font _mFont = new Font("Arial", 11);
        private readonly Pen _mPen = new Pen(Color.Black);
        private readonly Brush _mBrush = new SolidBrush(Color.White);

        public void Paint(Graphics graphics)
        {
            _graphics = graphics;
            Paint();
        }

        public void SetDocument(CellDocument cellDocument)
        {
            _cellDocument = cellDocument;
            _cellDocument.ExecuteDocRowPaint = ExecuteDocRowPaint;
            _cellDocument.ExecuteMemberPaint = ExecuteMemberPaint;
        }

        public void Paint()
        {
            if (_cellDocument == null) return;

            //绘制背景色
            var cell = _cellDocument.OwnerCell;
            _graphics.FillRectangle(_mBrush, cell.X, cell.Y, cell.Width, cell.Height);

            //绘制边框
            _graphics.DrawRectangle(_mPen, cell.X, cell.Y, cell.Width, cell.Height);

            //成员绘制
            _cellDocument.Paint();

            //行被选择
            if (cell.OwnerRow.IsSelected)
            {
                _graphics.FillRectangle(_brush1, cell.X, cell.Y, cell.Width, cell.Height);
            }

            //单元格被选择
            if (cell.IsSelected)
            {
                _graphics.FillRectangle(_brush1, cell.X, cell.Y, cell.Width, cell.Height);
            }
        }

        private void ExecuteDocRowPaint(DDocRow docRow)
        {          
            var member = docRow.GetShortestMember(MemberType.Unknown);
            var offset = member == null ? docRow.Height / 2 : (member.Height + _enterArrow.Height) / 2;

            //回车符
            if (docRow.FirstMember == null)
            {
                _graphics.DrawImage(_enterArrow, docRow.X,
                                    docRow.Y + docRow.Height - offset);
            }
            else if (!docRow.IsContinue)
            {
                if (docRow.LastMember != null)
                {
                    _graphics.DrawImage(_enterArrow, docRow.LastMember.X + docRow.LastMember.Width,
                                        docRow.Y + docRow.Height - offset);
                }
                else
                {
                    _graphics.DrawImage(_enterArrow, docRow.FirstMember.X + docRow.FirstMember.Width,
                                        docRow.Y + docRow.Height - offset);
                }

            }
            else if (docRow.FirstMember.IsBreakTail)
            {
                _graphics.DrawImage(_enterArrow, docRow.FirstMember.X + docRow.FirstMember.Width,
                                        docRow.Y + docRow.Height - offset);
            }
            else if (docRow.LastMember != null && docRow.LastMember.IsBreakTail)
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
