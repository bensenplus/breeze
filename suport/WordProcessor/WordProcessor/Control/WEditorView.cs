using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordProcessor.API;
using WordProcessor.Command;
using WordProcessor.Designer;
using WordProcessor.Dom;
using WordProcessor.Painter;
using WordProcessor.WinForm;
using Timer = System.Windows.Forms.Timer;

namespace WordProcessor.Control
{
    /// <remarks>
    /// 页面对象
    /// </remarks>
    public partial class WEditorView : WPagination
    {
        #region 变量

        /// <summary>
        /// 通知代理
        /// </summary>
        /// <param name="notifyArgs"></param>
        public delegate void NotifyChangeHandler(NotifyArags notifyArgs);
        public NotifyChangeHandler NotifyChange;

        /// <summary>
        /// 鼠标当前坐标
        /// </summary>
        public Point MouseCurrentPosition;

        /// <summary>
        /// 选择开始坐标
        /// </summary>
        public Point BeginSelectLocation;

        /// <summary>
        /// 拖动开始坐标
        /// </summary>
        public Point BeginDragLocation;

        /// <summary>
        /// 存放所有编辑器与文档实例关系
        /// </summary>
        public static Dictionary<WEditorView, EditorDocument> ViewMapDocument =
            new Dictionary<WEditorView, EditorDocument>();

        /// <summary>
        /// 弹出框
        /// </summary>
        public readonly WToolStrip MToolStrip;

        /// <summary>
        /// 输入法环境句柄
        /// </summary>
        private readonly int _hImc = 0;

        /// <summary>
        /// 光标操作类
        /// </summary>
        private readonly Win32Caret _caret;

        /// <summary>
        /// 光标坐标及大小
        /// </summary>
        private int _cursorX;
        private int _cursorY;
        private const int CursorWidth = 2;
        private int _cursorHeight;

        /// <summary>
        /// 画图图面
        /// </summary>
        public readonly Graphics Graphic;

        /// <summary>
        /// 输入法标志及计数器
        /// </summary>
        private bool _isImmInput;
        private int _isImmCount;

        /// <summary>
        /// 组合按键使用标志
        /// </summary>
        private bool _isCtrlA;
        private bool _isCtrlC;
        private bool _isCtrlV;

        /// <summary>
        /// 自动滚动
        /// </summary>
        public bool IsAutoScroll = true;

        /// <summary>
        /// 显示光标
        /// </summary>
        public bool IsShowCursor = true;

        /// <summary>
        /// 通知事件参数
        /// </summary>
        private readonly NotifyArags _notifyArags;

        /// <summary>
        /// 鼠标按下标志
        /// </summary>
        private bool _isMouseDown;

        /// <summary>
        /// 绘图工具
        /// </summary>
        private EditorPainter _wPagePainter;

        /// <summary>
        /// 命令集合
        /// </summary>
        private static Dictionary<WCommandType, Command.Command> _commands;

        #endregion 

        #region 常量

        public const int WmImeSetcontext = 0x281;
        public const int WmChar = 0x102;
        public const int WmImeChar = 0x286;
        public const int WmKillFocus = 0x008;
        public const int WmMouseLButtonDown = 0x201;

        public enum KeyType
        {
            Enter,
            Left,
            Right,
            Up,
            Down,
            Delete,
            KeySpace,
            BackSpace,
            Character,
            Focus,
            MouseDownMove,
            MouseDown,
            MouseDrag,
            MouseClick,
            MouseDoubleClick,
            CtrlA,
            CtrlC,
            CtrlV
        }

        public enum ComboType
        {
            PlainText, CheckBox, TreeView
        }

        #endregion

        #region 方法

