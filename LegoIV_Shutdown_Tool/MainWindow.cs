﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Management;
namespace LegoIV_Power_Tool
{
    public partial class MainWindow : Form
    {
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MONITORPOWER = 0xF170;
        int HWND_BROADCAST = 0xffff;
        const int EWX_POWEROFF = 0x00000008;

        //Action
        bool isShutdown = false;
        bool isRestart = false;
        bool isSleep = false;
        bool isHibernate = false;
        bool isSignout = false;
        bool isLock = false;
        bool isSwitch = false;
        bool isMonitorOff = false;
        //Button BackColor
        int bgColorShutdown, bgColorRestart, bgColorSleep, bgColorHibernate, bgColorSignout, bgColorLock, bgColorSwitch, bgColorMonitorOff;

        #region Other Functions
        private void SetButtonStatus(Button _Button, bool _isEnabled)
        {
            _Button.Enabled = _isEnabled;
            if (_isEnabled == false)
            {
                _Button.BackColor = this.BackColor;
            }
            if(_isEnabled == true)
            {
                _Button.FlatAppearance.BorderSize = 1;
                _Button.FlatAppearance.BorderColor = Color.Black;
            }
        }
        private void ButtonStatusChangedEvent(bool _isShutdown, bool _isRestart, bool _isSleep, bool _isHibernate, bool _isSignout, bool _isLock, bool _isSwitch, bool _isMonitorOff)
        {
            SetButtonStatus(btnShutdown, _isShutdown);
            SetButtonStatus(btnRestart, _isRestart);
            SetButtonStatus(btnSleep, _isSleep);
            SetButtonStatus(btnHibernate, _isHibernate);
            SetButtonStatus(btnSignout, _isSignout);
            SetButtonStatus(btnLock, _isLock);
            SetButtonStatus(btnSwitch, _isSwitch);
            SetButtonStatus(btnMonitorOff, _isMonitorOff);
        }
        private void EnableAllButton()
        {
            this.btnShutdown.Enabled = this.btnRestart.Enabled = this.btnSleep.Enabled = this.btnHibernate.Enabled = this.btnSignout.Enabled = this.btnLock.Enabled = this.btnSwitch.Enabled = this.btnMonitorOff.Enabled = true;
        }
        private string DelayTime()
        {
            string _DelayTime = "0";
            if (this.rdbtnNow.Checked == true)
            {
                _DelayTime = "0";
            }
            else if (this.rdbtnAfter.Checked == true)
            {
                _DelayTime = "";
                if (this.nmrcHour.Value != 0)
                {
                    _DelayTime += this.nmrcHour.Value.ToString() + " hour(s)";
                }
                if (this.nmrdMinute.Value != 0)
                {
                    _DelayTime += this.nmrdMinute.Value.ToString() + "minute(s)";
                }
                if (this.nmrdSecond.Value != 0)
                {
                    _DelayTime += this.nmrdSecond.Value.ToString() + "second(s)";
                }
                if (_DelayTime == "")
                {
                    _DelayTime = "";
                }
            }
                
            return _DelayTime;
        }
        private void UpdateSettings()
        {
            this.lblSettingsBox.Text = "";
            this.lblSettingsBox.Text += "Shutdown = " + isShutdown.ToString();
            this.lblSettingsBox.Text += "\nRestart = " + isRestart.ToString();
            this.lblSettingsBox.Text += "\nSleep = " + isSleep.ToString();
            this.lblSettingsBox.Text += "\nHibernate = " + isHibernate.ToString();
            this.lblSettingsBox.Text += "\nSignout = " + isSignout.ToString();
            this.lblSettingsBox.Text += "\nLock = " + isLock.ToString();
            this.lblSettingsBox.Text += "\nSwitch = " + isSwitch.ToString();
            this.lblSettingsBox.Text += "\nMonitor off = " + isMonitorOff.ToString();
            this.lblSettingsBox.Text += "\nDelay time = " + DelayTime();

        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            UpdateSettings();
            bgColorShutdown = this.btnShutdown.BackColor.ToArgb();
            bgColorRestart = this.btnRestart.BackColor.ToArgb();
            bgColorSleep = this.btnSleep.BackColor.ToArgb();
            bgColorHibernate = this.btnHibernate.BackColor.ToArgb();
            bgColorSignout = this.btnSignout.BackColor.ToArgb();
            bgColorLock = this.btnLock.BackColor.ToArgb();
            bgColorSwitch = this.btnSwitch.BackColor.ToArgb();
            bgColorMonitorOff = this.btnMonitorOff.BackColor.ToArgb();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
        #region Power Action Function
        private void PowerAction(int _Action)
        {
            switch (_Action)
            {
                case 1:
                    _ShutdownComputer();
                    break;
                case 2:
                    _RestartComputer();
                    break;
                case 3:
                    _SleepComputer();
                    break;
                case 4:
                    _HibernateComputer();
                    break;
                case 5:
                    _SignoutComputer();
                    break;
                case 6:
                    _LockComputer();
                    break;
                case 7:
                    _SwitchUser();
                    break;
                case 8:
                    _MonitorOff();
                    break;
                default:
                    break;
            }
        }
        private void _ShutdownComputer()
        {
            ManagementBaseObject mboShutdown = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();
            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");
            // Flag 1 means we want to shut down the system
            mboShutdownParams["Flags"] = "1";
            mboShutdownParams["Reserved"] = "0";
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mboShutdown = manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
            }
            this.Close();
        }

