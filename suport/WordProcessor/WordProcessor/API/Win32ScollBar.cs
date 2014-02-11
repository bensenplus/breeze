using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.API
{
    class Win32ScollBar
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int GetSystemMetrics(int code);

        [DllImport("user32.dll")]
        static public extern bool EnableScrollBar(System.IntPtr hWnd, uint wSBflags, uint wArrows);

        [DllImport("user32.dll")]
        static public extern int SetScrollRange(System.IntPtr hWnd, int nBar, int nMinPos, int nMaxPos, bool bRedraw);

        [DllImport("user32.dll")]
        static public extern int SetScrollPos(System.IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        static public extern int GetScrollPos(System.IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        static public extern bool ShowScrollBar(System.IntPtr hWnd, int wBar, bool bShow);

        [DllImport("user32.dll")]
        static public extern IntPtr SendMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static public extern int HIWORD(System.IntPtr wParam);

        static public int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        static public IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        static public int HiWord(int number)
        {
            if ((number & 0x80000000) == 0x80000000)
                return (number >> 16);
            else
                return (number >> 16) & 0xffff;
        }

        static public int LoWord(int number)
        {
            return number & 0xffff;
        }
    }
}
