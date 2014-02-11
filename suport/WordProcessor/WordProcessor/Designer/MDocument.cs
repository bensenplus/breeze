using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Control;
using WordProcessor.Dom;

namespace WordProcessor.Designer
{
    /// <summary>
    /// 文档对象
    /// </summary>
    public class MDocument
    {
        /// <summary>
        /// 成员绘制方法代理
        /// </summary>
        /// <param name="member"></param>
        public delegate void MemberPaintMethod(DMember member);
        public MemberPaintMethod ExecuteMemberPaint;

        /// <summary>
        /// 行绘制方法代理
        /// </summary>
        /// <param name="docRow"></param>
        public delegate void DocRowPaintMethod(DDocRow docRow);
        public DocRowPaintMethod ExecuteDocRowPaint;

        /// <summary>
        /// 首行
        /// </summary>
        public DDocRow FirstRow;

        /// <summary>
        /// 末行
        /// </summary>
        public DDocRow LastRow;

        /// <summary>
        /// 当前行
        /// </summary>
        public DDocRow CurrentRow;

        /// <summary>
        /// 行集合
        /// </summary>
        public readonly List<DDocRow> Rows = new List<DDocRow>();

        /// <summary>
        /// 挂起成员
        /// </summary>
        public DRowMember HangUpMember;

        /// <summary>
        /// 接触成员
        /// </summary>
        public DMember TouchMember;

        /// <summary>
        /// 视图环境
        /// </summary>
        public WEditorView Context;

        /// <summary>
        /// 被选择成员
        /// </summary>
        public readonly List<DDocRow> SelectedRows = new List<DDocRow>();

        /// <summary>
        /// 被鼠标单击成员
        /// </summary>
        public readonly List<DMember> ClickMembers = new List<DMember>();

        /// <summary>
        /// 鼠标接触成员
        /// </summary>
        public readonly List<DMember> TouchMembers = new List<DMember>();

        /// <summary>
        /// 鼠标拖动的成员
        /// </summary>
        public readonly List<DMember> DragMembers = new List<DMember>();

        /// <summary>
        /// 鼠标拖动行
        /// </summary>
        public readonly List<DDocRow> DragRows = new List<DDocRow>(); 

        /// <summary>
        /// 被鼠标双击成员
        /// </summary>
        public readonly List<DMember> DoubleClickMembers = new List<DMember>();

        /// <summary>
        /// 表格集合
        /// </summary>
        public readonly List<DTable> Tables = new List<DTable>(); 

        /// <summary>
        /// 构造参数
        /// </summary>
        public MDocument()
        {
        }

        /// <summary>
        /// 构造参数
        /// </summary>
        /// <param name="context">视图环境</param>
        public MDocument(WEditorView context)
        {
            Context = context;
        }

        /// <summary>
        /// 设置视图环境
        /// </summary>
        /// <param name="context">视图环境</param>
        public void SetContext(WEditorView context)
        {
            Context = context;
        }

        /// <summary>
        /// 更新文档内容
        /// </summary>
        /// <param name="eventType">事件类型</param>
        public virtual void UpdateDocument(MConstant.EventType eventType)
        {
        }

        /// <summary>
        /// 更新文档状态
        /// </summary>
        /// <param name="eventType">事件类型</param>
        public virtual void UpdateStatus(MConstant.EventType eventType)
        {
        }

        /// <summary>
        /// 创建文本
        /// </summary>
        /// <param name="graphics">绘图图面</param>
        /// <param name="text">文本内容</param>
        /// <returns></returns>
        public static DTextChar CreateTextChar(Graphics graphics, string text)
        {
            var size = graphics.MeasureString(text, EditorSetting.CurrentFont, 0, EditorSetting.DefaultStringFormat);
            return new DTextChar()
            {
                Value = text,
                WFont = EditorSetting.CurrentFont,
                FontColor = EditorSetting.CurrentFontColor,
                Width = (int)Math.Ceiling(size.Width),
                Height = (int)Math.Ceiling(size.Height)
            };
        }

        /// <summary>
        /// 创建文本对象
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontColor"></param>
        /// <param name="fontStyle"></param>
        /// <returns></returns>
        public DTextChar CreateTextChar(string text, string fontName, float fontSize, Color fontColor,
                                        FontStyle fontStyle)
        {
            var dTextChar = new DTextChar { Value = text, FontColor = fontColor, WFont = new Font(fontName, fontSize, fontStyle) };
            var size = Context.Graphic.MeasureString(dTextChar.Value, dTextChar.WFont, 0, EditorSetting.DefaultStringFormat);
            dTextChar.Width = Convert.ToInt32(size.Width);
            dTextChar.Height = Convert.ToInt32(size.Height);
            return dTextChar;
        }

