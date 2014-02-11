using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WordProcessor.Designer;

namespace WordProcessor.Dom
{
    public class DComboBox : DTextInput
    {
        public delegate void Set(string text);
        public Set SetText;

        public readonly List<TextData> Items = new List<TextData>();
        public ComboType MComboType = ComboType.PlainText;
        public string DisplayMember;
        public string ValueMember;
        private readonly DRowMember _tempMember;

        public enum ComboType
        {
            PlainText,CheckBox,TreeView
        }

        public DComboBox()
        {
            MType = MemberType.ComboBox;
            _tempMember = MDocument.CreateTextChar(EditorSetting.DefaultGraphics, "下拉框");
            Add(_tempMember);
            SetText = delegate(string text)
                {
                    Reset();
                    Add(MDocument.CreateTextChar(EditorSetting.DefaultGraphics, text));
                    Invalidate();
                };
        }

        public override void MouseClick()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            context.ShowPlainTextStrip<TextData>(Items, this.X, this.Y + this.Height, SetText);
        }

        public override void MouseCapture()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            context.ChangeCursor(MConstant.CursorType.Default);
        }

        public override void MouseRelease()
        {
            var context = OwnerDocRow.OwnerDocument.Context;
            if (IsMouseEnter(context.MouseCurrentPosition.X, context.MouseCurrentPosition.Y)) return;
            context.ChangeCursor(MConstant.CursorType.Ibeam);
        }

        public override DMember Copy()
        {
            var comboBox = new DComboBox()
            {
                FontColor = this.FontColor,
                WFont = this.WFont,
                Value = this.Value,
                IsBreakTail = this.IsBreakTail,
                DisplayMember = this.DisplayMember,
                ValueMember = this.ValueMember,
                Width = this.Width,
                Height = this.Height
            };

            comboBox.Items.AddRange(this.Items);

            return comboBox;
        }

        public new XmlElement Convert2Xml(XmlDocument xmlDocument)
        {
            var comboBoxElement = xmlDocument.CreateElement("comboBox");
            var itemsElement = xmlDocument.CreateElement("items");
            foreach (var textData in Items)
            {
                var itemElement = xmlDocument.CreateElement("item");
                itemElement.InnerText = textData.Value;
                itemsElement.AppendChild(itemElement);
            }
            comboBoxElement.AppendChild(itemsElement);
            var textList = GetTextList();
            if (textList.Count > 0)
            {
                var textElement = xmlDocument.CreateElement("textChar");
                comboBoxElement.AppendChild(textElement);
                foreach (var text in textList)
                {
                    if("下拉框".Equals(text.Value)) continue;
                    textElement.InnerText += text.Value;
                }
                textElement.SetAttribute("fontName", textList[0].WFont.Name);
                textElement.SetAttribute("fontSize", Convert.ToString(textList[0].WFont.Size));
                textElement.SetAttribute("fontColor", System.Drawing.ColorTranslator.ToHtml(textList[0].FontColor));
                textElement.SetAttribute("fontStyle", Convert.ToString(textList[0].WFont.Style));
            }
            return comboBoxElement;
        }

        public new DTextInput Xml2Object(XmlElement xmlElement, EditorDocument editorDocument)
        {
            foreach (XmlNode node in xmlElement.ChildNodes)
            {
                if ("items".Equals(node.Name))
                {
                    var element = (XmlElement) node;
                    foreach (XmlElement item in element.ChildNodes)
                    {
                        this.Items.Add(new TextData(){Value = item.InnerText});
                    }
                }
                else if ("textChar".Equals(node.Name))
                {
                    var element = (XmlElement)node;
                    var color = element.GetAttribute("fontColor");
                    var fontColor = Color.Black;
                    if (!string.IsNullOrEmpty(color))
                    {
                        fontColor = System.Drawing.ColorTranslator.FromHtml(color);
                    }
                    var fontName = element.GetAttribute("fontName");
                    var fontSize = Convert.ToSingle(element.GetAttribute("fontSize"));
                    var fontStyle = element.GetAttribute("fontStyle");

                    var text = element.InnerText;
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
    }

    public class TextData
    {
        public string Value { get; set; }
    }
}
