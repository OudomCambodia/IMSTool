using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    class cus_button: Button
    {
        public cus_button() : base()
        {
            base.BackColor = Color.FromArgb(0, 9, 47);
            base.ForeColor = Color.White;
            base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            base.FlatAppearance.BorderSize = 2;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            base.BackColor = Color.FromArgb(0, 32, 171);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            base.BackColor = Color.FromArgb(0, 9, 47);
        }
    }
}
