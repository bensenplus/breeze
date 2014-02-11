using System;
using System.Collections.Generic;
using System.Drawing;
using WordProcessor.Designer;
using WordProcessor.Interface;

namespace WordProcessor.Dom
{
    /// <remarks>
    ///  文档行
    /// </remarks>
    public class DDocRow : DMember,ICompatible
    {
        public RowParagraph Paragraph = RowParagraph.Left;
        public override int Width { get; set; }
        private int _height;
        public override int Height
        {
            get
            {
                var member = GetHighestMember(MemberType.Unknown);
                return member == null ? _height : member.Height;
            }

            set { _height = value; }
        }

        private int _index;
        public int Index
        {
            get
            {
                if (PreDocRow == null) return _index;
                return PreDocRow.Index + 1;
            }

            set { _index = value; }
        }

        public int RowSpacing;
        private int _x;
        private int _y;

        public override int X
        {
            get
            {
                if (Paragraph == RowParagraph.Right)
                {
                    return OwnerDocument.GetPageBodyX() +
                           OwnerDocument.GetPageBodyWidth();
                }
                else if (Paragraph == RowParagraph.Middle)
                {
                    return OwnerDocument.GetPageBodyX() +
                           OwnerDocument.GetPageBodyWidth()/2;
                }
                return OwnerDocument.GetPageBodyX();
            }
            set { _x = value; }
        }

        public override int Y
        {
            get
            {
                return PreDocRow == null || IsPageFirst ? _y
                           : PreDocRow.Y + PreDocRow.Height + RowSpacing;
            }
            set { _y = value; }
        }

        public bool IsPageFirst; //分页首行标志
        public bool IsContinue; //成员满行且连续
        public bool IsHold; //持有该行，不做删除
        public bool IsReadOnly;
        public MDocument OwnerDocument;
        public DDocRow PreDocRow;
        public DDocRow NextDocRow;
        public DRowMember CurrentMember;
        public DRowMember LastMember;
        public DRowMember FirstMember;
        public DRowMember LargestTxtMember;
        public readonly List<DRowMember> SelectedMembers = new List<DRowMember>();
        public delegate void Write(DRowMember linkedListHead);
        public readonly Write DWriteHead;
        public readonly Write DWriteTail;
        public readonly Write DWriteAmong;

        public DDocRow()
        {
            DWriteHead = WriteHead;
            DWriteTail = WriteTail;
            DWriteAmong = WriteAmong;
        }

        public void AddRowMember(DRowMember member)
        {
            member.OwnerDocRow = this;
            if (FirstMember == null)
            {
                member.X = this.X;
                member.Y = this.Y;
                FirstMember = member;
            }
            else if (LastMember == null)
            {
                FirstMember.NextMember = member;
                member.PreMember = FirstMember;
                LastMember = member;
            }
            else
            {
                LastMember.NextMember = member;
                member.PreMember = LastMember;
                LastMember = member;
            }
        }

        public virtual void Click(int x, int y)
        {
            var touchMember = OwnerDocument.TouchMember;
            if (touchMember != null &&
                touchMember.MType != MemberType.TextChar &&
                touchMember.TouchMe(x, y))
            {
                ProcessCurrentMember((DRowMember)touchMember, x);
                return;
            }

            CurrentMember = null;

            var member = FirstMember;
            if (member == null) return;

            while (member != null)
            {
                if (IsInMemeberRange(member, x, y))
                {
                    CurrentMember = member;
                    return;
                }
                member = member.NextMember;
            }
        }

        public virtual void MoveIn(int x, int y)
        {
            var member = FirstMember;
            while (member != null)
            {
                if (member.IsMouseEnter(x, y))
                {
                    OwnerDocument.TouchMember = member;
                    OwnerDocument.TouchMembers.Add(member);
                    member.MouseCapture();
                    return;
                }
                member = member.NextMember;
            }
            OwnerDocument.Context.ChangeCursor(MConstant.CursorType.Ibeam);
        }

        public virtual void DragBegin(int x, int y)
        {
        }

