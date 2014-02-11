using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordProcessor.Control;
using WordProcessor.Dom;
using WordProcessor.Interface;
using WordProcessor.WinForm;
using Timer = System.Threading.Timer;

namespace WordProcessor.Designer
{
    public class EditorDocument : MDocument
    {
        public MConstant.ViewType CurrentViewType;
        public readonly List<DPage> Pages = new List<DPage>();
        public DPage CurrentPage;
        public DPage FirstPage
        {
            get
            {
                return Pages.Count == 0 ? null : Pages[0];
            }
        }

        public EditorDocument(WEditorView context)
        {
            //默认分页视图
            CurrentViewType = MConstant.ViewType.Pagination;
            Context = context;
            Initialize();
        }

        public void Initialize()
        {
            if (CurrentViewType == MConstant.ViewType.Pagination)
            {
                InsertPage();
            }
            CreateRow();
        }

        public void LocateCurrentPage(int x, int y, bool isLocateChild = true)
        {
            if (Pages.Count == 1)
            {
                CurrentPage = Pages.FirstOrDefault(page => 0 <= y && page.Y + page.Height + page.Spacing >= y);
            }
            else
            {
                CurrentPage = Pages.FirstOrDefault(page => page.Y <= y && page.Y + page.Height + page.Spacing >= y);
            }
            if (!isLocateChild) return;
            if (CurrentPage != null) CurrentPage.Locate(x, y);
        }

        public DPage GetPage(ICompatible compatible)
        {
            return compatible == null ? null : Pages.FirstOrDefault(page => compatible.Equals(page.PageBody.Data));
        }

        public DPage GetPage(int x, int y)
        {
            if (Pages.Count == 0) return null;
            if (Pages.Count == 1)
            {
                return Pages.FirstOrDefault(page => 0 <= y && page.Y + page.Height + page.Spacing >= y);
            }
            return Pages.FirstOrDefault(page => page.Y <= y && page.Y + page.Height + page.Spacing >= y);
        }

        public DPage InsertPage()
        {
            var page = new DPage();
            page.SetDocument(this);

            if (Pages.Count == 0)
            {
                page.Index = 1;
            }
            else
            {
                var lpage = Pages[Pages.Count - 1];
                lpage.NextPage = page;
                page.PrePage = lpage;
                page.X = lpage.X;
                page.SetDocument(lpage.GetDocument());
            }

            page.Initialize();

            Pages.Add(page);
            CurrentPage = page;

            Context.AutoScrollMinSize = new Size(page.MarginLeft + page.Width + page.MarginRight,
                page.MarginTop + (page.Height + page.Spacing) * Pages.Count + page.OffsetY);

            return page;
        }

        public DPage InsertPageAfter(DPage prePage)
        {
            var index = Pages.IndexOf(prePage);
            if (index < 0) return null;

            var page = new DPage();
            if (prePage.NextPage == null)
            {
                prePage.NextPage = page;
                page.PrePage = prePage;
            }
            else
            {
                page.PrePage = prePage;
                page.NextPage = prePage.NextPage;
                prePage.NextPage.PrePage = page;
                prePage.NextPage = page;
            }

            page.SetDocument(page.PrePage.GetDocument());
            page.X = prePage.X;
            page.Initialize();

            Pages.Add(page);
            CurrentPage = page;

            Context.AutoScrollMinSize = new Size(page.MarginLeft + page.Width + page.MarginRight,
                page.MarginTop + (page.Height + page.Spacing) * Pages.Count + page.OffsetY);

            return page;
        }

        public void DeletePage(DPage page)
        {
            var index = Pages.IndexOf(page);
            if (index < 0) return;

            if (page.PrePage != null)
            {
                if (page.NextPage != null)
                {
                    page.PrePage.NextPage = page.NextPage;
                    page.NextPage.PrePage = page.PrePage;
                    page.NextPage = null;
                    page.PrePage = null;
                }
                else
                {
                    page.PrePage.NextPage = null;
                    page.PrePage = null;
                }
            }
            else if (page.NextPage != null)
            {
                page.NextPage.PrePage = null;
                page.NextPage = null;
            }

            Pages.Remove(page);
            if (Pages.Count > 0) CurrentPage = Pages[Pages.Count - 1];

            Context.AutoScrollMinSize = new Size(page.MarginLeft + page.Width + page.MarginRight,
                page.MarginTop + (page.Height + page.Spacing) * Pages.Count + page.OffsetY);
        }

        public void DeleteAfterAllPage(DPage page)
        {
            if (page == null) return;
            var tempPage = page;
            while (tempPage != null)
            {
                var nPage = tempPage.NextPage;
                DeletePage(tempPage);
                tempPage = nPage;
            }
        }

        public override int GetPageBodyX()
        {
            return FirstPage.PageBody.X;
        }

        public override int GetPageBodyWidth()
        {
            return FirstPage.PageBody.Width;
        }

        public override DDocRow CreateRow()
        {
            var row = new DDocRow()
            {
                OwnerDocument = this,
                Height = EditorSetting.DefaultDocRowHeight,
                Width = EditorSetting.DefaultDocRowWidth
            };

            CurrentRow = row;

            if (FirstRow == null)
            {
                FirstRow = row;
                row.Index = 1;
                row.IsPageFirst = true; //标记为真表示是某一分页的首行
                CurrentPage.PageBody.Data = row; //将首行加入到分页中
                CurrentPage.Resize();
                return FirstRow;
            }

            if (LastRow == null)
            {
                row.PreDocRow = FirstRow;
                FirstRow.NextDocRow = row;
            }
            else
            {
                row.PreDocRow = LastRow;
                LastRow.NextDocRow = row;
            }

            LastRow = row;
            row.Paragraph = row.PreDocRow.Paragraph;

            return row;
        }

