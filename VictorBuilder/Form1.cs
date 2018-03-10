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
        Tags.CardTags cardTags;
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
            //cboWeaponSlot.SelectedIndex = 0;

            //START Temporary OnLoad logic
            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Sword, Tags.WeaponTags.WeaponDistance.Melee, 44, 75, 0, 35, 100);
            btnInventoryWeapons00.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Legendary, weaponTags);
            btnInventoryWeapons00.Image = Image.FromFile("..\\..\\images\\weapons\\icon_sword.png");

            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Scythe, Tags.WeaponTags.WeaponDistance.Melee, 10, 190, 10, 15, 100);
            btnInventoryWeapons10.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Rare, weaponTags);
            btnInventoryWeapons10.Image = Image.FromFile("..\\..\\images\\weapons\\icon_scythe.png");

            cardTags = new Tags.CardTags("Warrior", "+10% Melee Damage", 2);
            btnInventoryCards00.Tag = FillItemTags(Tags.ItemType.Card, Tags.RarityType.Common, cardTags);
            btnInventoryCards00.Image = Image.FromFile("..\\..\\images\\cards\\icon_warrior.png");
            //END Temporary OnLoad logic
        }

        //Calculate and update stats along top bar
        private void CalculateStats(Tags slotTags)
        {
            //Determine what type of item was changed that prompted a recalc of stats (ie. weapon, card, etc)
            if (slotTags.weaponTags != null)
            {
                CalculateStatsFromNewWeapon(slotTags);

                //With the new weapon in place, add on the card mods
                CalculateStatsFromEquippedCards();
            }
            else if (slotTags.cardTags != null && (btnEquippedWeapon1.Tag != null || btnEquippedWeapon2.Tag != null))
            {
                CalculateStatsFromNewCard(slotTags);
            }

            //CalculateSkills(slotTags);
        }

        private void CalculateStatsFromNewWeapon(Tags slotTags)
        {
            //A weapon was changed, calculate stats based on the weapon and use the base values to start
            //lblHealth.Text = //not affected by weapons
            lblDamage.Text = (BaseDmgMin + slotTags.weaponTags.dmgMin).ToString() + "-" + (BaseDmgMax + slotTags.weaponTags.dmgMax).ToString();
            //lblArmor.Text = //not affected by weapons
            lblArmorPenetration.Text = (BaseArmorPenetration + slotTags.weaponTags.armorPenetration).ToString();
            lblCritChance.Text = (BaseCritChance + slotTags.weaponTags.critChance).ToString() + "%";
            lblCritMulti.Text = (BaseCritMulti + slotTags.weaponTags.critMulti).ToString() + "%";
        }

       private void CalculateStatsFromNewCard(Tags slotTags)
        {
            //A card was changed, and we have a weapon equipped, calculate stats based on the card and use the existing stats calc'd from having a weapon
            lblHealth.Text = string.Format("{0:n0}", HealthAsNumber() + slotTags.cardTags.health);
            //lblDamage.Text = //not affected by cards
            lblArmor.Text = string.Format("{0:n0}", ArmorAsNumber() + slotTags.cardTags.armor);
            lblArmorPenetration.Text = (ArmorPenetrationAsNumber() + slotTags.cardTags.armorPenetration).ToString();
            lblCritChance.Text = (CritChanceAsNumber() + slotTags.cardTags.critChance).ToString() + "%";
            lblCritMulti.Text = (CritMultiAsNumber() + slotTags.cardTags.critMulti).ToString() + "%";        
        }

        private void CalculateStatsFromEquippedCards()
        { 
            //Loop through all cards
            foreach (Button equippedCard in pnlEquippedCards.Controls)
            {
                //Ensure this slot has a card in it
                if ((Tags)equippedCard.Tag != null)
                {
                    //Take the mod from the card and update the stats or skills if it affects it
                }
            }
        }

        //Used for all Inventory slot controls to copy the item clicked by the user and move it to an equipped slot
        private void SelectItem(object sender, MouseEventArgs e)
        {
            bool itemEquipped = false;
            Button slot = (Button)sender;

            switch (e.Button)
            {
                //case MouseButtons.Left:
                //    break;
                case MouseButtons.Right:
                    EquipItem(slot, Control.ModifierKeys == Keys.Shift, ref itemEquipped);
                    break;
                default:
                    break;
            }
        }

        /********************************
         * START Equip item logic *
         * ******************************/
        #region Equip item logic

        private void EquipItem(Button slot, bool secondarySlot, ref bool itemEquipped)
        {
            Tags slotTags = (Tags)slot.Tag;

            switch (slotTags.itemType)
            {
                case Tags.ItemType.Card:
                    EquipCard(slot, slotTags, ref itemEquipped);
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
                    EquipWeapon(slot, slotTags, secondarySlot, ref itemEquipped);
                    break;
                default:
                    break;
            }
        }

        private void EquipCard(Button slot, Tags slotTags, ref bool itemEquipped)
        {
            //go through all card slots in the group of equippable card slots until you find an empty one
            foreach (Button equippedCard in pnlEquippedCards.Controls)
            {
                //Check if the card slot is empty
                if ((Tags)equippedCard.Tag == null)
                {
                    //Empty card slot found; copy the card into this equippable card slot
                    equippedCard.Image = slot.Image;
                    equippedCard.Tag = slotTags;
                    itemEquipped = true;
                    //CalculateStats(slotTags);
                    break;
                }
            }
        }

        private void EquipWeapon(Button slot, Tags slotTags, bool secondarySlot, ref bool itemEquipped)
        {
            if (secondarySlot)
            {
                //Copy item to secondary slot
                btnEquippedWeapon2.Image = slot.Image;
                btnEquippedWeapon2.Tag = slotTags;
                itemEquipped = true;
                if (cboWeaponSlot.SelectedIndex == 1)
                {
                    CalculateStats(slotTags);
                }
            }
            else
            {
                //Copy item to primary slot
                btnEquippedWeapon1.Image = slot.Image;
                btnEquippedWeapon1.Tag = slotTags;
                itemEquipped = true;
                if (cboWeaponSlot.SelectedIndex == 0)
                {
                    CalculateStats(slotTags);
                }
            }
        }

        #endregion
        /********************************
         * END Equip item logic *
         * ******************************/

        /********************************
         * START Unequip item logic *
         * ******************************/
        #region Unequip item logic

        private void UnequipItem(object sender, EventArgs e) //Button slot, bool secondarySlot)
        {
            bool itemUnequipped = false;
            Button slot = (Button)sender;
            Tags slotTags = (Tags)slot.Tag;

            switch (slotTags.itemType)
            {
                case Tags.ItemType.Card:
                    UnequipCard(slot, slotTags, ref itemUnequipped);
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
                    UnequipWeapon(slot, slotTags, ref itemUnequipped); //, secondarySlot);
                    break;
                default:
                    break;
            }
        }

        private void UnequipCard(Button slot, Tags slotTags, ref bool itemUnequipped)
        {
            //Remove the equipped card from the slot
            slot.Image = null;
            slot.Tag = null;
            itemUnequipped = true;
            //CalculateStats(slotTags); TODO
        }

        private void UnequipWeapon(Button slot, Tags slotTags, ref bool itemUnequipped)
        {
            //Remove the equipped weapon from the slot
            slot.Image = null;
            slot.Tag = null;
            itemUnequipped = true;
            //CalculateStats(slotTags); TODO
        }

        #endregion
        /********************************
         * END Unequip item logic *
         * ******************************/

        //Determine which Weapon slot the user selected from the drop-down and call CalculateStats() to recalculate the stats along the top bar
        private void SelectedWeaponSlotChanged(object sender, EventArgs e)
        {
            switch (cboWeaponSlot.SelectedIndex)
            {
                case 0:
                    CalculateStats((Tags)btnEquippedWeapon1.Tag);
                    break;
                case 1:
                    CalculateStats((Tags)btnEquippedWeapon2.Tag);
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

        private void EquippedItem_MouseUp(object sender, MouseEventArgs e)
        {
            UnequipItem(sender, e);
        }

        private void Inventory_MouseUp(object sender, MouseEventArgs e)
        {
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

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType,
                                  Tags.CardTags aCardTags)
        {
            return new Tags(aItemType, aRarityType, aCardTags);
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

        /***************************
         *  START Stats as numbers *
         *  ************************/
        #region Stats as numbers
        private int HealthAsNumber()
        {
            return Convert.ToInt32(lblHealth.Text.Replace(",", ""));
        }

        private int DmgMinAsNumber()
        {
            return Convert.ToInt32(lblDamage.Text.Substring(0, lblDamage.Text.IndexOf("-")));
        }

        private int DmgMaxAsNumber()
        {
            return Convert.ToInt32(lblDamage.Text.Substring(lblDamage.Text.IndexOf("-") + 1));
        }

        private int ArmorAsNumber()
        {
            return Convert.ToInt32(lblArmor.Text);
        }

        private int ArmorPenetrationAsNumber()
        {
            return Convert.ToInt32(lblArmorPenetration.Text);
        }

        private int CritChanceAsNumber()
        {
            return Convert.ToInt32(lblCritChance.Text.Substring(0, lblCritChance.Text.Length - 1));
        }

        private int CritMultiAsNumber()
        {
            return Convert.ToInt32(lblCritMulti.Text.Substring(0, lblCritMulti.Text.Length - 1));
        }

        #endregion
        /***************************
         *  END Stats as numbers *
         *  ************************/     
    }
}
