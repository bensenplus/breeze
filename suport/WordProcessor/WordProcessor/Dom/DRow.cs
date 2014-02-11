using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Designer;
using WordProcessor.Interface;

namespace WordProcessor.Dom
{
    public class DRow
    {
        public int Width;
        public int Height;
        public int RowSpacing;
        private int _x;
        private int _y;

        public int X
        {
            get { return PreDocRow == null || IsPageFirst ? _x : PreDocRow.X; }
            set { _x = value; }
        }

        public int Y
        {
            get
            {
                return PreDocRow == null || IsPageFirst ? _y
                           : PreDocRow.Y + PreDocRow.Height + RowSpacing;
            }
            set { _y = value; }
        }

        public bool IsPageFirst;
        public bool IsContinue;
        public bool IsHold;
        public MDocument OwnerDocument;
        public DDocRow PreDocRow;
        public DDocRow NextDocRow;
        public DMember CurrentMember;
        public DMember LastMember;
        public DMember FirstMember;
        public DMember LargestTxtMember;
        public readonly List<DMember> SelectedMembers = new List<DMember>();
        public bool IsSelected;

    }
}