        public override DDocRow InsertRowBefore(DDocRow row)
        {
            var newDocRow = new DDocRow()
            {
                OwnerDocument = this,
                Height = EditorSetting.DefaultDocRowHeight,
                Width = EditorSetting.DefaultDocRowWidth
            };

            if (row == null && FirstRow == null)
            {
                FirstRow = newDocRow;
                newDocRow.IsPageFirst = true;
                CurrentPage.PageBody.Data = newDocRow;
                return newDocRow;
            }

            if (row == null) return null;

            if (row.PreDocRow == null)
            {
                newDocRow.X = row.X;
                newDocRow.Y = row.Y;
                newDocRow.Index = 1;
                row.PreDocRow = newDocRow;
                newDocRow.NextDocRow = row;
                FirstRow = newDocRow;
                row.IsPageFirst = false;
                newDocRow.IsPageFirst = true;
                CurrentPage.PageBody.Data = newDocRow;
            }
            else if (row.IsPageFirst)
            {
                newDocRow.X = row.X;
                newDocRow.Y = row.Y;
                newDocRow.Index = 1;
                newDocRow.PreDocRow = row.PreDocRow;
                newDocRow.NextDocRow = row;
                row.PreDocRow.NextDocRow = newDocRow;
                row.PreDocRow = newDocRow;
                row.IsPageFirst = false;
                newDocRow.IsPageFirst = true;
                var page = GetPage(row);
                if (page != null)
                {
                    page.PageBody.Data = newDocRow;
                }
            }
            else
            {
                newDocRow.PreDocRow = row.PreDocRow;
                newDocRow.NextDocRow = row;
                row.PreDocRow.NextDocRow = newDocRow;
                row.PreDocRow = newDocRow;
            }

            if (LastRow == null)
            {
                LastRow = row;
            }
            else if (LastRow.Y < row.Y)
            {
                LastRow = row;
            }

            newDocRow.Paragraph = newDocRow.NextDocRow.Paragraph;

            return newDocRow;
        }

        public DDocRow InsertRowAfter(DDocRow row)
        {
            if (row == null) return null;

            var newDocRow = new DDocRow()
            {
                OwnerDocument = this,
                Height = EditorSetting.DefaultDocRowHeight,
                Width = EditorSetting.DefaultDocRowWidth
            };

            if (row.NextDocRow != null)
            {
                newDocRow.PreDocRow = row;
                newDocRow.NextDocRow = row.NextDocRow;
                row.NextDocRow.PreDocRow = newDocRow;
                row.NextDocRow = newDocRow;
            }
            else
            {
                newDocRow.PreDocRow = row;
                row.NextDocRow = newDocRow;
                LastRow = newDocRow;
            }

            newDocRow.Paragraph = newDocRow.PreDocRow.Paragraph;

            return newDocRow;
        }

        public override void UpdateDocument(MConstant.EventType eventType)
        {
            switch (eventType)
            {
                case MConstant.EventType.Write:
                    Write();
                    break;
                case MConstant.EventType.Enter:
                    WordWrap();
                    break;
                case MConstant.EventType.Delete:
                    Delete();
                    break;
                case MConstant.EventType.DelSelect:
                    SelectedDelete();
                    break;
                case MConstant.EventType.DoubleClick:
                    DoubleClick();
                    break;
                case MConstant.EventType.Copy:
                    Copy();
                    break;
                case MConstant.EventType.Paste:
                    Paste();
                    break;
            }
        }

        public override void UpdateStatus(MConstant.EventType eventType)
        {
            switch (eventType)
            {
                case MConstant.EventType.Left:
                    if (CurrentRow.CurrentMember == null)
                    {
                        if (CurrentRow.PreDocRow == null) return;
                        CurrentRow = CurrentRow.PreDocRow;
                        CurrentRow.CurrentMember = CurrentRow.LastMember;
                        return;
                    }
                    CurrentRow.CurrentMember = CurrentRow.CurrentMember.PreMember;
                    break;
                case MConstant.EventType.Right:
                    if (CurrentRow.CurrentMember == null)
                    {
                        if (CurrentRow.FirstMember == null)
                        {
                            if (CurrentRow.NextDocRow == null)
                            {
                                return;
                            }
                            CurrentRow = CurrentRow.NextDocRow;
                        }
                        else
                        {
                            CurrentRow.CurrentMember = CurrentRow.FirstMember;
                        }
                        return;
                    }
                    else if (CurrentRow.CurrentMember.Equals(CurrentRow.LastMember))
                    {
                        if (CurrentRow.NextDocRow == null)
                        {
                            return;
                        }
                        CurrentRow = CurrentRow.NextDocRow;
                        CurrentRow.CurrentMember = null;
                        return;
                    }
                    CurrentRow.CurrentMember = CurrentRow.CurrentMember.NextMember;
                    break;
                case MConstant.EventType.Up:
                    if (CurrentRow.PreDocRow == null) return;
                    var tempX1 = CurrentRow.CurrentMember == null
                                     ? CurrentRow.X
                                     : CurrentRow.CurrentMember.X + CurrentRow.CurrentMember.Width;
                    if (CurrentRow.CurrentMember == null)
                    {
                        CurrentRow = CurrentRow.PreDocRow;
                        CurrentRow.CurrentMember = null;
                    }
                    else
                    {
                        CurrentRow = CurrentRow.PreDocRow;
                        CurrentRow.Locate(tempX1);
                    }
                    break;
                case MConstant.EventType.Down:
                    if (CurrentRow.NextDocRow == null) return;
                    var tempX2 = CurrentRow.CurrentMember == null
                                     ? CurrentRow.X
                                     : CurrentRow.CurrentMember.X + CurrentRow.CurrentMember.Width;
                    if (CurrentRow.CurrentMember == null)
                    {
                        CurrentRow = CurrentRow.NextDocRow;
                        CurrentRow.CurrentMember = null;
                    }
                    else
                    {
                        CurrentRow = CurrentRow.NextDocRow;
                        CurrentRow.Locate(tempX2);
                    }
                    break;
                case MConstant.EventType.Select:
                    Select();
                    break;
                case MConstant.EventType.ClearSelect:
                    ClearSelect();
                    break;
                case MConstant.EventType.SelectAll:
                    SelectAll();
                    break;
            }
        }

        public DMember GetCurrentMember()
        {
            if (CurrentRow == null || CurrentRow.CurrentMember == null) return null;
            return CurrentRow.CurrentMember;
        }

        private void DoubleClick()
        {
            ClearEffectMembers(MConstant.Effect.MouseDoubleClick);
            var position = Context.MouseCurrentPosition;
            LocateCurrentPage(position.X, position.Y, false);
            var page = CurrentPage;
            if (page == null) return;

            //在页眉的范围内
            if (page.PageHeader.IsInRange(position.X, position.Y))
            {
                page.PageBody.SetEdit(false);

                DoubleClickMembers.Add(page.PageFooter);
                DoubleClickMembers.Add(page.PageHeader);

                page.PageFooter.MouseDbClick();
                page.PageHeader.MouseDbClick();
            }
            //在页脚的范围内
            else if (page.PageFooter.IsInRange(position.X, position.Y))
            {
                page.PageBody.SetEdit(false);

                DoubleClickMembers.Add(page.PageHeader);
                DoubleClickMembers.Add(page.PageFooter);

                page.PageHeader.MouseDbClick();
                page.PageFooter.MouseDbClick();
            }
            else
            {
                page.PageBody.SetEdit(true);
            }
        }

