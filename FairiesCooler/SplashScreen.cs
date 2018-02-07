using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FairiesCooler
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  Do nothing here!
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            //gfx.DrawImage(Images.SplashScreen, new Rectangle(0, 0, this.Width, this.Height));

        }

        private void pushMeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void noPushMeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}