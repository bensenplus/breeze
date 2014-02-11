using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.API
{
    public class Win32User
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)] 
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)] 
        public static extern bool ReleaseCapture(); 
    }
}
