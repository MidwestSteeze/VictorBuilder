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

            //Prevent flicker on button highlight changes
            this.DoubleBuffered = true;

            //Scale the app at startup to half its original size
            //SizeF factor = new SizeF(scaleFactor, scaleFactor);
            //this.Scale(factor);

            //Remove the weird default border that a TabControl keeps around its children tab pages
            tcInventoryWeapons.Region = new Region(new RectangleF(tbInventoryWeaponsPage1.Left, tbInventoryWeaponsPage1.Top, tbInventoryWeaponsPage1.Width, tbInventoryWeaponsPage1.Height));
            tcInventoryCards.Region = new Region(new RectangleF(tbInventoryCardsPage1.Left, tbInventoryCardsPage1.Top, tbInventoryCardsPage1.Width, tbInventoryCardsPage1.Height));

            //Default weapon slot to show stats from as Weapon 1
            cboWeaponSlot.SelectedIndex = 0;

            //START Temporary OnLoad logic
            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Sword, 44, 75, 0, 35, 100);
            btnInventoryWeapons00.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Legendary, weaponTags);
            btnInventoryWeapons00.Image = Image.FromFile("..\\..\\images\\weapons\\icon_sword.png");

            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Scythe, 10, 190, 10, 15, 100);
            btnInventoryWeapons10.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Rare, weaponTags);
            btnInventoryWeapons10.Image = Image.FromFile("..\\..\\images\\weapons\\icon_scythe.png");

            btnInventoryCards00.Image = Image.FromFile("..\\..\\images\\cards\\icon_warrior.png");
            //END Temporary OnLoad logic
        }

        //Calculate and update stats along top bar
        private void CalculateStats(Tags slotTags)
        {
            if (slotTags != null)
            {
                //Update additional information as a result of the weapon swap (stats, dmg, etc)
                //lblHealth.Text =                 
                lblDamage.Text = (BaseDmgMin + slotTags.weaponTags.dmgMin).ToString() + "-" + (BaseDmgMax + slotTags.weaponTags.dmgMax).ToString();
                //lblArmor.Text = 
                lblArmorPenetration.Text = (BaseArmorPenetration + slotTags.weaponTags.armorPenetration).ToString();
                lblCritChance.Text = (BaseCritChance + slotTags.weaponTags.critChance).ToString() + "%";
                lblCritMulti.Text = (BaseCritMulti + slotTags.weaponTags.critMulti).ToString() + "%";
            }
        }

        //Used for all Inventory slot controls to copy the item clicked by the user and move it to an equipped slot
        private void SelectItem(object sender, MouseEventArgs e)
        {
            bool itemEquipped;
            Button slot = (Button)sender;

            switch (e.Button)
            {
                //case MouseButtons.Left:
                //    break;
                case MouseButtons.Right:
                    itemEquipped = EquipItem(slot, Control.ModifierKeys == Keys.Shift);
                    break;
                default:
                    break;
            }
        }

        private bool EquipItem(Button slot, bool secondarySlot)
        {
            bool itemEquipped = false;
            Tags slotTags = (Tags)slot.Tag;

            switch (slotTags.itemType)
            {
                case Tags.ItemType.Card:
                    break;
                case Tags.ItemType.Consumable:
                    break;
                case Tags.ItemType.DemonPower:
                    break;
                case Tags.ItemType.Empty:
                    break;
                case Tags.ItemType.Outfit:
                    break;
                case Tags.ItemType.Weapon:
                    if (secondarySlot)
                    {
                        //Copy item to secondary slot
                        btnWeapon2.Image = slot.Image;
                        btnWeapon2.Tag = slotTags;
                        itemEquipped = true;
                        if (cboWeaponSlot.SelectedIndex == 1)
                        {
                            CalculateStats(slotTags);
                        }
                    }
                    else
                    {
                        //Copy item to primary slot
                        btnWeapon1.Image = slot.Image;
                        btnWeapon1.Tag = slotTags;
                        itemEquipped = true;
                        if (cboWeaponSlot.SelectedIndex == 0)
                        {
                            CalculateStats(slotTags);
                        }
                    }
                    break;
                default:
                    break;
            }

            return itemEquipped;
        }

        //Determine which Weapons lot the user selected from the drop-down and call CalculateStats() to recalculate the stats along the top bar
        private void SelectedWeaponSlotChanged(object sender, EventArgs e)
        {
            switch (cboWeaponSlot.SelectedIndex)
            {
                case 0:
                    CalculateStats((Tags)btnWeapon1.Tag);
                    break;
                case 1:
                    CalculateStats((Tags)btnWeapon2.Tag);
                    break;
                default:
                    break;
            }
        }

        //Subproc called from mouse event for all Inventory slots to set a border around the currently hovered slot to highlight it
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

        //Subproc called from mouse event for all Inventory slots to remove a border around the previously hovered slot to unhighlight it
        private void UnhighlightSlot(object sender, EventArgs e)
        {
            Button slot = (Button)sender;
            
            slot.FlatAppearance.BorderSize = 1;
            slot.FlatAppearance.BorderColor = Color.Black;
        }

        private void Inventory_MouseUp(object sender, MouseEventArgs e)
        {
            Button slot = (Button)sender;
            Tags slotTags = (Tags)slot.Tag;

            SelectItem(sender, e);
        }

        //Will call a subproc to highlight the item being hovered
        private void Inventory_MouseHover(object sender, EventArgs e)
        {
            HighlightSlot(sender, e);
        }

        //Will call a subproc to unhighlight the item that was being hovered
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

        //Subproc called from mouse event when user clicks a category header button to swap the inventory display (eg. Weapons --> Cards)
        private void ChangeInventoryPage(string pageName)
        { 
            //Hide all inventory tabs
            tcInventoryWeapons.Visible = false;
            //tbInventoryConsumables.Visible = false;
            //tbInventoryDemonPowers.Visible = false;
            tcInventoryCards.Visible = false;
            //tbInventoryOther.Visible = false;

            //create multiple panels and hide/show the selected one
            switch (pageName)
            {
                case "weapons":
                    tcInventoryWeapons.Visible = true;
                    break;

                case "cards":
                    tcInventoryCards.Visible = true;
                    break;
                default:
                    break;
            }
        }

        //Event handler for category header button mouse click to adjust the highlight of the selected category header button
        private void InventoryCategoryChange(object sender, MouseEventArgs e)
        {
            PictureBox selected = (PictureBox)sender;

            //Display the inventory page corresponding to the header category button the user just clicked
            ChangeInventoryPage(selected.Tag.ToString());

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

            //Determine which icon was selected and highlight it while showing the others as normal
            switch (selected.Tag.ToString())
            {
                case "weapons":
                    pbIconWeaponsHighlighted.Visible = true;
                    pbIconConsumables.Visible = true;
                    //pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;
                    pbIconOther.Visible = true;
                    break;
                case "consumables":
                    //pbIconConsumablesHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    //pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;
                    pbIconOther.Visible = true;
                    break;
                //case "demonPowers":
                    //pbIconDemonPowersHighlighted.Visible = true;
                    //pbIconWeapons.Visible = true;
                    //pbIconConsumables.Visible = true;
                    //pbIconCards.Visible = true;
                    //pbIconOther.Visible = true;
                    //break;
                case "cards":
                    pbIconCardsHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    pbIconConsumables.Visible = true;
                    //pbIconDemonPowers.Visible = true;
                    pbIconOther.Visible = true;
                    break;
                case "other":
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