        public virtual void DragEnd()
        {
        }

        public DRowMember DragMember(int x, int y)
        {
            var member = FirstMember;
            if (member == null) return null;
            while (member != null)
            {
                if (member.IsMouseEnter(x, y))
                {
                    member.MouseDragBegin(x, y);
                    return member;
                }
                member = member.NextMember;
            }
            return null;
        }

        public void SelectAllMember()
        {
            var member = FirstMember;
            if (member == null) return;

            while (member != null)
            {
                member.IsSelected = true;
                member.Select(true);
                SelectedMembers.Add(member);
                member = member.NextMember;
            }
        }

        public void SelectMember(int startX, int endX)
        {
            var member = FirstMember;
            if (member == null) return;

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

            while (member != null)
            {
                if (member.MType == MemberType.Table && x2 > member.X)
                {
                    member.IsSelected = true;
                    member.Select();
                    SelectedMembers.Add(member);
                }
                else if (member.X + member.Width/2 > x1 && x2 > member.X + member.Width/2)
                {
                    member.IsSelected = true;
                    member.Select();
                    SelectedMembers.Add(member);
                }
                member = member.NextMember;
            }
        }

        public void ClearSelectMembers()
        {
            foreach (var selectMember in SelectedMembers)
            {
                selectMember.IsSelected = false;
                selectMember.ClearSelect();
            }
            SelectedMembers.Clear();
        }

        /// <summary>
        /// 从指定成员开始重新排版
        /// </summary>
        public void ReComposing(DRowMember member)
        {
            if (member == null) return;
            var leftPointer = member;
            var rightPointer = member.NextMember;
            DRowMember linkedListHead = null;
            while (true)
            {
                if (rightPointer == null) break;
                //超出界限
                if (rightPointer.X + rightPointer.Width > this.X + this.Width)
                {
                    leftPointer.NextMember = null;
                    rightPointer.PreMember = null;
                    linkedListHead = rightPointer;
                    LastMember = leftPointer;
                    IsContinue = true;
                }
                leftPointer = rightPointer;
                rightPointer = rightPointer.NextMember;
            }

            if (linkedListHead == null) return;

            if (NextDocRow == null)
            {
                NextDocRow = OwnerDocument.CreateRow();
                NextDocRow.WriteHead(linkedListHead);
            }
            else if (linkedListHead.GetTailMember().IsBreakTail)
            {
                NextDocRow = OwnerDocument.InsertRowBefore(NextDocRow);
                NextDocRow.WriteHead(linkedListHead);
            }
            else
            {
                NextDocRow.WriteHead(linkedListHead);
            }
        }

        /// <summary>
        /// 将传入的成员链末尾与该行首成员进行连接，超出的
        /// 尾部链自动向下一行遍历连接
        /// </summary>
        public void WriteHead(DRowMember linkedListHead)
        {
            if (linkedListHead == null) return;

            linkedListHead.RecursiveChangeDocRow(this);
            linkedListHead.PreMember = null;
            var lastMember = linkedListHead.GetTailMember();
            lastMember.NextMember = this.FirstMember;

            if (this.FirstMember != null)
            {
                this.FirstMember.PreMember = lastMember;
            }

            //定义两个活动指针
            var leftPointer = lastMember;
            var rightPointer = this.FirstMember;
            DRowMember oLinkedListHeader = null;
            this.FirstMember = linkedListHead;
            var isOutRange = false;

            while (true)
            {
                leftPointer.AfterChangeRow();
                if (rightPointer == null) break;
                //此处断链子
                if (GetRangeResult(rightPointer) == 1)
                {
                    if (Paragraph == RowParagraph.Right || Paragraph == RowParagraph.Middle)
                    {
                        rightPointer = GetOutRangeMember();
                        leftPointer = rightPointer.PreMember;
                    }
                    leftPointer.NextMember = null;
                    rightPointer.PreMember = null;
                    oLinkedListHeader = rightPointer;
                    IsContinue = true;
                    isOutRange = true;
                    break;
                }
                else if (GetRangeResult(rightPointer) == 0)
                {
                    IsContinue = true;
                }
                else
                {
                    IsContinue = false;
                }
                leftPointer.IsBreakTail = false;
                leftPointer = rightPointer;
                rightPointer = rightPointer.NextMember;
            }

            this.LastMember = leftPointer.Equals(linkedListHead) ? null : leftPointer;

            //如果该行未越界则返回
            if (!isOutRange) return;

            if (NextDocRow == null)
            {
                NextDocRow = OwnerDocument.CreateRow();
                NextDocRow.WriteHead(oLinkedListHeader);
            }
            else if (oLinkedListHeader.GetTailMember().IsBreakTail)
            {
                NextDocRow = OwnerDocument.InsertRowBefore(NextDocRow);
                NextDocRow.WriteHead(oLinkedListHeader);
            }
            else
            {
                NextDocRow.WriteHead(oLinkedListHeader);
            }
        }

