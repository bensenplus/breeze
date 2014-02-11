using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WordProcessor.Designer;
using WordProcessor.Dom;

namespace WordProcessor.Dom
{
    public class DTextInput : DRowMember
    {
        private DTextChar _firstMember;
        private DTextChar _lastMember;
        public DRowMember CurrentMember;
        private static readonly Pen MPen = new Pen(Color.Black);
        private static readonly Brush MBrush = new SolidBrush(Color.FromArgb(32, Color.Brown));
        private const int LeftMargin = 2;
        private const int RightMargin = 2;
        private const int OffSet = 2;

        public DTextInput()
        {
            MType = MemberType.TextInput;
            _firstMember = MDocument.CreateTextChar(EditorSetting.DefaultGraphics, "[");
            _lastMember = MDocument.CreateTextChar(EditorSetting.DefaultGraphics, "]");
            this.Width = LeftMargin + _firstMember.Width + _lastMember.Width + RightMargin;
            this.Height = _firstMember.Height;
            _firstMember.NextMember = _lastMember;
            _lastMember.PreMember = _firstMember;
        }

        public override void MouseClick()
        {
            DRowMember member = _firstMember;
            var page = OwnerDocRow.OwnerDocument.Context;
            while (member != null)
            {
                if (IsInMemberRange(member, page.MouseCurrentPosition.X))
                {
                    CurrentMember = member;
                    return;
                }
                member = member.NextMember;
            }
            CurrentMember = null;
        }

        public void Add(DRowMember member)
        {
            if (CurrentMember == null)
            {
                _firstMember.NextMember = member;
                _lastMember.PreMember = member;
                member.NextMember = _lastMember;
                member.PreMember = _firstMember;
            }
            else
            {
                member.PreMember = CurrentMember;
                member.NextMember = CurrentMember.NextMember;
                CurrentMember.NextMember.PreMember = member;
                CurrentMember.NextMember = member;
            }
            CurrentMember = member;
            _firstMember.Height = _firstMember.NextMember.Height;
            _lastMember.Height = _lastMember.PreMember.Height;
            this.Width += member.Width;
            if (member.Height > this.Height) this.Height = member.Height;
            ResetPosition(GetHighestMember());
        }

        public void Remove(DRowMember member = null)
        {
            if (member == null) member = CurrentMember;
            if (member == null || member.Equals(_firstMember) || member.Equals(_lastMember)) return;
            if (member.PreMember == null || member.NextMember == null) return;
            member.PreMember.NextMember = member.NextMember;
            member.NextMember.PreMember = member.PreMember;
            CurrentMember = member.PreMember;
            member.PreMember = null;
            member.NextMember = null;
            _firstMember.Height = _firstMember.NextMember.Height;
            _lastMember.Height = _lastMember.PreMember.Height;
            this.Width -= member.Width;
            var hmember = GetHighestMember();
            this.Height = hmember.Height;
            ResetPosition(hmember);
        }

        public List<DTextChar> GetTextList()
        {
            var textList = new List<DTextChar>();
            var tempMember = _firstMember.NextMember;
            while (tempMember != null && !tempMember.Equals(_lastMember))
            {
                textList.Add((DTextChar)tempMember);
                tempMember = tempMember.NextMember;
            }
            return textList;
        } 

        public void Reset()
        {
            var member = _firstMember.NextMember;
            while (member != null && !member.Equals(_lastMember))
            {
                Remove(member);
                member = member.NextMember;
            }
        }

        public override void RecursiveChangeDocRow(DDocRow docRow)
        {
            OwnerDocRow = docRow;
            _firstMember.X = this.X + LeftMargin;
            _firstMember.Y = this.Y;
            _lastMember.Y = this.Y;
            if (NextMember == null) return;
            NextMember.RecursiveChangeDocRow(docRow);
        }