        public WEditorView()
        {
            //设置输入法上下文
            this._hImc = Win32Imm.ImmGetContext(Handle.ToInt32());
            //设置光标
            this._caret = new Win32Caret(this);

            //设置归属文档
            OwnerDocument = new EditorDocument(this);
            CurrentDocument = OwnerDocument;

            ViewMapDocument.Add(this, OwnerDocument);
            //设置绘制器
            _wPagePainter = new EditorPainter();
            _wPagePainter.SetContext(this);
            Graphic = Graphics.FromHwnd(Handle);
            EditorSetting.DefaultGraphics = Graphic;
            //设置页面弹出框
            MToolStrip = new WToolStrip();
            //设置界面通知对象
            _notifyArags = new NotifyArags();
            //初始化命令
            InitCommand();
            InitializeComponent();
        }

        public ImageList GetImageList8()
        {
            return this.imageList8;
        }

        public void ShowPlainTextStrip<T>(List<T> dataSource, int x, int y, Delegate method)
        {
            MToolStrip.SetDataSource(dataSource, method);
            MToolStrip.Show(this, x, y);
        }

        public void HideCursor()
        {
            _caret.Hide();
        }

        public void ShowCursor()
        {
            if (!this.SetCursorPro()) return;
            this.ShowCursor(_cursorX, _cursorY, CursorWidth, _cursorHeight);
        }

        public void ForceShowCursor()
        {
            int x, y, height;
            bool isShow;
            CurrentDocument.GetCursorLocation(out x, out y, out height, out isShow);
            if (!isShow) return;
            this.ShowCursor(x, y, CursorWidth, height);
        }

        private void ShowCursor(int x, int y, int width, int height)
        {
            _caret.Create(0, width, height);
            _caret.SetPos(x, y);
            _caret.Show();
        }

        private bool SetCursorPro()
        {
            int x, y, height;
            bool isShow;
            CurrentDocument.GetCursorLocation(out x, out y, out height, out isShow);
            if (!isShow) return false;
            if (x == _cursorX && y == _cursorY && height == _cursorHeight) return false;
            _cursorX = x;
            _cursorY = y;
            _cursorHeight = height;
            return true;
        }