        public override bool Drag(int x, int y)
        {
            if (FirstRow == null) return false;
            var row = FirstRow;
            //说明只有一行
            if (row.NextDocRow == null && IsInRowRange(row, y))
            {
                DragRows.Add(row);
                row.DragBegin(x, y);
                var member = row.DragMember(x, y);
                if (member == null) return false;
                DragMembers.Add(member);
                return member.IsDraging();
            }
            while (row != null)
            {
                if (IsInRowRange(row, y))
                {
                    DragRows.Add(row);
                    row.DragBegin(x, y);
                    var member = row.DragMember(x, y);
                    if (member == null) return false;
                    DragMembers.Add(member);
                    return member.IsDraging();
                }
                row = row.NextDocRow;
            }
            return false;
        }

        public override void MoveIn(int x, int y)
        {
            ClearEffectMembers(MConstant.Effect.MouseEnter);
            if (CurrentPage.PageBody.IsEdit)
            {
                if (FirstRow == null) return;
                var row = FirstRow;
                //说明只有一行
                if (row.NextDocRow == null && IsInRowRange(row, y))
                {
                    row.MoveIn(x, y);
                    return;
                }

                while (row != null)
                {
                    if (IsInRowRange(row, y))
                    {
                        row.MoveIn(x, y);
                        return;
                    }
                    row = row.NextDocRow;
                }
            }
            CurrentPage.PageBody.MouseEnter = MouseEnter;
            CurrentPage.PageBody.MouseEnter(x, y);
        }

        private void MouseEnter(int x, int y)
        {
            var page = GetPage(x, y);
            if (page == null) return;
            var pageBody = page.PageBody;
            if (x >= pageBody.X && x <= pageBody.X + pageBody.Width &&
                y >= pageBody.Y && y <= pageBody.Y + pageBody.Height)
            {
                Context.ChangeCursor(pageBody.IsEdit ? MConstant.CursorType.Ibeam : MConstant.CursorType.Default);
            }
            else if (x >= page.PageHeader.X && x <= page.PageHeader.X + page.PageHeader.Width &&
                y >= page.PageHeader.Y && y <= page.PageHeader.Y + page.PageHeader.Height)
            {
                Context.ChangeCursor(page.PageHeader.IsEdit ? MConstant.CursorType.Ibeam : MConstant.CursorType.Default);
            }
            else if (x >= page.PageFooter.X && x <= page.PageFooter.X + page.PageFooter.Width &&
                y >= page.PageFooter.Y && y <= page.PageFooter.Y + page.PageFooter.Height)
            {
                Context.ChangeCursor(page.PageFooter.IsEdit ? MConstant.CursorType.Ibeam : MConstant.CursorType.Default);
            }            
        }

        public override void Paint()
        {
            if (ExecuteMemberPaint == null ||
                ExecuteDocRowPaint == null) return;
            var row = FirstRow;
            var y = Math.Abs(Context.AutoScrollPosition.Y);
            while (row != null)
            {
                if (row.Y + row.Height < y)
                {
                    row = row.NextDocRow;
                    continue;
                }
                else if (row.Y > y + Context.Height)
                {
                    break;
                }
                var member = row.FirstMember;
                while (member != null)
                {
                    ExecuteMemberPaint(member);
                    member = member.NextMember;
                }
                ExecuteDocRowPaint(row);
                row = row.NextDocRow;
            }
        }

        public override Point GetBeginSelectLocation()
        {
            int x, y;
            if (CurrentRow != null)
            {
                x = CurrentRow.X;
                y = CurrentRow.Y;
                var member = CurrentRow.CurrentMember;
                if (member == null)
                {
                    if (CurrentRow.Paragraph == RowParagraph.Right || 
                        CurrentRow.Paragraph == RowParagraph.Middle)
                    {
                        x = CurrentRow.FirstMember != null ? CurrentRow.FirstMember.X : CurrentRow.X;
                    }
                }
                else
                {
                    x = member.X + member.Width;
                }
            }
            else
            {
                x = Context.MouseCurrentPosition.X;
                y = Context.MouseCurrentPosition.Y;
            }
            return new Point(x, y);
        }

        public override void GetCursorLocation(out int cursorX, out int cursorY, out int cursorHeight, out bool isShow)
        {
            base.GetCursorLocation(out cursorX, out cursorY, out cursorHeight, out isShow);
            cursorX = CurrentPage.X + CurrentPage.PaddingLeft;
            cursorY = CurrentPage.Y + CurrentPage.PaddingTop;
            cursorHeight = EditorSetting.CurrentFont.Height;
            isShow = true;

            if (this.CurrentRow == null)
            {
                cursorX -= Math.Abs(Context.AutoScrollPosition.X);
                cursorY -= Math.Abs(Context.AutoScrollPosition.Y);
                return;
            }

            if (this.CurrentRow.IsReadOnly)
            {
                isShow = false;
                return;
            }
            cursorX = this.CurrentRow.X;
            cursorY = this.CurrentRow.FirstMember == null ? this.CurrentRow.Y : this.CurrentRow.FirstMember.Y;
            cursorHeight = this.CurrentRow.FirstMember == null
                                ? EditorSetting.CurrentFont.Height
                                : this.CurrentRow.FirstMember.Height;

            if (this.CurrentRow.CurrentMember == null)
            {
                if (this.CurrentRow.Paragraph == RowParagraph.Right ||
                    this.CurrentRow.Paragraph == RowParagraph.Middle)
                {
                    cursorX = this.CurrentRow.FirstMember == null ? this.CurrentRow.X : this.CurrentRow.FirstMember.X;
                    cursorY = this.CurrentRow.FirstMember == null ? this.CurrentRow.Y : this.CurrentRow.FirstMember.Y;
                }
            }
            else
            {
                cursorX = this.CurrentRow.CurrentMember.X + this.CurrentRow.CurrentMember.Width;
                cursorHeight = this.CurrentRow.CurrentMember.MType == MemberType.TextChar
                                   ? this.CurrentRow.CurrentMember.Height
                                   : EditorSetting.CurrentFont.Height;
                cursorY = this.CurrentRow.CurrentMember.MType == MemberType.TextChar
                              ? this.CurrentRow.CurrentMember.Y
                              : this.CurrentRow.CurrentMember.Y +
                                (this.CurrentRow.CurrentMember.Height - cursorHeight)/2;

                if (this.CurrentRow.CurrentMember.MType == MemberType.TextInput)
                {
                    if (TouchMember == null || TouchMember.MType != MemberType.TextInput) return;
                    var textInput = (DTextInput)this.CurrentRow.CurrentMember;
                    if (!textInput.IsTouchMe() || textInput.CurrentMember == null) return;
                    cursorX = textInput.CurrentMember.X + textInput.CurrentMember.Width;
                    cursorY = textInput.CurrentMember.Y;
                    cursorHeight = textInput.CurrentMember.Height;
                }
            }

            cursorX -= Math.Abs(Context.AutoScrollPosition.X);
            cursorY -= Math.Abs(Context.AutoScrollPosition.Y);
        }