        /// <summary>
        /// 将传入的成员链头与该行末成员进行连接，超出的
        /// 尾部链自动向下一行遍历连接
        /// </summary>
        public void WriteTail(DRowMember linkedListHead)
        {
            if (linkedListHead == null) return;

            linkedListHead.RecursiveChangeDocRow(this);
            linkedListHead.PreMember = this.LastMember;
            this.LastMember.NextMember = linkedListHead;

            //定义两个活动指针
            var leftPointer = this.LastMember;
            var rightPointer = linkedListHead;
            DRowMember oLinkedListHeader = null;
            var isOutRange = false;

            while (true)
            {
                //此处断链子
                if (GetRangeResult(rightPointer) == 1)
                {
                    rightPointer.GetTailMember().IsBreakTail = leftPointer.IsBreakTail;
                    leftPointer.IsBreakTail = false;
                    leftPointer.NextMember = null;
                    rightPointer.PreMember = null;
                    oLinkedListHeader = rightPointer;
                    IsContinue = true;
                    isOutRange = true;
                    break;
                }
                else if (GetRangeResult(rightPointer) == 0)
                {
                    IsContinue = true;
                }
                else
                {
                    IsContinue = false;
                }
                rightPointer.AfterChangeRow();
                //断点属性传递
                rightPointer.IsBreakTail = leftPointer.IsBreakTail;
                leftPointer.IsBreakTail = false;
                leftPointer = rightPointer;
                rightPointer = rightPointer.NextMember;
                if (rightPointer == null) break;
            }

            this.LastMember = leftPointer;

            //如果该行未越界则返回
            if (!isOutRange) return;

            if (NextDocRow == null)
            {
                NextDocRow = OwnerDocument.CreateRow();
                NextDocRow.WriteHead(oLinkedListHeader);
            }
            else if (oLinkedListHeader.GetTailMember().IsBreakTail)
            {
                NextDocRow = OwnerDocument.InsertRowBefore(NextDocRow);
                NextDocRow.WriteHead(oLinkedListHeader);
            }
            else
            {
                NextDocRow.WriteHead(oLinkedListHeader);
            }
        }