        public void ChangeCursor(MConstant.CursorType cursorType)
        {
            switch (cursorType)
            {
               case MConstant.CursorType.Default:
                    Cursor = Cursors.Default;
                    break;
               case MConstant.CursorType.Arrow:
                    Cursor = Cursors.Arrow;
                    break;
               case MConstant.CursorType.Ibeam:
                    Cursor = Cursors.IBeam;
                    break;
               case MConstant.CursorType.Vsplit:
                    Cursor = Cursors.VSplit;
                    break;
               case MConstant.CursorType.Hsplit:
                    Cursor = Cursors.HSplit;
                    break;
               case MConstant.CursorType.SizeAll:
                    Cursor = Cursors.SizeAll;
                    break;
               case MConstant.CursorType.Sizenwse:
                    Cursor = Cursors.SizeNWSE;
                    break;
               case MConstant.CursorType.Sizens:
                    Cursor = Cursors.SizeNS;
                    break;
               case MConstant.CursorType.Sizewe:
                    Cursor = Cursors.SizeWE;
                    break;
               case MConstant.CursorType.Sizenesw:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        /// <summary>
        /// 主要针对页眉页脚切换时，页面可编辑部分的控制
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool CanEdit(KeyType key)
        {
            //双击有打开或解除编辑状态的效果，故过滤掉
            if (key == KeyType.MouseDoubleClick) return true;
            return !OwnerDocument.Equals(CurrentDocument) || OwnerDocument.CurrentPage.PageBody.IsEdit;
        }

        /// <summary>
        /// 键盘事件入口
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        private void ProcessKey(KeyType key, string text=null)
        {
            if (!CanEdit(key)) return;
            switch (key)
            {
                case KeyType.Character:
                    CurrentDocument.HangUpMember = MDocument.CreateTextChar(Graphic, text);
                    CurrentDocument.UpdateDocument(MConstant.EventType.Write);
                    break;
                case KeyType.BackSpace:
                    CurrentDocument.UpdateDocument(OwnerDocument.HasSelect()
                                                     ? MConstant.EventType.DelSelect
                                                     : MConstant.EventType.Delete);
                    break;
                case KeyType.Enter:
                    CurrentDocument.UpdateDocument(MConstant.EventType.Enter);
                    break;
                case KeyType.Delete:
                    CurrentDocument.UpdateDocument(MConstant.EventType.DelSelect);
                    IsAutoScroll = false;
                    break;
                case KeyType.MouseDoubleClick:
                    OwnerDocument.UpdateDocument(MConstant.EventType.DoubleClick);
                    IsAutoScroll = false;
                    break;
                case KeyType.Left:
                    CurrentDocument.UpdateStatus(MConstant.EventType.Left);
                    break;
                case KeyType.Right:
                    CurrentDocument.UpdateStatus(MConstant.EventType.Right);
                    break;
                case KeyType.Up:
                    CurrentDocument.UpdateStatus(MConstant.EventType.Up);
                    break;
                case KeyType.Down:
                    CurrentDocument.UpdateStatus(MConstant.EventType.Down);
                    break;
                case KeyType.MouseDown:
                    IsAutoScroll = false;
                    CurrentDocument.UpdateStatus(MConstant.EventType.ClearSelect);
                    break;
                case KeyType.MouseDownMove:
                    IsAutoScroll = false;
                    IsShowCursor = false;
                    CurrentDocument.UpdateStatus(MConstant.EventType.Select);
                    break;
                case KeyType.CtrlA:
                    IsAutoScroll = false;
                    IsShowCursor = false;
                    CurrentDocument.UpdateStatus(MConstant.EventType.SelectAll);
                    break;
                case KeyType.CtrlC:
                    IsAutoScroll = false;
                    CurrentDocument.UpdateDocument(MConstant.EventType.Copy);
                    break;
                case KeyType.CtrlV:
                    IsAutoScroll = false;
                    CurrentDocument.UpdateDocument(MConstant.EventType.Paste);
                    break;
            }
            OnScroll(IsAutoScroll);
            if (IsShowCursor) ForceShowCursor();
            Invalidate();
        }
        
        /// <summary>
        /// 设置鼠标当前坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetMousePosition(int x, int y)
        {
            MouseCurrentPosition = new Point(x + Math.Abs(this.AutoScrollPosition.X),
                                             y + Math.Abs(this.AutoScrollPosition.Y));
        }

        /// <summary>
        /// 设置内容选择开始坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetBeginSelectLocation()
        {
            BeginSelectLocation = CurrentDocument.GetBeginSelectLocation();
        }

        /// <summary>
        /// 拖动开始坐标
        /// </summary>
        private void SetBeginDragLocation()
        {
            BeginDragLocation = new Point(MouseCurrentPosition.X, MouseCurrentPosition.Y);
        }

        private void NotifyUi()
        {
            if (NotifyChange == null) return;
            var member = CurrentDocument.CurrentRow.CurrentMember;
            _notifyArags.Flag = member != null && member.IsBreakTail;
            _notifyArags.Coordinate = MouseCurrentPosition;
            _notifyArags.PageCount = OwnerDocument.Pages.Count;
            _notifyArags.CurrentPageNumber = OwnerDocument.CurrentPage == null ? -1 : OwnerDocument.CurrentPage.Index;
            _notifyArags.CurrentRowLocation = CurrentDocument.CurrentRow == null
                                                  ? new Point(-1, -1)
                                                  : new Point(CurrentDocument.CurrentRow.X, CurrentDocument.CurrentRow.Y);
            NotifyChange(_notifyArags);
        }

        public void InitCommand()
        {
            _commands = new Dictionary<WCommandType, Command.Command>
                {
                    {WCommandType.NewDocument, new CNewDocument()},
                    {WCommandType.NewTextInput, new CInsertTextInput()},
                    {WCommandType.NewTable, new CInsertTable()},
                    {WCommandType.NewCheckBox, new CInsertCheckBox()},
                    {WCommandType.NewComboBox, new CInsertComboBox()},
                    {WCommandType.NewHorizonLine, new CInsertHorizonLine()},
                    {WCommandType.NewImage, new CInsertImage()},
                    {WCommandType.XmlImport, new CXmlImport()},
                    {WCommandType.XmlExport, new CXmlExport()},
                    {WCommandType.FontSet, new CFontSet()},
                    {WCommandType.FontColorSet, new CFontColorSet()},
                    {WCommandType.ParagraphLeft, new CParagraphLeft()},
                    {WCommandType.ParagraphMiddle, new CParagraphMiddle()},
                    {WCommandType.ParagraphRight, new CParagraphRight()}
                };
        }

        public static Command.Command GetCommand(WCommandType commandType)
        {
            Command.Command command;
            _commands.TryGetValue(commandType, out command);
            return command;
        }

        #endregion
        
        #region 方法复写

        protected override void OnScroll(bool autoScroll)
        {
            if (!autoScroll) return;
            var y = Math.Abs(this.AutoScrollPosition.Y);
            if (CurrentDocument.CurrentRow.Y + CurrentDocument.CurrentRow.Height - y >= this.Height)
            {
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X,
                                                    CurrentDocument.CurrentRow.Y + CurrentDocument.CurrentRow.Height -
                                                    this.Height);
            }
            else if (CurrentDocument.CurrentRow.Y - y <= 0)
            {
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X,
                                                    Math.Abs(this.AutoScrollPosition.Y) -
                                                    (CurrentDocument.CurrentRow.PreDocRow == null
                                                         ? CurrentDocument.CurrentRow.Height
                                                         : (CurrentDocument.CurrentRow.Y -
                                                            CurrentDocument.CurrentRow.PreDocRow.Y)));
            }
        }