        /// <summary>
        /// 获取光标坐标和高度
        /// </summary>
        /// <param name="cursorX">X坐标</param>
        /// <param name="cursorY">Y坐标</param>
        /// <param name="cursorHeight">高度</param>
        public virtual void GetCursorLocation(out int cursorX, out int cursorY, out int cursorHeight, out bool isShow)
        {
            cursorX = 0;
            cursorY = 0;
            cursorHeight = 0;
            isShow = true;
        }

        /// <summary>
        /// 创建行
        /// </summary>
        /// <returns></returns>
        public virtual DDocRow CreateRow()
        {
            return null;
        }

        /// <summary>
        /// 在制定行前插入行
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public virtual DDocRow InsertRowBefore(DDocRow row)
        {
            return null;
        }

        /// <summary>
        /// 鼠标拖动处理
        /// </summary>
        /// <param name="x">起始X坐标</param>
        /// <param name="y">起始Y坐标</param>
        public virtual bool Drag(int x, int y)
        {
            return false;
        }

        /// <summary>
        /// 鼠标移入处理
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void MoveIn(int x, int y)
        {
        }

        /// <summary>
        /// 获取选择起始坐标
        /// </summary>
        /// <returns></returns>
        public virtual Point GetBeginSelectLocation()
        {
            return new Point(0,0);
        }

        /// <summary>
        /// 测量文本大小
        /// </summary>
        /// <param name="textChar"></param>
        /// <returns></returns>
        public virtual SizeF MeasureString(DTextChar textChar)
        {
            return new SizeF();
        }

        /// <summary>
        /// 文档内容绘制方法
        /// </summary>
        public virtual void Paint()
        {
        }

        /// <summary>
        /// 是否有成员被选择
        /// </summary>
        /// <returns></returns>
        public bool HasSelect()
        {
            return SelectedRows.Count > 0;
        }

        /// <summary>
        /// 改变环境当前文档
        /// </summary>
        /// <param name="document"></param>
        public void ChangeContextCurrentDoc(MDocument document)
        {
            Context.CurrentDocument = document;
        }

        /// <summary>
        /// 创建下拉框
        /// </summary>
        /// <returns></returns>
        public virtual DComboBox CreateComboBox()
        {
            return null;
        }

        /// <summary>
        /// 创建输入框
        /// </summary>
        /// <returns></returns>
        public virtual DTextInput CreateTextInput()
        {
            return null;
        }

        /// <summary>
        /// 创建图片
        /// </summary>
        /// <returns></returns>
        public virtual DImage CreateImage()
        {
            return null;
        }

        /// <summary>
        /// 创建横线
        /// </summary>
        /// <returns></returns>
        public virtual DHorizonLine CreateHorizonLine()
        {
            return null;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        public virtual DTable CreateTable()
        {
            return null;
        }

        /// <summary>
        /// 穿件复选框
        /// </summary>
        /// <returns></returns>
        public virtual DCheckBox CreateCheckBox()
        {
            return null;
        }

        /// <summary>
        /// 创建新文档
        /// </summary>
        /// <returns></returns>
        public virtual EditorDocument NewDocument()
        {
            return null;
        }

        /// <summary>
        /// 导出XML
        /// </summary>
        public virtual void XmlExport()
        {
        }

        /// <summary>
        /// 导入XML
        /// </summary>
        public virtual void XmlImport()
        {
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        public virtual void SetFont()
        {
        }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        public virtual void SetFontColor()
        {
        }

        /// <summary>
        /// 段落居左
        /// </summary>
        public virtual void SetParagraphLeft()
        {
            
        }

        /// <summary>
        /// 段落居中
        /// </summary>
        public virtual void SetParagraphMiddle()
        {

        }

        /// <summary>
        /// 段落居右
        /// </summary>
        public virtual void SetParagraphRight()
        {

        }

        /// <summary>
        /// 撤销
        /// </summary>
        public virtual void Undo()
        {
            
        }

        /// <summary>
        /// 重做
        /// </summary>
        public virtual void Redo()
        {
            
        }

        /// <summary>
        /// 文档分页
        /// </summary>
        public virtual void Pagination()
        {
        }

        /// <summary>
        /// 获取页面body区域坐标
        /// </summary>
        /// <returns></returns>
        public virtual int GetPageBodyX()
        {
            return 0;
        }

        /// <summary>
        /// 获取页面body宽度
        /// </summary>
        /// <returns></returns>
        public virtual int GetPageBodyWidth()
        {
            return 0;
        }
    }
}
