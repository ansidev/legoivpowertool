using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoIV_Power_Tool
{
    class WindowsAPI
    {
        public string Arch()
        {
            string _arch = null;
            if (IntPtr.Size == 8)
            {
                _arch = "x64";
            }
            else
            {
                _arch = "x86";
            }
            return _arch;
        }
    }
}