        public EditorDocument Reset()
        {
            var x = OwnerDocument.Pages[0].X;
            var editorDocument = new EditorDocument(this);
            OwnerDocument = editorDocument;
            CurrentDocument = editorDocument;

            editorDocument.Pages[0].X = x;
            editorDocument.Pages[0].Resize();

            ViewMapDocument.Remove(this);
            ViewMapDocument.Add(this, editorDocument);

            AutoScrollPosition = new Point(0, 0);
            ForceShowCursor();

            _wPagePainter = new EditorPainter();
            _wPagePainter.SetContext(this);

            this.Invalidate();

            return OwnerDocument;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _wPagePainter.Paint(e.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //设置鼠标按下标志
                _isMouseDown = true;
                //显示光标
                IsShowCursor = true;
                //设置当前文档
                CurrentDocument = OwnerDocument;
                //设置鼠标坐标
                SetMousePosition(e.X, e.Y);
                //清除单击效果
                OwnerDocument.ClearEffectMembers(MConstant.Effect.MouseClick);
                //定位当前页
                OwnerDocument.LocateCurrentPage(MouseCurrentPosition.X, MouseCurrentPosition.Y);
                //设置选择开始坐标
                SetBeginSelectLocation();
                //设置拖动开始坐标
                SetBeginDragLocation();
                //通知界面
                NotifyUi();
                //处理键盘响应
                ProcessKey(KeyType.MouseDown);
            }
            else if (e.Button == MouseButtons.Right)
            {
                //设置鼠标坐标
                SetMousePosition(e.X, e.Y);
                TableMenu.Show(this, e.X, e.Y);
                tsmiMergeCell.Click -= tsmiMergeCell_Click;
                tsmiMergeCell.Click += tsmiMergeCell_Click;
                tsmiSplitCell.Click -= TsmiSplitCellOnClick;
                tsmiSplitCell.Click += TsmiSplitCellOnClick;
                tsmiInsertRowUp.Click -= TsmiInsertRowUpOnClick;
                tsmiInsertRowUp.Click += TsmiInsertRowUpOnClick;
                tsmiInsertRowDown.Click -= TsmiInsertRowDownOnClick;
                tsmiInsertRowDown.Click += TsmiInsertRowDownOnClick;
                tsmiInsertColumnLeft.Click -= tsmiInsertColumnLeft_Click;
                tsmiInsertColumnLeft.Click += tsmiInsertColumnLeft_Click;
                tsmiInsertColumnRight.Click -= tsmiInsertColumnRight_Click;
                tsmiInsertColumnRight.Click += tsmiInsertColumnRight_Click;

            }
            
            base.OnMouseDown(e);
        }

