using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WordProcessor.Control;
using WordProcessor.Dom;

namespace WordProcessor.Designer
{
    public class PFooterDocument : MDocument
    {
        public DPageFooter OwnerPfooter;

        public PFooterDocument(DPageFooter pageFooter)
        {
            OwnerPfooter = pageFooter;
            CreateRow();
        }

        public override DDocRow CreateRow()
        {
            var row = new DDocRow() { OwnerDocument = this, IsHold = true, IsContinue = false };

            CurrentRow = row;

            if (FirstRow == null)
            {
                FirstRow = row;
                row.Index = 1;
                ResetPosition();
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

            row.Width = row.PreDocRow.Width;
            row.Height = EditorSetting.DefaultDocRowHeight;

            LastRow = row;

            return row;
        }

        public override DDocRow InsertRowBefore(DDocRow row)
        {
            var newDocRow = new DDocRow()
            {
                OwnerDocument = this,
                IsHold = true,
                IsContinue = false,
                Height = EditorSetting.DefaultDocRowHeight
            };

            if (row == null && FirstRow == null)
            {
                FirstRow = newDocRow;
                return newDocRow;
            }

            if (row == null) return null;

            newDocRow.Width = row.Width;

            if (row.PreDocRow == null)
            {
                newDocRow.X = row.X;
                newDocRow.Y = row.Y;
                newDocRow.Index = 1;
                row.PreDocRow = newDocRow;
                newDocRow.NextDocRow = row;
                FirstRow = newDocRow;
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

            return newDocRow;
        }

        public DDocRow InsertRowAfter(DDocRow row)
        {
            if (row == null) return null;

            var newDocRow = new DDocRow()
            {
                OwnerDocument = this,
                IsHold = true,
                IsContinue = false,
                Height = EditorSetting.DefaultDocRowHeight,
                Width = row.Width
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

            return newDocRow;
        }

        public void ResetPosition()
        {
            if (FirstRow == null) return;
            FirstRow.X = OwnerPfooter.X + OwnerPfooter.PaddingLeft;
            FirstRow.Y = OwnerPfooter.Y;
            FirstRow.Height = EditorSetting.DefaultDocRowHeight;
            FirstRow.Width = OwnerPfooter.Width - OwnerPfooter.PaddingLeft - OwnerPfooter.PaddingRight;
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
                    //SelectedDelete();
                    break;
                case MConstant.EventType.DoubleClick:
                    //DoubleClick();
                    break;
                case MConstant.EventType.Copy:
                    //Copy();
                    break;
                case MConstant.EventType.Paste:
                    //Paste();
                    break;
            }
            SynchronizeData();
        }

        public void SynchronizeData()
        {
            var tempFooter = OwnerPfooter.GetTopPageFooter();
            while (tempFooter != null)
            {
                if (tempFooter.Equals(OwnerPfooter))
                {
                    tempFooter = tempFooter.NextPageFooter;
                    continue;
                }
                SynchronizeData(tempFooter);
                tempFooter = tempFooter.NextPageFooter;
            }
        }

        public void SynchronizeData(DPageFooter pageFooter)
        {
            if (pageFooter == null) return;
            var doc = pageFooter.OwnerDocument;
            doc.FirstRow = null;
            doc.LastRow = null;
            var data = CopyData();
            foreach (var dDocRow in data)
            {
                dDocRow.OwnerDocument = doc;
                if (doc.FirstRow == null)
                {
                    doc.FirstRow = dDocRow;
                }
                else if (doc.LastRow == null)
                {
                    doc.LastRow = dDocRow;
                    dDocRow.PreDocRow = doc.FirstRow;
                    doc.FirstRow.NextDocRow = dDocRow;
                }
                else
                {
                    doc.LastRow.NextDocRow = dDocRow;
                    dDocRow.PreDocRow = doc.LastRow;
                    doc.LastRow = dDocRow;
                }
            }
            doc.ResetPosition();
        }

        private IEnumerable<DDocRow> CopyData()
        {
            var rows = new List<DDocRow>();
            var tempRow = FirstRow;
            while (tempRow != null)
            {
                var row = tempRow.CopyData();
                rows.Add(row);
                tempRow = tempRow.NextDocRow;
            }
            return rows;
        }

        public override void UpdateStatus(MConstant.EventType eventType)
        {
        }

        /// <summary>
        /// 键盘输入或菜单插入成员入行
        /// </summary>
        private void Write()
        {
            if (HangUpMember == null) return;

            /**************** 页面横向的容纳判断和处理 ***********************/

            //表示行首输入
            if (CurrentRow.CurrentMember == null)
            {
                CurrentRow.WriteHead(HangUpMember);
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

            //查看新成员的插入是否需要扩充单元格
            ResizeCell();

            CurrentRow = HangUpMember.OwnerDocRow;
            CurrentRow.CurrentMember = HangUpMember;
            Context.ForceShowCursor();

            HangUpMember = null;
        }

        private void ResizeCell()
        {
            //OwnerCell.Resize();
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
                ResizeCell();
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
                    ResizeCell();
                    SetFocus(tempRow);
                    return;
                }
                tempRow = InsertRowBefore(CurrentRow.NextDocRow);
                ResizeCell();
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

                ResizeCell();

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

            ResizeCell();

            SetFocus(curRow.NextDocRow);
        }

        private void SetFocus(DDocRow row)
        {
            if (row == null) return;
            CurrentRow = row;
            CurrentRow.CurrentMember = row.CurrentMember;
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
                if (preRow == null)
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
                //Context.LocateCurrentPage(preRow.X, preRow.Y, false);
                ResizeCell();
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
            ResizeCell();
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

        /// <summary>
        /// 改变所有行宽度与单元格比例一致
        /// </summary>
        public void ResetRowWidth()
        {
            var tempRow = FirstRow;
            while (tempRow != null)
            {
                //tempRow.Width = OwnerCell.Width - OwnerCell.PaddingLeft - OwnerCell.PaddingRight;
                tempRow = tempRow.NextDocRow;
            }
        }

        /// <summary>
        /// 对文档内容的重排版
        /// </summary>
        public void ComposeContent()
        {
            //首先将所有成员存入缓存
            var allMembers = new List<DMember>();
            var allRows = new List<DDocRow>();
            var rowPointer = FirstRow;
            while (rowPointer != null)
            {
                var memberPointer = rowPointer.FirstMember;
                //空行插入一个空成员
                if (memberPointer == null)
                {
                    allMembers.Add(new DMember());
                }

                while (memberPointer != null)
                {
                    allMembers.Add(memberPointer);
                    memberPointer = memberPointer.NextMember;
                }
                allRows.Add(rowPointer);
                rowPointer.FirstMember = null;
                rowPointer.LastMember = null;
                rowPointer.CurrentMember = null;
                rowPointer = rowPointer.NextDocRow;
            }

            //然后重新写入
            LastRow = null;

            var rowIndex = 0;
            var memberIndex = 0;
            DDocRow row = null;

            while (true)
            {
                //成员写完但有行多出，则删除行后退出
                if (memberIndex > allMembers.Count - 1 && rowIndex < allRows.Count)
                {
                    LastRow = FirstRow.Equals(row) ? null : row;
                    for (var i = rowIndex; i < allRows.Count; i++)
                    {
                        if (allRows[i].IsHold) continue;
                        if (allRows[i].PreDocRow != null) allRows[i].PreDocRow.NextDocRow = null;
                        allRows[i].PreDocRow = null;
                        break;
                    }
                    break;
                }
                //所有成员已经写完则退出
                else if (memberIndex > allMembers.Count - 1)
                {
                    LastRow = FirstRow.Equals(row) ? null : row;
                    break;
                }
                //所有行写满，还有成员未写，则插入新行
                else if (row != null && rowIndex >= allRows.Count && memberIndex <= allMembers.Count - 1)
                {
                    row = InsertRowAfter(row);
                    //表示是环境(单元格)大小改变而产生的新行，非写入产生的新行
                    row.IsHold = false; 
                    memberIndex = RowCompose(row, allMembers, memberIndex);
                    continue;
                }
                //行移动
                else
                {
                    row = allRows[rowIndex];
                    if (rowIndex == 0) FirstRow = row;
                }
                memberIndex = RowCompose(row, allMembers, memberIndex);
                rowIndex++;
            }
        }

        private int RowCompose(DDocRow row, List<DMember> allMembers, int index)
        {
            var tempIndex = 0;
            var firstMember = allMembers[index] as DRowMember;
            for (var i = index; i < allMembers.Count; i++)
            {
                var member = allMembers[i] as DRowMember;
                if (member == null) continue;
                tempIndex = i;
                //表示首个写入成员是空成员，则独立于一行
                if (firstMember != null && firstMember.OwnerDocRow == null)
                {
                    tempIndex = index + 1;
                    break;
                }
                //表示首个写入成员是断点，连接后退出
                else if (firstMember != null && firstMember.IsBreakTail)
                {
                    firstMember.OwnerDocRow = row;
                    row.FirstMember = firstMember;
                    firstMember.NextMember = null;
                    tempIndex = index + 1;
                    break;
                }
                else if (row.FirstMember == null)
                {
                    member.OwnerDocRow = row;
                    row.FirstMember = member;
                    member.PreMember = null;
                    member.NextMember = null;
                    //如果最后一个成员已写入，将序号加1，使得compseContent循环退出
                    if (i == allMembers.Count - 1) tempIndex = i + 1;
                }
                else if (row.LastMember == null)
                {
                    if (row.FirstMember.IsBreakTail)
                    {
                        member.PreMember = null;
                        break;
                    }
                    else if(row.FirstMember.X + row.FirstMember.Width + allMembers[i].Width > row.X + row.Width)
                    {
                        row.IsContinue = true;
                        member.PreMember = null;
                        break;
                    }
                    else
                    {
                        member.OwnerDocRow = row;
                        row.LastMember = member;
                        member.PreMember = row.FirstMember;
                        row.FirstMember.NextMember = member;
                        member.NextMember = null;
                        //如果最后一个成员已写入，将序号加1，使得compseContent循环退出
                        if (i == allMembers.Count - 1) tempIndex = i + 1;
                    }
                }
                else
                {
                    if (row.LastMember.IsBreakTail)
                    {
                        member.PreMember = null;
                        break;
                    }
                    else if(row.LastMember.X + row.LastMember.Width + allMembers[i].Width > row.X + row.Width)
                    {
                        row.IsContinue = true;
                        member.PreMember = null;
                        break;
                    }
                    else
                    {
                        member.OwnerDocRow = row;
                        row.LastMember.NextMember = member;
                        member.PreMember = row.LastMember;
                        member.NextMember = null;
                        row.LastMember = member;
                        //如果最后一个成员已写入，将序号加1，使得compseContent循环退出
                        if (i == allMembers.Count - 1) tempIndex = i + 1;
                    }
                }
            }
            return tempIndex;
        }

        public override void GetCursorLocation(out int cursorX, out int cursorY, out int cursorHeight, out bool isShow)
        {
            base.GetCursorLocation(out cursorX, out cursorY, out cursorHeight, out isShow);

            if (this.CurrentRow != null)
            {
                cursorX = this.CurrentRow.X;
                cursorY = this.CurrentRow.FirstMember == null ? this.CurrentRow.Y : this.CurrentRow.FirstMember.Y;
                cursorHeight = this.CurrentRow.FirstMember == null
                                   ? EditorSetting.CurrentFont.Height
                                   : this.CurrentRow.FirstMember.Height;
            }

            if (this.CurrentRow != null && this.CurrentRow.CurrentMember != null)
            {
                cursorY = this.CurrentRow.CurrentMember.Y;
                cursorX = this.CurrentRow.CurrentMember.X + this.CurrentRow.CurrentMember.Width;
                cursorHeight = this.CurrentRow.CurrentMember.Height;
            }

            cursorX -= Math.Abs(Context.AutoScrollPosition.X);
            cursorY -= Math.Abs(Context.AutoScrollPosition.Y);
        }

        public void ResetRowLocation()
        {
            if (FirstRow == null) return;
            //FirstRow.X = OwnerCell.X + OwnerCell.PaddingLeft;
            //FirstRow.Y = OwnerCell.Y + OwnerCell.PaddingTop;
        }

        public void LocateCurrentRow(int x, int y)
        {
            var tempRow = FirstRow;
            while (tempRow != null)
            {
                if (IsInRowRange(tempRow, y))
                {
                    CurrentRow = tempRow;
                    tempRow.LocateCurrentMember(x, y);
                    break;
                }
                tempRow = tempRow.NextDocRow;
            }
        }

        public override void Paint()
        {
            ResetRowLocation();
            if (ExecuteMemberPaint == null ||
                ExecuteDocRowPaint == null) return;
            var row = FirstRow;
            while (row != null)
            {
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

        public int SumRowHeight()
        {
            var height = 0;
            var tempRow = FirstRow;
            while (tempRow != null)
            {
                height += tempRow.Height + tempRow.RowSpacing;
                tempRow = tempRow.NextDocRow;
            }
            return height;
            return 0;
        }

        private bool IsInRowRange(DDocRow row, int y)
        {
            return y >= row.Y && y < row.Y + row.Height;
        }

        public List<XmlElement> Convert2Xml(XmlDocument xmlDocument)
        {
            var xmlTranlator = XmlTranslator.Instance(this);
            var element = xmlTranlator.Convert2Xml(this, xmlDocument);
            return element.ChildNodes.Cast<XmlElement>().ToList();
        }

        public void Xml2Object(XmlElement xmlElement)
        {
            var xmlTranlator = XmlTranslator.Instance(this);
            this.FirstRow = null;
            xmlTranlator.Xml2Object(xmlElement);
        }

        public override int GetPageBodyX()
        {
            return OwnerPfooter.X + OwnerPfooter.PaddingLeft;
        }

        public override int GetPageBodyWidth()
        {
            return OwnerPfooter.Width;
        }
    }
}
