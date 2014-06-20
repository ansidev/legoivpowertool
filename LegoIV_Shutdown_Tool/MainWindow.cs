using System;
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

        //Action
        private enum ButtonArray
        {
            btnShutdown,
            btnRestart,
            btnSleep,
            btnHibernate,
            btnSignout,
            btnLock,
            btnSwitch,
            btnMonitorOff,
        };

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

        private void ButtonStatusChangedEvent(MetroButton mtButton)
        {

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
            //bgColorShutdown = this.btnShutdown.BackColor.ToArgb();
            //bgColorRestart = this.btnRestart.BackColor.ToArgb();
            //bgColorSleep = this.btnSleep.BackColor.ToArgb();
            //bgColorHibernate = this.btnHibernate.BackColor.ToArgb();
            //bgColorSignout = this.btnSignout.BackColor.ToArgb();
            //bgColorLock = this.btnLock.BackColor.ToArgb();
            //bgColorSwitch = this.btnSwitch.BackColor.ToArgb();
            //bgColorMonitorOff = this.btnMonitorOff.BackColor.ToArgb();
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
            this.Close();
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
            this.Close();
        }

        private void _SleepComputer()
        {
            SetSuspendState(false, false, false);
            _MonitorOff();
            this.Close();
        }
        private void _HibernateComputer()
        {
            Application.SetSuspendState(PowerState.Hibernate, false, false );
            _MonitorOff();
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
        #endregion

        #region Button Click Events
        private void btnShutdown_Click(object sender, EventArgs e)
        {
            this.btnShutdown.Selected = (this.btnShutdown.Selected == false) ? true : false;
            if(this.btnShutdown.Selected == true)
            {
                this.btnMonitorOff.Selected = true;
            }
            else
            {
                EnableAllButton();
            }
            //ButtonStatusChangedEvent(true, false, false, false, false, false, false, true);
            UpdateSettings();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.btnRestart.Selected = (this.btnRestart.Selected == false) ? true : false;
            //ButtonStatusChangedEvent(false, true, false, false, false, false, false, true);
            UpdateSettings();
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            this.btnSleep.Selected = (this.btnSleep.Selected == false) ? true : false;
            //ButtonStatusChangedEvent(false, false, true, false, false, false, false, true);
            UpdateSettings();
        }

        private void btnHibernate_Click(object sender, EventArgs e)
        {
            this.btnHibernate.Selected = (this.btnHibernate.Selected == false) ? true : false;
            //ButtonStatusChangedEvent(false, false, false, true, false, false, false, true);
            UpdateSettings();
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            this.btnSignout.Selected = this.btnSignout.Selected == false ? true : false;
            //ButtonStatusChangedEvent(false, false, false, false, true, false, false, true);
            UpdateSettings();
        }
        private void btnLock_Click(object sender, EventArgs e)
        {
            this.btnLock.Selected = (this.btnLock.Selected == false) ? true : false;
            //ButtonStatusChangedEvent(false, false, false, false, false, true, false, true);
            UpdateSettings();
        }
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            this.btnSwitch.Selected = (this.btnSwitch.Selected == false) ? true : false;
            //ButtonStatusChangedEvent(false, false, false, false, false, false, true, false);
            UpdateSettings();
        }
        private void btnMonitorOff_Click(object sender, EventArgs e)
        {
            this.btnMonitorOff.Selected = (this.btnMonitorOff.Selected == false) ? true : false;
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
            PowerAction(1);
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
