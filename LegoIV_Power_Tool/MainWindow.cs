﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Reflection;
using System.Net;
using System.IO;
using System.Xml;
using Newtonsoft.Json;


namespace LegoIV_Power_Tool
{
    public partial class MainWindow : Form
    {
        //Button
        Button[] ButtonArray = new Button[8];
        internal static int _Handle = 0;
        private bool isHidden
        {
            get;
            set;
        }
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
            string strDelayTime = "0";
            if (this.rdbtnNow.Checked == true)
            {
                strDelayTime = "0";
            }
            else if (this.rdbtnAfter.Checked == true)
            {
                strDelayTime = "";
                if (this.nmrcHour.Value != 0)
                {
                    strDelayTime += this.nmrcHour.Value.ToString() + " hour(s) ";
                }
                if (this.nmrdMinute.Value != 0)
                {
                    strDelayTime += this.nmrdMinute.Value.ToString() + " minute(s) ";
                }
                if (this.nmrdSecond.Value != 0)
                {
                    strDelayTime += this.nmrdSecond.Value.ToString() + " second(s)";
                }
                if (strDelayTime == "")
                {
                    strDelayTime = "0";
                }
            }
                
            return strDelayTime;
        }
        private string Prefix()
        {
            string _prefix = (this.lblSettingsBox.Text == "") ? "" : "\n";
            return _prefix;
        }
        private void UpdateSettings()
        {
            Functions._DelayTime = (this.rdbtnNow.Checked == true) ? 0 : Int32.Parse(this.nmrcHour.Value.ToString()) * 3600 + Int32.Parse(this.nmrdMinute.Value.ToString()) * 60 + Int32.Parse(this.nmrdSecond.Value.ToString());
            this.pgBar.Maximum = Functions._DelayTime * 1000 + 1000;
            this.pgBar.Step = 1;
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
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            string cmdPath = Assembly.GetEntryAssembly().Location;

            // create a jump list
            JumpList jumpList = JumpList.CreateJumpList();

            JumpListCustomCategory category = new JumpListCustomCategory("Action");
            category.AddJumpListItems(
                new JumpListLink(cmdPath, "Shut down") { Arguments = "/p" },
                new JumpListLink(cmdPath, "Restart") { Arguments = "/r" },
                new JumpListLink(cmdPath, "Sleep") { Arguments = "/s" },
                new JumpListLink(cmdPath, "Hibernate") { Arguments = "/h" },
                new JumpListLink(cmdPath, "Sign out") { Arguments = "/q" },
                new JumpListLink(cmdPath, "Lock") { Arguments = "/l" },
                new JumpListLink(cmdPath, "Switch user") { Arguments = "/c" },
                new JumpListLink(cmdPath, "Turn off monitor") { Arguments = "/m" });
            jumpList.AddCustomCategories(category);

            jumpList.Refresh();
        }
        public void MainWindow_Load(object sender, EventArgs e)
        {
            Functions._HWND = this.Handle.ToInt32();
        }

        #region Power Action Function

