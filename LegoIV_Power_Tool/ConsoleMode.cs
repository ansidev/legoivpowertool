using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace LegoIV_Power_Tool
{

    static class ConsoleMode
    {
        //[DllImport("user32.dll")]
        //private static extern int GetDesktopWindow();

        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessID);
        private const int ATTACH_PARENT_PROCESS = -1;
        //[DllImport("kernel32.dll")]
        //private static extern bool AllocConsole();
        internal static void Start(string[] args)
        {
            AttachConsole(ATTACH_PARENT_PROCESS);            
            DateTime startTime = DateTime.Now;
            DateTime endTime;
            Functions._ActionCode = -1;
            //Functions._HWND = (0xFFFF > System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle.ToInt32()) ? 0xFFFF : System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle.ToInt32();
            //Functions._HWND = (0xFFFF < GetDesktopWindow()) ? 0xFFFF : GetDesktopWindow();
            bool paramCheck = true;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "/shutdown":
                    case "/poweroff":
                    case "/p":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 1;
                                paramCheck = false;
                            }
                            break;
                        }
                    case "/restart":
                    case "/reboot":
                    case "/r":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 2;
                                paramCheck = false;

                            }
                            break;
                        }
                    case "/sleep":
                    case "/s":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 3;
                                paramCheck = false;
                            }
                            break;
                        }
                    case "/hibernate":
                    case "/h":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 4;
                                paramCheck = false;
                            }
                            break;
                        }
                    case "/signout":
                    case "/quit":
                    case "/q":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 5;
                                paramCheck = false;
                            }
                            break;
                        }
                    case "/lock":
                    case "/l":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 6;
                                paramCheck = false;
                            }
                            break;
                        }
                    case "/switch":
                    case "/changeacc":
                    case "/c":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 7;
                                paramCheck = false;
                            }
                            break;
                        }
                    case "/monitoroff":
                    case "/m":
                        {
                            if (paramCheck)
                            {
                                Functions._ActionCode = 8;
                                paramCheck = false;
                            }
                            break;
                        }
                    case "/t":
                        {
                            try
                            {
                                Functions._DelayTime = Int16.Parse(args[i + 1]);
                            }
                            catch
                            {
                                MessageBox.Show("Error: Invalid time format.");
                            }
                            i++;
                            break;
                        }
                    default:
                        Functions._ActionCode = -1;
                        break;
                }
            }
            endTime = startTime.AddSeconds(Functions._DelayTime);
            while (DateTime.Now != endTime)
            {
                if (DateTime.Now >= endTime)
                {
                    Functions.PowerAction(Functions._ActionCode);
                    Environment.Exit(1);
                    break;
                }
                //else
                //    System.Threading.Thread.Sleep(1000);
            }
            if (DateTime.Now >= endTime)
            {
                Functions.PowerAction(Functions._ActionCode);
            }
        }
    }
}