        /// <summary>
        /// 将传入的成员链头与该行当前成员进行连接，超出的
        /// 尾部链自动向下一行遍历连接
        /// </summary>
        public void WriteAmong(DRowMember linkedListHead)
        {
            if (linkedListHead == null) return;
            
            linkedListHead.RecursiveChangeDocRow(this);
            linkedListHead.PreMember = this.CurrentMember;
            linkedListHead.NextMember = this.CurrentMember.NextMember;
            this.CurrentMember.NextMember = linkedListHead;
            if (linkedListHead.NextMember != null)
                linkedListHead.NextMember.PreMember = linkedListHead;

            //定义两个活动指针
            var leftPointer = this.CurrentMember;
            var rightPointer = linkedListHead;
            DRowMember oLinkedListHeader = null;
            var isOutRange = false;

            while (true)
            {
                //此处断链子
                if (GetRangeResult(rightPointer) == 1)
                {
                    if (Paragraph == RowParagraph.Right)
                    {
                        rightPointer = GetOutRangeMember();
                        leftPointer = rightPointer.PreMember;
                    }
                    leftPointer.NextMember = null;
                    rightPointer.PreMember = null;
                    oLinkedListHeader = rightPointer;
                    IsContinue = true;
                    isOutRange = true;
                    break;
                }
                else if (GetRangeResult(rightPointer) == 0)
                {
                    IsContinue = true;
                }
                else
                {
                    IsContinue = false;
                }
                rightPointer.AfterChangeRow();
                //断点属性传递
                if(!rightPointer.IsBreakTail) rightPointer.IsBreakTail = leftPointer.IsBreakTail;
                leftPointer.IsBreakTail = false;
                leftPointer = rightPointer;
                rightPointer = rightPointer.NextMember;
                if (rightPointer == null) break;
            }

            this.LastMember = leftPointer;

            //如果该行未越界则返回
            if (!isOutRange) return;

            if (NextDocRow == null)
            {
                NextDocRow = OwnerDocument.CreateRow();
                NextDocRow.WriteHead(oLinkedListHeader);
            }
            else if (oLinkedListHeader.GetTailMember().IsBreakTail)
            {
                NextDocRow = OwnerDocument.InsertRowBefore(NextDocRow);
                NextDocRow.WriteHead(oLinkedListHeader);
            }
            else
            {
                NextDocRow.WriteHead(oLinkedListHeader);
            }
        }

        private DRowMember GetOutRangeMember()
        {
            var width = 0;
            var tempMember = FirstMember;
            while (tempMember != null)
            {
                width += tempMember.Width;
                if (width > OwnerDocument.GetPageBodyWidth())
                {
                    break;
                }
                tempMember = tempMember.NextMember;
            }
            return tempMember;
        }

        private int GetRangeResult(DRowMember rightMember)
        {
            var flag = -1;
            if (Paragraph == RowParagraph.Left)
            {
                if (rightMember.X + rightMember.Width > this.X + this.Width)
                {
                    flag = 1;
                }
                else if (rightMember.X + rightMember.Width == this.X + this.Width)
                {
                    flag = 0;
                }
            }
            else if(Paragraph == RowParagraph.Middle)
            {
                if (rightMember.X + rightMember.Width > this.X + this.Width/2)
                {
                    flag = 1;
                }
                else if (rightMember.X + rightMember.Width == this.X + this.Width/2)
                {
                    flag = 0;
                }
            }
            else
            {
                if (FirstMember.X < this.X - this.Width)
                {
                    flag = 1;
                }
                else if (FirstMember.X == this.X - this.Width)
                {
                    flag = 0;
                }
            }
            return flag;
        }

        /// <summary>
        /// 为删除动作连接成员
        /// </summary>
        /// <param name="member">连接的成员</param>
        /// <param name="isDel"></param>
        /// <returns>连接成功TRUE,失败FALSE</returns>
        public bool ConcatMemberForDelete(DRowMember member, bool isDel)
        {
            if (member == null) return false;

            if (FirstMember == null)
            {
                FirstMember = member;
                member.OwnerDocRow = this;
                return true;
            }
            else if (LastMember == null)
            {
                if (IsContinue && isDel)
                {
                    FirstMember = member;
                    member.OwnerDocRow = this;
                    return true;
                }
                else if (IsOutRange(FirstMember, member))
                {
                    IsContinue = true;
                    return false;
                }
                JoinMember(FirstMember, member);
                LastMember = member;
                member.OwnerDocRow = this;
                return true;
            }

            if (isDel && !LastMember.IsBreakTail)
            {
                var tempMember = LastMember.PreMember;
                CutMember(LastMember.PreMember, LastMember);
                LastMember = tempMember;
            }

            if (IsOutRange(LastMember, member))
            {
                IsContinue = true;
                return false;
            }

            JoinMember(LastMember, member);
            if (member.IsBreakTail) IsContinue = false;
            LastMember = member;
            member.OwnerDocRow = this;
            return true;
        }