        void tsmiInsertColumnRight_Click(object sender, EventArgs e)
        {
        }

        void tsmiInsertColumnLeft_Click(object sender, EventArgs e)
        {
        }

        void TsmiInsertRowUpOnClick(object sender, EventArgs eventArgs)
        {
        }

        void TsmiInsertRowDownOnClick(object sender, EventArgs eventArgs)
        {
        }

        void TsmiSplitCellOnClick(object sender, EventArgs eventArgs)
        {
        }

        void tsmiMergeCell_Click(object sender, EventArgs e)
        {
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            CurrentDocument = OwnerDocument;
            SetMousePosition(e.X, e.Y);
            ProcessKey(KeyType.MouseDoubleClick);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //鼠标选择文本
            SetMousePosition(e.X, e.Y);
            if (_isMouseDown)
            {
                var flag = OwnerDocument.Drag(BeginDragLocation.X, BeginDragLocation.Y);
                if (!flag) ProcessKey(KeyType.MouseDownMove);
            }
            else
            {
                OwnerDocument.MoveIn(MouseCurrentPosition.X, MouseCurrentPosition.Y);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isMouseDown = false;
            OwnerDocument.ClearEffectMembers(MConstant.Effect.MouseDrag);
            base.OnMouseUp(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //退格、回车、ESC、CTRL+A键过滤掉
            if (e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 27 
                || _isCtrlA || _isCtrlC || _isCtrlV)
            {
                _isCtrlA = false;
                _isCtrlC = false;
                _isCtrlV = false;
                return;
            }
            ProcessKey(KeyType.Character, Convert.ToString(e.KeyChar));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                _isCtrlA = true;
                ProcessKey(KeyType.CtrlA);
                return;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                _isCtrlC = true;
                ProcessKey(KeyType.CtrlC);
                return;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                _isCtrlV = true;
                ProcessKey(KeyType.CtrlV);
                return;
            }
            
            base.OnKeyDown(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            KeyType key;
            switch (keyData)
            {
                case Keys.Back:
                    key = KeyType.BackSpace;
                    break;
                case Keys.Enter:
                    key = KeyType.Enter;
                    break;
                case Keys.Up:
                    key = KeyType.Up;
                    break;
                case Keys.Down:
                    key = KeyType.Down;
                    break;
                case Keys.Left:
                    key = KeyType.Left;
                    break;
                case Keys.Right:
                    key = KeyType.Right;
                    break;
                case Keys.Delete:
                    key = KeyType.Delete;
                    break;
                default:
                    return base.ProcessDialogKey(keyData);
            }
            ProcessKey(key);
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            NotifyUi();
            ShowCursor();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //ForceShowCursor();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WmChar:
                    if (_isImmInput)
                    {
                        _isImmCount--;
                        if (_isImmCount == 0) _isImmInput = false;
                        return;
                    }
                    break;
                case WmImeChar:
                    _isImmCount++;
                    _isImmInput = true;
                    break;
                case WmImeSetcontext:
                    Win32Imm.ImmAssociateContext(Handle.ToInt32(), _hImc);
                    break;
                case WmMouseLButtonDown:
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

    }

    /// <remarks>
    /// 通知代理参数
    /// </remarks>
    public class NotifyArags
    {
        public int PageCount = 1;
        public int CurrentPageNumber = 1;
        public int CurrentRowNumber = 0;
        public Point CurrentRowLocation;
        public int CharCount = 0;
        public Point Coordinate;
        public bool Flag;
        public string Msg = "";
    }
}
