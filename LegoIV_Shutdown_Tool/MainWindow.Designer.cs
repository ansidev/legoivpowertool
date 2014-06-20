namespace LegoIV_Power_Tool
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.lblAction = new System.Windows.Forms.Label();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grpbxDelayTime = new System.Windows.Forms.GroupBox();
            this.lblSecond = new System.Windows.Forms.Label();
            this.nmrdSecond = new System.Windows.Forms.NumericUpDown();
            this.lblMinute = new System.Windows.Forms.Label();
            this.nmrdMinute = new System.Windows.Forms.NumericUpDown();
            this.lblHour = new System.Windows.Forms.Label();
            this.nmrcHour = new System.Windows.Forms.NumericUpDown();
            this.rdbtnAfter = new System.Windows.Forms.RadioButton();
            this.rdbtnNow = new System.Windows.Forms.RadioButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.sttStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSettings = new System.Windows.Forms.Label();
            this.lblSettingsBox = new System.Windows.Forms.Label();
            this.btnStart = new LegoIV_Power_Tool.MetroButton();
            this.btnMonitorOff = new LegoIV_Power_Tool.MetroButton();
            this.btnSwitch = new LegoIV_Power_Tool.MetroButton();
            this.btnSignout = new LegoIV_Power_Tool.MetroButton();
            this.btnLock = new LegoIV_Power_Tool.MetroButton();
            this.btnSleep = new LegoIV_Power_Tool.MetroButton();
            this.btnRestart = new LegoIV_Power_Tool.MetroButton();
            this.btnHibernate = new LegoIV_Power_Tool.MetroButton();
            this.btnShutdown = new LegoIV_Power_Tool.MetroButton();
            this.grpbxDelayTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrdSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrdMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcHour)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lnkAbout
            // 
            this.lnkAbout.AutoSize = true;
            this.lnkAbout.Location = new System.Drawing.Point(597, 375);
            this.lnkAbout.Name = "lnkAbout";
            this.lnkAbout.Size = new System.Drawing.Size(40, 15);
            this.lnkAbout.TabIndex = 8;
            this.lnkAbout.TabStop = true;
            this.lnkAbout.Text = "About";
            // 
            // lblAction
            // 
            this.lblAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(10, 12);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(102, 15);
            this.lblAction.TabIndex = 9;
            this.lblAction.Text = "Choose an action:";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(10, 323);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(304, 27);
            this.pgBar.TabIndex = 10;
            // 
            // grpbxDelayTime
            // 
            this.grpbxDelayTime.Controls.Add(this.lblSecond);
            this.grpbxDelayTime.Controls.Add(this.nmrdSecond);
            this.grpbxDelayTime.Controls.Add(this.lblMinute);
            this.grpbxDelayTime.Controls.Add(this.nmrdMinute);
            this.grpbxDelayTime.Controls.Add(this.lblHour);
            this.grpbxDelayTime.Controls.Add(this.nmrcHour);
            this.grpbxDelayTime.Controls.Add(this.rdbtnAfter);
            this.grpbxDelayTime.Controls.Add(this.rdbtnNow);
            this.grpbxDelayTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbxDelayTime.Location = new System.Drawing.Point(10, 190);
            this.grpbxDelayTime.Name = "grpbxDelayTime";
            this.grpbxDelayTime.Size = new System.Drawing.Size(304, 95);
            this.grpbxDelayTime.TabIndex = 11;
            this.grpbxDelayTime.TabStop = false;
            this.grpbxDelayTime.Text = "Delay time";
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.Location = new System.Drawing.Point(264, 59);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(13, 15);
            this.lblSecond.TabIndex = 5;
            this.lblSecond.Tag = "";
            this.lblSecond.Text = "S";
            // 
            // nmrdSecond
            // 
            this.nmrdSecond.Location = new System.Drawing.Point(217, 57);
            this.nmrdSecond.Name = "nmrdSecond";
            this.nmrdSecond.Size = new System.Drawing.Size(44, 23);
            this.nmrdSecond.TabIndex = 2;
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Location = new System.Drawing.Point(191, 59);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(18, 15);
            this.lblMinute.TabIndex = 5;
            this.lblMinute.Tag = "";
            this.lblMinute.Text = "M";
            // 
            // nmrdMinute
            // 
            this.nmrdMinute.Location = new System.Drawing.Point(143, 57);
            this.nmrdMinute.Name = "nmrdMinute";
            this.nmrdMinute.Size = new System.Drawing.Size(44, 23);
            this.nmrdMinute.TabIndex = 2;
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Location = new System.Drawing.Point(122, 59);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(16, 15);
            this.lblHour.TabIndex = 5;
            this.lblHour.Tag = "";
            this.lblHour.Text = "H";
            // 
            // nmrcHour
            // 
            this.nmrcHour.Location = new System.Drawing.Point(75, 57);
            this.nmrcHour.Name = "nmrcHour";
            this.nmrcHour.Size = new System.Drawing.Size(44, 23);
            this.nmrcHour.TabIndex = 2;
            // 
            // rdbtnAfter
            // 
            this.rdbtnAfter.AutoSize = true;
            this.rdbtnAfter.Location = new System.Drawing.Point(8, 57);
            this.rdbtnAfter.Name = "rdbtnAfter";
            this.rdbtnAfter.Size = new System.Drawing.Size(51, 19);
            this.rdbtnAfter.TabIndex = 1;
            this.rdbtnAfter.Text = "After";
            this.rdbtnAfter.UseVisualStyleBackColor = true;
            // 
            // rdbtnNow
            // 
            this.rdbtnNow.AutoSize = true;
            this.rdbtnNow.Checked = true;
            this.rdbtnNow.Location = new System.Drawing.Point(8, 27);
            this.rdbtnNow.Name = "rdbtnNow";
            this.rdbtnNow.Size = new System.Drawing.Size(50, 19);
            this.rdbtnNow.TabIndex = 0;
            this.rdbtnNow.TabStop = true;
            this.rdbtnNow.Text = "Now";
            this.rdbtnNow.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sttStatusBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 371);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(655, 22);
            this.statusStrip.TabIndex = 12;
            this.statusStrip.Text = "LegoIV Power Tool";
            // 
            // sttStatusBar
            // 
            this.sttStatusBar.Name = "sttStatusBar";
            this.sttStatusBar.Size = new System.Drawing.Size(106, 17);
            this.sttStatusBar.Text = "LegoIV Power Tool";
            // 
            // lblSettings
            // 
            this.lblSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSettings.AutoSize = true;
            this.lblSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettings.Location = new System.Drawing.Point(318, 12);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(79, 15);
            this.lblSettings.TabIndex = 9;
            this.lblSettings.Text = "Your settings:";
            // 
            // lblSettingsBox
            // 
            this.lblSettingsBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSettingsBox.Location = new System.Drawing.Point(322, 33);
            this.lblSettingsBox.Name = "lblSettingsBox";
            this.lblSettingsBox.Size = new System.Drawing.Size(321, 317);
            this.lblSettingsBox.TabIndex = 13;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(10, 291);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(305, 32);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnMonitorOff
            // 
            this.btnMonitorOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnMonitorOff.FlatAppearance.BorderSize = 0;
            this.btnMonitorOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonitorOff.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonitorOff.ForeColor = System.Drawing.Color.White;
            this.btnMonitorOff.Location = new System.Drawing.Point(167, 150);
            this.btnMonitorOff.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonitorOff.Name = "btnMonitorOff";
            this.btnMonitorOff.Size = new System.Drawing.Size(148, 32);
            this.btnMonitorOff.TabIndex = 7;
            this.btnMonitorOff.Text = "TURN OFF MONITOR";
            this.btnMonitorOff.UseVisualStyleBackColor = false;
            this.btnMonitorOff.Click += new System.EventHandler(this.btnMonitorOff_Click);
            this.btnMonitorOff.MouseLeave += new System.EventHandler(this.btnMonitorOff_MouseLeave);
            this.btnMonitorOff.MouseHover += new System.EventHandler(this.btnMonitorOff_MouseHover);
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSwitch.FlatAppearance.BorderSize = 0;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitch.ForeColor = System.Drawing.Color.White;
            this.btnSwitch.Location = new System.Drawing.Point(167, 111);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(148, 32);
            this.btnSwitch.TabIndex = 6;
            this.btnSwitch.Text = "SWITCH USER";
            this.btnSwitch.UseVisualStyleBackColor = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            this.btnSwitch.MouseLeave += new System.EventHandler(this.btnSwitch_MouseLeave);
            this.btnSwitch.MouseHover += new System.EventHandler(this.btnSwitch_MouseHover);
            // 
            // btnSignout
            // 
            this.btnSignout.BackColor = System.Drawing.Color.DarkRed;
            this.btnSignout.FlatAppearance.BorderSize = 0;
            this.btnSignout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignout.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignout.ForeColor = System.Drawing.Color.White;
            this.btnSignout.Location = new System.Drawing.Point(167, 33);
            this.btnSignout.Name = "btnSignout";
            this.btnSignout.Size = new System.Drawing.Size(148, 32);
            this.btnSignout.TabIndex = 4;
            this.btnSignout.Text = "SIGN OUT";
            this.btnSignout.UseVisualStyleBackColor = false;
            this.btnSignout.Click += new System.EventHandler(this.btnSignout_Click);
            this.btnSignout.MouseLeave += new System.EventHandler(this.btnSignout_MouseLeave);
            this.btnSignout.MouseHover += new System.EventHandler(this.btnSignout_MouseHover);
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(138)))), ((int)(((byte)(0)))));
            this.btnLock.FlatAppearance.BorderSize = 0;
            this.btnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLock.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLock.ForeColor = System.Drawing.Color.White;
            this.btnLock.Location = new System.Drawing.Point(167, 72);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(148, 32);
            this.btnLock.TabIndex = 5;
            this.btnLock.Text = "LOCK SCREEN";
            this.btnLock.UseVisualStyleBackColor = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            this.btnLock.MouseLeave += new System.EventHandler(this.btnLock_MouseLeave);
            this.btnLock.MouseHover += new System.EventHandler(this.btnLock_MouseHover);
            // 
            // btnSleep
            // 
            this.btnSleep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(224)))));
            this.btnSleep.FlatAppearance.BorderSize = 0;
            this.btnSleep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSleep.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSleep.ForeColor = System.Drawing.Color.White;
            this.btnSleep.Location = new System.Drawing.Point(10, 111);
            this.btnSleep.Name = "btnSleep";
            this.btnSleep.Size = new System.Drawing.Size(148, 32);
            this.btnSleep.TabIndex = 2;
            this.btnSleep.Text = "SLEEP";
            this.btnSleep.UseVisualStyleBackColor = false;
            this.btnSleep.Click += new System.EventHandler(this.btnSleep_Click);
            this.btnSleep.MouseLeave += new System.EventHandler(this.btnSleep_MouseLeave);
            this.btnSleep.MouseHover += new System.EventHandler(this.btnSleep_MouseHover);
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.OrangeRed;
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.Location = new System.Drawing.Point(10, 72);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(148, 32);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "RESTART";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            this.btnRestart.MouseLeave += new System.EventHandler(this.btnRestart_MouseLeave);
            this.btnRestart.MouseHover += new System.EventHandler(this.btnRestart_MouseHover);
            // 
            // btnHibernate
            // 
            this.btnHibernate.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnHibernate.FlatAppearance.BorderSize = 0;
            this.btnHibernate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHibernate.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHibernate.ForeColor = System.Drawing.Color.White;
            this.btnHibernate.Location = new System.Drawing.Point(10, 150);
            this.btnHibernate.Name = "btnHibernate";
            this.btnHibernate.Size = new System.Drawing.Size(148, 32);
            this.btnHibernate.TabIndex = 3;
            this.btnHibernate.Text = "HIBERNATE";
            this.btnHibernate.UseVisualStyleBackColor = false;
            this.btnHibernate.Click += new System.EventHandler(this.btnHibernate_Click);
            this.btnHibernate.MouseLeave += new System.EventHandler(this.btnHibernate_MouseLeave);
            this.btnHibernate.MouseHover += new System.EventHandler(this.btnHibernate_MouseHover);
            // 
            // btnShutdown
            // 
            this.btnShutdown.BackColor = System.Drawing.Color.Red;
            this.btnShutdown.FlatAppearance.BorderSize = 0;
            this.btnShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShutdown.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShutdown.ForeColor = System.Drawing.Color.White;
            this.btnShutdown.Location = new System.Drawing.Point(10, 33);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(148, 32);
            this.btnShutdown.TabIndex = 0;
            this.btnShutdown.Text = "SHUT DOWN";
            this.btnShutdown.UseVisualStyleBackColor = false;
            this.btnShutdown.SelectedChanged += new System.EventHandler(this.btnShutdown_SelectedChanged);
            this.btnShutdown.BackColorChanged += new System.EventHandler(this.btnShutdown_BackColorChanged);
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            this.btnShutdown.MouseLeave += new System.EventHandler(this.btnShutdown_MouseLeave);
            this.btnShutdown.MouseHover += new System.EventHandler(this.btnShutdown_MouseHover);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(655, 393);
            this.Controls.Add(this.lblSettingsBox);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.grpbxDelayTime);
            this.Controls.Add(this.pgBar);
            this.Controls.Add(this.lblSettings);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.lnkAbout);
            this.Controls.Add(this.btnMonitorOff);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.btnSignout);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnSleep);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnHibernate);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LegoIV Power Tool";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.grpbxDelayTime.ResumeLayout(false);
            this.grpbxDelayTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrdSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrdMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcHour)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroButton btnShutdown;
        private MetroButton btnRestart;
        private MetroButton btnSleep;
        private MetroButton btnHibernate;
        private MetroButton btnLock;
        private MetroButton btnSignout;
        private MetroButton btnSwitch;
        private MetroButton btnMonitorOff;
        private System.Windows.Forms.LinkLabel lnkAbout;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.GroupBox grpbxDelayTime;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.NumericUpDown nmrdSecond;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.NumericUpDown nmrdMinute;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.NumericUpDown nmrcHour;
        private System.Windows.Forms.RadioButton rdbtnAfter;
        private System.Windows.Forms.RadioButton rdbtnNow;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel sttStatusBar;
        private MetroButton btnStart;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Label lblSettingsBox;
    }
}

