using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.Designer
{
    public abstract class MEnum
    {
        /// <remarks>
        /// 事件类型
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
            DoubleClick
        }

        /// <summary>
        /// 作用效果
        /// </summary>
        public enum Effect
        {
            MouseClick,
            MouseEnter,
            MouseDoubleClick
        }
    }
}
