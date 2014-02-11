using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordProcessor.Designer;

namespace WordProcessor.Dom
{
    /// <remarks>
    /// 成员对象
    /// </remarks>
    public class DMember
    {
        public MemberType MType;
        public Font WFont;
        public Color FontColor = Color.Black;
        public string Value;
        public object Tag;
        public bool IsSelected;
        public bool IsMouseCapture;
        public bool IsEdit;
        public bool Focused;
        public bool Visible;
        public int MarginLeft;
        public int MarginRight;
        public int MarginTop;
        public int MarginBottom;
        public int PaddingLeft;
        public int PaddingRight;
        public int PaddingTop;
        public int PaddingBottom;

        protected bool IsTouch;
        protected bool IsDrag;
        protected static readonly Brush SelectBrush = new SolidBrush(Color.FromArgb(100, Color.DodgerBlue));

        public virtual int Width { get; set; }

        public virtual int Height { get; set; }

        public virtual int X { get; set; }

        public virtual int Y { get; set; }

        /// <summary>
        /// 根据坐标判断鼠标是否在该成员作用域内,并设置状态
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual bool TouchMe(int x, int y)
        {
            return false;
        }

        /// <summary>
        /// 鼠标是否在作用域内，调用TouchMe方法后有效
        /// </summary>
        /// <returns></returns>
        public bool IsTouchMe()
        {
            return IsTouch;
        }

        /// <summary>
        /// 鼠标坐标是否在成员范围内
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual bool IsMouseEnter(int x, int y)
        {
            IsMouseCapture = x >= this.X && x <= this.X + this.Width
                && y >= this.Y && y <= this.Y + this.Height;
            return IsMouseCapture;
        }

        /// <summary>
        /// 是否在拖动中
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDraging()
        {
            return IsDrag;
        }

        /// <summary>
        /// 鼠标捕获效果
        /// </summary>
        public virtual void MouseCapture()
        {
        }

        /// <summary>
        /// 释放鼠标捕获效果
        /// </summary>
        public virtual void MouseRelease()
        {
        }

        /// <summary>
        /// 鼠标点击效果
        /// </summary>
        public virtual void MouseClick()
        {
        }

        /// <summary>
        /// 鼠标点击效果释放
        /// </summary>
        public virtual void MouseUnClick()
        {
        }

        /// <summary>
        /// 鼠标双击效果
        /// </summary>
        public virtual void MouseDbClick()
        {
        }

        /// <summary>
        /// 鼠标双击击效果释放
        /// </summary>
        public virtual void MouseUnDbClick()
        {
        }

        /// <summary>
        /// 鼠标拖动开始
        /// </summary>
        /// <param name="x">起始X坐标</param>
        /// <param name="y">起始Y坐标</param>
        public virtual void MouseDragBegin(int x, int y)
        {
        }

        /// <summary>
        /// 鼠标拖动结束
        /// </summary>
        public virtual void MouseDragEnd()
        {
        }

        /// <summary>
        /// 成员被选择时的逻辑处理
        /// </summary>
        public virtual void Select(bool isSelectAll=false)
        {
        }

        /// <summary>
        /// 取消选择时的逻辑处理
        /// </summary>
        public virtual void ClearSelect()
        {
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Paint(Graphics graphics)
        {
        }

        /// <summary>
        /// 获取成员副本
        /// </summary>
        /// <returns></returns>
        public virtual DMember Copy()
        {
            return null;
        }

        /// <summary>
        /// 坐标是否在该成员范围
        /// </summary>
        /// <returns></returns>
        public virtual bool IsInRange(int x, int y)
        {
            return false;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public virtual void SendMessage(MsgType msgType, Object param)
        {
        }

        /// <summary>
        /// 修正字体底部对齐所需偏移量
        /// </summary>
        /// <param name="member1"></param>
        /// <param name="member2"></param>
        /// <returns></returns>
        public static float GetFontHeightOffSet(DMember member1, DMember member2)
        {
            if (member1.WFont == null || member2.WFont == null || member1.WFont.Size.Equals(member2.WFont.Size))
                return 0;
            var fontFamily = member1.WFont.FontFamily;
            var descent = fontFamily.GetCellDescent(FontStyle.Regular);
            //字体下部距离
            var descentPixel1 = member1.WFont.Size * descent / fontFamily.GetEmHeight(FontStyle.Regular);
            //var descentPixel2 = member2.WFont.Size * descent / fontFamily.GetEmHeight(FontStyle.Regular);
            //return descentPixel1 - descentPixel2;
            return descentPixel1;
        }

    }

    /// <remarks>
    /// 成员类型
    /// </remarks>
    public enum MemberType
    {
        CheckBox,
        ComboBox,
        HorizonLine,
        Image,
        TextChar,
        Table,
        TextInput,
        PageBody,
        PageHeader,
        PageFooter,
        Unknown
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        Write,
        Insert,
        Enter,
        Delete,
        DelSelect,
        Left,
        Right,
        Up,
        Down,
        Copy,
        Paste,
        Select,
        SelectAll,
        ClearSelect,
        DoubleClick,
        Drag
    }
}
