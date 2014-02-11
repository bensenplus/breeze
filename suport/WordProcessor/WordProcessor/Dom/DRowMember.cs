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
    public class DRowMember : DMember
    {
        public DRowMember PreMember;
        public DRowMember NextMember;
        public DDocRow OwnerDocRow;

        public bool IsBreakTail;

        private int _x;
        private int _y;

        public override int X
        {
            get
            {
                if (PreMember == null)
                {
                    if(OwnerDocRow != null)
                    if (OwnerDocRow.Paragraph == RowParagraph.Right)
                    {
                        return OwnerDocRow.OwnerDocument.GetPageBodyX() + OwnerDocRow.OwnerDocument.GetPageBodyWidth()
                               - OwnerDocRow.SumMembersWidth();
                    }
                    else if (OwnerDocRow.Paragraph == RowParagraph.Middle)
                    {
                        return OwnerDocRow.OwnerDocument.GetPageBodyX() + (OwnerDocRow.OwnerDocument.GetPageBodyWidth()
                               - OwnerDocRow.SumMembersWidth())/2;
                    }
                    return OwnerDocRow != null ? OwnerDocRow.X : _x;
                }
                return PreMember.X + PreMember.Width;
            }
            set { _x = value; }
        }

        public override int Y
        {
            get
            {
                if (OwnerDocRow == null)
                {
                    return _y;
                }
                var hMember = OwnerDocRow.GetHighestMember(MemberType.Unknown) ?? this;
                return OwnerDocRow.Y + hMember.Height - this.Height -
                       Convert.ToInt32((float) GetFontHeightOffSet(hMember, this));
            }
            set { _y = value; }
        }

        /// <summary>
        /// 递归改变链表成员所属行为指定行
        /// </summary>
        /// <param name="docRow"></param>
        public virtual void RecursiveChangeDocRow(DDocRow docRow)
        {
            OwnerDocRow = docRow;
            if (NextMember == null) return;
            NextMember.RecursiveChangeDocRow(docRow);
        }

        /// <summary>
        /// 获取所在链表的尾部成员
        /// </summary>
        /// <returns></returns>
        public DRowMember GetTailMember()
        {
            return NextMember == null ? this : NextMember.GetTailMember();
        }

        /// <summary>
        /// 获取所在链表成员最高值
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int GetMaxHeight(int height)
        {
            var tempHeight = height > Height ? height : Height;
            if (NextMember != null) tempHeight = NextMember.GetMaxHeight(tempHeight);
            return tempHeight;
        }

        /// <summary>
        /// 递归改变所在链表从当前成员往后所有成员为选中状态
        /// </summary>
        public void RecursiveSelect()
        {
            IsSelected = true;
            OwnerDocRow.AddSelecteMember(this);
            if (NextMember == null) return;
            NextMember.RecursiveSelect();
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Invalidate(int x, int y, int width, int height)
        {
            if (OwnerDocRow == null || OwnerDocRow.OwnerDocument == null) return;
            OwnerDocRow.OwnerDocument.Context.Invalidate(new Rectangle(x, y, width, height));
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        public void Invalidate()
        {
            if (OwnerDocRow == null || OwnerDocRow.OwnerDocument == null) return;
            OwnerDocRow.OwnerDocument.Context.Invalidate();
        }

        /// <summary>
        /// 在改变行后需要处理的逻辑
        /// </summary>
        public virtual void AfterChangeRow()
        {
        }
    }
}
