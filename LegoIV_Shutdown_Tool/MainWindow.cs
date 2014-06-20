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
        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);


        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MONITORPOWER = 0xF170;
        int HWND_BROADCAST = 0xffff;

        //Button
        Button[] ButtonArray = new Button[8];

        #region Other Functions
        private void EnableAllButton(MetroButton mtButton)
        {
            if (mtButton.Selected == false)
            {
                this.btnShutdown.Enabled = this.btnRestart.Enabled = this.btnSleep.Enabled = this.btnHibernate.Enabled = this.btnSignout.Enabled = this.btnLock.Enabled = this.btnSwitch.Enabled = this.btnMonitorOff.Enabled = true;
            }
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
        private string Prefix()
        {
            string _prefix = (this.lblSettingsBox.Text == "") ? "" : "\n";
            return _prefix;
        }
        private void UpdateSettings()
        {
            this.lblSettingsBox.Text = "";
            if (this.btnShutdown.Selected == true)
            {
                this.lblSettingsBox.Text += "Shutdown = " + this.btnShutdown.Selected.ToString();
            }
            if (this.btnRestart.Selected == true)
            {
                this.lblSettingsBox.Text += Prefix() + "Restart = " + this.btnRestart.Selected.ToString();
            }
            if (this.btnSleep.Selected == true)
            {
                this.lblSettingsBox.Text += Prefix() + "Sleep = " + this.btnSleep.Selected.ToString();
            }
            if (this.btnHibernate.Selected == true)
            {
                this.lblSettingsBox.Text += Prefix() + "Hibernate = " + this.btnHibernate.Selected.ToString();
            }
            if (this.btnSignout.Selected == true)
            {
                this.lblSettingsBox.Text += Prefix() + "Signout = " + this.btnSignout.Selected.ToString();
            }
            if (this.btnLock.Selected == true)
            {
                this.lblSettingsBox.Text += Prefix() + "Lock = " + this.btnLock.Selected.ToString();
            }
            if (this.btnSwitch.Selected == true)
            {
                this.lblSettingsBox.Text += Prefix() + "Switch = " + this.btnSwitch.Selected.ToString();
            }
            if (this.btnMonitorOff.Selected == true)
                this.lblSettingsBox.Text += Prefix() + "Monitor off = " + this.btnMonitorOff.Selected.ToString();
            if (DelayTime() != "0")
            {
                this.lblSettingsBox.Text += Prefix() + "Delay time = " + DelayTime();
            }
            else
            {
                this.lblSettingsBox.Text += Prefix() + "No delay time";
            }

        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            UpdateSettings();
            //Get Button
            ButtonArray[0] = btnShutdown;
            ButtonArray[1] = btnRestart;
            ButtonArray[2] = btnSleep;
            ButtonArray[3] = btnHibernate;
            ButtonArray[4] = btnSignout;
            ButtonArray[5] = btnLock;
            ButtonArray[6] = btnSwitch;
            ButtonArray[7] = btnMonitorOff;
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        #region Power Action Function
        private void ExitWin(int _flag, int _reversed)
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
        private void PowerAction()
        {
            this.Hide();
            this.Close();
        }
        private void PowerAction(int _Action)
        {
            this.Hide();
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
            this.Close();
        }
        private void _ShutdownComputer()
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
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            _MonitorOff();
        }

        private void _RestartComputer()
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            _MonitorOff();
        }

        private void _SleepComputer()
        {
            SetSuspendState(false, false, false);
            _MonitorOff();
        }
        private void _HibernateComputer()
        {
            Application.SetSuspendState(PowerState.Hibernate, false, false );
            _MonitorOff();
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
        }
        private void _LockComputer()
        {
            LockWorkStation();
        }
        private void _SwitchUser()
        {
        }
        private void _MonitorOff()
        {
            // Turn off monitor
            Thread.Sleep(500);
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, 2);
        }
        #endregion

        #region Button Click Events
        private void OnClick(MetroButton _mtButton)
        {
            _mtButton.Selected = (_mtButton.Selected == false) ? true : false;
            if (_mtButton.Selected == true)
            {
                _mtButton.FlatAppearance.BorderSize = 2;
                _mtButton.FlatAppearance.BorderColor = Color.Black;
            }
            else
            {
                _mtButton.FlatAppearance.BorderSize = 0;
            }
            if(_mtButton.Selected == false)
            {
                foreach (MetroButton mtButton in ButtonArray)
                {
                    mtButton.Enabled = true;
                    mtButton.Selected = false;
                }
            }
            else
                foreach (MetroButton mtrButton in ButtonArray)
                {
                    if(mtrButton != _mtButton)
                    {
                        mtrButton.Enabled = false;
                    }
                    else
                    {
                        continue;
                    }
                }
            UpdateSettings();
        }
        private void btnShutdown_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnShutdown);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnRestart);
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnSleep);
        }

        private void btnHibernate_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnHibernate);
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnSignout);
        }
        private void btnLock_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnLock);
        }
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnSwitch);
        }
        private void btnMonitorOff_Click(object sender, EventArgs e)
        {
            this.OnClick(this.btnMonitorOff);
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
                //btnShutdown.BackColor = Color.FromArgb(bgColorShutdown);
                //    SetButtonStatus(btnRestart, false);
                //    SetButtonStatus(btnSleep, false);
                //    SetButtonStatus(btnHibernate, false);
                //    SetButtonStatus(btnSignout, false);
                //    SetButtonStatus(btnLock, false);
                //    SetButtonStatus(btnSwitch, false);
            }
            if (this.Enabled == false)
            {
                MessageBox.Show("EnabledChanged Events!");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            PowerAction();
        }

        private void btnShutdown_SelectedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("SelectedChanged Event!");
        }

        private void btnShutdown_BackColorChanged(object sender, EventArgs e)
        {
            MessageBox.Show("BackColorChanged Event!");
        }
    }
}
