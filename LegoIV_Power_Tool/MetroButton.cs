﻿using System;
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

        [Category("Behavior")]
        [Description("Indicates whether the control is selected.")]
        [DefaultValue(false)]
        public bool Selected
        {
            get;
            set;
        }

        [Category("Propperty Changed")]
        [Description("Occurs when the control's selected state changes.")]
        public event EventHandler SelectedChanged;
        protected void OnSelectedChanged(EventArgs e)
        {
            EventHandler handler = SelectedChanged; 
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public delegate void SelectedChangedEventHandler(SelectedChangedEventArgs e);
        
        public class SelectedChangedEventArgs : EventArgs
        {
            public int Selected { get; set; }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            //if (this.Selected == true) this.FlatAppearance.BorderSize = 1;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            //if (this.Selected == true)
            //{
            //    this.FlatAppearance.BorderSize++;
            //}
        }
    }
}