        public DDocRow GetTailRow()
        {
            return NextDocRow == null ? this : NextDocRow.GetTailRow();
        }

        /// <summary>
        /// 将该行某个成员去除
        /// </summary>
        /// <param name="member"></param>
        public void RemoveMember(DRowMember member)
        {
            if (member == null) return;
            if (member.Equals(FirstMember))
            {
                if (member.NextMember == null)
                {
                    FirstMember = null;
                    return;
                }
                else if (member.NextMember.Equals(LastMember))
                {
                    FirstMember = member.NextMember;
                    LastMember = null;
                    member.NextMember.PreMember = null;
                    member.NextMember = null;
                    return;
                }
                FirstMember = member.NextMember;
                member.NextMember.PreMember = null;
                member.NextMember = null;
            }
            else if (member.Equals(LastMember))
            {
                if (member.PreMember.Equals(FirstMember))
                {
                    member.PreMember.NextMember = null;
                    member.PreMember = null;
                    LastMember = null;
                    return;
                }
                LastMember = member.PreMember;
                member.PreMember.NextMember = null;
                member.PreMember = null;
            }
            else
            {
                if(member.PreMember != null)
                    member.PreMember.NextMember = member.NextMember;
                if(member.NextMember != null)
                    member.NextMember.PreMember = member.PreMember;
                member.PreMember = null;
                member.NextMember = null;
            }
        }

        public void AddSelecteMember(DRowMember member)
        {
            if (SelectedMembers.Contains(member)) return;
            SelectedMembers.Add(member);
        }

        public DRowMember GetHighestMember(MemberType type)
        {
            if (FirstMember == null) return null;

            DRowMember hMember = null;
            var tempMember = FirstMember;

            while (tempMember != null)
            {
                if (type != MemberType.Unknown && tempMember.MType != type)
                {
                    tempMember = tempMember.NextMember;
                    continue;
                }

                if (hMember == null || tempMember.Height > hMember.Height) hMember = tempMember;

                tempMember = tempMember.NextMember;
            }

            return hMember;
        }

        public DRowMember GetShortestMember(MemberType type)
        {
            if (FirstMember == null) return null;

            DRowMember hMember = null;
            var tempMember = FirstMember;

            while (tempMember != null)
            {
                if (type != MemberType.Unknown && tempMember.MType != type)
                {
                    tempMember = tempMember.NextMember;
                    continue;
                }

                if (hMember == null || tempMember.Height < hMember.Height) hMember = tempMember;

                tempMember = tempMember.NextMember;
            }

            return hMember;
        }

