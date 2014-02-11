namespace WordProcessor.API
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Win32Imm
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern int ImmAssociateContext(int hWnd, int hIMC);
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern int ImmCreateContext();
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImmDestroyContext(int hImc);
        [DllImport("imm32.dll")]
        public static extern int ImmGetCompositionString(int hIMC, int dwIndex, StringBuilder lpBuf, int dwBufLen);
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern int ImmGetContext(int hwnd);
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImmGetOpenStatus(int hImc);
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImmReleaseContext(int hwnd, int hImc);
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImmSetCandidateWindow(int hImc, ref CandidateForm frm);
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImmSetCompositionWindow(int hImc, ref CompositionForm frm);
        [DllImport("imm32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImmSetStatusWindowPos(int hImc, ref Point pos);

        public static bool IsImmOpen(int hWnd)
        {
            int hImc = ImmGetContext(hWnd);
            if (hImc == 0)
            {
                return false;
            }
            bool flag = ImmGetOpenStatus(hImc);
            ImmReleaseContext(hWnd, hImc);
            return flag;
        }

        public static void SetImmPos(int hWnd, int x, int y)
        {
            int hImc = ImmGetContext(hWnd);
            if (hImc != 0)
            {
                var frm = new CompositionForm();
                frm.CurrentPos.x = x;
                frm.CurrentPos.y = y;
                frm.Style = 2;
                ImmSetCompositionWindow(hImc, ref frm);
                ImmReleaseContext(hWnd, hImc);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CandidateForm
        {
            public int dwIndex;
            public int dwStyle;
            public Point ptCurrentPos;
            public Rect rcArea;
        }

        public enum CandidateFormStyle
        {
            CfsCandidatepos = 0x40,
            CfsDefault = 0,
            CfsExclude = 0x80,
            CfsForcePosition = 0x20,
            CfsPoint = 2,
            CfsRect = 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CompositionForm
        {
            public int Style;
            public Point CurrentPos;
            public Rect Area;
        }
    }
}

