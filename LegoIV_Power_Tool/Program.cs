﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LegoIV_Power_Tool
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [DllImport("user32.dll")]
        private static extern int GetDesktopWindow();
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                System.Console.WriteLine("Hello World");
                DateTime startTime = DateTime.Now;
                DateTime endTime;
                Functions._ActionCode = -1;
                //Functions._HWND = (0xFFFF > System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle.ToInt32()) ? 0xFFFF : System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle.ToInt32();
                Functions._HWND = (0xFFFF < GetDesktopWindow()) ? 0xFFFF : GetDesktopWindow();
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
                        //System.Threading.Thread.Sleep(2000);
                        Functions.PowerAction(Functions._ActionCode);
                        //System.Threading.Thread.Sleep(500);
                        Environment.Exit(1);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(1000);
                }
                if (DateTime.Now >= endTime)
                {
                    Functions.PowerAction(Functions._ActionCode);
                }
                Environment.Exit(1);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
        }
    }
}