        public void Resize(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Locate(int x)
        {
            if (FirstMember == null)
            {
                CurrentMember = null;
                return;
            }

            var tempMember = FirstMember;
            while (tempMember != null)
            {
                if (tempMember.NextMember != null)
                {
                    if (tempMember.X + tempMember.Width/2 <= x &&
                        tempMember.NextMember.X + tempMember.NextMember.Width/2 > x)
                    {
                        CurrentMember = tempMember;
                        break;
                    }
                }
                else
                {
                    if (tempMember.X + tempMember.Width / 2 <= x)
                    {
                        CurrentMember = tempMember;
                        break;
                    }
                }
                tempMember = tempMember.NextMember;
            }
        }

        public void Locate(int x, int y)
        {
            if (this.NextDocRow == null && y > this.Y + this.Height)
            {
                OwnerDocument.CurrentRow = this;
                OwnerDocument.CurrentRow.CurrentMember = this.LastMember ?? this.FirstMember;
                return;
            }

            if (y >= this.Y && y <= this.Y + this.Height)
            {
                OwnerDocument.CurrentRow = this;
                this.Click(x, y);
            }
            else if (NextDocRow != null && y >= NextDocRow.Y)
            {
                NextDocRow.Locate(x , y);
            }
        }

        public int MemberCount()
        {
            var count = 0;
            var member = FirstMember;
            while (member != null)
            {
                count++;
                member = member.NextMember;
            }
            return count;
        }

        public void ChangeFont(Font font)
        {
            foreach (var member in SelectedMembers)
            {
                if (member.MType == MemberType.TextChar)
                {
                    member.WFont = font;
                    var size = OwnerDocument.MeasureString((DTextChar)member);
                    member.Width = (int) Math.Ceiling(size.Width);
                    member.Height = (int) Math.Ceiling(size.Height);
                }
            }
        }

        public void ChangeFontColor(Color color)
        {
            foreach (var member in SelectedMembers)
            {
                if (member.MType == MemberType.TextChar)
                {
                    member.FontColor = color;
                }
            }
        }

        public void LocateCurrentMember(int x, int y)
        {
            CurrentMember = null;
            var tempMember = FirstMember;
            while (tempMember != null)
            {
                if (IsInMemeberRange(tempMember, x, y))
                {
                    CurrentMember = tempMember;
                    break;
                }
                tempMember = tempMember.NextMember;
            }
        }

        public DDocRow CopyData()
        {
            var row = new DDocRow();
            var tempMember = FirstMember;
            while (tempMember != null)
            {
                var copyMember = (DRowMember)tempMember.Copy();
                copyMember.OwnerDocRow = row;

                if (row.FirstMember == null)
                {
                    row.FirstMember = copyMember;
                }
                else if (row.LastMember == null)
                {
                    row.LastMember = copyMember;
                    row.FirstMember.NextMember = row.LastMember;
                    row.LastMember.PreMember = row.FirstMember;
                }
                else
                {
                    row.LastMember.NextMember = copyMember;
                    copyMember.PreMember = row.LastMember;
                    row.LastMember = copyMember;
                }
                tempMember = tempMember.NextMember;
            }
            return row;
        }

        private void ProcessCurrentMember(DRowMember member, int x)
        {
            switch (member.MType)
            {
                case MemberType.TextInput:
                    CurrentMember = member;
                    break;
                case MemberType.CheckBox:
                    break;
                case MemberType.ComboBox:
                    break;
                case MemberType.Table:
                    OwnerDocument.ClickMembers.Add(member);
                    break;
                case MemberType.Image:
                    OwnerDocument.ClickMembers.Add(member);
                    break;
                case MemberType.HorizonLine:
                    OwnerDocument.ClickMembers.Add(member);
                    break;
            }
            member.MouseClick();
        }

        private void JoinMember(DRowMember fMember, DRowMember sMember)
        {
            if (fMember == null || sMember == null) return;
            fMember.NextMember = sMember;
            sMember.PreMember = fMember;
        }

        private void CutMember(DRowMember fMember, DRowMember sMember)
        {
            if(fMember != null) fMember.NextMember = null;
            if(sMember != null) sMember.PreMember = null;
        }

        private bool IsOutRange(DRowMember fmember, DRowMember sMember)
        {
            return fmember.X + fmember.Width + sMember.Width > this.X + this.Width;
        }

        private bool IsInMemeberRange(DRowMember member, int x, int y)
        {
            if (member.NextMember == null)
            {
                return ((x > member.X + member.Width / 2 && x <= member.X + member.Width)
                        || (x > member.X + member.Width)) &&
                        y >= member.Y && y < member.Y + member.Height;
            }
            else
            {
                return (x > member.X + member.Width / 2 && x <= member.X + member.Width &&
                        y >= member.Y && y < member.Y + member.Height)
                        || (x > member.NextMember.X && x <= member.NextMember.X + member.NextMember.Width / 2 &&
                            y >= member.NextMember.Y && y < member.NextMember.Y + member.NextMember.Height);
            }
        }

        public int SumMembersWidth()
        {
            var width = 0;
            if (FirstMember == null) return 0; 
            var tempMember = FirstMember;
            while (tempMember != null)
            {
                width += tempMember.Width;
                tempMember = tempMember.NextMember;
            }
            return width;
        }
    }

    public enum RowParagraph
    {
        Left, 
        Middle,
        Right
    }
}