        private void PowerAction()
        {
            String warning = "";
            int delay = 0;
            bool ok = false;
            foreach (MetroButton mtButton in ButtonArray)
            {
                Functions._ActionCode++;
                if (mtButton.Selected == true)
                {
                    warning = "";
                    ok = true;
                    break;
                }
            }
            if(this.rdbtnAfter.Checked == true)
            {
                delay = (Int32.Parse(nmrcHour.Value.ToString()) * 3600 + Int32.Parse(nmrdMinute.Value.ToString()) * 60 + Int32.Parse(nmrdSecond.Value.ToString()));
            }
            if (ok)
            {
                btnStart.Text = "PAUSE";
                ChangeEnabledProperty(false);
                this.tmCountdown.Start();
            }
            else
            {
                warning += "You must choose an action!";
                MessageBox.Show(warning, "Warning!");
                Functions._ActionCode = -1;
            }
        }
        #endregion
        private void ChangeEnabledProperty(bool b)
        {
            foreach (MetroButton mtButton in ButtonArray)
            {
                mtButton.Enabled = b;
            }
        }
        #region Button Click Events
        private void OnClick(MetroButton _mtButton)
        {
            _mtButton.Selected = (_mtButton.Selected == false) ? true : false;
            if (_mtButton.Selected == true)
            {
                _mtButton.FlatAppearance.BorderSize = 1;
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
                        mtrButton.Selected = false;
                        mtrButton.FlatAppearance.BorderSize = 0;
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
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "START")
            {
                this.btnArrow.Enabled = true;
                PowerAction();
            }
            else if (btnStart.Text == "PAUSE")
            {
                this.tmCountdown.Stop();
                btnStart.Text = "RESUME";
                this.sttStatusBar.Text = "Paused!";
            }
            else if (btnStart.Text == "RESUME")
            {
                btnStart.Text = "PAUSE";
                this.tmCountdown.Start();
            }
            else if (btnStart.Text == "STOP")
            {
                this.tmCountdown.Stop();
                btnStart.Text = "START";
                this.sttStatusBar.Text = "Stopped!";
                this.pgBar.Value = 0;
                ChangeEnabledProperty(true);
            }
        }
        private void btnStart_TextChanged(object sender, EventArgs e)
        {
            if (this.btnStart.Text == "START")
            {
                this.startToolStripMenuItem.Text = this.btnStart.Text;
                this.startToolStripMenuItem.Enabled = false;
                this.pauseToolStripMenuItem.Enabled = true;
                this.stopToolStripMenuItem.Enabled = true;
            }
            if (this.btnStart.Text == "PAUSE" || this.btnStart.Text == "RESUME")
            {
                this.startToolStripMenuItem.Enabled = false;
                this.pauseToolStripMenuItem.Text = this.btnStart.Text;
                this.stopToolStripMenuItem.Enabled = true;
            }
            else if (this.btnStart.Text == "STOP")
            {
                this.startToolStripMenuItem.Enabled = true;
                this.pauseToolStripMenuItem.Enabled = false;
                this.stopToolStripMenuItem.Text = this.btnStart.Text;
                this.stopToolStripMenuItem.Enabled = false;
            }
        }
        private void btnArrow_Click(object sender, EventArgs e)
        {
            //this.btnArrow.FlatAppearance.BorderSize = 0;
            Point p = new Point(this.Location.X + this.btnArrow.Location.X + this.btnArrow.Size.Width + 8, this.Location.Y + this.btnArrow.Location.Y + this.btnArrow.Size.Height + 5);
            this.ctmsChooseActionMenu.Show(p, ToolStripDropDownDirection.Default);
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnStart.Text = this.startToolStripMenuItem.Text;
            PowerAction();
        }
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pauseToolStripMenuItem.Text == "PAUSE")
            {
                this.tmCountdown.Stop();
                this.btnStart.Text = "RESUME";
                this.sttStatusBar.Text = "Paused!";
            }
            else if (this.pauseToolStripMenuItem.Text == "RESUME")
            {
                this.tmCountdown.Start();
                this.btnStart.Text = "PAUSE";
            }
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tmCountdown.Stop();
            this.btnStart.Text = this.stopToolStripMenuItem.Text;
            this.sttStatusBar.Text = "Stopped!";
            this.pgBar.Value = 0;
            ChangeEnabledProperty(true);
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

        #region Changed
        private bool IsDelayTimeEqualToZero()
        {
            if(this.nmrcHour.Value == 0 && this.nmrdMinute.Value == 0 && this.nmrdSecond.Value == 0)
            {
                this.rdbtnNow.Checked = true;
                return true;
            }
            else
            {
                this.rdbtnAfter.Checked = true;
                return false;
            }
        }
        private void nmrcHour_ValueChanged(object sender, EventArgs e)
        {
            IsDelayTimeEqualToZero();
            UpdateSettings();
        }

        private void nmrdMinute_ValueChanged(object sender, EventArgs e)
        {
            IsDelayTimeEqualToZero();
            UpdateSettings();
        }

        private void nmrdSecond_ValueChanged(object sender, EventArgs e)
        {
            IsDelayTimeEqualToZero();
            UpdateSettings();
        }

        private void rdbtnNow_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void rdbtnAfter_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSettings();
            if(Functions._DelayTime == 0)
            {
                rdbtnNow.Checked = true;
            }
        }
        #endregion
        private void tmCountdown_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (Functions._DelayTime != 0)
                {
                    this.pgBar.Value++;
                }
            }
            this.sttStatusBar.Text = "\nStart in " + Functions._DelayTime.ToString() + " seconds";
            Functions._DelayTime--;
            if (Functions._DelayTime == -1)
            {
                Functions._DelayTime = 0;
                this.tmCountdown.Stop();
                this.Hide();
                Functions.PowerAction(Functions._ActionCode);
                this.ntfIcon.Icon = null;
                this.ntfIcon.Visible = false;
                //System.Threading.Thread.Sleep(2000);
                this.Close();
                System.Windows.Forms.Application.Exit();
            }
        }

        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form _frmAbout = new About();
            Point p = new Point(this.Location.X + this.Width / 2 - _frmAbout.Width / 2, this.Location.Y + this.Height / 2 - _frmAbout.Height / 2);
            _frmAbout.Location = p;
            _frmAbout.ShowDialog();
        }

        private void ntfIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ctmsSystemTray.Show(Cursor.Position);
        }

        private void ntfIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.ctmsSystemTray.Show(Cursor.Position);
            }
        }

        private void showHideWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isHidden)
            {
                this.showHideWindowToolStripMenuItem.Text = "Show Window";
                this.Hide();
                this.ShowInTaskbar = false;
                isHidden = true;
            }
            else
            {
                this.showHideWindowToolStripMenuItem.Text = "Hide Window";
                this.ShowInTaskbar = true;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                isHidden = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ntfIcon.Icon = null;
            Application.Exit();
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions.PowerAction(1);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Functions.PowerAction(2);
            System.Windows.Forms.Application.Exit();
        }

        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Functions.PowerAction(3);
            System.Windows.Forms.Application.Exit();
        }

        private void hibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Functions.PowerAction(4);
            System.Windows.Forms.Application.Exit();
        }

        private void signoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Functions.PowerAction(5);
            System.Windows.Forms.Application.Exit();
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Functions.PowerAction(6);
            System.Windows.Forms.Application.Exit();
        }

        private void switchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Functions.PowerAction(7);
            System.Windows.Forms.Application.Exit();
        }

        private void monitoroffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Functions.PowerAction(8);
            System.Windows.Forms.Application.Exit();
        }

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private void lnkCheckForUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sttStatusBar.Text = "Checking for new release...";
            string UpdateURL = "https://api.github.com/repos/ansidev/legoivpowertool/releases";
            string UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2049.0 Safari/537.36";
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", UserAgent);
            //string beforeData = "{\r\n'?xml': {\r\n'@version': '1.0',\r\n'@encoding': 'UTF-8'\r\n},\r\n'root': {\r\n'release': ";
            //string afterData = @"}\r\n}";
            //string rawdata = beforeData + client.DownloadString(UpdateURL) + afterData;
            string rawdata = client.DownloadString(UpdateURL);
            //MessageBox.Show(rawdata);
            //XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(rawdata);
            List<GitHubRelease> releases = new List<GitHubRelease>();
            releases = JsonConvert.DeserializeObject<List<GitHubRelease>>(rawdata);
            GitHubAPI GHAPI = new GitHubAPI();
            lblReleaseInfo.Text = GHAPI.DisplayReleaseInfo(releases[0]);

            #region Comment 
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UpdateURL);
            //request.Method = "GET";
            //request.ServicePoint.Expect100Continue = false;
            //request.UserAgent = UserAgent;
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    Stream stream = response.GetResponseStream();
            //    StreamReader reader = null;
            //    if (response.CharacterSet == null)
            //    {
            //        reader = new StreamReader(stream);
            //    }
            //    else
            //    {
            //        reader = new StreamReader(stream, Encoding.GetEncoding(response.CharacterSet));
            //    }
            //    string data = reader.ReadToEnd();
            //    MessageBox.Show(data);
            //    reader.Close();
            //    response.Close();
            //}
            #endregion
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
