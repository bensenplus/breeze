namespace WordProcessor.API
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class Win32Caret
    {
        private readonly IWin32Window _myControl;

        public Win32Caret(int hwnd)
        {
            this._myControl = null;
            var handle = new Win32Handle {
                handle = new IntPtr(hwnd)
            };
            this._myControl = handle;
        }

        public Win32Caret(IntPtr hwnd)
        {
            this._myControl = null;
            var handle = new Win32Handle
            {
                handle = hwnd
            };
            this._myControl = handle;
        }

        public Win32Caret(IWin32Window ctl)
        {
            this._myControl = null;
            this._myControl = ctl;
        }

        public bool Create(int hBitmap, int nWidth, int nHeight)
        {
            if (this._myControl == null)
            {
                return false;
            }
            return CreateCaret(this._myControl.Handle, hBitmap, nWidth, nHeight);
        }

        [DllImport("User32.dll")]
        private static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);
        public bool Destroy()
        {
            return DestroyCaret();
        }

        [DllImport("User32.dll")]
        private static extern bool DestroyCaret();
        public bool Hide()
        {
            if (this._myControl == null)
            {
                return false;
            }
            return HideCaret(this._myControl.Handle);
        }

        [DllImport("User32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool SetCaretPos(int x, int y);
        public bool SetPos(int x, int y)
        {
            if (this._myControl == null)
            {
                return false;
            }
            return SetCaretPos(x, y);
        }

        public bool Show()
        {
            if (this._myControl == null)
            {
                return false;
            }
            return ShowCaret(this._myControl.Handle);
        }

        [DllImport("User32.dll")]
        private static extern bool ShowCaret(IntPtr hWnd);

        private class Win32Handle : IWin32Window
        {
            public IntPtr handle = IntPtr.Zero;

            public IntPtr Handle
            {
                get
                {
                    return this.handle;
                }
            }
        }
    }
}

