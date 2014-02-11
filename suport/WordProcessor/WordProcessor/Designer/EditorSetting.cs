using System;
using System.Drawing;
using WordProcessor.Dom;

namespace WordProcessor.Designer
{
    public abstract class EditorSetting
    {
        public static Graphics DefaultGraphics;
        public static Font DefaultFont = new Font("Arial", 16);
        public static Font CurrentFont = new Font("Arial",16);
        public static Color DefaultFontColor = Color.Black;
        public static Color CurrentFontColor = Color.Black;
        private static StringFormat _defaultStringFormat ;
        public static StringFormat DefaultStringFormat
        {
            get
            {
                if (_defaultStringFormat == null)
                {
                    _defaultStringFormat = StringFormat.GenericTypographic;
                    _defaultStringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                }
                return _defaultStringFormat;
            }
            set { _defaultStringFormat = value; }
        }
        public static StringFormatFlags DefaultStringFormatFlags = StringFormatFlags.MeasureTrailingSpaces;
        public static int DefaultDocRowGap = 0;
        public static int DefaultDocRowX = 100;
        public static int DefaultDocRowY = 100;
        public static int DefaultDocRowHeight = CurrentFont.Height;
        public static int DefaultDocRowWidth = 600;

        public static int CurrentDocRowHeight = CurrentFont.Height;
        public static int CurrentDocRowWidth = 600;

        public static int DefaultPageWidth = 800;
        public static int DefaultPageHeight = 1000;
        public static int DefaultBodyWidth = 600;
        public static int DefaultBodyHeight = 800;
        public static int DefaultHeaderWidth = 800;
        public static int DefaultHeaderHeight = 100;
        public static int DefaultFooterWidth = 800;
        public static int DefaultFooterHeight = 100;
        public static int DefaultPaddingLeft = 100;
        public static int DefaultPaddingRight = 100;
        public static int DefaultPaddingTop = 100;
        public static int DefaultPaddingBottom = 100;
        public static int DefaultEdgeLineLength = 30;

        public static int DefaultTableCellHeight = 30;
        public static int DefaultTableRowWidth = 600;
        public static int DefaultTableRowHeight = 30;
        public static int DefaultTableColumnWidth = 600;
        public static int DefaultTableColumnHeight = 30;

        public static bool IsShowGridLine;
        public static bool IsShowRowIndex;

        public static int CurrentTableRowCount = 3;
        public static int CurrentTableColumnCount = 3;
    }
}