        public override void Paint(Graphics graphics)
        {
            DRowMember tempMember = _firstMember;

            ResetPosition(GetHighestMember());

            while (tempMember != null)
            {
                tempMember.Y = tempMember.Y == 0 ? this.Y : tempMember.Y;
                if (tempMember.Equals(_firstMember))
                {
                    var flWidth = tempMember.Width - OffSet;
                    graphics.DrawLine(MPen, tempMember.X, tempMember.Y, tempMember.X + flWidth, tempMember.Y);
                    graphics.DrawLine(MPen, tempMember.X, tempMember.Y, tempMember.X, tempMember.Y + tempMember.Height);
                    graphics.DrawLine(MPen, tempMember.X, tempMember.Y + tempMember.Height, tempMember.X + flWidth, tempMember.Y + tempMember.Height);
                }
                else if (tempMember.Equals(_lastMember))
                {
                    var flWidth = tempMember.Width - OffSet;
                    var lStartX = tempMember.X + OffSet;
                    graphics.DrawLine(MPen, lStartX, tempMember.Y, lStartX + flWidth, tempMember.Y);
                    graphics.DrawLine(MPen, lStartX + flWidth, tempMember.Y, lStartX + flWidth, tempMember.Y + tempMember.Height);
                    graphics.DrawLine(MPen, lStartX, tempMember.Y + tempMember.Height, lStartX + flWidth, tempMember.Y + tempMember.Height);
                }
                else
                {
                    graphics.DrawString(tempMember.Value, tempMember.WFont, new SolidBrush(tempMember.FontColor), tempMember.X,
                                        tempMember.Y, EditorSetting.DefaultStringFormat);
                }
                if (tempMember.NextMember == null) break;
                tempMember = tempMember.NextMember;
            }

            if(IsMouseCapture) graphics.FillRectangle(MBrush, this.X, this.Y, this.Width, this.Height);
        }

        public override bool TouchMe(int x, int y)
        {
            IsTouch = x >= this._firstMember.X + this._firstMember.Width / 2 && x < this._lastMember.X + this._lastMember.Width / 2
                && y >= this.Y && y <= this.Y + this.Height;
            return IsTouch;
        }

        public override void MouseCapture()
        {
            OwnerDocRow.OwnerDocument.Context.ChangeCursor(MConstant.CursorType.Ibeam);
            Invalidate(this.X, this.Y, this.Width, this.Height);
        }

        public override void MouseRelease()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            if (IsMouseEnter(context.MouseCurrentPosition.X, context.MouseCurrentPosition.Y)) return;
            Invalidate(this.X, this.Y, this.Width, this.Height);
        }

        private bool IsInMemberRange(DRowMember member, int x)
        {
            return x >= member.X + member.Width / 2 && member.NextMember != null &&
                   x <= member.NextMember.X + member.NextMember.Width / 2;
        }

        private DMember GetHighestMember()
        {
            DMember hMember = null;
            var tempMember = _firstMember.NextMember;

            while (tempMember != null && !tempMember.Equals(_lastMember))
            {
                if (hMember == null || tempMember.Height > hMember.Height) hMember = tempMember;
                tempMember = tempMember.NextMember;
            }

            return hMember ?? _firstMember;
        }

        private void ResetPosition(DMember hMember)
        {
            var tempMember = _firstMember.NextMember;
            while (tempMember != null)
            {
                tempMember.Y = this.Y + hMember.Height - tempMember.Height -
                               (int)GetFontHeightOffSet(hMember, tempMember);
                tempMember = tempMember.NextMember;
            }
            _firstMember.Y = _firstMember.NextMember.Y;
            _lastMember.Y = _lastMember.PreMember.Y;
            _firstMember.X = this.X;
        }

        public override DMember Copy()
        {
            var textInput = new DTextInput()
            {
                FontColor = this.FontColor,
                WFont = this.WFont,
                Value = this.Value,
                IsBreakTail = this.IsBreakTail,
                Width = this.Width,
                Height = this.Height
            };

            var tempMembers = new List<DRowMember>();
            var member = this._firstMember;
            while (member != null)
            {
                tempMembers.Add((DRowMember)member.Copy());
                if (tempMembers.Count > 1)
                {
                    JoinMember(tempMembers[tempMembers.Count - 2], tempMembers[tempMembers.Count - 1]);
                }
                member = (DTextChar)member.NextMember;
            }

            textInput._firstMember = (DTextChar)tempMembers[0];
            textInput._lastMember = (DTextChar)tempMembers[tempMembers.Count - 1];

            return textInput;
        }

