using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.Interface
{
    /// <summary>
    /// 用来将行绑定到分页上
    /// </summary>
    public interface ICompatible
    {
        /// <summary>
        /// 行与页位置同步
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void Resize(int x, int y);

        /// <summary>
        /// 定位到行
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void Locate(int x, int y);
    }
}
