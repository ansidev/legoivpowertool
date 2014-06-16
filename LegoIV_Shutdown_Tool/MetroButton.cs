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
    public partial class MetroButton : Button
    {
        public MetroButton()
        {
            InitializeComponent();
        }

        public bool Checked
        {
            get;
            set;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