        private void JoinMember(DRowMember member1, DRowMember member2)
        {
            if (member1 != null) member1.NextMember = member2;
            if (member2 != null) member2.PreMember = member1;
        }

        public XmlElement Convert2Xml(XmlDocument xmlDocument)
        {
            var textInputElement = xmlDocument.CreateElement("textInput");
            var textCharElements = new List<XmlElement>();
            var membersElement = xmlDocument.CreateElement("members");
            var tempMember = _firstMember.NextMember;

            while (tempMember != null && !tempMember.Equals(_lastMember))
            {
                if (!tempMember.PreMember.Equals(_firstMember) && 
                    PropertyEquals(tempMember, tempMember.PreMember))
                {
                    var textCharElement = textCharElements[textCharElements.Count - 1];
                    textCharElement.InnerText += tempMember.Value;
                }
                else
                {
                    var textCharElement = xmlDocument.CreateElement("textChar");
                    textCharElements.Add(textCharElement);
                    membersElement.AppendChild(textCharElement);
                    textCharElement.InnerText = tempMember.Value;
                    textCharElement.SetAttribute("fontName", tempMember.WFont.Name);
                    textCharElement.SetAttribute("fontSize", Convert.ToString(tempMember.WFont.Size));
                    textCharElement.SetAttribute("fontColor", System.Drawing.ColorTranslator.ToHtml(tempMember.FontColor));
                    textCharElement.SetAttribute("fontStyle", Convert.ToString(tempMember.WFont.Style));
                }
                tempMember = tempMember.NextMember;
            }
            textInputElement.AppendChild(membersElement);
            return textInputElement;
        }

        public DTextInput Xml2Object(XmlElement xmlElement, MDocument editorDocument)
        {
            foreach (XmlNode node in xmlElement.ChildNodes)
            {
                if (!"members".Equals(node.Name)) continue;
                var element = (XmlElement) node;
                var memberElements = element.ChildNodes;
                foreach (XmlElement memberElement in memberElements)
                {
                    var color = memberElement.GetAttribute("fontColor");
                    var fontColor = Color.Black;
                    if (!string.IsNullOrEmpty(color))
                    {
                        fontColor = System.Drawing.ColorTranslator.FromHtml(color);
                    }
                    var fontName = memberElement.GetAttribute("fontName");
                    var fontSize = Convert.ToSingle(memberElement.GetAttribute("fontSize"));
                    var fontStyle = memberElement.GetAttribute("fontStyle");

                    var text = memberElement.InnerText;
                    var singleTexts = text.ToCharArray();
                    foreach (var singleText in singleTexts)
                    {
                        var textChar = editorDocument.CreateTextChar(Convert.ToString(singleText), fontName, fontSize,
                                                                fontColor, GetFontStyle(fontStyle));
                        this.Add(textChar);
                    }
                }
            }
            return this;
        }

        protected FontStyle GetFontStyle(string style)
        {
            if (style == null) return FontStyle.Regular;

            var lstyle = style.Split(',');
            var fs = FontStyle.Regular;
            for (var i = 0; i < lstyle.Length; i++)
            {
                if (lstyle[i] == null) continue;
                if (i == 0) fs -= FontStyle.Regular;
                switch (lstyle[i].Trim())
                {
                    case "Regular":
                        fs |= FontStyle.Regular;
                        break;
                    case "Bold":
                        fs |= FontStyle.Bold;
                        break;
                    case "Italic":
                        fs |= FontStyle.Italic;
                        break;
                    case "Strikeout":
                        fs |= FontStyle.Strikeout;
                        break;
                    case "Underline":
                        fs |= FontStyle.Underline;
                        break;
                }
            }

            return fs;
        }

        private static bool PropertyEquals(DMember member1, DMember member2)
        {
            if (member1 == null || member2 == null) return false;
            return member1.WFont.Equals(member2.WFont) && member1.FontColor.Equals(member2.FontColor);
        }

    }
}
