using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VictorBuilder
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        // Used for all Weapon Inventory picture box controls to select the weapon clicked by the user
        private void SwapWeapon(object sender, MouseEventArgs e, PictureBox pb)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    //Copy the selected weapon to weapon slot 1
                    pbWeapon1.Image = pb.Image;
                    break;
                default:
                    break;
            }
        }

        // Used for all Weapon Inventory picture box controls to set a border around the currently hovered weapon to highlight it
        private void HighlightWeapon(object sender, PaintEventArgs e, PictureBox pb)
        {
            //Highlight the currently hovered weapon
            ControlPaint.DrawBorder(e.Graphics, pb.ClientRectangle, Color.Gold, ButtonBorderStyle.Solid);
        }

        // Used for all Weapon Inventory picture box controls to remove a border around the previously hovered weapon to unhighlight it
        private void UnhighlightWeapon(object sender, EventArgs e, PictureBox pb)
        {
            //Remove the border to unhighlight the item
            pb.BorderStyle = BorderStyle.None;
        }

        private void pbInventory1_1_MouseClick(object sender, MouseEventArgs e)
        {
            SwapWeapon(sender, e, pbInventory1_1);
        }

        private void pbInventory1_2_MouseClick(object sender, MouseEventArgs e)
        {
            SwapWeapon(sender, e, pbInventory1_2);
        }

        private void pbInventory1_1_MouseHover(object sender, EventArgs e)
        {
            pbInventory1_1.Refresh();
        }

        private void pbInventory1_2_MouseHover(object sender, EventArgs e)
        {
            pbInventory1_2.Refresh();
        }

        private void pbInventory1_1_Paint(object sender, PaintEventArgs e)
        {
            HighlightWeapon(sender, e, pbInventory1_1);
        }

        private void pbInventory1_1_MouseLeave(object sender, EventArgs e)
        {
            UnhighlightWeapon(sender, e, pbInventory1_1);
        }
    }
}
