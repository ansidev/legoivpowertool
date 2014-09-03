using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegoIV_Power_Tool
{
    public partial class About : Form
    {
        //About app
        private static string AppName = "LegoIV Power Tool";
        public static string Version = "1.4.14.0903";
        private static string Channel = "Stable";
        private static string License = "GPLv3";
        private static string ProjectHomepage = "http://blog.ansidev.tk/products/legoiv-power-tool/";

        //About author
        private static string Author = "ansidev";
        private static string Email = "ansidev@gmail.com";
        private static string AllProject = "https://github.com/ansidev";
        private static string AuthorHomepage = "http://blog.ansidev.tk";

        //Source code
        private string SourceCode = "https://github.com/ansidev/legoivpowertool";
        
        public About()
        {
            InitializeComponent();
            this.Text = "About " + AppName;
            this.lblAppName.Text = AppName;
            this.lblVersion.Text = Version + " " + Channel;
            this.lblLicense.Text = License;
            this.lkblProjectHomepage.Text = ProjectHomepage;

            this.lblAuthor.Text = Author;
            this.lblEmail.Text = Email;
            this.lkblAllProjects.Text = AllProject;
            this.lkblHomepage.Text = AuthorHomepage;

            this.lkblSourceCode.Text = SourceCode;
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void lkblProjectHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.lkblProjectHomepage.Text);
        }

        private void lkblAllProjects_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.lkblAllProjects.Text);
        }

        private void lkblHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.lkblHomepage.Text);
        }
    }
}
