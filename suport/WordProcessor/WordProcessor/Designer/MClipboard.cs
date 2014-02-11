using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordProcessor.Dom;

namespace WordProcessor.Designer
{
    /// <summary>
    /// 自定义剪贴板
    /// </summary>
    public class MClipboard
    {
        private static Dictionary<DDocRow, List<DMember>> _rowMapMember =
            new Dictionary<DDocRow, List<DMember>>(); 
        private static readonly List<DDocRow> Rows = new List<DDocRow>(); 
        private static string _format;
        private static bool _isCopy;

        public static void SetData(string format, List<DDocRow> rows)
        {
            _isCopy = false;
            _format = format;

            Rows.Clear();
            _rowMapMember.Clear();

            Rows.AddRange(rows);
            foreach (var dDocRow in Rows)
            {
                var members = dDocRow.SelectedMembers.Select(dMember => dMember.Copy()).ToList();
                var tempList = members.Cast<DMember>().ToList();
                _rowMapMember.Add(dDocRow, tempList);
            }

            //TODO 兼容其他文本编辑器，将内容转成文本和图片等格式
            Clipboard.Clear();
        }

        public static void SetData(string format, object obj)
        {
            Clipboard.SetData(format, obj);
        }

        public static bool IsDataFormat(string format)
        {
            return _format != null && _format.Equals(format);
        }

        private static void CopyData()
        {
            if (!_isCopy) return;
            var tempMap = new Dictionary<DDocRow, List<DMember>>();
            foreach (var map in _rowMapMember)
            {
                var value = map.Value;
                var members = value.Select(dMember => dMember.Copy()).ToList();
                tempMap.Add(map.Key, members);
            }
            _rowMapMember = tempMap;
        }

        public static List<DDocRow> GetDataList()
        {
            CopyData();
            _isCopy = true;
            return Rows;
        } 

        public static List<DMember> GetDataMember(DDocRow docRow)
        {
            List<DMember> members = null;
            _rowMapMember.TryGetValue(docRow, out members);
            return members;
        } 
    }
}
