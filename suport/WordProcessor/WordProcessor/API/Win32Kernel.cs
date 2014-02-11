namespace WordProcessor.API
{
    using System;
    using System.Runtime.InteropServices;

    public class Win32Kernel
    {
        public const int FormatMessageAllocateBuffer = 0x100;
        public const int FormatMessageArgumentArray = 0x2000;
        public const int FormatMessageFromHmodule = 0x800;
        public const int FormatMessageFromString = 0x400;
        public const int FormatMessageFromSystem = 0x1000;
        public const int FormatMessageIgnoreInserts = 0x200;
        public const int FormatMessageMaxWidthMask = 0xff;

        public static string FormatErrorMessage(int errorCode)
        {
            var charBuffer = new char[0x800];
            var num = FormatMessage(0x1200, 0, errorCode, 0, charBuffer, charBuffer.Length, 0);
            if (num > 1)
            {
                return new string(charBuffer, 0, num - 1);
            }
            return null;
        }

        [DllImport("Kernel32.dll", CharSet=CharSet.Auto)]
        public static extern int FormatMessage(int flags, int pSource, int messageId, int languageId, char[] charBuffer, int bufferSize, int arguments);
        [DllImport("Kernel32.dll", CharSet=CharSet.Auto)]
        public static extern uint GetLastError();
    }
}

