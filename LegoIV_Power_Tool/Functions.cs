using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;

namespace LegoIV_Power_Tool
{
    class Functions
    {
        internal static int _DelayTime { get; set; }
        internal static int _ActionCode { get; set; }
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);
        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);

        internal static int _HWND { get; set; }
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MONITORPOWER = 0xF170;
        const int HWND_BROADCAST = 0xFFFF;
        const int WTS_CURRENT_SESSION = -1;
        static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        internal static void ExitWin(int _flag, int _reversed)
        {
            ManagementBaseObject mboShutdown = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();
            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");
            // Flag 1 means we want to shut down the system
            mboShutdownParams["Flags"] = _flag.ToString();
            mboShutdownParams["Reserved"] = _reversed.ToString();
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mboShutdown = manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
            }
        }
        internal static void ShutdownComputer()
        {
            try
            {
                ExitWin(8, 0);
            }
            catch
            {
                try
                {
                    ExitWin(1, 0);
                }
                catch
                {
                    try
                    {
                        ExitWin(5, 0);
                    }
                    catch
                    {
                        throw new Win32Exception();
                    }
                }
            }
            MonitorOff();
        }

        internal static void RestartComputer()
        {
            try
            {
                ExitWin(2, 0);
            }
            catch
            {
                try
                {
                    ExitWin(6, 0);
                }
                catch
                {
                    throw new Win32Exception();
                }
            }
            MonitorOff();
        }

        internal static void SleepComputer()
        {
            SetSuspendState(false, false, false);
            MonitorOff();
        }
        internal static void HibernateComputer()
        {
            Application.SetSuspendState(PowerState.Hibernate, false, false);
            MonitorOff();
        }
        internal static void SignoutComputer()
        {
            try
            {
                ExitWindowsEx(0, 0);
            }
            catch
            {
                try
                {
                    ExitWindowsEx(4, 0);
                }
                catch
                {
                    throw new Win32Exception();
                }
            }
            MonitorOff();
        }
        internal static void LockComputer()
        {
            MonitorOff();
            LockWorkStation();
            MonitorOn();

        }
        internal static void SwitchUser()
        {
            WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false);
        }
        //internal static void MonitorOff(Form _FRM)
        //{
        //    // Turn off monitor
        //    SendMessage(_FRM.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2);
        //}
        internal static void MonitorOff()
        {
            // Turn off monitor
            SendMessage(_HWND, WM_SYSCOMMAND, SC_MONITORPOWER, 2);
        }
        internal static void MonitorOn()
        {
            // Turn on monitor
            SendMessage(_HWND, WM_SYSCOMMAND, SC_MONITORPOWER, 1);
        }

        internal static void PowerAction(int _Action)
        {
            switch (_Action)
            {
                case 1:
                    Functions.ShutdownComputer();
                    break;
                case 2:
                    Functions.RestartComputer();
                    break;
                case 3:
                    Functions.SleepComputer();
                    break;
                case 4:
                    Functions.HibernateComputer();
                    break;
                case 5:
                    Functions.SignoutComputer();
                    break;
                case 6:
                    Functions.LockComputer();
                    break;
                case 7:
                    Functions.SwitchUser();
                    break;
                case 8:
                    Functions.MonitorOff();
                    break;
                default:
                    break;
            }
        }
    }
}