        public override void Pagination()
        {
            MoveRow(FirstRow, FirstPage);
        }

        /// <summary>
        /// 回车折行
        /// </summary>
        private void WordWrap()
        {
            if (CurrentRow == null) return;
            var member = CurrentRow.CurrentMember;

            //位置在行首，整行下降
            if (member == null)
            {
                InsertRowBefore(CurrentRow);
                MoveRow(CurrentRow, CurrentPage);
                SetFocus(CurrentRow);
                return;
            }

            //回车折行，造成当前成员与下个成员之间被切断，变成链尾
            member.IsBreakTail = true;
            var nextMember = member.NextMember;

            //位置在行尾，插入新行
            if (nextMember == null)
            {
                CurrentRow.IsContinue = false;
                DDocRow tempRow = null;
                if (CurrentRow.NextDocRow == null)
                {
                    tempRow = CreateRow();
                    MoveRow(tempRow, CurrentPage);
                    SetFocus(tempRow);
                    return;
                }
                tempRow = InsertRowBefore(CurrentRow.NextDocRow);
                MoveRow(tempRow, CurrentPage);
                SetFocus(tempRow);
                return;
            }

            //位置在行中，当前行与下一行是不连续状态或是连续状态但下一行是空
            if (!CurrentRow.IsContinue || (CurrentRow.IsContinue && CurrentRow.NextDocRow == null))
            {
                CurrentRow.IsContinue = false;

                var curMember = CurrentRow.CurrentMember;
                var lastMember = CurrentRow.LastMember;
                var nMember = curMember.NextMember;

                curMember.NextMember = null;
                nMember.PreMember = null;

                CurrentRow.LastMember = curMember.Equals(CurrentRow.FirstMember) ? null : curMember;

                var docRow =
                    CurrentRow.NextDocRow == null ?
                    CreateRow() :
                    InsertRowBefore(CurrentRow.NextDocRow);

                nMember.RecursiveChangeDocRow(docRow);

                docRow.FirstMember = nMember;
                docRow.LastMember = nMember.Equals(lastMember) ? null : lastMember;

                MoveRow(docRow, CurrentPage);

                SetFocus(docRow);
                return;
            }

            //位置在行中，当前行是连续且下一行不为空
            var curRow = CurrentRow;
            curRow.IsContinue = false;

            var tempMember = curRow.CurrentMember.NextMember;

            curRow.CurrentMember.NextMember.PreMember = null;
            curRow.CurrentMember.NextMember = null;

            curRow.NextDocRow.WriteHead(tempMember);

            curRow.LastMember = curRow.CurrentMember.Equals(curRow.FirstMember) ? null : curRow.CurrentMember;

            MoveRow(curRow.NextDocRow, CurrentPage);

            SetFocus(curRow.NextDocRow);
        }

        private void SetFocus(DDocRow row)
        {
            if (row == null) return;
            CurrentRow = row;
            CurrentRow.CurrentMember = row.CurrentMember;
        }

        /// <summary>
        /// 键盘输入或菜单插入成员入行
        /// </summary>
        private void Write()
        {
            if (HangUpMember == null || CurrentRow.IsReadOnly) return;

            /**************** 页面横向的容纳判断和处理 ***********************/

            //表示行首输入
            if (CurrentRow.CurrentMember == null)
            {
                CurrentRow.WriteHead(HangUpMember);
            }
            //表示在文本输入框内输入
            else if (CurrentRow.CurrentMember.MType == MemberType.TextInput
                && ((DTextInput)CurrentRow.CurrentMember).IsTouchMe())
            {
                var textInput = (DTextInput)CurrentRow.CurrentMember;
                textInput.Add(HangUpMember);
                return;
            }
            //表示行尾输入
            else if (CurrentRow.CurrentMember.Equals(CurrentRow.LastMember))
            {
                CurrentRow.WriteTail(HangUpMember);
            }
            //表示行中输入
            else
            {
                CurrentRow.WriteAmong(HangUpMember);
            }

            /**************** 页面竖向的容纳判断和处理 ***********************/

            //查看新成员的插入是否会导致竖向页距被超出而需要移动行
            //MoveRow(HangUpMember.OwnerDocRow, Context.CurrentPage);

            CurrentRow = HangUpMember.OwnerDocRow;
            CurrentRow.CurrentMember = HangUpMember;
            Context.ForceShowCursor();

            HangUpMember = null;
        }

