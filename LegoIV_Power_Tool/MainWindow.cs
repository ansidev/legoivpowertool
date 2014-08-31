using System;
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
        private bool isHidden { get; set; }
        List<GitHubRelease> releases = new List<GitHubRelease>();
        #region Other Functions
        private void EnableAllButton(MetroButton mtButton)
        {
            if (mtButton.Selected == false)
            {
                btnShutdown.Enabled = btnRestart.Enabled = btnSleep.Enabled = btnHibernate.Enabled = btnSignout.Enabled = btnLock.Enabled = btnSwitch.Enabled = btnMonitorOff.Enabled = true;
            }
        }
        private string DelayTime()
        {
            string strDelayTime = "0";
            if (rdbtnNow.Checked == true)
            {
                strDelayTime = "0";
            }
            else if (rdbtnAfter.Checked == true)
            {
                strDelayTime = "";
                if (nmrcHour.Value != 0)
                {
                    strDelayTime += nmrcHour.Value.ToString() + " hour(s) ";
                }
                if (nmrdMinute.Value != 0)
                {
                    strDelayTime += nmrdMinute.Value.ToString() + " minute(s) ";
                }
                if (nmrdSecond.Value != 0)
                {
                    strDelayTime += nmrdSecond.Value.ToString() + " second(s)";
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
            string _prefix = (lblSettingsBox.Text == "") ? "" : "\n";
            return _prefix;
        }
        private void UpdateSettings()
        {
            Functions._DelayTime = (rdbtnNow.Checked == true) ? 0 : Int32.Parse(nmrcHour.Value.ToString()) * 3600 + Int32.Parse(nmrdMinute.Value.ToString()) * 60 + Int32.Parse(nmrdSecond.Value.ToString());
            pgBar.Maximum = Functions._DelayTime * 1000 + 1000;
            pgBar.Step = 1;
            lblSettingsBox.Text = "";
            if (btnShutdown.Selected == true)
            {
                lblSettingsBox.Text += "Shutdown = " + btnShutdown.Selected.ToString();
            }
            if (btnRestart.Selected == true)
            {
                lblSettingsBox.Text += Prefix() + "Restart = " + btnRestart.Selected.ToString();
            }
            if (btnSleep.Selected == true)
            {
                lblSettingsBox.Text += Prefix() + "Sleep = " + btnSleep.Selected.ToString();
            }
            if (btnHibernate.Selected == true)
            {
                lblSettingsBox.Text += Prefix() + "Hibernate = " + btnHibernate.Selected.ToString();
            }
            if (btnSignout.Selected == true)
            {
                lblSettingsBox.Text += Prefix() + "Signout = " + btnSignout.Selected.ToString();
            }
            if (btnLock.Selected == true)
            {
                lblSettingsBox.Text += Prefix() + "Lock = " + btnLock.Selected.ToString();
            }
            if (btnSwitch.Selected == true)
            {
                lblSettingsBox.Text += Prefix() + "Switch = " + btnSwitch.Selected.ToString();
            }
            if (btnMonitorOff.Selected == true)
                lblSettingsBox.Text += Prefix() + "Monitor off = " + btnMonitorOff.Selected.ToString();
            if (DelayTime() != "0")
            {
                lblSettingsBox.Text += Prefix() + "Delay time = " + DelayTime();
            }
            else
            {
                lblSettingsBox.Text += Prefix() + "No delay time";
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
            Functions._HWND = Handle.ToInt32();
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
            if(rdbtnAfter.Checked == true)
            {
                delay = (Int32.Parse(nmrcHour.Value.ToString()) * 3600 + Int32.Parse(nmrdMinute.Value.ToString()) * 60 + Int32.Parse(nmrdSecond.Value.ToString()));
            }
            if (ok)
            {
                btnStart.Text = "PAUSE";
                ChangeEnabledProperty(false);
                tmCountdown.Start();
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
            OnClick(btnShutdown);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            OnClick(btnRestart);
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            OnClick(btnSleep);
        }

        private void btnHibernate_Click(object sender, EventArgs e)
        {
            OnClick(btnHibernate);
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            OnClick(btnSignout);
        }
        private void btnLock_Click(object sender, EventArgs e)
        {
            OnClick(btnLock);
        }
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            OnClick(btnSwitch);
        }
        private void btnMonitorOff_Click(object sender, EventArgs e)
        {
            OnClick(btnMonitorOff);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "START")
            {
                btnArrow.Enabled = true;
                PowerAction();
            }
            else if (btnStart.Text == "PAUSE")
            {
                tmCountdown.Stop();
                btnStart.Text = "RESUME";
                sttStatusBar.Text = "Paused!";
            }
            else if (btnStart.Text == "RESUME")
            {
                btnStart.Text = "PAUSE";
                tmCountdown.Start();
            }
            else if (btnStart.Text == "STOP")
            {
                tmCountdown.Stop();
                btnStart.Text = "START";
                sttStatusBar.Text = "Stopped!";
                pgBar.Value = 0;
                ChangeEnabledProperty(true);
            }
        }
        private void btnStart_TextChanged(object sender, EventArgs e)
        {
            if (btnStart.Text == "START")
            {
                startToolStripMenuItem.Text = btnStart.Text;
                startToolStripMenuItem.Enabled = false;
                pauseToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = true;
            }
            if (btnStart.Text == "PAUSE" || btnStart.Text == "RESUME")
            {
                startToolStripMenuItem.Enabled = false;
                pauseToolStripMenuItem.Text = btnStart.Text;
                stopToolStripMenuItem.Enabled = true;
            }
            else if (btnStart.Text == "STOP")
            {
                startToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Text = btnStart.Text;
                stopToolStripMenuItem.Enabled = false;
            }
        }
        private void btnArrow_Click(object sender, EventArgs e)
        {
            //btnArrow.FlatAppearance.BorderSize = 0;
            Point p = new Point(Location.X + btnArrow.Location.X + btnArrow.Size.Width + 8, Location.Y + btnArrow.Location.Y + btnArrow.Size.Height + 5);
            ctmsChooseActionMenu.Show(p, ToolStripDropDownDirection.Default);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStart.Text = startToolStripMenuItem.Text;
            PowerAction();
        }
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pauseToolStripMenuItem.Text == "PAUSE")
            {
                tmCountdown.Stop();
                btnStart.Text = "RESUME";
                sttStatusBar.Text = "Paused!";
            }
            else if (pauseToolStripMenuItem.Text == "RESUME")
            {
                tmCountdown.Start();
                btnStart.Text = "PAUSE";
            }
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmCountdown.Stop();
            btnStart.Text = stopToolStripMenuItem.Text;
            sttStatusBar.Text = "Stopped!";
            pgBar.Value = 0;
            ChangeEnabledProperty(true);
        }
        #endregion
        
        #region Button Mouse Hover Events
        private void btnShutdown_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Shut down your computer!";
        }

        private void btnRestart_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Restart your computer!";
        }

        private void btnSleep_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Sleep your computer!";
        }

        private void btnHibernate_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Hibernate your computer!";
        }

        private void btnSignout_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Close all application and sign out your computer!";
        }

        private void btnLock_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Lock your computer screen!";
        }

        private void btnSwitch_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Switch between users on your computer!";
        }

        private void btnMonitorOff_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Turn off your computer screen!";
        }
        private void lnkCheckForUpdate_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "Check for new release";
        }
        private void lnkAbout_MouseHover(object sender, EventArgs e)
        {
            sttStatusBar.Text = "About LegoIV Power Tool";
        }
        #endregion
        
        #region Mouse Leave Events
        private void item_MouseLeave(object sender, EventArgs e)
        {
            sttStatusBar.Text = String.Empty;
        }
        #endregion

        #region Changed
        private bool IsDelayTimeEqualToZero()
        {
            if(nmrcHour.Value == 0 && nmrdMinute.Value == 0 && nmrdSecond.Value == 0)
            {
                rdbtnNow.Checked = true;
                return true;
            }
            else
            {
                rdbtnAfter.Checked = true;
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
                    pgBar.Value++;
                }
            }
            sttStatusBar.Text = "\nStart in " + Functions._DelayTime.ToString() + " seconds";
            Functions._DelayTime--;
            if (Functions._DelayTime == -1)
            {
                Functions._DelayTime = 0;
                tmCountdown.Stop();
                Hide();
                Functions.PowerAction(Functions._ActionCode);
                ntfIcon.Icon = null;
                ntfIcon.Visible = false;
                //System.Threading.Thread.Sleep(2000);
                Close();
                System.Windows.Forms.Application.Exit();
            }
        }

        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form _frmAbout = new About();
            Point p = new Point(Location.X + Width / 2 - _frmAbout.Width / 2, Location.Y + Height / 2 - _frmAbout.Height / 2);
            _frmAbout.Location = p;
            _frmAbout.ShowDialog();
        }

        private void ntfIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ctmsSystemTray.Show(Cursor.Position);
        }

        private void ntfIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ctmsSystemTray.Show(Cursor.Position);
            }
        }

        private void showHideWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isHidden)
            {
                showHideWindowToolStripMenuItem.Text = "Show Window";
                Hide();
                ShowInTaskbar = false;
                isHidden = true;
            }
            else
            {
                showHideWindowToolStripMenuItem.Text = "Hide Window";
                ShowInTaskbar = true;
                Show();
                WindowState = FormWindowState.Normal;
                isHidden = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            ntfIcon.Icon = null;
            Application.Exit();
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions.PowerAction(1);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Functions.PowerAction(2);
            System.Windows.Forms.Application.Exit();
        }

        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Functions.PowerAction(3);
            System.Windows.Forms.Application.Exit();
        }

        private void hibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Functions.PowerAction(4);
            System.Windows.Forms.Application.Exit();
        }

        private void signoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Functions.PowerAction(5);
            System.Windows.Forms.Application.Exit();
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Functions.PowerAction(6);
            System.Windows.Forms.Application.Exit();
        }

        private void switchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Functions.PowerAction(7);
            System.Windows.Forms.Application.Exit();
        }

        private void monitoroffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
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
            //System.Threading.Thread.Sleep(1000);
            string UpdateURL = "https://api.github.com/repos/ansidev/legoivpowertool/releases";
            string _client_id = "bad2abc91b66b4a2b0da";
            string _client_secret = "2038c9100f0aca9f4f6b01ca8d59376eaeb3b74e";
            string UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.102 Safari/537.36";
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", UserAgent);
            //string beforeData = "{\r\n'?xml': {\r\n'@version': '1.0',\r\n'@encoding': 'UTF-8'\r\n},\r\n'root': {\r\n'release': ";
            //string afterData = @"}\r\n}";
            //string rawdata = beforeData + client.DownloadString(UpdateURL) + afterData;
            string rawdata = client.DownloadString(UpdateURL + "?client_id=" + _client_id + "&client_secret=" + _client_secret);
            //MessageBox.Show(rawdata);
            //XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(rawdata);
            releases = JsonConvert.DeserializeObject<List<GitHubRelease>>(rawdata);
            if (!this.Controls.Contains(lblRelease) || !this.Controls.Contains(lblReleaseInfo) || !this.Controls.Contains(btnCloseUpdate))
            {
                this.lblRelease = new System.Windows.Forms.Label();
                this.lblReleaseInfo = new System.Windows.Forms.Label();
                this.btnCloseUpdate = new LegoIV_Power_Tool.MetroButton();
                //this.SuspendLayout();

                // 
                // lblRelease
                // 
                lblRelease.AutoSize = true;
                lblRelease.Location = new System.Drawing.Point(325, 226);
                lblRelease.Name = "lblRelease";
                lblRelease.Size = new System.Drawing.Size(83, 15);
                lblRelease.TabIndex = 19;
                lblRelease.Text = "Latest Release:";
                // 
                // lblReleaseInfo
                // 
                lblReleaseInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                lblReleaseInfo.Location = new System.Drawing.Point(322, 245);
                lblReleaseInfo.Name = "lblReleaseInfo";
                lblReleaseInfo.Size = new System.Drawing.Size(321, 90);
                lblReleaseInfo.TabIndex = 20;
                // 
                // btnCloseUpdate
                // 
                btnCloseUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
                btnCloseUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnCloseUpdate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnCloseUpdate.ForeColor = System.Drawing.Color.White;
                btnCloseUpdate.Location = new System.Drawing.Point(584, 339);
                btnCloseUpdate.Name = "btnCloseUpdate";
                btnCloseUpdate.Size = new System.Drawing.Size(60, 26);
                btnCloseUpdate.TabIndex = 24;
                btnCloseUpdate.Text = "Close";
                btnCloseUpdate.UseVisualStyleBackColor = true;
                btnCloseUpdate.Click += new System.EventHandler(btnCloseUpdate_Click);

                this.Controls.Add(btnCloseUpdate);
                this.Controls.Add(lblRelease);
                this.Controls.Add(lblReleaseInfo);
            }
            lblReleaseInfo.Text = releases[0].DisplayReleaseInfo();
            string latestVersion = releases[0].GetReleaseVersion().Replace('.'.ToString(), String.Empty);
            string localVersion = About.Version.Replace('.'.ToString(), String.Empty);
            if (Int16.Parse(latestVersion) >= Int16.Parse(localVersion))
            {
                this.btnDownloadx86 = new LegoIV_Power_Tool.MetroButton();
                this.btnDownloadx64 = new LegoIV_Power_Tool.MetroButton();
                this.btnBrowse = new LegoIV_Power_Tool.MetroButton();
                this.SuspendLayout();

                // 
                // btnDownloadx86
                // 
                btnDownloadx86.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
                btnDownloadx86.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnDownloadx86.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnDownloadx86.ForeColor = System.Drawing.Color.White;
                btnDownloadx86.Location = new System.Drawing.Point(321, 339);
                btnDownloadx86.Name = "btnDownloadx86";
                btnDownloadx86.Size = new System.Drawing.Size(60, 26);
                btnDownloadx86.TabIndex = 22;
                btnDownloadx86.Text = "x86";
                btnDownloadx86.UseVisualStyleBackColor = true;
                btnDownloadx86.Click += new EventHandler(btnDownloadx86_Click);
                // 
                // btnDownloadx64
                // 
                btnDownloadx64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
                btnDownloadx64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnDownloadx64.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnDownloadx64.ForeColor = System.Drawing.Color.White;
                btnDownloadx64.Location = new System.Drawing.Point(385, 339);
                btnDownloadx64.Name = "btnDownloadx64";
                btnDownloadx64.Size = new System.Drawing.Size(60, 26);
                btnDownloadx64.TabIndex = 23;
                btnDownloadx64.Text = "x64";
                btnDownloadx64.UseVisualStyleBackColor = true;
                btnDownloadx64.Click += new EventHandler(btnDownloadx64_Click);

                this.Controls.Add(btnDownloadx86);
                this.Controls.Add(btnDownloadx64);
            }
            else
            {
                this.SuspendLayout();
                MessageBox.Show("You are running latest version!", "Congratulation");
            }
            sttStatusBar.Text = "Finished";
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

        private void btnDownloadx64_Click(object sender, EventArgs e)
        {
            string UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.102 Safari/537.36";
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", UserAgent);
            GitHubAPI GHAPI = new GitHubAPI();
            GitHubReleaseAsset _asset = GHAPI.ReleaseAsset(releases, "x64");
            client.DownloadFile(_asset.browser_download_url, _asset.name);
        }
        private void btnDownloadx86_Click(object sender, EventArgs e)
        {
            string UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.102 Safari/537.36";
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", UserAgent);
            GitHubAPI GHAPI = new GitHubAPI();
            GitHubReleaseAsset _asset = GHAPI.ReleaseAsset(releases, "x86");
            client.DownloadFile(_asset.browser_download_url, _asset.name);
        }
        private void btnCloseUpdate_Click(object sender, EventArgs e)
        {
            if (this.Controls.Contains(lblRelease))
            {
                this.Controls.Remove(lblRelease);
            }
            if (this.Controls.Contains(lblReleaseInfo))
            {
                this.Controls.Remove(lblReleaseInfo);
            }
            if (this.Controls.Contains(btnCloseUpdate))
            {
                this.Controls.Remove(btnCloseUpdate);
            }
            if (this.Controls.Contains(btnDownloadx86))
            {
                this.Controls.Remove(btnDownloadx86);
            }
            if (this.Controls.Contains(btnDownloadx64))
            {
                this.Controls.Remove(btnDownloadx64);
            }
            this.SuspendLayout();
        }
    }
}
