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

            Tags tags = new Tags(Tags.ItemType.Weapon, Tags.RarityType.Legendary);
            tags.weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Sword);

            btnInventory1_1.Tag = tags;

            Tags newTags = new Tags(tags.itemType, Tags.RarityType.Rare);
            newTags.weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Scythe);

            btnInventory1_2.Tag = newTags;
        }

        // Used for all Weapon Inventory slot controls to select the weapon clicked by the user
        private void SwapWeapon(object sender, MouseEventArgs e)
        {
            Button slot = (Button)sender;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        //Copy the selected weapon to weapon slot 2
                        btnWeapon2.Image = slot.Image;
                    }
                    else
                    {
                        //Copy the selected weapon to weapon slot 1
                        btnWeapon1.Image = slot.Image;
                    }
                    break;
                default:
                    break;
            }
        }

        // Used for all Inventory slots to set a border around the currently hovered slot to highlight it
        private void HighlightSlot(object sender, EventArgs e)
        {
            Button slot = (Button)sender;
            Tags slotTags = (Tags)slot.Tag;

            if (slotTags != null)
            {
                //Highlight the currently hovered inventory slot
                slot.FlatAppearance.BorderSize = 3;

                switch (slotTags.rarity)
                {
                    case Tags.RarityType.Common:
                        slot.FlatAppearance.BorderColor = Color.Gray;
                        break;

                    case Tags.RarityType.Uncommon:
                        slot.FlatAppearance.BorderColor = Color.Gold;
                        break;

                    case Tags.RarityType.Rare:
                        slot.FlatAppearance.BorderColor = Color.Orange;
                        break;

                    case Tags.RarityType.Legendary:
                        slot.FlatAppearance.BorderColor = Color.Purple;
                        break;

                    default:
                        break;
                }
            }
            else
            {
                slot.FlatAppearance.BorderSize = 5;
                slot.FlatAppearance.BorderColor = Color.Gray;
            }
        }

        // Used for all Inventory slots to remove a border around the previously hovered slot to unhighlight it
        private void UnhighlightSlot(object sender, EventArgs e)
        {
            Button slot = (Button)sender;
            
            slot.FlatAppearance.BorderSize = 0;
        }

        private void Inventory_MouseClick(object sender, MouseEventArgs e)
        {
            SwapWeapon(sender, e);
        }

        private void Inventory_MouseHover(object sender, EventArgs e)
        {
            HighlightSlot(sender, e);
        }

        private void Inventory_MouseLeave(object sender, EventArgs e)
        {
            UnhighlightSlot(sender, e);
        }
    }
}