        private void _RestartComputer()
        {
            try
            {
                ExitWindowsEx(2, 0);
            }
            catch
            {
                try
                {
                    ExitWindowsEx(6, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            this.Close();
        }

        private void _SleepComputer()
        {
            this.Close();
        }
        private void _HibernateComputer()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
            this.Close();
        }
        private void _SignoutComputer()
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            this.Close();
        }
        #endregion
        private void _LockComputer()
        {
            LockWorkStation();
            this.Close();
        }
        private void _SwitchUser()
        {
            this.Close();
        }
        private void _MonitorOff()
        {
            // Turn off monitor
            Thread.Sleep(500);
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, 2);
            this.Close();
        }
        #region Button Click Events
        private void btnShutdown_Click(object sender, EventArgs e)
        {
            isShutdown = (isShutdown == false) ? true : false;
            ButtonStatusChangedEvent(true, false, false, false, false, false, false, true);
            UpdateSettings();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            isRestart = (isRestart == false) ? true : false;
            ButtonStatusChangedEvent(false, true, false, false, false, false, false, true);
            UpdateSettings();
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            isSleep = (isSleep == false) ? true : false;
            ButtonStatusChangedEvent(false, false, true, false, false, false, false, true);
            UpdateSettings();
        }

        private void btnHibernate_Click(object sender, EventArgs e)
        {
            isHibernate = (isHibernate == false) ? true : false;
            ButtonStatusChangedEvent(false, false, false, true, false, false, false, true);
            UpdateSettings();
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            isSignout = (isSignout == false) ? true : false;
            ButtonStatusChangedEvent(false, false, false, false, true, false, false, true);
            UpdateSettings();
        }
        private void btnLock_Click(object sender, EventArgs e)
        {
            isLock = (isLock == false) ? true : false;
            ButtonStatusChangedEvent(false, false, false, false, false, true, false, true);
            UpdateSettings();
        }
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            isSwitch = (isSwitch == false) ? true : false;
            ButtonStatusChangedEvent(false, false, false, false, false, false, true, false);
            UpdateSettings();
        }
        private void btnMonitorOff_Click(object sender, EventArgs e)
        {
            isMonitorOff = (isMonitorOff == false) ? true : false;
            UpdateSettings();
        }
        #endregion
        
        #region Button Mouse Hover Events
        private void btnShutdown_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Shut down your computer!";
        }

        private void btnRestart_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Restart your computer!";
        }

        private void btnSleep_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Sleep your computer!";
        }

        private void btnHibernate_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Hibernate your computer!";
        }

        private void btnSignout_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Close all application and sign out your computer!";
        }

        private void btnLock_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Lock your computer screen!";
        }

        private void btnSwitch_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Switch between users on your computer!";
        }

        private void btnMonitorOff_MouseHover(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "Turn off your computer screen!";
        }
        #endregion
        
        #region Mouse Leave Events
        private void btnShutdown_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        private void btnRestart_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        private void btnSleep_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        private void btnHibernate_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        private void btnSignout_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        private void btnLock_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        private void btnSwitch_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        private void btnMonitorOff_MouseLeave(object sender, EventArgs e)
        {
            this.sttStatusBar.Text = "";
        }
        #endregion
        private void btnShutdown_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == true)
            {
                btnShutdown.BackColor = Color.FromArgb(bgColorShutdown);
            //    SetButtonStatus(btnRestart, false);
            //    SetButtonStatus(btnSleep, false);
            //    SetButtonStatus(btnHibernate, false);
            //    SetButtonStatus(btnSignout, false);
            //    SetButtonStatus(btnLock, false);
            //    SetButtonStatus(btnSwitch, false);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            PowerAction(1);
        }
    }
}
