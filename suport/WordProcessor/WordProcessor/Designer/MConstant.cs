using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordProcessor.Designer
{
    public abstract class MConstant
    {
        /// <summary>
        /// 视图类型
        /// </summary>
        public enum ViewType
        {
            Pagination
        }
        /// <remarks>
        /// 视图事件类型
        /// </remarks>
        public enum EventType
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

        /// <summary>
        /// 视图效果
        /// </summary>
        public enum Effect
        {
            MouseClick,
            MouseEnter,
            MouseDrag,
            MouseDoubleClick
        }

        /// <summary>
        /// 复制数据格式
        /// </summary>
        public abstract class MDataFormats
        {
            public const string ComboBox = "comboBox";
            public const string CheckBox = "checkBox";
            public const string HorizonLine = "horizonLine";
            public const string Image = "image";
            public const string Row = "row";
            public const string Table = "table";
            public const string TextInput = "textInput";
        }

        /// <summary>
        /// 光标类型
        /// </summary>
        public enum CursorType
        {
            Ibeam,
            Default,
            Arrow,
            Hsplit,
            Vsplit,
            SizeAll,
            Sizenwse,
            Sizens,
            Sizewe,
            Sizenesw
        }

    }
}