        /// <summary>
        /// 移动行
        /// </summary>
        private void MoveRow(DDocRow row, DPage currentPage)
        {
            if (row == null) return;

            //该行超出所在页面范围，则移至下一页
            var rowPointer = row;
            var pagePointer = currentPage;

            var maxHeight = pagePointer.Y + pagePointer.Height - pagePointer.PaddingBottom;

            while (rowPointer != null)
            {
                //第一行肯定是页首行，所以不能更改页首行标志
                if (!rowPointer.Equals(FirstRow))
                {
                    rowPointer.IsPageFirst = false;
                }

                //如果该行超出该页高度，则与下一页合并成为首行
                if (rowPointer.Y >= maxHeight ||
                    rowPointer.Y + rowPointer.Height > maxHeight)
                {
                    //下一页为空或者行的高度等于或超过页的高度，则创建新页
                    if (pagePointer.NextPage == null || rowPointer.Height >= pagePointer.Height)
                    {
                        var page = InsertPageAfter(pagePointer);
                        rowPointer.IsPageFirst = true;
                        page.PageBody.Data = rowPointer;
                        page.Resize();
                        Context.AutoScrollPosition = new Point(0, row.Y + row.Height - Context.Height);
                        Context.ForceShowCursor();
                        MoveRow(rowPointer, page);
                        return;
                    }

                    //将指针指向下一页
                    pagePointer = pagePointer.NextPage;

                    //将该行设置为下一页行首
                    rowPointer.IsPageFirst = true;
                    pagePointer.PageBody.Data = rowPointer;
                    pagePointer.Resize();

                    //行指针下移
                    rowPointer = rowPointer.NextDocRow;

                    if (rowPointer == null) break;

                    rowPointer.IsPageFirst = false;

                    MoveRow(rowPointer, pagePointer);

                    return;
                }

                var prePage = pagePointer.PrePage;
                //如果超出上一页则放置在当前页
                if (prePage != null)
                {
                    var maxHeight2 = prePage.Y + prePage.Height - prePage.PaddingBottom;
                    if (rowPointer.Y >= maxHeight2 || rowPointer.Y + rowPointer.Height > maxHeight2)
                    {
                        if (rowPointer.Equals(pagePointer.PageBody.Data))
                        {
                            rowPointer.IsPageFirst = true;
                        }
                        else if (rowPointer.PreDocRow != null &&
                            rowPointer.PreDocRow.Y + rowPointer.PreDocRow.Height <= maxHeight2)
                        {
                            rowPointer.IsPageFirst = true;
                            pagePointer.PageBody.Data = rowPointer;
                            pagePointer.Resize();
                        }
                    }
                }

                //如果已经遍历到最后一行，对最后一行所在页之后的空页进行删除
                if (rowPointer.NextDocRow == null)
                {
                    var page = GetPage(rowPointer.X, rowPointer.Y);
                    DeleteAfterAllPage(pagePointer.NextPage);
                }
                rowPointer = rowPointer.NextDocRow;
            }
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        private void Delete()
        {
            //表示在行首退格删除
            if (CurrentRow.CurrentMember == null)
            {
                var preRow = CurrentRow.PreDocRow;
                if (preRow == null || preRow.IsReadOnly)
                {
                    return;
                }
                else if (CurrentRow.FirstMember == null)
                {
                    var tempRow = CurrentRow;
                    CutRow(preRow, CurrentRow);
                    JoinRow(preRow, tempRow.NextDocRow);
                    if (LastRow != null && LastRow.Equals(CurrentRow))
                    {
                        LastRow = preRow.Equals(FirstRow) ? null : preRow;
                    }
                    CurrentRow = preRow;
                }
                else
                {
                    FollowUp(preRow, preRow.NextDocRow);
                }
                LocateCurrentPage(preRow.X, preRow.Y, false);
                MoveRow(preRow, CurrentPage);
                SetFocus(preRow);
                return;
            }
            //表示在文本输入框内退格删除
            else if (CurrentRow.CurrentMember.MType == MemberType.TextInput
                && ((DTextInput)CurrentRow.CurrentMember).IsTouchMe())
            {
                var textInput = (DTextInput)CurrentRow.CurrentMember;
                textInput.Remove();
            }
            //表示在行尾退格删除
            else if (CurrentRow.CurrentMember.Equals(CurrentRow.LastMember))
            {
                var lastMember = CurrentRow.LastMember.PreMember;
                lastMember.IsBreakTail = CurrentRow.LastMember.IsBreakTail;
                CurrentRow.LastMember.PreMember.NextMember = null;
                CurrentRow.LastMember.PreMember = null;
                CurrentRow.LastMember = lastMember.Equals(CurrentRow.FirstMember) ? null : lastMember;
                CurrentRow.CurrentMember = lastMember;
            }
            //表示在第一个成员处退格删除
            else if (CurrentRow.CurrentMember.Equals(CurrentRow.FirstMember))
            {
                var tempMember = CurrentRow.FirstMember.NextMember;
                CurrentRow.FirstMember.NextMember = null;
                CurrentRow.FirstMember = tempMember;
                if (tempMember != null)
                {
                    tempMember.PreMember = null;
                    CurrentRow.LastMember = tempMember.GetTailMember().Equals(CurrentRow.FirstMember)
                                                ? null
                                                : CurrentRow.LastMember;
                }
                CurrentRow.CurrentMember = null;
            }
            else
            {
                if (CurrentRow.CurrentMember.PreMember != null)
                    CurrentRow.CurrentMember.PreMember.NextMember = CurrentRow.CurrentMember.NextMember;
                if (CurrentRow.CurrentMember.NextMember != null)
                    CurrentRow.CurrentMember.NextMember.PreMember = CurrentRow.CurrentMember.PreMember;
                var curMember = CurrentRow.CurrentMember.PreMember;
                CurrentRow.CurrentMember.PreMember = null;
                CurrentRow.CurrentMember.NextMember = null;
                CurrentRow.CurrentMember = curMember;
            }
            var member = CurrentRow.LastMember ?? CurrentRow.FirstMember;
            if (member != null && member.IsBreakTail) return;
            FollowUp(CurrentRow, CurrentRow.NextDocRow, false);
            LocateCurrentPage(CurrentRow.X, CurrentRow.Y, false);
            MoveRow(CurrentRow, CurrentPage);
        }

        /// <summary>
        /// 删除时后面成员跟进处理
        /// </summary>
        /// <param name="startRow">开始行</param>
        /// <param name="nextRow">下一行</param>
        /// <param name="isDelTailMember">是否对开始行的末尾成员进行删除操作</param>
        private void FollowUp(DDocRow startRow, DDocRow nextRow, bool isDelTailMember = true)
        {
            if (startRow == null || nextRow == null) return;

            //定义三个活动指针
            var sRowPointer = startRow;
            var nRowPointer = nextRow;
            var mPointer = nRowPointer.FirstMember;

            while (true)
            {
                if (sRowPointer.ConcatMemberForDelete(mPointer, isDelTailMember))
                {
                    if (isDelTailMember)
                    {
                        isDelTailMember = false;
                        CurrentRow = startRow;
                        CurrentRow.CurrentMember = mPointer.PreMember ??
                            (mPointer.Equals(sRowPointer.FirstMember) ? null : mPointer);
                    }
                    //连接成功，则指针下移且断开与之前已被连接成员
                    mPointer = mPointer.NextMember;
                    if (mPointer != null)
                    {
                        mPointer.PreMember.NextMember = null;
                        mPointer.PreMember = null;
                    }
                    continue;
                }

                if (isDelTailMember)
                {
                    isDelTailMember = false;
                    CurrentRow = startRow;
                    CurrentRow.CurrentMember = startRow.LastMember ?? startRow.FirstMember;
                }

                //成员前移置顶
                if (mPointer != null)
                {
                    nRowPointer.FirstMember = mPointer;
                    var tailMember = mPointer.GetTailMember();
                    nRowPointer.LastMember = tailMember.Equals(mPointer) ? null : tailMember;
                }
                else
                {
                    CutRow(sRowPointer, nRowPointer);
                    JoinRow(sRowPointer, nRowPointer.NextDocRow);
                    if (LastRow != null && LastRow.Equals(nRowPointer))
                    {
                        LastRow = nRowPointer.NextDocRow ?? sRowPointer;
                    }
                    break;
                }

                //到断点循环结束
                var tempMember1 = sRowPointer.LastMember ?? sRowPointer.FirstMember;
                var tempMember2 = nRowPointer.LastMember ?? nRowPointer.FirstMember;
                var isEnd = (tempMember1 == null || tempMember1.IsBreakTail) || (tempMember2 == null || tempMember2.IsBreakTail);

                if (isEnd) break;

                //行指针下移
                sRowPointer = nRowPointer;
                nRowPointer = nRowPointer.NextDocRow;

                if (nRowPointer == null) break;

                //成员指针指向下一行第一个元素
                mPointer = nRowPointer.FirstMember;
            }
        }

        /// <summary>
        /// 鼠标选择成员
        /// </summary>
        private void Select()
        {
            var point = Context.BeginSelectLocation;
            SelectRow(point.X, point.Y, Context.MouseCurrentPosition.X, Context.MouseCurrentPosition.Y);
            if ((SelectedRows.Count == 1 && SelectedRows[0].SelectedMembers.Count > 0) || SelectedRows.Count > 1)
            {
                Context.HideCursor();
            }
        }

        /// <summary>
        /// 需找鼠标选择区域内的成员
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        private void SelectRow(int startX, int startY, int endX, int endY)
        {
            ClearSelect();

            var currentRow = CurrentRow;
            if (currentRow == null) return;

            var isUp = startY > endY;

            int x1, x2;
            if (startX <= endX)
            {
                x1 = startX;
                x2 = endX;
            }
            else
            {
                x1 = endX;
                x2 = startX;
            }

            var row = currentRow;
            while (row != null)
            {
                if (isUp)
                {
                    //当前行
                    if (row.Y <= startY && row.Y + row.Height > startY && row.Y <= endY && row.Y + row.Height > endY)
                    {
                        row.SelectMember(x1, x2);
                    }
                    //超出当前行
                    else if (row.Y <= startY && row.Y + row.Height > startY)
                    {
                        row.SelectMember(row.X, startX);
                    }
                    //当前行以上行部分选择
                    else if (row.Y + row.Height <= startY && row.Y + row.Height > endY && row.Y < endY)
                    {
                        row.SelectMember(endX, row.X + row.Width);
                    }
                    //当前行以上行全部选择
                    else if (row.Y + row.Height <= startY && row.Y > endY)
                    {
                        row.SelectAllMember();
                    }
                    else
                    {
                        break;
                    }
                    AddSelectedRow(row);
                    row = row.PreDocRow;
                }
                else
                {
                    //当前行
                    if (row.Y <= startY && row.Y <= endY && row.Y + row.Height > endY)
                    {
                        row.SelectMember(x1, x2);
                    }
                    //超出当前行
                    else if (row.Y <= startY && row.Y + row.Height > startY)
                    {
                        row.SelectMember(startX, row.X + row.Width);
                    }
                    //当前行以下行部分选择
                    else if (row.Y > startY && row.Y < endY && row.Y + row.Height > endY)
                    {
                        row.SelectMember(row.X, endX);
                    }
                    //当前航以下行全部选择
                    else if (row.Y >= startY + currentRow.Height && row.Y + row.Height < endY)
                    {
                        row.SelectAllMember();
                    }
                    else if (row.Y > endY)
                    {
                        break;
                    }
                    AddSelectedRow(row);
                    row = row.NextDocRow;
                }
            }
        }

        private void AddSelectedRow(DDocRow row)
        {
            if (SelectedRows.Contains(row)) return;
            row.IsSelected = true;
            SelectedRows.Add(row);
        }

        /// <summary>
        /// 清除选择成员记录
        /// </summary>
        private void ClearSelect()
        {
            foreach (var selectedRow in SelectedRows)
            {
                selectedRow.IsSelected = false;
                selectedRow.ClearSelectMembers();
            }
            SelectedRows.Clear();
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void SelectAll()
        {
            var row = FirstRow;
            while (row != null)
            {
                row.IsSelected = true;
                SelectedRows.Add(row);
                row.SelectAllMember();
                row = row.NextDocRow;
            }
            CurrentPage = Pages[0];
            if (SelectedRows.Count > 0)
                Context.HideCursor();
        }

        /// <summary>
        /// 对选择区域的文本进行删除操作
        /// </summary>
        private void SelectedDelete()
        {
            if (SelectedRows.Count == 0) return;

            if (SelectedRows.Count == 1)
            {
                var row = SelectedRows[0];
                SelDelOfSingleRow(row);
                return;
            }

            SelectedRows.Sort((row, docRow) => row.Index > docRow.Index ? 1 : -1);
            var count = SelectedRows.Count;
            if (count > 2)
            {
                for (var i = 1; i <= count - 2; i++)
                {
                    RemoveRow(SelectedRows[i]);
                }
            }
            var sRow = SelectedRows[0];
            var lRow = SelectedRows[count - 1];
            SelDelOfSingleRow(sRow);
            SelDelOfSingleRow(lRow);
            FollowUp(sRow, lRow, false);
            MoveRow(sRow, CurrentPage);
            ClearSelect();
        }

        private void SelDelOfSingleRow(DDocRow row)
        {
            if (row.SelectedMembers.Count == row.MemberCount())
            {
                if (row.Equals(FirstRow))
                {
                    row.FirstMember = null;
                    row.LastMember = null;
                    CurrentRow = row;
                    CurrentRow.CurrentMember = null;
                }
                else
                {
                    RemoveRow(row);
                    CurrentRow = row.PreDocRow ?? FirstRow;
                    CurrentRow.CurrentMember = CurrentRow.LastMember ?? CurrentRow.FirstMember;
                }
            }
            else
            {
                CurrentRow = row;
                if (row.SelectedMembers.Count == 0) return;
                var beginMember = row.SelectedMembers[0];
                var endMember = row.SelectedMembers[row.SelectedMembers.Count - 1];
                if (beginMember.PreMember != null)
                {
                    beginMember.PreMember.NextMember = endMember.NextMember;
                    if (endMember.NextMember != null)
                    {
                        endMember.NextMember.PreMember = beginMember.PreMember;
                    }
                    else
                    {
                        CurrentRow.LastMember = beginMember.PreMember.Equals(CurrentRow.FirstMember)
                                                    ? null
                                                    : beginMember.PreMember;
                    }
                    CurrentRow.CurrentMember = beginMember.PreMember;
                }
                else if (endMember.NextMember != null)
                {
                    CurrentRow.FirstMember = endMember.NextMember;
                    var tailMember = endMember.GetTailMember();
                    CurrentRow.LastMember = tailMember.Equals(endMember.NextMember) ? null : tailMember;
                    CurrentRow.CurrentMember = null;
                    endMember.NextMember.PreMember = null;
                }
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void Copy()
        {
            MClipboard.SetData(MConstant.MDataFormats.Row, SelectedRows);
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void Paste()
        {
            var curRow = CurrentRow;
            var dataList = MClipboard.GetDataList();
            dataList.Sort((row, docRow) => row.Index > docRow.Index ? 1 : -1);
            PasteOfRow(dataList);
            MoveRow(curRow, GetPage(curRow.X, curRow.Y));
        }

        private void PasteOfRow(IEnumerable<DDocRow> rows)
        {
            var isTop = true;
            foreach (var dDocRow in rows)
            {
                var members = MClipboard.GetDataMember(dDocRow);
                //最上面的行需要判断粘贴的位置
                if (isTop)
                {
                    if (members.Count == 0)
                    {
                        InsertRowAfter(CurrentRow);
                    }
                    else if (CurrentRow.CurrentMember == null)
                    {
                        PasteOfMember(CurrentRow.DWriteHead, members, true);
                    }
                    else if (CurrentRow.CurrentMember.Equals(CurrentRow.LastMember))
                    {
                        PasteOfMember(CurrentRow.DWriteTail, members);
                    }
                    else
                    {
                        PasteOfMember(CurrentRow.DWriteAmong, members);
                    }
                    isTop = false;
                    continue;
                }
                //从第二行进行粘贴
                var row = InsertRowAfter(dDocRow.PreDocRow);
                PasteOfMember(CurrentRow.DWriteHead, members, true);
            }
        }

        private void PasteOfMember(Delegate method, List<DMember> members, bool isReverse = false)
        {
            if (isReverse)
            {
                for (var i = members.Count - 1; i >= 0; i--)
                {
                    method.DynamicInvoke(members[i]);
                }
            }
            else
            {
                foreach (var dMember in members)
                {
                    method.DynamicInvoke(dMember);
                }
            }
        }

        private void RemoveRow(DDocRow row)
        {
            if (row == null) return;
            var ownerDoc = row.OwnerDocument;
            if (row.Equals(ownerDoc.FirstRow))
            {
                if (row.NextDocRow == null)
                {
                    ownerDoc.FirstRow = null;
                    return;
                }
                else if (row.NextDocRow.Equals(ownerDoc.LastRow))
                {
                    ownerDoc.FirstRow = row.NextDocRow;
                    ownerDoc.LastRow = null;
                    row.NextDocRow.PreDocRow = null;
                    row.NextDocRow = null;
                    return;
                }
                ownerDoc.FirstRow = row.NextDocRow;
                row.NextDocRow.PreDocRow = null;
                row.NextDocRow = null;
            }
            else if (row.Equals(ownerDoc.LastRow))
            {
                if (row.PreDocRow.Equals(ownerDoc.FirstRow))
                {
                    row.PreDocRow.NextDocRow = null;
                    row.PreDocRow = null;
                    ownerDoc.LastRow = null;
                    return;
                }
                ownerDoc.LastRow = row.PreDocRow;
                row.PreDocRow.NextDocRow = null;
                row.PreDocRow = null;
            }
            else
            {
                if (row.PreDocRow != null)
                    row.PreDocRow.NextDocRow = row.NextDocRow;
                if (row.NextDocRow != null)
                    row.NextDocRow.PreDocRow = row.PreDocRow;
                row.PreDocRow = null;
                row.NextDocRow = null;
            }
        }

        public void JoinRow(DDocRow fRow, DDocRow sRow)
        {
            if (fRow == null || sRow == null) return;
            fRow.NextDocRow = sRow;
            sRow.PreDocRow = fRow;
        }

        public void JoinRow(DDocRow fRow, DDocRow sRow, DDocRow tRow)
        {
            if (fRow == null || sRow == null || tRow == null) return;
            fRow.NextDocRow = sRow;
            sRow.PreDocRow = fRow;
            sRow.NextDocRow = tRow;
            tRow.PreDocRow = sRow;
        }

        public void CutRow(DDocRow fRow, DDocRow sRow)
        {
            fRow.NextDocRow = null;
            sRow.PreDocRow = null;
        }

        public void ClearEffectMembers(MConstant.Effect effect)
        {
            switch (effect)
            {
                case MConstant.Effect.MouseEnter:
                    foreach (var touchMember in TouchMembers)
                    {
                        touchMember.MouseRelease();
                    }
                    TouchMembers.Clear();
                    break;
                case MConstant.Effect.MouseClick:
                    foreach (var clickMember in ClickMembers)
                    {
                        clickMember.MouseUnClick();
                    }
                    ClickMembers.Clear();
                    break;
                case MConstant.Effect.MouseDoubleClick:
                    foreach (var doubleClickMember in DoubleClickMembers)
                    {
                        doubleClickMember.MouseUnDbClick();
                    }
                    DoubleClickMembers.Clear();
                    break;
                case MConstant.Effect.MouseDrag:
                    foreach (var dragRow in DragRows)
                    {
                        dragRow.DragEnd();
                    }
                    foreach (var dragMember in DragMembers)
                    {
                        dragMember.MouseDragEnd();
                    }
                    DragMembers.Clear();
                    break;
            }
        }

        private bool IsInRowRange(DDocRow row, int y)
        {
            return y >= row.Y && y <= row.Y + row.Height;
        }

        public override SizeF MeasureString(DTextChar textChar)
        {
            return Context.Graphic.MeasureString(textChar.Value, textChar.WFont, 0, EditorSetting.DefaultStringFormat);
        }

        public override DComboBox CreateComboBox()
        {
            var comboBox = new DComboBox();
            HangUpMember = comboBox;
            UpdateDocument(MConstant.EventType.Write);
            Context.Invalidate(new Rectangle(CurrentPage.X, CurrentPage.Y, CurrentPage.Width,
                                             CurrentPage.Height));
            return comboBox;
        }

        public override DTextInput CreateTextInput()
        {
            var textInput = new DTextInput();
            HangUpMember = textInput;
            UpdateDocument(MConstant.EventType.Write);
            Context.Invalidate(new Rectangle(CurrentPage.X, CurrentPage.Y, CurrentPage.Width,
                                             CurrentPage.Height));
            return textInput;
        }

        public override DImage CreateImage()
        {
            var openFileDialog = new OpenFileDialog { Filter = @"jpg文件|*.jpg|gif文件|*.gif|bmp文件|*.bmp" };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return null;
            var dImage = new DImage(Image.FromFile(openFileDialog.FileName));
            HangUpMember = dImage;
            UpdateDocument(MConstant.EventType.Write);
            Context.Invalidate(new Rectangle(CurrentPage.X, CurrentPage.Y, CurrentPage.Width,
                                             CurrentPage.Height));
            return dImage;
        }

        public override DHorizonLine CreateHorizonLine()
        {
            var dHorizonLine = new DHorizonLine();
            HangUpMember = dHorizonLine;
            UpdateDocument(MConstant.EventType.Write);
            Context.Invalidate(new Rectangle(CurrentPage.X, CurrentPage.Y, CurrentPage.Width,
                                             CurrentPage.Height));
            return dHorizonLine;
        }

        public override DTable CreateTable()
        {
            var tableDialog = new TableDialog();
            if (tableDialog.ShowDialog() != DialogResult.OK) return null;

            if (CurrentRow == null) return null;

            var table = new DTable(tableDialog.RowNum, tableDialog.ColNum);
            table.SetDocument(this);

            JoinRow(CurrentRow.PreDocRow, table.Rows[0]);
            JoinRow(table.Rows[table.Rows.Count - 1], CurrentRow);

            if (CurrentRow.Equals(FirstRow))
            {
                CurrentRow.IsPageFirst = false;
                FirstRow = table.Rows[0];
                FirstRow.IsPageFirst = true;
                CurrentPage.PageBody.Data = FirstRow;
                CurrentPage.Resize();
            }
            LastRow = CurrentRow;
            Context.ForceShowCursor();
            Context.Invalidate();
            return table;
        }

        public override DCheckBox CreateCheckBox()
        {
            var dCheckBox = new DCheckBox();
            HangUpMember = dCheckBox;
            UpdateDocument(MConstant.EventType.Write);
            Context.Invalidate(new Rectangle(CurrentPage.X, CurrentPage.Y, CurrentPage.Width,
                                             CurrentPage.Height));
            return dCheckBox;
        }

        public void CreatePage()
        {
            var page = InsertPage();
            var row = CreateRow();
            row.IsPageFirst = true; //标记为真表示是某一分页的首行
            page.PageBody.Data = row; //将首行加入到分页中
            page.Resize();
            Context.AutoScrollPosition = new Point(0, row.Y + row.Height - Context.Height);
            page.Locate(row.X, row.Y);
            Context.ForceShowCursor();
        }

        public override EditorDocument NewDocument()
        {
            return Context.Reset();
        }

        public override void SetFont()
        {
            var fontDialogg = new FontDialog();
            if (fontDialogg.ShowDialog() != DialogResult.OK) return;
            var font = fontDialogg.Font;
            SelectedRows.Sort((row, docRow) => row.Index > docRow.Index ? 1 : -1);
            foreach (var selectedRow in SelectedRows)
            {
                selectedRow.ChangeFont(font);
            }
            if (SelectedRows.Count > 0)
            {
                var selectedRow = SelectedRows[0];
                var members = selectedRow.SelectedMembers;
                if (members.Count > 0)
                {
                    selectedRow.ReComposing(members[0]);
                }
                MoveRow(selectedRow, GetPage(selectedRow.X, selectedRow.Y));
            }
            Context.Invalidate();
        }

        public override void SetFontColor()
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            var color = colorDialog.Color;
            foreach (var selectedRow in SelectedRows)
            {
                selectedRow.ChangeFontColor(color);
            }
            Context.Invalidate();
        }

        public override void XmlExport()
        {
            var saveFileDialog = new SaveFileDialog() { Filter = @"xml文件|*.xml" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var xmlTranlator = XmlTranslator.Instance(this);
            xmlTranlator.Export(this, saveFileDialog.FileName);
        }

        public override void XmlImport()
        {
            var openFileDialog = new OpenFileDialog { Filter = @"xml文件|*.xml" };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            var xmlTranlator = XmlTranslator.Instance(this);
            this.FirstRow = null;
            xmlTranlator.Import(openFileDialog.FileName);
            MoveRow(FirstRow, FirstPage);
            Context.Invalidate();
        }

        public override void SetParagraphLeft()
        {
            if (CurrentRow == null) return;
            CurrentRow.Paragraph = RowParagraph.Left;
            Context.ForceShowCursor();
            Context.Invalidate();
        }

        public override void SetParagraphMiddle()
        {
            if (CurrentRow == null) return;
            CurrentRow.Paragraph = RowParagraph.Middle;
            Context.ForceShowCursor();
            Context.Invalidate();
        }

        public override void SetParagraphRight()
        {
            if (CurrentRow == null) return;
            CurrentRow.Paragraph = RowParagraph.Right;
            Context.ForceShowCursor();
            Context.Invalidate();
        }

        public override void Undo()
        {

        }

        public override void Redo()
        {

        }
    }

}
