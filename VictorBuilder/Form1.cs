using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace VictorBuilder
{
    public partial class frmMain : Form
    {
        //Global objects
        Tags.WeaponTags weaponTags;
        Tags.WeaponTags.AttackTags attackTags1;
        Tags.WeaponTags.AttackTags attackTags2;
        Tags.WeaponTags.AttackTags attackTags3;
        Tags.CardTags cardTags;
        Tags.OutfitTags outfitTags;
        Tags newItemTags;
        List<Button> equippedItemControls;

        //Global variables
        string urlAttacks = "..\\..\\images\\attacks\\";
        string urlBackgrounds = "..\\..\\images\\backgrounds\\";
        string urlOutfits = "..\\..\\images\\outfits\\";
        string urlCards = "..\\..\\images\\cards\\";
        string urlWeapons = "..\\..\\images\\weapons\\";
        string urlImageNotFound = "..\\..\\images\\imagenotfound.png";

        //Base stat values
        int BaseHealth = 4000; //TODO - whats base life at max lvl?
        int BaseArmor = 0;
        // Primary
        int BaseArmorPenetration = 0;
        int BaseCritChance = 0;
        int BaseCritMulti = 0;
        int BaseDmgMax = 0;
        int BaseDmgMin = 0;
        // Secondary
        int BaseArmorPenetrationSecondary = 0;
        int BaseCritChanceSecondary = 0;
        int BaseCritMultiSecondary = 0;
        int BaseDmgMaxSecondary = 0;
        int BaseDmgMinSecondary = 0;
        int maximumCardPoints = 24;

        //Stat modifiers (a runnning total from cards, outfit, etc) for calculating purposes
        int modifierIncDamage = 0;
        int modifierIncMeleeDamage = 0;
        int modifierIncRangedDamage = 0;
        int modifierFlatHealthCards = 0;
        int modifierFlatHealthOutfit = 0;
        int modifierFlatArmorCards = 0;
        int modifierFlatArmorOutfit = 0;
        int modifierFlatArmorPenetration = 0;
        int modifierFlatArmorPenetrationSecondary = 0;
        int modifierFlatCritChance = 0;
        int modifierFlatCritChanceSecondary = 0;
        int modifierFlatCritMulti = 0;
        int modifierFlatCritMultiSecondary = 0;

        //Other running totals
        int totalCardPoints = 0;

        //float scaleFactor = 0.5f;

        public frmMain()
        {
            InitializeComponent();

            //Prevent flicker on button highlight changes
            this.DoubleBuffered = true;

            PreloadOutfits();

            //Build the list of equipped item controls so we can iterate through it when Saving/Importing a build
            BuildListOfEquippedItems();

            //Scale the app at startup to half its original size
            //SizeF factor = new SizeF(scaleFactor, scaleFactor);
            //this.Scale(factor);

            //Remove the weird default border that a TabControl keeps around its children tab pages
            tcInventoryWeapons.Region = new Region(new RectangleF(tbInventoryWeaponsPage1.Left, tbInventoryWeaponsPage1.Top, tbInventoryWeaponsPage1.Width, tbInventoryWeaponsPage1.Height));
            tcInventoryConsumables.Region = new Region(new RectangleF(tbInventoryConsumablesPage1.Left, tbInventoryConsumablesPage1.Top, tbInventoryConsumablesPage1.Width, tbInventoryConsumablesPage1.Height));
            tcInventoryDemonPowers.Region = new Region(new RectangleF(tbInventoryDemonPowersPage1.Left, tbInventoryDemonPowersPage1.Top, tbInventoryDemonPowersPage1.Width, tbInventoryDemonPowersPage1.Height));
            tcInventoryCards.Region = new Region(new RectangleF(tbInventoryCardsPage1.Left, tbInventoryCardsPage1.Top, tbInventoryCardsPage1.Width, tbInventoryCardsPage1.Height));
            tcInventoryOther.Region = new Region(new RectangleF(tbInventoryOtherPage1.Left, tbInventoryOtherPage1.Top, tbInventoryOtherPage1.Width, tbInventoryOtherPage1.Height));

            //START Temporary OnLoad logic
            attackTags1 = new Tags.WeaponTags.AttackTags("Sword Hack", "..\\..\\images\\attacks\\Sword_Hack.png", "..\\..\\images\\attacks\\Sword_Hack_HoverText.png");
            attackTags2 = new Tags.WeaponTags.AttackTags("Slash", "..\\..\\images\\attacks\\Sword_Slash.png", "..\\..\\images\\attacks\\Sword_Slash_HoverText.png");
            attackTags3 = new Tags.WeaponTags.AttackTags("Dash", "..\\..\\images\\attacks\\Sword_Dash.png", "..\\..\\images\\attacks\\Sword_Dash_HoverText.png");            
            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Sword, Tags.WeaponTags.WeaponDistance.Melee, 44, 75, 0, 35, 100, attackTags1, attackTags2, attackTags3);

            btnInventoryWeapons00.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Legendary, "Storm", "Attack speed increased by 25%" + Environment.NewLine + "Critical chance increased by 15%" + Environment.NewLine + "Critical hits create ball lightnings", "sword.png", weaponTags);
            btnInventoryWeapons00.BackgroundImage = Image.FromFile("..\\..\\images\\weapons\\sword.png");

            attackTags1 = new Tags.WeaponTags.AttackTags("Reap", "..\\..\\images\\attacks\\Scythe_Reap.png", "..\\..\\images\\attacks\\Scythe_Reap_HoverText.png");
            attackTags2 = new Tags.WeaponTags.AttackTags("Shockwave", "..\\..\\images\\attacks\\Scythe_Shockwave.png", "..\\..\\images\\attacks\\Scythe_Shockwave_HoverText.png");
            attackTags3 = new Tags.WeaponTags.AttackTags("Whirlwind", "..\\..\\images\\attacks\\Scythe_Whirlwind.png", "..\\..\\images\\attacks\\Scythe_Whirlwind_HoverText.png");
            weaponTags = new Tags.WeaponTags(Tags.WeaponTags.WeaponType.Scythe, Tags.WeaponTags.WeaponDistance.Melee, 10, 190, 10, 15, 100, attackTags1, attackTags2, attackTags3);
            btnInventoryWeapons10.Tag = FillItemTags(Tags.ItemType.Weapon, Tags.RarityType.Rare, "Vengeance", "Damage increased by 32% when health is below 50%" + Environment.NewLine + "Gain 10.0% of max health on crit (5 sec. cooldown)" + Environment.NewLine + "Triggers a Meteor storm when your health drops below 40%. Cannot trigger more than once every 60 seconds", "scythe.png", weaponTags);
            btnInventoryWeapons10.BackgroundImage = Image.FromFile("..\\..\\images\\weapons\\scythe.png");

            cardTags = new Tags.CardTags(2);
            cardTags.prefix = new Affix("The Warrior", Affix.Modifier.Damage, 10, "+#% Damage");
            btnInventoryCards00.Tag = FillItemTags(Tags.ItemType.Card, Tags.RarityType.Common, "The Sun", "", "thesun2.png" , cardTags);
            btnInventoryCards00.BackgroundImage = Image.FromFile("..\\..\\images\\cards\\thesun2.png");

            // Inventory page display
            lblInventoryHeader.Text = "Destiny Cards";
            tcInventoryWeapons.Visible = false;
            tcInventoryCards.Visible = false;
            tcInventoryOther.Visible = true;
            pbIconWeapons.Visible = true;
            pbIconWeaponsHighlighted.Visible = false;
            pbIconConsumables.Visible = true;
            pbIconConsumablesHighlighted.Visible = false;
            pbIconDemonPowers.Visible = true;
            pbIconDemonPowersHighlighted.Visible = false;
            pbIconCardsHighlighted.Visible = false;
            pbIconOther.Visible = false;
            pbIconOtherHighlighted.Visible = true;

            btnEquippedWeapon.Tag = btnInventoryWeapons00.Tag;
            btnEquippedWeapon.BackgroundImage = btnInventoryWeapons00.BackgroundImage;
            CalculateStats((Tags)btnEquippedWeapon.Tag);

            btnEquippedWeaponSecondary.Tag = btnInventoryWeapons10.Tag;
            btnEquippedWeaponSecondary.BackgroundImage = btnInventoryWeapons10.BackgroundImage;
            CalculateStats((Tags)btnEquippedWeaponSecondary.Tag, true);

            lblEquippedDestinyPoints.Text = totalCardPoints.ToString() + "/" + maximumCardPoints.ToString();
            //END Temporary OnLoad logic
        }

        //Calculate and update stats along top bar
        private void CalculateStats(Tags slotTags, bool secondarySlot = false)
        {
            //Determine what type of item was changed that prompted a recalc of stats (ie. weapon, card, etc)
            if (slotTags.weaponTags != null)
            {
                CalculateStatsFromWeaponChange(slotTags, secondarySlot);

                //With the new weapon in place, add on the card mods
                CalculateStatsFromEquippedCards();

                //Recalculate all weapon attacks incase we changed a weapon/card/modifiers that affect each attack's damages
                CalculateWeaponSkills(slotTags);
            }
            else if (slotTags.cardTags != null && (btnEquippedWeapon.Tag != null || btnEquippedWeaponSecondary.Tag != null))
            {
                //We added or removed a card and we have at least one weapon equipped, so recalc stats considering all cards
                CalculateStatsFromEquippedCards();

                //Recalculate all weapon attacks incase we changed a weapon/card/modifiers that affect each attack's damages
                CalculateWeaponSkills((Tags)btnEquippedWeapon.Tag);
                CalculateWeaponSkills((Tags)btnEquippedWeaponSecondary.Tag);
            }
            else if (slotTags.outfitTags != null)
            {
                CalculateStatsFromEquippedCards();

                CalculateStatsFromOutfit(slotTags);
            }
        }

        private void CalculateStatsFromWeaponChange(Tags slotTags, bool secondarySlot)
        {
            //A weapon was changed, reset the base values and then re-calculate the stats
            if (secondarySlot)
            {
                BaseDmgMinSecondary = slotTags.weaponTags.dmgMin;
                BaseDmgMaxSecondary = slotTags.weaponTags.dmgMax;
                BaseArmorPenetrationSecondary = slotTags.weaponTags.armorPenetration;
                BaseCritChanceSecondary = slotTags.weaponTags.critChance;
                BaseCritMultiSecondary = slotTags.weaponTags.critMulti;
            }
            else
            {
                BaseDmgMin = slotTags.weaponTags.dmgMin;
                BaseDmgMax = slotTags.weaponTags.dmgMax;
                BaseArmorPenetration = slotTags.weaponTags.armorPenetration;
                BaseCritChance = slotTags.weaponTags.critChance;
                BaseCritMulti = slotTags.weaponTags.critMulti;
            }

            UpdateStatLabels();
        }

        private void CalculateStatsFromEquippedCards()
        { 
            //Default the running total modifiers before we re-calc them
            modifierIncDamage = 0;
            modifierIncMeleeDamage = 0;
            modifierIncRangedDamage = 0;

            modifierFlatHealthCards = 0;
            modifierFlatArmorCards = 0;
            modifierFlatArmorPenetration = 0;
            modifierFlatArmorPenetrationSecondary = 0;
            modifierFlatCritChance = 0;
            modifierFlatCritChanceSecondary = 0;
            modifierFlatCritMulti = 0;
            modifierFlatCritMultiSecondary = 0;  
            
            totalCardPoints = 0;

            //Loop through all cards
            foreach (Button equippedCard in pnlEquippedCards.Controls)
            {
                Tags slotTags = (Tags)equippedCard.Tag;

                //Ensure this slot has a card in it
                if (slotTags != null)
                {
                    //Keep a running total of the currently used Destiny Points
                    totalCardPoints += slotTags.cardTags.points;

                    //Add the prefix and suffix, which may contain modifiers, to a collection to iterate over
                    Affix[] affixes = new Affix[] { slotTags.cardTags.prefix, slotTags.cardTags.suffix};

                    foreach (Affix affix in affixes)
                    {
                        //Not all cards have a prefix and a suffix
                        if (affix != null)
                        {
                            //Take the mod from the card and add it to the running total modifier for calculating the stats at the end of this loop
                            switch (affix.modifier)
                            {
                                case Affix.Modifier.Health:
                                    modifierFlatHealthCards += affix.value;
                                    break;

                                case Affix.Modifier.Armor:
                                    modifierFlatArmorCards += affix.value;
                                    break;

                                case Affix.Modifier.ArmorPenetration:
                                    if (btnEquippedWeapon.Tag != null)
                                    {
                                        modifierFlatArmorPenetration += affix.value;
                                    }
                                    if (btnEquippedWeaponSecondary.Tag != null)
                                    {
                                        modifierFlatArmorPenetrationSecondary += affix.value;
                                    }
                                    break;

                                case Affix.Modifier.CritChance:
                                    if (btnEquippedWeapon.Tag != null)
                                    {
                                        modifierFlatCritChance += affix.value;
                                    }
                                    if (btnEquippedWeaponSecondary.Tag != null)
                                    {
                                        modifierFlatCritChanceSecondary += affix.value;
                                    }
                                    break;

                                case Affix.Modifier.CritMulti:
                                    if (btnEquippedWeapon.Tag != null)
                                    {
                                        modifierFlatCritMulti += affix.value;
                                    }
                                    if (btnEquippedWeaponSecondary.Tag != null)
                                    {
                                        modifierFlatCritMultiSecondary += affix.value;
                                    }
                                    break;

                                case Affix.Modifier.Damage:
                                    modifierIncDamage += affix.value;
                                    break;

                                case Affix.Modifier.MeleeDamage:
                                    modifierIncMeleeDamage += affix.value;
                                    break;

                                case Affix.Modifier.RangedDamage:
                                    modifierIncRangedDamage += affix.value;
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            //Now we have all the total modifiers; update all stat and skill labels to reflect their additions
            UpdateStatLabels();
            UpdateSkillLabels();
            lblEquippedDestinyPoints.Text = totalCardPoints.ToString() + "/" + maximumCardPoints.ToString();
        }

        private void CalculateStatsFromOutfit(Tags slotTags)
        {
            modifierFlatHealthOutfit = 0;
            modifierFlatArmorOutfit = 0;

            //Outfits can add armor, health or crit chance; tack them onto the totals
            modifierFlatArmorOutfit += slotTags.outfitTags.armor;
            modifierFlatHealthOutfit += slotTags.outfitTags.health;
            modifierFlatCritChance += slotTags.outfitTags.critChance;
            modifierFlatCritChanceSecondary += slotTags.outfitTags.critChance;

            UpdateStatLabels();
        }

        private void CalculateWeaponSkills(Tags slotTags)
        {
            //Prefix the attack image urls with the full relative filepath
            slotTags.weaponTags.attack1.attackImageURL = urlAttacks + slotTags.weaponTags.attack1.attackImageURL;
            slotTags.weaponTags.attack1.attackImageHoverTextURL = urlAttacks + slotTags.weaponTags.attack1.attackImageHoverTextURL;

            //We have our weapon base mods set  (ie. dmg, armor pen, crit, etc)
            // and we have our modifiers from cards set
            // plug those into the calculations based on the calc used for attack1/attack2/attack3 (need to store these in db i think...)
            switch (slotTags.weaponTags.weaponType)
            {
                case Tags.WeaponTags.WeaponType.Hammer:
                    //Pound (weapon base damage * cards)
                    //121-161 --> 121-161
                    slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Crush (weapon base damage * cards * 5) //TODO Cards before or after the * 5?
                    //121-161 --> 605-805
                    //131-175 --> 655-875
                    slotTags.weaponTags.attack2.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);
                    slotTags.weaponTags.attack2.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);

                    //Smash (weapon base damage * cards * 2.5) //TODO Cards before or after the * 2.5?
                    //121-161 --> 302-402
                    //131-175 --> 327-437
                    slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.496);
                    slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.497);
                    break;
                case Tags.WeaponTags.WeaponType.HandMortar:
                    //Bouncing Betty (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    //slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Fire Lake (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack2.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);
                    //slotTags.weaponTags.attack2.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);

                    //Explosive Jump (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.496);
                    //slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.497);
                    break;
                case Tags.WeaponTags.WeaponType.Shotgun:
                    //Fire (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    //slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Aimed Shot (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack2.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);
                    //slotTags.weaponTags.attack2.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);

                    //Point-Blank Shot (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.496);
                    //slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.497);
                    break;
                case Tags.WeaponTags.WeaponType.LightningGun:
                    //Shock (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    //slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Ball Lightning (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack2.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);
                    //slotTags.weaponTags.attack2.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);

                    //Lightning Trap (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.496);
                    //slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.497);
                    break;
                case Tags.WeaponTags.WeaponType.Rapier:
                    //Flurry (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    //slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Charge (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack2.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);
                    //slotTags.weaponTags.attack2.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);

                    //Coup De Grace (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.496);
                    //slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.497);
                    break;
                case Tags.WeaponTags.WeaponType.Sword:
                    //Sword Hack (weapon base damage * cards)
                    //46-84 --> 46-84
                    slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Slash (weapon base damage * cards * 4) //TODO Cards before or after the * 4?
                    //46-84  --> 184-344
                    //57-108 --> 228-432
                    slotTags.weaponTags.attack2.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 4);
                    slotTags.weaponTags.attack2.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 4);

                    //Dash (weapon base damage * cards * 2) //TODO Cards before or after the * 2?
                    //46-84  --> 92-172
                    //57-108 --> 114-216
                    slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2);
                    slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2);
                    break;
                case Tags.WeaponTags.WeaponType.Scythe:
                    //Reap (weapon base damage * cards)
                    //20-203 --> 20-203
                    slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Shockwave (no damage)
                    slotTags.weaponTags.attack2.attackDmgMin = 0;
                    slotTags.weaponTags.attack2.attackDmgMax = 0;

                    //Whirlwind (weapon base damage * cards)
                    //20-203 --> 20-203
                    //16-161 --> 16-161
                    slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    break;
                case Tags.WeaponTags.WeaponType.Tome:
                    //Magic Missiles (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack1.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));
                    //slotTags.weaponTags.attack1.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0));

                    //Dimension Wave (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack2.attackDmgMin = (int)Math.Round(slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);
                    //slotTags.weaponTags.attack2.attackDmgMax = (int)Math.Round(slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0) * 5);

                    //Singularity Orb (weapon base damage * cards * ?)
                    //?-? --> ?-?
                    //slotTags.weaponTags.attack3.attackDmgMin = (int)Math.Round((slotTags.weaponTags.dmgMin * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.496);
                    //slotTags.weaponTags.attack3.attackDmgMax = (int)Math.Round((slotTags.weaponTags.dmgMax * (1 + (modifierIncDamage + modifierIncMeleeDamage) / 100.0)) * 2.497);
                    break;
                default:
                    break;
            }

            //Update the labels that display these values beside each equipped weapon slot
            UpdateSkillLabels();
        }

        private void UpdateStatLabels()
        {
            lblHealth.Text = string.Format("{0:n0}", BaseHealth + modifierFlatHealthCards + modifierFlatHealthOutfit);
            lblArmor.Text = string.Format("{0:n0}", BaseArmor + modifierFlatArmorCards + modifierFlatArmorOutfit);

            if (btnEquippedWeapon.Tag != null)
            {
                //Add on the proper damage modifier(s) depending on the weapon type (ie. melee vs. ranged)
                if (((Tags)btnEquippedWeapon.Tag).weaponTags.weaponDistance == Tags.WeaponTags.WeaponDistance.Melee)
                {
                    lblDamage.Text = Math.Round(BaseDmgMin * (1 + ((modifierIncDamage + modifierIncMeleeDamage) / 100.0))).ToString() + "-" + Math.Round(BaseDmgMax * (1 + ((modifierIncDamage + modifierIncMeleeDamage) / 100.0))).ToString();
                }
                if (((Tags)btnEquippedWeapon.Tag).weaponTags.weaponDistance == Tags.WeaponTags.WeaponDistance.Ranged)
                {
                    lblDamage.Text = Math.Round(BaseDmgMin * (1 + ((modifierIncDamage + modifierIncRangedDamage) / 100.0))).ToString() + "-" + Math.Round(BaseDmgMax * (1 + ((modifierIncDamage + modifierIncRangedDamage) / 100.0))).ToString();
                }

                lblArmorPenetration.Text = (BaseArmorPenetration + modifierFlatArmorPenetration).ToString();
                lblCritChance.Text = (BaseCritChance + modifierFlatCritChance).ToString() + "%";
                lblCritMulti.Text = (BaseCritMulti + modifierFlatCritMulti).ToString() + "%";
            }
            
            if (btnEquippedWeaponSecondary.Tag != null)
            {
                //Add on the proper damage modifier(s) depending on the weapon type (ie. melee vs. ranged)
                if (((Tags)btnEquippedWeaponSecondary.Tag).weaponTags.weaponDistance == Tags.WeaponTags.WeaponDistance.Melee)
                {
                    lblDamageSecondary.Text = Math.Round(BaseDmgMinSecondary * (1 + ((modifierIncDamage + modifierIncMeleeDamage) / 100.0))).ToString() + "-" + Math.Round(BaseDmgMaxSecondary * (1 + ((modifierIncDamage + modifierIncMeleeDamage) / 100.0))).ToString();
                }
                if (((Tags)btnEquippedWeaponSecondary.Tag).weaponTags.weaponDistance == Tags.WeaponTags.WeaponDistance.Ranged)
                {
                    lblDamageSecondary.Text = Math.Round(BaseDmgMinSecondary * (1 + ((modifierIncDamage + modifierIncRangedDamage) / 100.0))).ToString() + "-" + Math.Round(BaseDmgMaxSecondary * (1 + ((modifierIncDamage + modifierIncRangedDamage) / 100.0))).ToString();
                }

                lblArmorPenetrationSecondary.Text = (BaseArmorPenetrationSecondary + modifierFlatArmorPenetrationSecondary).ToString();
                lblCritChanceSecondary.Text = (BaseCritChanceSecondary + modifierFlatCritChanceSecondary).ToString() + "%";
                lblCritMultiSecondary.Text = (BaseCritMultiSecondary + modifierFlatCritMultiSecondary).ToString() + "%";
            }
        }

        private void UpdateSkillLabels()
        {
            Tags slotTags;

            //Show the attack damages on their corresponding labels
            if (btnEquippedWeapon.Tag != null)
            {
                slotTags = (Tags)btnEquippedWeapon.Tag;

                lblAttackStats1.Text = slotTags.weaponTags.attack1.attackName + ": " + slotTags.weaponTags.attack1.attackDmgMin + "-" + slotTags.weaponTags.attack1.attackDmgMax;
                lblAttackStats2.Text = slotTags.weaponTags.attack2.attackName + ": " + slotTags.weaponTags.attack2.attackDmgMin + "-" + slotTags.weaponTags.attack2.attackDmgMax;
                lblAttackStats3.Text = slotTags.weaponTags.attack3.attackName + ": " + slotTags.weaponTags.attack3.attackDmgMin + "-" + slotTags.weaponTags.attack3.attackDmgMax;
            }

            if (btnEquippedWeaponSecondary.Tag != null)
            {
                slotTags = (Tags)btnEquippedWeaponSecondary.Tag;

                lblAttackStatsSecondary1.Text = slotTags.weaponTags.attack1.attackName + ": " + slotTags.weaponTags.attack1.attackDmgMin + "-" + slotTags.weaponTags.attack1.attackDmgMax;
                lblAttackStatsSecondary2.Text = slotTags.weaponTags.attack2.attackName + ": " + slotTags.weaponTags.attack2.attackDmgMin + "-" + slotTags.weaponTags.attack2.attackDmgMax;
                lblAttackStatsSecondary3.Text = slotTags.weaponTags.attack3.attackName + ": " + slotTags.weaponTags.attack3.attackDmgMin + "-" + slotTags.weaponTags.attack3.attackDmgMax;
            }
        }

        //Used for all Inventory slot controls to copy the item clicked by the user and move it to an equipped slot
        private void SelectItem(Button slot, MouseEventArgs e)
        {
            bool itemEquipped = false;

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
                    //EquipConsumable(slot, slotTags, secondarySlot, ref itemEquipped);
                    break;
                case Tags.ItemType.DemonPower:
                    //EquipDemonPower(slot, slotTags, secondarySlot, ref itemEquipped);
                    break;
                case Tags.ItemType.Empty:
                    break;
                case Tags.ItemType.Outfit:
                    EquipOutfit(slot, slotTags, ref itemEquipped);
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
                    //Only equip the card if we have enough available Destiny Points to do so
                    if ((totalCardPoints + slotTags.cardTags.points) <= maximumCardPoints)
                    {
                        //Empty card slot found; copy the card into this equippable card slot
                        equippedCard.BackgroundImage = Image.FromFile(urlCards + slotTags.image);
                        equippedCard.Tag = slotTags;

                        itemEquipped = true;
                        CalculateStats(slotTags);
                        break;
                    }
                    else
                    {
                        itemEquipped = false;
                        break;
                    }
                }
            }
        }

        private void EquipOutfit(Button slot, Tags slotTags, ref bool itemEquipped)
        {
            //Copy item to outfit slot
            this.BackgroundImage = Image.FromFile(urlBackgrounds + slotTags.outfitTags.urlOutfitBackgroundImage);
            btnEquippedOutfit.Tag = slotTags;
            itemEquipped = true;
            CalculateStats(slotTags);
        }

        private void EquipWeapon(Button slot, Tags slotTags, bool secondarySlot, ref bool itemEquipped)
        {
            if (secondarySlot)
            {
                //Copy item to secondary slot
                btnEquippedWeaponSecondary.BackgroundImage = Image.FromFile(urlWeapons + slotTags.image);
                btnEquippedWeaponSecondary.Tag = slotTags;

                //Show the attack icons for the new weapon
                btnAttackSecondary1.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack1.attackImageURL);
                btnAttackSecondary2.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack2.attackImageURL);
                btnAttackSecondary3.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack3.attackImageURL);

                itemEquipped = true;
                
                //Update the stats (ie. dmg, etc)
                CalculateStats(slotTags, secondarySlot);
                pnlEquippedWeaponAttacksSecondary.Visible = true;
            }
            else
            {
                //Copy item to primary slot
                btnEquippedWeapon.BackgroundImage = Image.FromFile(urlWeapons + slotTags.image);
                btnEquippedWeapon.Tag = slotTags;

                //Show the attack icons for the new weapon
                btnAttack1.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack1.attackImageURL);
                btnAttack2.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack2.attackImageURL);
                btnAttack3.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack3.attackImageURL);

                itemEquipped = true;
                CalculateStats(slotTags);

                //Show the attacks panel
                pnlEquippedWeaponAttacks.Visible = true;
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

        private void UnequipItem(Button slot, EventArgs e) //Button slot, bool secondarySlot)
        {
            bool itemUnequipped = false;
            Tags slotTags = (Tags)slot.Tag;

            switch (slotTags.itemType)
            {
                case Tags.ItemType.Card:
                    UnequipCard(slot, slotTags, ref itemUnequipped);
                    break;
                case Tags.ItemType.Consumable:
                    //UnequipConsumable(slot, slotTags, slot.Name.Contains("Secondary"), ref itemUnequipped);
                    break;
                case Tags.ItemType.DemonPower:
                    //UnequipDemonPower(slot, slotTags, slot.Name.Contains("Secondary"), ref itemUnequipped);
                    break;
                case Tags.ItemType.Empty:
                    break;
                case Tags.ItemType.Outfit:
                    break;
                case Tags.ItemType.Weapon:
                    UnequipWeapon(slot, slotTags, slot.Name.Contains("Secondary"), ref itemUnequipped);
                    break;
                default:
                    break;
            }
        }

        private void UnequipCard(Button slot, Tags slotTags, ref bool itemUnequipped)
        {
            //Remove the equipped card from the slot
            slot.BackgroundImage = null;
            slot.Tag = null;

            itemUnequipped = true;
            CalculateStats(slotTags);
        }

        private void UnequipWeapon(Button slot, Tags slotTags, bool secondarySlot, ref bool itemUnequipped)
        {
            //Remove the equipped weapon from the slot
            slot.BackgroundImage = null;
            slot.Tag = null;
            itemUnequipped = true;

            if (secondarySlot)
            {
                pnlEquippedWeaponAttacksSecondary.Visible = false;
            }
            else
            {
                pnlEquippedWeaponAttacks.Visible = false;
            }

            ClearWeaponSlot(secondarySlot);
        }

        private void ClearWeaponSlot(bool secondarySlot)
        {
            //A weapon was unequipped, clear the stats
            if (secondarySlot)
            {
                //lblHealth.Text = //not affected by weapons
                lblDamageSecondary.Text = "0-0";
                //lblArmor.Text = //not affected by weapons
                lblArmorPenetrationSecondary.Text = "0";
                lblCritChanceSecondary.Text = "0%";
                lblCritMultiSecondary.Text = "0%";
            }
            else
            {
                //lblHealth.Text = //not affected by weapons
                lblDamage.Text = "0-0";
                //lblArmor.Text = //not affected by weapons
                lblArmorPenetration.Text = "0";
                lblCritChance.Text = "0%";
                lblCritMulti.Text = "0%";
            }

            //ClearWeaponSkills(secondarySlot);
        }

        #endregion
        /********************************
         * END Unequip item logic *
         * ******************************/

        //Subproc called from mouse event for all Inventory slots to set a border around the currently hovered slot to highlight it
        private void HighlightSlot(Button slot, Tags slotTags)
        {
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
        private void UnhighlightSlot(Button slot)
        {            
            slot.FlatAppearance.BorderSize = 1;
            slot.FlatAppearance.BorderColor = Color.Black;
        }

        private void EquippedItem_MouseUp(object sender, MouseEventArgs e)
        {
            Button slot = (Button)sender;

            if (slot.Tag != null)
            {
                UnequipItem(slot, e);
            }
        }

        private void Inventory_MouseUp(object sender, MouseEventArgs e)
        {
            Button slot = (Button)sender;

            if (slot.Tag != null)
            {
                SelectItem(slot, e);
            }
        }

        //Will call a subproc to highlight the item being hovered and show hover text
        private void Inventory_MouseHover(object sender, EventArgs e)
        {
            Button slot = (Button)sender;
            Tags slotTags = (Tags)slot.Tag;

            HighlightSlot(slot, slotTags);
            
            //Adjust hover text visibility and info if the hovered slot actually contains an item
            if (slotTags != null)
            {
                SetHoverTextVisibility(slot, slotTags, true);
            }
        }

        //Will call a subproc to unhighlight the item that was being hovered and hide hover text
        private void Inventory_MouseLeave(object sender, EventArgs e)
        {
            Button slot = (Button)sender;
            Tags slotTags = (Tags)slot.Tag;

            UnhighlightSlot(slot);

            //Hide the hover text
            SetHoverTextVisibility(slot, slotTags, false);
        }

        private void SetHoverTextVisibility(Button slot, Tags slotTags, bool visibility)
        {
            //Hide all panels; we'll conditionally show the correct one after this
            pnlHoverTextWeapon.Visible = false;
            pnlHoverTextCard.Visible = false;
            pnlHoverTextOutfit.Visible = false;

            if (visibility)
            {
                //Set the appropriate visibility and text of the labels inside the hover text panel based on the item type
                switch (slotTags.itemType)
                {
                    case Tags.ItemType.Card:
                        //TEXT
                        //LINE 1 (name)
                        lblHoverTextCardName.Text = slotTags.name;
                        //LINE 2 (stats)
                        lblHoverTextCardPoints.Text = slotTags.cardTags.points.ToString();
                        //LINE 3 (card mod description)
                        lblHoverTextCardDescription.Text = slotTags.description;

                        pnlHoverTextCard.Visible = true;
                        break;
                    //case Tags.ItemType.Consumable:
                    //    break;
                    //case Tags.ItemType.DemonPower:
                    //    break;
                    //case Tags.ItemType.Empty:
                    //    break;
                    case Tags.ItemType.Outfit:
                        //TEXT
                        //LINE 1 (name)
                        lblHoverTextOutfitName.Text = slotTags.name;
                        //LINE 2 (stats)
                        lblHoverTextOutfitArmor.Text = slotTags.outfitTags.armor.ToString();
                        //LINE 3 (weapon text/description eg. prefix/suffix)
                        lblHoverTextOutfitDescription.Text = slotTags.description;

                        pnlHoverTextOutfit.Visible = true;
                        break;
                    case Tags.ItemType.Weapon:
                        //TEXT
                        //LINE 1 (name)
                        lblHoverTextWeaponName.Text = slotTags.name;
                        //LINE 2 (stats)
                        lblHoverTextWeaponDamage.Text = slotTags.weaponTags.dmgMin.ToString() + "-" + slotTags.weaponTags.dmgMax.ToString();
                        lblHoverTextWeaponArmorPenetration.Text = slotTags.weaponTags.armorPenetration.ToString();
                        lblHoverTextWeaponCritChance.Text = slotTags.weaponTags.critChance.ToString() + "%";
                        lblHoverTextWeaponCritMulti.Text = slotTags.weaponTags.critMulti.ToString() + "%";
                        //LINE 3 (weapon text/description eg. prefix/suffix)
                        lblHoverTextWeaponDescription.Text = slotTags.description;

                        pnlHoverTextWeapon.Visible = true;
                        break;
                    default:
                        break;
                }
            }
        }

        //Will call a subproc to highlight the item being hovered and show hover text
        private void Attack_MouseHover(object sender, EventArgs e)
        {
            Tags slotTags;
            Button slot = (Button)sender;

            if (slot.Name.Contains("Secondary"))
            {
                slotTags = (Tags)btnEquippedWeaponSecondary.Tag;
                int position = Convert.ToInt32(slot.Name.Substring(slot.Name.Length - 1));

                //Adjust hover text visibility and info
                switch (position)
                {
                    case 1:
                        pnlAttackSecondaryHoverText.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack1.attackImageHoverTextURL);
                        pnlAttackSecondaryHoverText.Visible = true;
                        break;
                    case 2:
                        pnlAttackSecondaryHoverText.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack2.attackImageHoverTextURL);
                        pnlAttackSecondaryHoverText.Visible = true;
                        break;
                    case 3:
                        pnlAttackSecondaryHoverText.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack3.attackImageHoverTextURL);
                        pnlAttackSecondaryHoverText.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                slotTags = (Tags)btnEquippedWeapon.Tag;
                int position = Convert.ToInt32(slot.Name.Substring(slot.Name.Length - 1));

                //Adjust hover text visibility and info
                switch (position)
                {
                    case 1:
                        pnlAttackHoverText.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack1.attackImageHoverTextURL);
                        pnlAttackHoverText.Visible = true;
                        break;
                    case 2:
                        pnlAttackHoverText.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack2.attackImageHoverTextURL);
                        pnlAttackHoverText.Visible = true;
                        break;
                    case 3:
                        pnlAttackHoverText.BackgroundImage = Image.FromFile(urlAttacks + slotTags.weaponTags.attack3.attackImageHoverTextURL);
                        pnlAttackHoverText.Visible = true;
                        break;
                    default:
                        break;
                }
            }

        }

        private void Attack_MouseLeave(object sender, EventArgs e)
        {
            Button slot = (Button)sender;

            if (slot.Name.Contains("Secondary"))
            {
                pnlAttackSecondaryHoverText.Visible = false;
            }
            else
            {
                pnlAttackHoverText.Visible = false;
            }
        }

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType, string aName, string aDescription)
        {
            return new Tags(aItemType, aRarityType, aName, aDescription);
        }

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType, string aName, string aDescription, string aImage,
                                  Tags.WeaponTags.WeaponType aWeaponType)
        {
            Tags.WeaponTags weaponTags = new Tags.WeaponTags(aWeaponType);
            return new Tags(aItemType, aRarityType, aName, aDescription, aImage, weaponTags);
        }

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType, string aName, string aDescription, string aImage,
                                  Tags.WeaponTags aWeaponTags)
        {
            return new Tags(aItemType, aRarityType, aName, aDescription, aImage, aWeaponTags);
        }

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType, string aName, string aDescription, string aImage,
                                  Tags.CardTags aCardTags)
        {
            return new Tags(aItemType, aRarityType, aName, aDescription, aImage, aCardTags);
        }

        private Tags FillItemTags(Tags.ItemType aItemType, Tags.RarityType aRarityType, string aName, string aDescription, Tags.OutfitTags aOutfitTags)
        {
            return new Tags(aItemType, aRarityType, aName, aDescription, aOutfitTags);
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
            //tcInventoryConsumables.Visible = false;
            //tcInventoryDemonPowers.Visible = false;
            tcInventoryCards.Visible = false;
            tcInventoryOther.Visible = false;

            //create multiple panels and hide/show the selected one
            switch (pageName)
            {
                case "weapons":
                    tcInventoryWeapons.Visible = true;
                    break;

                case "cards":
                    tcInventoryCards.Visible = true;
                    break;

                case "other":
                    tcInventoryOther.Visible = true;
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
            pbIconConsumablesHighlighted.Visible = false;
            pbIconDemonPowers.Visible = false;
            pbIconDemonPowersHighlighted.Visible = false;
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
                    pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;
                    pbIconOther.Visible = true;

                    lblInventoryHeader.Text = "Weapons";
                    break;
                case "consumables":
                    pbIconConsumablesHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;
                    pbIconOther.Visible = true;

                    lblInventoryHeader.Text = "Consumables";
                    break;
                case "demonPowers":
                    pbIconDemonPowersHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    pbIconConsumables.Visible = true;
                    pbIconCards.Visible = true;
                    pbIconOther.Visible = true;

					lblInventoryHeader.Text = "Demon Powers";
                    break;
                case "cards":
                    pbIconCardsHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    pbIconConsumables.Visible = true;
                    pbIconDemonPowers.Visible = true;
                    pbIconOther.Visible = true;

                    lblInventoryHeader.Text = "Destiny Cards";
                    break;
                case "other":
                    pbIconOtherHighlighted.Visible = true;
                    pbIconWeapons.Visible = true;
                    pbIconConsumables.Visible = true;
                    pbIconDemonPowers.Visible = true;
                    pbIconCards.Visible = true;

                    lblInventoryHeader.Text = "Other";
                    break;
                default:
                    break;
            }
            
        }

        private void PreloadOutfits()
        {
            outfitTags = new Tags.OutfitTags(30, "adventurer.png");
            btnInventoryOther00.Tag = FillItemTags(Tags.ItemType.Outfit, Tags.RarityType.Legendary, "Adventurer's Outfit", "Increases the Destiny slots by 1.  Increases maximum Overdrive by 2,000.  Increases Overdrive gain by 30%.", outfitTags);
            btnInventoryOther00.BackgroundImage = Image.FromFile(urlOutfits + "adventurer.png");

            outfitTags = new Tags.OutfitTags(50, "cavalier.png");
            btnInventoryOther10.Tag = FillItemTags(Tags.ItemType.Outfit, Tags.RarityType.Legendary, "Cavalier's Outfit", "You gain 180 Overdrive every second, but attacks no longer grant Overdrive.", outfitTags);
            btnInventoryOther10.BackgroundImage = Image.FromFile(urlOutfits + "cavalier.png");

            outfitTags = new Tags.OutfitTags(70, "highlander.png");
            btnInventoryOther20.Tag = FillItemTags(Tags.ItemType.Outfit, Tags.RarityType.Legendary, "Highlander's Outfit", "You gain 300 Overdrive when you use a weapon special attack, but attacks no longer grant Overdrive." + Environment.NewLine + "Reduces weapon cooldowns by 15%.", outfitTags);
            btnInventoryOther20.BackgroundImage = Image.FromFile(urlOutfits + "highlander.png");

            outfitTags = new Tags.OutfitTags(80, "hunter.png");
            btnInventoryOther30.Tag = FillItemTags(Tags.ItemType.Outfit, Tags.RarityType.Legendary, "Hunter's Outfit", "Gain 200% of the damage taken as Overdrive.", outfitTags);
            btnInventoryOther30.BackgroundImage = Image.FromFile(urlOutfits + "hunter.png");

            //outfitTags = new Tags.OutfitTags(150, "vanguard.png");
            //btnInventoryOther30.Tag = FillItemTags(Tags.ItemType.Outfit, Tags.RarityType.Legendary, "Vanguard's Outfit", "High Armor.  Increases maximum Overdrive by 2,000.  Overdrive doesn't diminish outside of combat but Overdrive gain is decreased by 10%.", outfitTags);
            //btnInventoryOther30.BackgroundImage = Image.FromFile(urlOutfits + "vanguard.png");

            outfitTags = new Tags.OutfitTags(70, "vigilante.png");
            outfitTags.critChance = 10;
            btnInventoryOther40.Tag = FillItemTags(Tags.ItemType.Outfit, Tags.RarityType.Legendary, "Vigilante's Outfit", "Critical hits grant 300% more Overdrive.  Normal attacks no longer grant Overdrive.", outfitTags);
            btnInventoryOther40.BackgroundImage = Image.FromFile(urlOutfits + "vigilante.png");

            outfitTags = new Tags.OutfitTags(100, "zealot.png");
            outfitTags.health = 1000;
            btnInventoryOther01.Tag = FillItemTags(Tags.ItemType.Outfit, Tags.RarityType.Legendary, "Zealot's Outfit", "High Armor.  Increases Health by 1,000.", outfitTags);
            btnInventoryOther01.BackgroundImage = Image.FromFile(urlOutfits + "zealot.png");
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

        private int DmgMinSecondaryAsNumber()
        {
            return Convert.ToInt32(lblDamageSecondary.Text.Substring(0, lblDamageSecondary.Text.IndexOf("-")));
        }

        private int DmgMaxAsNumber()
        {
            return Convert.ToInt32(lblDamage.Text.Substring(lblDamage.Text.IndexOf("-") + 1));
        }

        private int DmgMaxSecondaryAsNumber()
        {
            return Convert.ToInt32(lblDamageSecondary.Text.Substring(lblDamageSecondary.Text.IndexOf("-") + 1));
        }

        private int ArmorAsNumber()
        {
            return Convert.ToInt32(lblArmor.Text);
        }

        private int ArmorPenetrationAsNumber()
        {
            return Convert.ToInt32(lblArmorPenetration.Text);
        }

        private int ArmorPenetrationSecondaryAsNumber()
        {
            return Convert.ToInt32(lblArmorPenetrationSecondary.Text);
        }

        private int CritChanceAsNumber()
        {
            return Convert.ToInt32(lblCritChance.Text.Substring(0, lblCritChance.Text.Length - 1));
        }

        private int CritChanceSecondaryAsNumber()
        {
            return Convert.ToInt32(lblCritChanceSecondary.Text.Substring(0, lblCritChanceSecondary.Text.Length - 1));
        }

        private int CritMultiAsNumber()
        {
            return Convert.ToInt32(lblCritMulti.Text.Substring(0, lblCritMulti.Text.Length - 1));
        }

        private int CritMultiSecondaryAsNumber()
        {
            return Convert.ToInt32(lblCritMultiSecondary.Text.Substring(0, lblCritMultiSecondary.Text.Length - 1));
        }

        #endregion

        /***************************
         *  END Stats as numbers *
         *  ************************/    

        private void btnCreateItem_Click(object sender, EventArgs e)
        {
            bool itemCreated = false;

            //Create and show the CreateItem form
            using (CreateItemForm frmCreateItem = new CreateItemForm())
            {
                itemCreated = (frmCreateItem.ShowDialog() == DialogResult.OK);

                if (itemCreated)
                {
                    //We created an item, so grab its values
                    newItemTags = frmCreateItem.newItemTags;

					//Add the new item into the inventory (ie. Weapon, Card, etc)
                    AddNewItemToInventory();
                }
            }
        }

        private void AddNewItemToInventory()
        {
            TabControl inventoryCategory;
            TabPage inventory;
            TableLayoutPanel inventorySlots;
            bool itemLoaded = false;
            string urlFilePath = string.Empty;

            //Determine which item category we created a new item for
            switch (newItemTags.itemType)
            {
                case Tags.ItemType.Card:
                    inventoryCategory = tcInventoryCards;
                    urlFilePath = urlCards;
                    break;
                //case Tags.ItemType.Consumable:
                //    break;
                //case Tags.ItemType.DemonPower:
                //    break;
                //case Tags.ItemType.Empty:
                //    break;
                //case Tags.ItemType.Outfit:
                //    break;
                case Tags.ItemType.Weapon:
                    inventoryCategory = tcInventoryWeapons;
                    urlFilePath = urlWeapons;
                    break;
                default:
                    throw new Exception("No item type found for " + newItemTags.itemType);
            }

            //Get the child controls of the inventory category we're going to add this new item into
            inventory = inventoryCategory.SelectedTab;
            inventorySlots = (TableLayoutPanel)inventory.Controls[inventory.Controls.Count - 1];

            if (inventorySlots.Controls.Count < 25)
            {
                //Create a new button and add it to the Inventory
                Button inventorySlot = CreateNewInventorySlot();

                //Assign the new item to the new inventory slot
                try
                {
                    inventorySlot.BackgroundImage = Image.FromFile(urlFilePath + newItemTags.image);
                }
                catch (FileNotFoundException e)
                {
                    inventorySlot.BackgroundImage = Image.FromFile(urlImageNotFound);
                }

                inventorySlot.Tag = newItemTags;

                inventorySlots.Controls.Add(inventorySlot);
                itemLoaded = true;
            }
            else
            {
                //We didn't find an empty slot, need to create a new TabPage with a TableLayoutPanel and a new button to hold this new item
                //TODO                
            }
        }

        private Button CreateNewInventorySlot()
        {
            Button inventorySlot = new Button();

            inventorySlot.BackColor = System.Drawing.Color.Transparent;
            inventorySlot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            inventorySlot.Cursor = System.Windows.Forms.Cursors.Default;
            inventorySlot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            inventorySlot.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            inventorySlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            inventorySlot.ForeColor = System.Drawing.SystemColors.ControlText;
            inventorySlot.Location = new System.Drawing.Point(3, 3);
            //inventorySlot.Name = "btnInventoryWeapons00";
            inventorySlot.Size = new System.Drawing.Size(61, 98);
            //inventorySlot.TabIndex = 1;
            inventorySlot.TabStop = false;
            inventorySlot.UseVisualStyleBackColor = false;
            inventorySlot.MouseLeave += new System.EventHandler(this.Inventory_MouseLeave);
            inventorySlot.MouseHover += new System.EventHandler(this.Inventory_MouseHover);
            inventorySlot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Inventory_MouseUp);

            return inventorySlot;
        }

        private void BuildListOfEquippedItems()
        {
            equippedItemControls = new List<Button>();

            foreach (Button equippedCard in pnlEquippedCards.Controls)
            {
                equippedItemControls.Add(equippedCard);
            }

            equippedItemControls.Add(btnEquippedOutfit);
            equippedItemControls.Add(btnEquippedWeapon);
            equippedItemControls.Add(btnEquippedWeaponSecondary);
            //equippedItemControls.Add(btnEquippedConsumable);
            //equippedItemControls.Add(btnEquippedConsumableSecondary);
            //equippedItemControls.Add(btnEquippedDemonPower);
            //equippedItemControls.Add(btnEquippedDemonPowerSecondary);
        }

        //Generate xml of the current build and open a Save File Dialog so the user can specify where to save their build
        private void btnSaveBuild_Click(object sender, EventArgs e)
        {
            //Open the file dialog to prompt the user where they want to save the build
            SaveFileDialog saveBuildDialog = new SaveFileDialog();
            saveBuildDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveBuildDialog.Title = "Save build...";

            if (saveBuildDialog.ShowDialog() == DialogResult.OK)
            {
                //Create objects that'll make up the xml of the build
                XmlDocument build = new XmlDocument();
                build.LoadXml("<Build></Build>");
                XmlElement root = build.DocumentElement;
                XmlElement items = build.CreateElement("Items");
                XmlElement item;
                XmlElement itemTags = build.CreateElement("ItemTags");
                Tags slotTags;

                //Add what will be the collection of Items underneath the root node of the xml document
                root.AppendChild(items);

                try
                {
                    foreach (Button equippedItemControl in equippedItemControls)  //TODO the last control is overwriting the previously written one
                    {
                        //Generate xml for the item if the current slot has an item equipped within it
                        if (equippedItemControl.Tag != null)
                        {
                            //Serialize the current item into an xml string to be appended to the build doc
                            slotTags = (Tags)equippedItemControl.Tag;
                            XmlSerializer serializer = new XmlSerializer(slotTags.GetType());

                            //Create a new item, so we don't overwrite the last one
                            item = build.CreateElement("Item");

							//Add the name of the control that this item belongs to for referencing when importing a build
                            item.InnerXml = "<Control>" + equippedItemControl.Name + "</Control>";

                            //Auto-generate the xml for the item by utilizing an XML serializer
                            using (StringWriter writer = new StringWriter())
                            {
                                serializer.Serialize(writer, equippedItemControl.Tag);
                                itemTags.InnerXml = writer.ToString();

                                item.AppendChild(itemTags.SelectSingleNode("Tags"));

                                //Add the item to the list of items in the build
                                items.AppendChild(item);
                            }
                        }
                    }
                }
                //if invalid cast, ignore and move on to the next control
                catch (Exception ex)
                {
                    MessageBox.Show("btnSaveBuild_Click threw an exception" + Environment.NewLine + ex.Message);
                    throw;
                }

                //Save the xml to the file specified by the user
                System.IO.File.WriteAllText(saveBuildDialog.FileName, build.OuterXml);
                MessageBox.Show("Build saved.");
            }            
        }

        private void btnLoadBuild_Click(object sender, EventArgs e)
        {
            Tags item;
            XmlDocument build;
            XmlNodeList items;
            Button slot;
            bool itemEquipped = false;
            Tags slotTags;

            //Open the file dialog to prompt the user where to pick their build file to load
            OpenFileDialog loadBuildDialog = new OpenFileDialog();
            loadBuildDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            loadBuildDialog.Title = "Load build...";

            if (loadBuildDialog.ShowDialog() == DialogResult.OK)
            {
                ClearEquippedItems();

                //Load the build into an xml document for further processing
                build = new XmlDocument();
                build.Load(loadBuildDialog.FileName);

                //Process all <item> nodes
                items = build.SelectNodes("Build/Items/Item");

                //Create an instance of the XmlSerializer specifying type and namespace
                XmlSerializer serializer = new XmlSerializer(typeof(Tags));

                //Loop through every item and load them into their corresponding control
                foreach (XmlNode importItem in items)
                {
                    //Deserialize the imported item in xml format to an object
                    item = (Tags)serializer.Deserialize(new XmlNodeReader(importItem.SelectSingleNode("Tags")));

                    //Assign the imported item to its corresponding control (setting the Tag and Image properties)   
                    slot = Controls.Find(importItem.SelectSingleNode("Control").InnerText, true).First() as Button;

                    switch (item.itemType)
                    {
                        case Tags.ItemType.Card:
                            EquipCard(slot, item, ref itemEquipped);
                            break;
                        //case Tags.ItemType.Consumable:
                        //    slot.BackgroundImage = Image.FromFile(urlConsumables + item.image);
                        //    break;
                        //case Tags.ItemType.DemonPower:
                        //    slot.BackgroundImage = Image.FromFile(urlDemonPowers + item.image);
                        //    break;
                        //case Tags.ItemType.Empty:
                        //    break;
                        case Tags.ItemType.Outfit:
                            EquipOutfit(slot, item, ref itemEquipped);
                            break;
                        case Tags.ItemType.Weapon:
                            if (slot.Name.Contains("Secondary"))
                            {
                                EquipWeapon(slot, item, true, ref itemEquipped);
                            }
                            else
                            {
                                EquipWeapon(slot, item, false, ref itemEquipped);
                            }                            
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ClearEquippedItems()
        {
            //Clear equipped item slots
            foreach (Button item in equippedItemControls)
            {
                //Clear the slot's displayed image and tag value
                item.BackgroundImage = null;
                item.Tag = null;
            }

            //Reset stats and skills (sub-calls from the following call)
            CalculateStatsFromEquippedCards();

            //Hide the attack panels and reset the top-bar damage stats
            pnlEquippedWeaponAttacks.Visible = false;
            pnlEquippedWeaponAttacksSecondary.Visible = false;
            ClearWeaponSlot(false);
            ClearWeaponSlot(true);

            //Clear the card points label
            totalCardPoints = 0;
            lblEquippedDestinyPoints.Text = totalCardPoints.ToString() + "/" + maximumCardPoints.ToString();
        }
    }
}