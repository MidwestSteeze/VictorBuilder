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
        //Globals
        Tags.WeaponTags weaponTags;
        int BaseArmor = 0;
        int BaseArmorPenetration = 0;
        int BaseCritChance = 0;
        int BaseCritMulti = 0;
        int BaseDmgMax = 0;
        int BaseDmgMin = 0;
        int BaseHealth = 4000; //TODO - whats base life at max lvl?

        float scaleFactor = 0.5f;

        public frmMain()
        {
            InitializeComponent();

            //Scale the app at startup to half its original size
            //SizeF factor = new SizeF(scaleFactor, scaleFactor);
            //this.Scale(factor);

            //START Temporary OnLoad logic
            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Sword, 44, 75, 0, 35, 100);
            btnInventory1_1.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Legendary, weaponTags);
            btnInventory1_1.Image = Image.FromFile("..\\..\\images\\weapons\\icon_sword.png");

            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Scythe, 10, 190, 10, 15, 100);
            btnInventory1_2.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Rare, weaponTags);
            //END Temporary OnLoad logic
        }

        // Used for all Weapon Inventory slot controls to select the weapon clicked by the user
        private void SwapWeapon(object sender, MouseEventArgs e)
        {
            bool weaponSwapped = false;
            Button slot = (Button)sender;
            Tags slotTags = (Tags)slot.Tag;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        //Copy the selected weapon to weapon slot 2
                        btnWeapon2.Image = slot.Image;
                        weaponSwapped = true;
                    }
                    else
                    {
                        //Copy the selected weapon to weapon slot 1
                        btnWeapon1.Image = slot.Image;
                        weaponSwapped = true;
                    }
                    break;
                default:
                    break;
            }

            if (weaponSwapped && slotTags != null)
            { 
                //Update additional information as a result of the swap (stats, dmg, etc)
                //lblHealth.Text =                 
                lblDamage.Text = (BaseDmgMin + slotTags.weaponTags.dmgMin).ToString() + "-" + (BaseDmgMax + slotTags.weaponTags.dmgMax).ToString();
                //lblArmor.Text = 
                lblArmorPenetration.Text = (BaseArmorPenetration + slotTags.weaponTags.armorPenetration).ToString();
                lblCritChance.Text = (BaseCritChance + slotTags.weaponTags.critChance).ToString() + "%";
                lblCritMulti.Text = (BaseCritMulti + slotTags.weaponTags.critMulti).ToString() + "%";
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
                //No item in the selected slot, use default highlighting
                slot.FlatAppearance.BorderSize = 5;
                slot.FlatAppearance.BorderColor = Color.Gray;
            }
        }

        // Used for all Inventory slots to remove a border around the previously hovered slot to unhighlight it
        private void UnhighlightSlot(object sender, EventArgs e)
        {
            Button slot = (Button)sender;
            
            slot.FlatAppearance.BorderSize = 1;
            slot.FlatAppearance.BorderColor = Color.Black;
        }

        private void Inventory_MouseUp(object sender, MouseEventArgs e)
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

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType)
        {
            return new Tags(aItemType, aRarityType);
        }

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType,
                                  Tags.WeaponTags.WeaponType aWeaponType)
        {
            Tags.WeaponTags weaponTags = new Tags.WeaponTags(aWeaponType);
            return new Tags(aItemType, aRarityType, weaponTags);
        }

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType,
                                  Tags.WeaponTags aWeaponTags)
        {
            return new Tags(aItemType, aRarityType, aWeaponTags);
        }

        private void ResizeFont(Control.ControlCollection coll, float scaleFactor)
        {
            foreach (Control c in coll)
            {
                if (c.HasChildren)
                {
                    ResizeFont(c.Controls, scaleFactor);
                }
                else
                {
                    //if (c.GetType().ToString() == "System.Windows.Form.Label")
                    if (true)
                    {
                        // scale font
                        c.Font = new Font(c.Font.FontFamily.Name, c.Font.Size * scaleFactor);
                    }
                    //else if (c.GetType().ToString() == "System.Windows.Form.Button")
                    //{
                    //    Button tmpButton = (Button)c;
                    //}
                }
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //Update all font sizes if the app was resized
            //ResizeFont(this.Controls, scaleFactor);
        }

        private void InventoryCategoryChange(object sender, MouseEventArgs e)
        {
            PictureBox selected = (PictureBox)sender;

            //Hide all images showing the selected icon since we'll be resetting it next
            pbIconWeapons.Visible = false;
            pbIconWeaponsHighlighted.Visible = false;
            pbIconConsumables.Visible = false;
            //pbIconConsumablesHighlighted.Visible = false;
            //pbIconDemonPowers.Visible = false;
            //pbIconDemonPowersHighlighted.Visible = false;
            pbIconCards.Visible = false;
            pbIconCardsHighlighted.Visible = false;
            pbIconOther.Visible = false;
            pbIconOtherHighlighted.Visible = false;

            //Determine which icon was selected and highlight it
            switch (selected.Tag.ToString())
            {
                case "weapons":
                    //ChangeInventoryPage(selected.Tag.ToString())
                    pbIconWeaponsHighlighted.Visible = true;
                    pbIconConsumables.Visible = true;
                    //pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;
                    pbIconOther.Visible = true;
                    break;
                case "consumables":
                    //ChangeInventoryPage(selected.Tag.ToString())
                    //pbIconConsumablesHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    //pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;
                    pbIconOther.Visible = true;
                    break;
                //case "demonPowers":
                //ChangeInventoryPage(selected.Tag.ToString())
                    //pbIconDemonPowersHighlighted.Visible = true;
                    //pbIconWeapons.Visible = true;
                    //pbIconConsumables.Visible = true;
                    //pbIconCards.Visible = true;
                    //pbIconOther.Visible = true;
                    //break;
                case "cards":
                    //ChangeInventoryPage(selected.Tag.ToString())
                    pbIconCardsHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    pbIconConsumables.Visible = true;
                    //pbIconDemonPowers.Visible = true;
                    pbIconOther.Visible = true;
                    break;
                case "other":
                    //ChangeInventoryPage(selected.Tag.ToString())
                    pbIconOtherHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    pbIconConsumables.Visible = true;
                    //pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;
                    break;
                default:
                    break;
            }
            
        }
    }
}
