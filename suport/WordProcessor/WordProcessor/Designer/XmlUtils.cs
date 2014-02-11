using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WordProcessor.Control;
using WordProcessor.Dom;

namespace WordProcessor.Designer
{
    /// <remarks>
    /// XML导入导出工具类
    /// </remarks>
    public abstract class XmlUtils
    {
        public static EditorDocument MDocument;

        public static void Export(EditorDocument document, string fileName)
        {
            //初始化XML文档
            var xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));
            var docElement = xmlDocument.CreateElement("document");
            xmlDocument.AppendChild(docElement);
            //导出页面设置
            ExportPage(xmlDocument, docElement, document.Context.Pages[0]);
            //导出行设置及内容属性
            var row = document.FirstRow;
            while (row != null)
            {
                ExportRow(xmlDocument, docElement, row);
                row = row.NextDocRow;
            }
            //保存文件
            xmlDocument.Save(fileName);
        }

        private static void ExportPage(XmlDocument xmlDocument, XmlElement docElement, WPagination.Page page)
        {
            var pageElement = xmlDocument.CreateElement("page");
            var headerElement = xmlDocument.CreateElement("header");
            var footerElement = xmlDocument.CreateElement("footer");
            pageElement.AppendChild(headerElement);
            pageElement.AppendChild(footerElement);
            //写入页面属性
            pageElement.SetAttribute("bodyWidth", Convert.ToString(page.BodyWidth));
            pageElement.SetAttribute("bodyHeight", Convert.ToString(page.BodyHeight));
            pageElement.SetAttribute("headerWidth", Convert.ToString(page.HeaderWidth));
            pageElement.SetAttribute("headerHeight", Convert.ToString(page.HeaderHeight));
            pageElement.SetAttribute("footerWidth", Convert.ToString(page.FooterWidth));
            pageElement.SetAttribute("footerHeight", Convert.ToString(page.FooterHeight));
            pageElement.SetAttribute("paddingLeft", Convert.ToString(page.PaddingLeft));
            pageElement.SetAttribute("paddingRight", Convert.ToString(page.PaddingRight));
            pageElement.SetAttribute("paddingTop", Convert.ToString(page.PaddingTop));
            pageElement.SetAttribute("paddingBottom", Convert.ToString(page.PaddingBottom));
            pageElement.SetAttribute("edgeLineLength", Convert.ToString(page.EdgeLineLength));
            pageElement.SetAttribute("offsetX", Convert.ToString(page.OffsetX));
            pageElement.SetAttribute("offsetY", Convert.ToString(page.OffsetY));
            pageElement.SetAttribute("width", Convert.ToString(page.Width));
            pageElement.SetAttribute("height", Convert.ToString(page.Height));
            pageElement.SetAttribute("marginLeft", Convert.ToString(page.MarginLeft));
            pageElement.SetAttribute("marginRight", Convert.ToString(page.MarginRight));
            pageElement.SetAttribute("marginTop", Convert.ToString(page.MarginTop));
            pageElement.SetAttribute("marginBottom", Convert.ToString(page.MarginBottom));
            pageElement.SetAttribute("spacing", Convert.ToString(page.Spacing));

            docElement.AppendChild(pageElement);
        }

        private static void ExportRow(XmlDocument xmlDocument, XmlElement docElement, DDocRow row)
        {
            var rowElement = xmlDocument.CreateElement("row");

            if (row.FirstMember == null)
            {
                docElement.AppendChild(rowElement);
                return;
            }

            var membersElement = xmlDocument.CreateElement("members");
            rowElement.AppendChild(membersElement);

            var textCharElements = new List<XmlElement>();

            var member = row.FirstMember;

            while (member != null)
            {
                switch (member.MType)
                {
                    case MemberType.TextChar:
                        if (PropertyEquals(member, member.PreMember))
                        {
                            var textCharElement = textCharElements[textCharElements.Count - 1];
                            textCharElement.InnerText += member.Value;
                        }
                        else
                        {
                            var textCharElement = xmlDocument.CreateElement("textChar");
                            textCharElements.Add(textCharElement);
                            membersElement.AppendChild(textCharElement);
                            textCharElement.InnerText = member.Value;
                            textCharElement.SetAttribute("fontName", member.WFont.Name);
                            textCharElement.SetAttribute("fontSize", Convert.ToString(member.WFont.Size));
                            textCharElement.SetAttribute("fontColor", System.Drawing.ColorTranslator.ToHtml(member.FontColor));
                            textCharElement.SetAttribute("fontStyle", Convert.ToString(member.WFont.Style));
                        }
                        break;
                    case MemberType.TextInput:
                        var textInput = (DTextInput) member;
                        membersElement.AppendChild(textInput.Convert2Xml(xmlDocument));
                        break;
                    case MemberType.Table:
                        break;
                    case MemberType.CheckBox:
                        var checkBox = (DCheckBox) member;
                        var checkBoxElement = xmlDocument.CreateElement("checkBox");
                        membersElement.AppendChild(checkBoxElement);
                        checkBoxElement.SetAttribute("isChecked", Convert.ToString(checkBox.IsChecked));
                        checkBoxElement.InnerText = checkBox.Value;
                        break;
                    case MemberType.ComboBox:
                        var comboBox = (DComboBox) member;
                        membersElement.AppendChild(comboBox.Convert2Xml(xmlDocument));
                        break;
                    case MemberType.Image:
                        var imageElement = xmlDocument.CreateElement("image");
                        membersElement.AppendChild(imageElement);
                        imageElement.InnerText = ConvertToString(((DImage)member).MImage);
                        break;
                    case MemberType.HorizonLine:
                        var hLineElement = xmlDocument.CreateElement("horizonLine");
                        membersElement.AppendChild(hLineElement);
                        hLineElement.SetAttribute("width", Convert.ToString(member.Width));
                        hLineElement.SetAttribute("height", Convert.ToString(member.Height));
                        break;
                }
                member = member.NextMember;
            }

            docElement.AppendChild(rowElement);
        }

        private static string ConvertToString(Image image)
        {
            using (var imageStream = new MemoryStream())
            {
                image.Save(imageStream, image.RawFormat);
                var imageContent = Convert.ToBase64String(imageStream.ToArray());
                return imageContent;
            }
        }

        private static bool PropertyEquals(DMember member1, DMember member2)
        {
            if (member1 == null || member2 == null || member1.MType != member2.MType) return false;

            switch (member1.MType)
            {
                case MemberType.TextChar:
                    return member1.WFont.Equals(member2.WFont) && member1.FontColor.Equals(member2.FontColor);
            }

            return false;
        }

        public static void Import(string fileName)
        {
            if (MDocument == null) return;
            var xml = new XmlDocument();
            xml.Load(fileName);
            var docElement = xml.SelectSingleNode("document");
            if (docElement == null) return;
            var elements = docElement.ChildNodes;
            foreach (XmlElement element in elements)
            {
                switch (element.Name)
                {
                    case "page":
                        ImportPage(element);
                        break;
                    case "row":
                        ImportRow(element);
                        break;
                }
            }
        }

        private static void ImportPage(XmlElement pageElement)
        {
            
        }

        private static void ImportRow(XmlElement rowElement)
        {
            var row = MDocument.CreateRow();
            var elements = rowElement.ChildNodes;
            foreach (XmlElement element in elements)
            {
                if ("members".Equals(element.Name))
                {
                    var memberElements = element.ChildNodes;
                    ImportMember(memberElements, row);
                }
            }
        }

        private static void ImportMember(XmlNodeList memberElements, DDocRow row)
        {
            foreach (XmlElement memberElement in memberElements)
            {
                switch (memberElement.Name)
                {
                    case "textChar":
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
                            var textChar = MDocument.CreateTextChar(Convert.ToString(singleText), fontName, fontSize,
                                                                    fontColor, GetFontStyle(fontStyle));
                            row.AddMember(textChar);
                        }
                        break;
                    case "textInput":
                        var textInput = new DTextInput();
                        row.AddMember(textInput.Xml2Object(memberElement, MDocument));
                        break;
                    case "table":
                        break;
                    case "image":
                        var content = memberElement.InnerText;
                        var image = new DImage(ConvertToImage(content));
                        row.AddMember(image);
                        break;
                    case "checkBox":
                        var checkBox = new DCheckBox
                            {
                                IsChecked = "True".Equals(memberElement.GetAttribute("isChecked")) ? true : false,
                                Value = memberElement.InnerText
                            };
                        row.AddMember(checkBox);
                        break;
                    case "comboBox":
                        var comboBox = new DComboBox();
                        row.AddMember(comboBox.Xml2Object(memberElement, MDocument));
                        break;
                    case "horizonLine":
                        var hLine = new DHorizonLine
                            {
                                Width = Convert.ToInt32(memberElement.GetAttribute("width")),
                                Height = Convert.ToInt32(memberElement.GetAttribute("height"))
                            };
                        break;
                }
            }
        }

        private static FontStyle GetFontStyle(string style)
        {
            if(style == null) return FontStyle.Regular;

            var lstyle = style.Split(',');
            var fs = FontStyle.Regular;
            for (var i = 0; i < lstyle.Length; i++)
            {
                if (lstyle[i] == null) continue;
                if(i == 0) fs -= FontStyle.Regular; 
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

        private static Image ConvertToImage(string content)
        {
            var imageBuffer = Convert.FromBase64String(content);
            var imageStream = new MemoryStream(imageBuffer);
            return Image.FromStream(imageStream);
        }
    }
}
