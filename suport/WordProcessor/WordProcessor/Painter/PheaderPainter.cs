using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Designer;
using WordProcessor.Dom;

namespace WordProcessor.Painter
{
    public sealed class PheaderPainter
    {
        private readonly PHeaderDocument _headerDocument;
        private Bitmap _bitmap;
        private Graphics _tempGraphics;
        private readonly Image _enterArrow = MImage.EnterArrow;
        private Graphics _graphics = null;
        private readonly Brush _brush1 = new SolidBrush(Color.FromArgb(100, Color.DodgerBlue));
        private readonly Brush _brush2 = new SolidBrush(Color.Black);
        private readonly Font _mFont = new Font("Arial", 11);

        public void Paint(Graphics graphics)
        {
            if (_headerDocument == null) return;
            _graphics = graphics;
            PaintMember();
        }

        public PheaderPainter(PHeaderDocument pHeaderDocument)
        {
            _headerDocument = pHeaderDocument;
            _headerDocument.ExecuteDocRowPaint = ExecuteDocRowPaint;
            _headerDocument.ExecuteMemberPaint = ExecuteMemberPaint;
        }

        public void PaintMember()
        {
            _headerDocument.Paint();
        }

        private void ExecuteDocRowPaint(DDocRow docRow)
        {
            if (!_headerDocument.OwnerPheader.IsEdit) return;
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
