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
        private string AppName = "LegoIV Power Tool";
        private string Version = "1.1";
        private string Channel = "Stable";
        private string License = "GPLv3";
        private string ProjectHomepage = "http://blog.ansidev.tk/products/legoiv-power-tool/";

        //About author
        private string Author = "ansidev";
        private string Email = "ansidev@gmail.com";
        private string AllProject = "https://github.com/ansidev";
        private string AuthorHomepage = "http://blog.ansidev.tk";

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
