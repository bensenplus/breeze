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
    public class XmlTranslator
    {
        public MDocument Document;

        protected XmlTranslator(MDocument document)
        {
            Document = document;
        }

        public static XmlTranslator Instance(MDocument document)
        {
            return new XmlTranslator(document);
        }

        public XmlElement Convert2Xml(MDocument document, XmlDocument xmlDocument)
        {
            var docElement = xmlDocument.CreateElement("document");
            //导出行设置及内容属性
            var row = document.FirstRow;
            while (row != null)
            {
                ExportRow(xmlDocument, docElement, row);
                row = row.NextDocRow;
            }
            return docElement;
        }

        public void Export(EditorDocument document, string fileName)
        {
            //初始化XML文档
            var xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null));
            var docElement = xmlDocument.CreateElement("document");
            xmlDocument.AppendChild(docElement);
            //导出页面设置
            //var bodyElement = ExportPage(xmlDocument, docElement, document.Context.Pages[0]);
            //导出行设置及内容属性
            var row = document.FirstRow;
            while (row != null)
            {
                //ExportRow(xmlDocument, bodyElement, row);
                row = row.NextDocRow;
            }
            //保存文件
            xmlDocument.Save(fileName);
        }

        private XmlElement ExportPage(XmlDocument xmlDocument, XmlElement docElement, DPage page)
        {
            var pageElement = xmlDocument.CreateElement("page");
            docElement.AppendChild(pageElement);

            var headerElement = xmlDocument.CreateElement("header");
            var footerElement = xmlDocument.CreateElement("footer");
            var bodyElement = xmlDocument.CreateElement("body");
            pageElement.AppendChild(headerElement);
            pageElement.AppendChild(footerElement);
            pageElement.AppendChild(bodyElement);

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

            //写入页眉属性
            headerElement.SetAttribute("width", Convert.ToString(page.PageHeader.Width));
            headerElement.SetAttribute("height", Convert.ToString(page.PageHeader.Height));
            headerElement.SetAttribute("paddingLeft", Convert.ToString(page.PageHeader.PaddingLeft));
            headerElement.SetAttribute("paddingRight", Convert.ToString(page.PageHeader.PaddingRight));
            headerElement.SetAttribute("paddingTop", Convert.ToString(page.PageHeader.PaddingTop));
            headerElement.SetAttribute("paddingBottom", Convert.ToString(page.PageHeader.PaddingBottom));
            var headerChildNodes = page.PageHeader.OwnerDocument.Convert2Xml(xmlDocument);
            headerChildNodes.ForEach(data=> headerElement.AppendChild(data));

            //写入页脚属性
            footerElement.SetAttribute("width", Convert.ToString(page.PageFooter.Width));
            footerElement.SetAttribute("height", Convert.ToString(page.PageFooter.Height));
            footerElement.SetAttribute("paddingLeft", Convert.ToString(page.PageFooter.PaddingLeft));
            footerElement.SetAttribute("paddingRight", Convert.ToString(page.PageFooter.PaddingRight));
            footerElement.SetAttribute("paddingTop", Convert.ToString(page.PageFooter.PaddingTop));
            footerElement.SetAttribute("paddingBottom", Convert.ToString(page.PageFooter.PaddingBottom));
            var footerChildNodes = page.PageFooter.OwnerDocument.Convert2Xml(xmlDocument);
            footerChildNodes.ForEach(data => footerElement.AppendChild(data));

            //写入页面body属性
            bodyElement.SetAttribute("width", Convert.ToString(page.PageBody.Width));
            bodyElement.SetAttribute("height", Convert.ToString(page.PageBody.Height));

            return bodyElement;
        }

        private void ExportRow(XmlDocument xmlDocument, XmlElement docElement, DDocRow row)
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
                        //var table = (DTable) member;
                        //membersElement.AppendChild(table.Convert2Xml(xmlDocument));
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

        private string ConvertToString(Image image)
        {
            using (var imageStream = new MemoryStream())
            {
                image.Save(imageStream, image.RawFormat);
                var imageContent = Convert.ToBase64String(imageStream.ToArray());
                return imageContent;
            }
        }

        private bool PropertyEquals(DMember member1, DMember member2)
        {
            if (member1 == null || member2 == null || member1.MType != member2.MType) return false;

            switch (member1.MType)
            {
                case MemberType.TextChar:
                    return member1.WFont.Equals(member2.WFont) && member1.FontColor.Equals(member2.FontColor);
            }

            return false;
        }

        public void Xml2Object(XmlElement xmlElement)
        {
            foreach (XmlNode element in xmlElement.ChildNodes)
            {
                if(!"row".Equals(element.Name)) continue;
                ImportRow((XmlElement)element);
            }
        }

        public void Import(string fileName)
        {
            if (Document == null) return;
            var xml = new XmlDocument(){PreserveWhitespace = true};
            xml.Load(fileName);
            var docElement = xml.SelectSingleNode("document");
            if (docElement == null) return;
            var elements = docElement.ChildNodes;
            foreach (XmlNode element in elements)
            {
                switch (element.Name)
                {
                    case "page":
                        ImportPage((XmlElement)element);
                        break;
                }
            }
        }

        private void ImportPage(XmlElement pageElement)
        {
            var page = ((EditorDocument)Document).FirstPage;

            page.BodyWidth = Convert.ToInt32(pageElement.GetAttribute("bodyWidth"));
            page.BodyHeight = Convert.ToInt32(pageElement.GetAttribute("bodyHeight"));
            page.HeaderWidth = Convert.ToInt32(pageElement.GetAttribute("headerWidth"));
            page.HeaderHeight = Convert.ToInt32(pageElement.GetAttribute("headerHeight"));
            page.FooterWidth = Convert.ToInt32(pageElement.GetAttribute("footerWidth"));
            page.FooterHeight = Convert.ToInt32(pageElement.GetAttribute("footerHeight"));
            page.PaddingLeft = Convert.ToInt32(pageElement.GetAttribute("paddingLeft"));
            page.PaddingRight = Convert.ToInt32(pageElement.GetAttribute("paddingRight"));
            page.PaddingTop = Convert.ToInt32(pageElement.GetAttribute("paddingTop"));
            page.PaddingBottom = Convert.ToInt32(pageElement.GetAttribute("paddingBottom"));
            page.EdgeLineLength = Convert.ToInt32(pageElement.GetAttribute("edgeLineLength"));
            page.OffsetX = Convert.ToInt32(pageElement.GetAttribute("offsetX"));
            page.OffsetY = Convert.ToInt32(pageElement.GetAttribute("offsetY"));
            page.Width = Convert.ToInt32(pageElement.GetAttribute("width"));
            page.Height = Convert.ToInt32(pageElement.GetAttribute("height"));
            page.MarginLeft = Convert.ToInt32(pageElement.GetAttribute("marginLeft"));
            page.MarginRight = Convert.ToInt32(pageElement.GetAttribute("marginRight"));
            page.MarginTop = Convert.ToInt32(pageElement.GetAttribute("marginTop"));
            page.MarginBottom = Convert.ToInt32(pageElement.GetAttribute("marginBottom"));
            page.Spacing = Convert.ToInt32(pageElement.GetAttribute("spacing"));

            var pageChildElements = pageElement.ChildNodes;
            foreach (XmlNode pageChildElement in pageChildElements)
            {
                XmlElement childElement = null;
                switch (pageChildElement.Name)
                {
                    case "header":
                        //读入页眉属性
                        childElement = (XmlElement)pageChildElement;
                        var pageHeader = page.PageHeader;
                        pageHeader.Width = Convert.ToInt32(childElement.GetAttribute("width"));
                        pageHeader.Height = Convert.ToInt32(childElement.GetAttribute("height"));
                        pageHeader.PaddingLeft = Convert.ToInt32(childElement.GetAttribute("paddingLeft"));
                        pageHeader.PaddingRight = Convert.ToInt32(childElement.GetAttribute("paddingRight"));
                        pageHeader.PaddingTop = Convert.ToInt32(childElement.GetAttribute("paddingTop"));
                        pageHeader.PaddingBottom = Convert.ToInt32(childElement.GetAttribute("paddingBottom"));
                        pageHeader.OwnerDocument.Xml2Object(childElement);
                        break;
                    case "footer":
                        //读入页脚属性
                        childElement = (XmlElement)pageChildElement;
                        var pageFooter = page.PageFooter;
                        pageFooter.Width = Convert.ToInt32(childElement.GetAttribute("width"));
                        pageFooter.Height = Convert.ToInt32(childElement.GetAttribute("height"));
                        pageFooter.PaddingLeft = Convert.ToInt32(childElement.GetAttribute("paddingLeft"));
                        pageFooter.PaddingRight = Convert.ToInt32(childElement.GetAttribute("paddingRight"));
                        pageFooter.PaddingTop = Convert.ToInt32(childElement.GetAttribute("paddingTop"));
                        pageFooter.PaddingBottom = Convert.ToInt32(childElement.GetAttribute("paddingBottom"));
                        pageFooter.OwnerDocument.Xml2Object(childElement);
                        break;
                    case "body":
                        //读入页脚属性
                        childElement = (XmlElement)pageChildElement;
                        var pageBody = page.PageBody;
                        pageBody.Width = Convert.ToInt32(childElement.GetAttribute("width"));
                        pageBody.Height = Convert.ToInt32(childElement.GetAttribute("height"));

                        var bodyChildElements = pageChildElement.ChildNodes;
                        foreach (XmlNode bodyChildElement in bodyChildElements)
                        {
                            if (!"row".Equals(bodyChildElement.Name)) continue;
                            ImportRow((XmlElement)bodyChildElement);
                        }
                        break;
                }
            }
        }

        private void ImportRow(XmlElement rowElement)
        {
            var row = Document.CreateRow();
            var elements = rowElement.ChildNodes;
            foreach (XmlNode element in elements)
            {
                if ("members".Equals(element.Name))
                {
                    var memberElements = element.ChildNodes;
                    ImportMember(memberElements, row);
                }
            }
        }

        private void ImportMember(XmlNodeList memberElements, DDocRow row)
        {
            foreach (XmlNode memberNode in memberElements)
            {
                XmlElement memberElement = null;
                switch (memberNode.Name)
                {
                    case "textChar":
                        memberElement = (XmlElement) memberNode;
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
                            var textChar = Document.CreateTextChar(Convert.ToString(singleText), fontName, fontSize,
                                                                    fontColor, GetFontStyle(fontStyle));
                            textChar.OwnerDocRow = row;
                            row.AddRowMember(textChar);
                        }
                        break;
                    case "textInput":
                        memberElement = (XmlElement)memberNode;
                        var textInput = new DTextInput(){OwnerDocRow = row};
                        row.AddRowMember(textInput.Xml2Object(memberElement, Document));
                        break;
                    case "table":
                        //memberElement = (XmlElement)memberNode;
                        //var table = new DTable(){OwnerDocRow = row};
                        //row.AddMember(table.Xml2Object(memberElement));
                        break;
                    case "image":
                        memberElement = (XmlElement)memberNode;
                        var content = memberElement.InnerText;
                        var image = new DImage(ConvertToImage(content)){OwnerDocRow = row};
                        row.AddRowMember(image);
                        break;
                    case "checkBox":
                        memberElement = (XmlElement)memberNode;
                        var checkBox = new DCheckBox
                            {
                                IsChecked = "True".Equals(memberElement.GetAttribute("isChecked")) ? true : false,
                                Value = memberElement.InnerText,
                                OwnerDocRow = row
                            };
                        checkBox.Value = checkBox.Value;
                        row.AddRowMember(checkBox);
                        break;
                    case "comboBox":
                        memberElement = (XmlElement)memberNode;
                        var comboBox = new DComboBox(){OwnerDocRow = row};
                        row.AddRowMember(comboBox.Xml2Object(memberElement, Document));
                        break;
                    case "horizonLine":
                        memberElement = (XmlElement)memberNode;
                        var hLine = new DHorizonLine
                            {
                                Width = Convert.ToInt32(memberElement.GetAttribute("width")),
                                Height = Convert.ToInt32(memberElement.GetAttribute("height")),
                                OwnerDocRow = row
                            };
                        row.AddRowMember(hLine);
                        break;
                }
            }
        }

        private FontStyle GetFontStyle(string style)
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

        private Image ConvertToImage(string content)
        {
            var imageBuffer = Convert.FromBase64String(content);
            var imageStream = new MemoryStream(imageBuffer);
            return Image.FromStream(imageStream);
        }
    }
}
