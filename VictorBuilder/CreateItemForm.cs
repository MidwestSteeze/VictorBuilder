using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VictorBuilder
{
    public partial class CreateItemForm : Form
    {
        string connectionString = Properties.Settings.Default.ItemsConnectionString;
        public Tags newItemTags;
        public Tags.CardTags cardTags;

        public CreateItemForm()
        {
            InitializeComponent();

            //Load up the Affixes list box selectable data for creating a weapon
            FillAffixesLists();

            //Load up the Cards list box selectable data for creating a card
            FillCardsLists();

            //Default the Card's DivineWicked drop-down to None
            cboDivineWicked.SelectedItem = "None";
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Enable the Rarity combo box
            cboWeaponRarity.Enabled = true;

            //Update the Legendary Weapons list box to contain options for the currently selected weapon type
            FillLegendaryWeaponsList();
        }

        private void cboRarity_SelectedIndexChanged(object sender, EventArgs e)
        {

			lstPrefixes.ClearSelected();
			lstSuffixes.ClearSelected();
			lstThirdAffix.ClearSelected();
			lstLegendaries.ClearSelected();
			lstPrefixes.Enabled = false;
			lstSuffixes.Enabled = false;
			lstThirdAffix.Enabled = false;
			lstLegendaries.Enabled = false;

            switch (cboWeaponRarity.SelectedItem.ToString())
            {
                case "Common":                    
                    tcWeaponMods.SelectedTab = tpAffixes;
                    //Enable affix lists
                    lstPrefixes.Enabled = true;
                    lstSuffixes.Enabled = true;
                    //Allow a third affix to be added
                    lstThirdAffix.Enabled = true;
                    //Clear the selected affixes (since Common weapons can only have 1)
                    lstPrefixes.ClearSelected();
                    lstSuffixes.ClearSelected();
                    break;
                case "Uncommon":
                case "Rare":
                    tcWeaponMods.SelectedTab = tpAffixes;
                    //Enable affix lists
                    lstPrefixes.Enabled = true;
                    lstSuffixes.Enabled = true;
                    //Allow a third affix to be added
                    lstThirdAffix.Enabled = true;
                    break;
                case "Legendary":
                    tcWeaponMods.SelectedTab = tpLegendaries;
                    //Enable list of legendary weapons to pick from
                    lstLegendaries.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void FillAffixesLists()
        {
            //Prefixes
            lstPrefixes.DisplayMember = "listBoxDisplay";
            lstPrefixes.Items.Add(new Affix("Cursed", Affix.Modifier.Damage, 40, "Damaged increased by #%, but decreases gold drops"));
            lstPrefixes.Items.Add(new Affix("Dancing", Affix.Modifier.None, 20, "#% increased attack speed"));
            lstPrefixes.Items.Add(new Affix("Devastating", Affix.Modifier.Damage, 24, "#% increased damage"));
            lstPrefixes.Items.Add(new Affix("Efficient", Affix.Modifier.None, 40, "#% faster weapon skill cooldowns"));
            lstPrefixes.Items.Add(new Affix("Elder", Affix.Modifier.None, 50, "While equipped, gain more experience points"));
            lstPrefixes.Items.Add(new Affix("Essence Slayer", Affix.Modifier.None, 40, "Increased damage against Essences"));
            lstPrefixes.Items.Add(new Affix("Executioner", Affix.Modifier.None, 30, "Increased damage when health is above 90%"));
            lstPrefixes.Items.Add(new Affix("Gargoyle Slayer", Affix.Modifier.None, 40, "Increased damage against Gargoyles"));
            lstPrefixes.Items.Add(new Affix("Gided", Affix.Modifier.None, 5, "Gold cost increased # times"));
            lstPrefixes.Items.Add(new Affix("Greedy", Affix.Modifier.None, 100, "Killed monsters drop extra gold"));
            lstPrefixes.Items.Add(new Affix("Inquisitor", Affix.Modifier.None, 30, "#% increased damage when Overdrive meter is full"));
            lstPrefixes.Items.Add(new Affix("Piercing", Affix.Modifier.ArmorPenetration, 60, "+# armor penetration"));
            lstPrefixes.Items.Add(new Affix("Skeleton Slayer", Affix.Modifier.None, 40, "#% increased damage against Skeletons"));
            lstPrefixes.Items.Add(new Affix("Spider Slayer", Affix.Modifier.None, 40, "#% increased damage against Spiders"));
            lstPrefixes.Items.Add(new Affix("Survivor", Affix.Modifier.None, 44, "Damage is increased by #% while health is below 50%"));
            lstPrefixes.Items.Add(new Affix("Vampire Slayer", Affix.Modifier.None, 40, "#% increased damage against Vampires"));
            lstPrefixes.Items.Add(new Affix("Vicious", Affix.Modifier.CritMulti, 80, "#% increased critical damage"));
            lstPrefixes.Items.Add(new Affix("Wraith Slayer", Affix.Modifier.None, 40, "#% increased damage against Wraiths"));
            lstPrefixes.Items.Add(new Affix("Zealous", Affix.Modifier.None, 100, "Overdrive meter fills #% faster"));

            //Suffixes
            lstSuffixes.DisplayMember = "listBoxDisplay";
            lstSuffixes.Items.Add(new Affix("of the Assassin", Affix.Modifier.CritChance, 20, "#% increased critical strike chance"));
            lstSuffixes.Items.Add(new Affix("of the Bear", Affix.Modifier.None, 0, "Knock back enemies on critical hit (5s. cooldown)"));
            lstSuffixes.Items.Add(new Affix("of the Duelist", Affix.Modifier.ArmorPenetration, 68, "+# armor penetration"));
            lstSuffixes.Items.Add(new Affix("of Extraordinary Luck", Affix.Modifier.None, 0, "While equipped, find better items"));
            lstSuffixes.Items.Add(new Affix("of the Fox", Affix.Modifier.None, 0, "Attack cooldowns reduced by 2s. on overkill (1s. cooldown)"));
            lstSuffixes.Items.Add(new Affix("of the Leech", Affix.Modifier.None, 150, "Gain # health on crit (5s. cooldown)"));
            lstSuffixes.Items.Add(new Affix("of Luck", Affix.Modifier.None, 30, "While equipped, find #% more items"));
            lstSuffixes.Items.Add(new Affix("of Mauling", Affix.Modifier.None, 20, "Knock back enemies with #% chance"));
            lstSuffixes.Items.Add(new Affix("of the Ram", Affix.Modifier.None, 0, "Inflict Daze on crit (20s. cooldown)"));
            lstSuffixes.Items.Add(new Affix("of Value", Affix.Modifier.None, 5, "Gold cost increases # times"));
            lstSuffixes.Items.Add(new Affix("Vampirism", Affix.Modifier.None, 68, "Gain # health on hit"));
            lstSuffixes.Items.Add(new Affix("of the Vulture", Affix.Modifier.None, 150, "Gain # health on overkill (10s. cooldown)"));
            lstSuffixes.Items.Add(new Affix("of the Wolf", Affix.Modifier.None, 5, "Inflict vulnerable with #% chance"));

            //Third Affix
            lstThirdAffix.DisplayMember = "listBoxDisplay";
            lstThirdAffix.Items.Add(new Affix("Cursed", Affix.Modifier.Damage, 44, "Damaged increased by #%, but decreases gold drops"));
            lstThirdAffix.Items.Add(new Affix("Dancing", Affix.Modifier.None, 22, "#% increased attack speed"));
            lstThirdAffix.Items.Add(new Affix("Devastating", Affix.Modifier.Damage, 24, "#% increased damage"));
            lstThirdAffix.Items.Add(new Affix("Efficient", Affix.Modifier.None, 44, "#% faster weapon skill cooldowns"));
            lstThirdAffix.Items.Add(new Affix("Elder", Affix.Modifier.None, 56, "While equipped, gain more experience points"));
            lstThirdAffix.Items.Add(new Affix("Essence Slayer", Affix.Modifier.None, 44, "Increased damage against Essences"));
            lstThirdAffix.Items.Add(new Affix("Executioner", Affix.Modifier.None, 34, "Increased damage when health is above 90%"));
            lstThirdAffix.Items.Add(new Affix("Gargoyle Slayer", Affix.Modifier.None, 44, "Increased damage against Gargoyles"));
            lstThirdAffix.Items.Add(new Affix("Greedy", Affix.Modifier.None, 116, "Killed monsters drop extra gold"));
            lstThirdAffix.Items.Add(new Affix("Inquisitor", Affix.Modifier.None, 34, "#% increased damage when Overdrive meter is full"));
            lstThirdAffix.Items.Add(new Affix("Piercing", Affix.Modifier.ArmorPenetration, 68, "+# armor penetration"));
            lstThirdAffix.Items.Add(new Affix("Skeleton Slayer", Affix.Modifier.None, 44, "#% increased damage against Skeletons"));
            lstThirdAffix.Items.Add(new Affix("Spider Slayer", Affix.Modifier.None, 44, "#% increased damage against Spiders"));
            lstThirdAffix.Items.Add(new Affix("Survivor", Affix.Modifier.None, 44, "Damage is increased by #% while health is below 50%"));
            lstThirdAffix.Items.Add(new Affix("Vampire Slayer", Affix.Modifier.None, 44, "#% increased damage against Vampires"));
            lstThirdAffix.Items.Add(new Affix("Vicious", Affix.Modifier.CritMulti, 88, "#% increased critical damage"));
            lstThirdAffix.Items.Add(new Affix("Wraith Slayer", Affix.Modifier.None, 44, "#% increased damage against Wraiths"));
            lstThirdAffix.Items.Add(new Affix("Zealous", Affix.Modifier.None, 110, "Overdrive meter fills #% faster"));
            lstThirdAffix.Items.Add(new Affix("of the Assassin", Affix.Modifier.CritChance, 22, "#% increased critical strike chance"));
            lstThirdAffix.Items.Add(new Affix("of the Bear", Affix.Modifier.None, 0, "Knock back enemies on critical hit (5s. cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of the Fox", Affix.Modifier.None, 0, "Attack cooldowns reduced by 2s. on overkill (1s. cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of the Leech", Affix.Modifier.None, 170, "Gain # health on crit (5s. cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of Luck", Affix.Modifier.None, 34, "While equipped, find #% more items"));
            lstThirdAffix.Items.Add(new Affix("of Mauling", Affix.Modifier.None, 22, "Knock back enemies with #% chance"));
            lstThirdAffix.Items.Add(new Affix("of the Ram", Affix.Modifier.None, 0, "Inflict Daze on crit (20s. cooldown)"));
            lstThirdAffix.Items.Add(new Affix("Vampirism", Affix.Modifier.None, 68, "Gain # health on hit"));
            lstThirdAffix.Items.Add(new Affix("of the Vulture", Affix.Modifier.None, 170, "Gain # health on overkill (10s. cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of the Wolf", Affix.Modifier.None, 5, "Inflict vulnerable with #% chance"));
        }

        private void FillCardsLists()
        {             
            //Manually populate the Divine and Wicked mods lists
            lstCardDivineMods.DisplayMember = "listBoxDisplay";
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 32, "Gain Regeneration for 10 sec. when below #% health. (30 sec. cooldown)"));
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 250, "Gain # health when the overdrive is filled."));
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 0, "Remove negative status on kill (10 second cooldown)"));
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 22, "300 Lightning damage against attacking distance fighters (#% probability)"));
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 25, "#% Chance to get additional drop from chests."));
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 50, "#% chance to negate a critical hit"));
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 20, "+#% damage when your health is above 90%"));
            lstCardDivineMods.Items.Add(new Affix("", Affix.Modifier.None, 24, "+# Armor Penetration"));

            lstCardWickedMods.DisplayMember = "listBoxDisplay";
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 0, "Inflict Cripple on crit (40% chance)" + Environment.NewLine + "*Cripple: Movement speed is decreased by 66%."));
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 0, "Inflict Bleeding on crit (15% chance)" + Environment.NewLine + "*Bleeding: Deals 2% of max health, but no more than 100 every second. Bleeding cannot kill the target."));
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 0, "-30% duration of negative conditions"));
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 0, "Gain speed for 7 sec. on kill (20 sec. cooldown)"));
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 4, "Gain #% of maximum overdrive on overkill")); //TODO 3.90% but param is integer
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 29, "Receive Brutality when overdrive is filled (#% probability)"));
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 0, "Receive Focus for 10 seconds after an Overkill (30 second cooldown)"));
            lstCardWickedMods.Items.Add(new Affix("", Affix.Modifier.None, 0, "Increased Dodge distance"));
        }

        private void FillLegendaryWeaponsList()
        {
            lstLegendaries.Items.Clear();

            //Pull the names from the database and fill the list
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                //Query the WeaponsLegendary table for a list of all legendary weapons
                string weaponsLegendaryQuery = "SELECT * FROM WeaponsLegendary WHERE "
                    + "Type = '" + cboType.SelectedItem + "';";

                OleDbCommand command = new OleDbCommand(weaponsLegendaryQuery, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //Add the legendary wepaon Name and Special Stat description to the list box
                        lstLegendaries.Items.Add(reader[1].ToString() + " | " + reader[9].ToString().Replace("/r/n", " | "));
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                Console.ReadLine();
            }
        }

        private void btnCreateWeapon_Click(object sender, EventArgs e)
        {
            if ((cboWeaponRarity.SelectedItem == null) || (cboType.SelectedItem == null))
            {
                MessageBox.Show("Please specify a Weapon Type and Rarity.", "Create Weapon");
                return;
            }

            if ((cboWeaponRarity.SelectedItem == "Legendary") && (lstLegendaries.SelectedItem == null))
            {
                MessageBox.Show("Please select a Legendary Weapon.", "Create Weapon");
                return;
            }

            //Find the base item in the database (type and rarity) and calculate on the affixes (if applicable)
            GetWeaponFromTable();
            CalculateStats();

            //Return to the main form
            this.DialogResult = DialogResult.OK;            
        }

        private void GetWeaponFromTable()
        {
            Tags.WeaponTags weaponTags;
            Tags.WeaponTags.AttackTags wpnAttack1 = null;
            Tags.WeaponTags.AttackTags wpnAttack2 = null;
            Tags.WeaponTags.AttackTags wpnAttack3 = null;

            //Generic data
            Tags.ItemType itemType = Tags.ItemType.Weapon;
            string wpnName;
            string wpnImage;

            //Weapon data
            Tags.WeaponTags.WeaponType wpnType;
            Tags.RarityType wpnRarity;
            int wpnDmgMin;
            int wpnDmgMax;
            int wpnArmorPenetration;
            int wpnCritChance;
            int wpnCritMulti;
            Tags.WeaponTags.WeaponDistance wpnDistance;
            Affix.Modifier modifier;
            string wpnDescription = string.Empty;

            //Attack data
            string attackSlot;
            string attackName;
            string attackImageURL;
            string attackImageHoverTextURL;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                #region GetAttacks
                //Query the Attacks table for the selected weapon's attacks
                string attackQuery = "SELECT * FROM Attacks WHERE "
                    + "WeaponType = '" + cboType.SelectedItem + "';";

                OleDbCommand command = new OleDbCommand(attackQuery, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        attackSlot = reader[1].ToString();
                        attackName = reader[2].ToString();
                        attackImageURL = reader[3].ToString();
                        attackImageHoverTextURL = reader[4].ToString();

                        switch (attackSlot)
                        {
                            case "Attack1":
                                wpnAttack1 = new Tags.WeaponTags.AttackTags(attackName, attackImageURL, attackImageHoverTextURL);
                                break;
                            case "Attack2":
                                wpnAttack2 = new Tags.WeaponTags.AttackTags(attackName, attackImageURL, attackImageHoverTextURL);
                                break;
                            case "Attack3":
                                wpnAttack3 = new Tags.WeaponTags.AttackTags(attackName, attackImageURL, attackImageHoverTextURL);
                                break;
                            default:
                                break;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                Console.ReadLine();
                #endregion

                #region GetWeaponBase
                //Query the Weapons table for base values
                string weaponQuery = "SELECT * FROM Weapons WHERE "
                     + "Type = '" + cboType.SelectedItem + "'"
                     + "AND Rarity = '" + cboWeaponRarity.SelectedItem + "';";

                command = new OleDbCommand(weaponQuery, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Enum.TryParse(reader[0].ToString().Replace(" ", ""), out wpnType);
                        Enum.TryParse(reader[1].ToString(), out wpnRarity);
                        wpnDmgMin = (int)reader[2];
                        wpnDmgMax = (int)reader[3];
                        wpnArmorPenetration = (int)reader[4];
                        wpnCritChance = (int)reader[5];
                        wpnCritMulti = (int)reader[6];
                        wpnName = reader[7].ToString();
                        Enum.TryParse(reader[8].ToString(), out wpnDistance);
                        wpnImage = reader[9].ToString();

                        weaponTags = new Tags.WeaponTags(wpnType, wpnDistance, wpnDmgMin, wpnDmgMax, wpnArmorPenetration, wpnCritChance, wpnCritMulti, wpnAttack1, wpnAttack2, wpnAttack3);
                        newItemTags = new Tags(itemType, wpnRarity, wpnName, wpnDescription, wpnImage, weaponTags);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                Console.ReadLine();
                #endregion

                //If we are creating a Legendary item, grab/override additional data from the WeaponsLegendary table
                if (newItemTags.rarity == Tags.RarityType.Legendary)
                {
                    #region SetLegendaryAffixesAndProperties
					//Substring the selection of lstLegendaries to just pull the Name (the list box is showing the Name and Special Stat description for the user to see)
                    string weaponLegendaryQuery = "SELECT * FROM WeaponsLegendary WHERE "
                         + "Type = '" + cboType.SelectedItem + "'" 
                         + "AND WeaponName = '" + lstLegendaries.SelectedItem.ToString().Substring(0, lstLegendaries.SelectedItem.ToString().IndexOf(" | ")) + "';";

                    command = new OleDbCommand(weaponLegendaryQuery, connection);

                    try
                    {
                        connection.Open();
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            newItemTags.name = reader[1].ToString();
                            newItemTags.imageURL = reader[2].ToString();

                            Enum.TryParse(reader[4].ToString(), out modifier);
                            newItemTags.weaponTags.prefix = new Affix("", modifier, (int)reader[3], reader[5].ToString().Replace("/r/n", Environment.NewLine));

                            Enum.TryParse(reader[7].ToString(), out modifier);
                            newItemTags.weaponTags.suffix = new Affix("", modifier, (int)reader[6], reader[8].ToString().Replace("/r/n", Environment.NewLine));

                            newItemTags.weaponTags.specialStat = reader[9].ToString().Replace("/r/n", Environment.NewLine);
                        }

                        newItemTags.description = newItemTags.weaponTags.prefix.listBoxDisplay + Environment.NewLine + newItemTags.weaponTags.suffix.listBoxDisplay + Environment.NewLine + newItemTags.weaponTags.specialStat;
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    Console.ReadLine();
                #endregion
                }
                else
                {
                    #region SetRareAffixesAndProperties
                    //Set the user-selected rare weapon affixes
                    newItemTags.weaponTags.prefix = (Affix)lstPrefixes.SelectedItem;
                    newItemTags.weaponTags.suffix = (Affix)lstSuffixes.SelectedItem;
                    newItemTags.weaponTags.thirdAffix = (Affix)lstThirdAffix.SelectedItem;

                    //Populate the hover text and update the weapon name
					PopulateItemDescription(ref newItemTags, newItemTags.weaponTags.prefix, newItemTags.weaponTags.suffix, newItemTags.weaponTags.thirdAffix);
                    #endregion
                }
            }
        }

        private void CalculateStats()
        {
            switch (newItemTags.itemType)
            {
                case Tags.ItemType.Card:
                    //CalculateCardStats();
                    break;
                case Tags.ItemType.Weapon:
                    CalculateWeaponStats();
                    break;
                default:
                    break;
            }
        }

        private void CalculateWeaponStats()
        { 
            //Add on the affix modifiers (if any) to the base weapon stats (ie. dmg, armor pen, crit, etc)
            CalculateOnModifier(newItemTags.weaponTags.prefix);
            CalculateOnModifier(newItemTags.weaponTags.suffix);
            CalculateOnModifier(newItemTags.weaponTags.thirdAffix);
        }

        private void CalculateOnModifier(Affix aAffix)
        {
            if (aAffix != null)
            {
                switch (aAffix.modifier)
                {
                    case Affix.Modifier.None:
                        break;
                    //case Affix.Modifier.Armor:
                    //break;
                    case Affix.Modifier.ArmorPenetration:
                        newItemTags.weaponTags.armorPenetration += aAffix.value;
                        break;
                    case Affix.Modifier.Damage:
                        newItemTags.weaponTags.dmgMin = (int)Math.Round(newItemTags.weaponTags.dmgMin * (1 + (aAffix.value / 100.0)));
                        newItemTags.weaponTags.dmgMax = (int)Math.Round(newItemTags.weaponTags.dmgMax * (1 + (aAffix.value / 100.0)));
                        break;
                    case Affix.Modifier.CritChance:
                        newItemTags.weaponTags.critChance += aAffix.value;
                        break;
                    case Affix.Modifier.CritMulti:
                        newItemTags.weaponTags.critMulti += aAffix.value;
                        break;
                    default:
                        break;
                }
            }
        }

        private void weaponsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.weaponsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.itemsDataSet);

        }

        private void CreateItemForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'itemsDataSet.Weapons' table. You can move, or remove it, as needed.
            this.weaponsTableAdapter.Fill(this.itemsDataSet.Weapons);

        }

        private void cboDivineWicked_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear the selection, if there is one, and disable/re-enable the appropriate listbox
            switch (cboDivineWicked.SelectedItem.ToString())
            {
                case "Divine":
                    lstCardWickedMods.ClearSelected();
                    lstCardDivineMods.Enabled = true;
                    lstCardWickedMods.Enabled = false;
                    break;
                case "Wicked":
                    lstCardDivineMods.ClearSelected();
                    lstCardDivineMods.Enabled = false;
                    lstCardWickedMods.Enabled = true;
                    break;
                case "None":
                    lstCardDivineMods.ClearSelected();
                    lstCardWickedMods.ClearSelected();
                    lstCardDivineMods.Enabled = false;
                    lstCardWickedMods.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void cboCardRarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tags card = new Tags();
            Tags.RarityType rarity;
            Affix.Modifier modifier;
            Affix.ModifierSpecific modifierSpecific;

            //Clear the list box so we can refill it with new data
            lstCards.Items.Clear();

            //Show the item in the listbox using the name and description
            lstCards.DisplayMember = "listBoxDisplay";

            //Query the Cards table for the base cards of the selected rarity
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                //Query to run against the Cards table
                string cardsQuery = "SELECT * FROM Cards WHERE "
                    + "Rarity = '" + cboCardRarity.SelectedItem + "'"
                    + "ORDER BY CardName ASC;";

                OleDbCommand command = new OleDbCommand(cardsQuery, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //Refresh the card object
                        card = new Tags();

                        //Fill card object of current row in the database
                        card.name = reader[0].ToString();
                        card.imageURL = reader[12].ToString();

                        Enum.TryParse(cboCardRarity.SelectedItem.ToString(), out rarity);
                        card.rarity = rarity;

                        cardTags = new Tags.CardTags();
                        cardTags.points = (int)reader[5];

                        Enum.TryParse(reader[2].ToString(), out modifier);
                        cardTags.prefix = new Affix("", modifier, (int)reader[3], reader[4].ToString().Replace("/r/n", Environment.NewLine));

                        Enum.TryParse(reader[6].ToString(), out modifier);
                        try
                        {
                            cardTags.suffix = new Affix("", modifier, (int)reader[8], reader[9].ToString().Replace("/r/n", Environment.NewLine));
                            
                            if (modifier == Affix.Modifier.Specific)
                            {
                                //We have a specific modifier (ie. +Destiny Card Points, Increased Rapier Charge damage, etc); capture that data
                                Enum.TryParse(reader[7].ToString(), out modifierSpecific);
                                cardTags.suffix.modifierSpecific = modifierSpecific;
                            }
                        }
                        catch (Exception)
                        {
                            //No secondary mod for this card, let suffix = null and continue on
                        }

                        cardTags.unique = (bool)reader[10];
                        cardTags.conditional = (bool)reader[11];
                        card.cardTags = cardTags;

                        //Update the description of the card by combining its base affixes (prefix and suffix)
                        PopulateItemDescription(ref card, card.cardTags.prefix, card.cardTags.suffix, card.cardTags.thirdAffix);

                        //Update the list box display value we'll use to show the card to the user
                        card.listBoxDisplay = card.name + " | " + card.description.Replace("\r\n", " | ");

                        //Store the card in the listbox of all available cards to create from
                        lstCards.Items.Add(card);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                Console.ReadLine();
            }
        }

        private void btnCreateCard_Click(object sender, EventArgs e)
        {
            if ((cboCardRarity.SelectedItem == null) || (lstCards.SelectedItem == null))
            {
                MessageBox.Show("Please specify a Rarity and select a Destiny Card.", "Create Destiny Card");
                return;
            }

            Tags.CardTags.DivineWicked divineWicked;

            //Pull the item from the list box and combine on the other stats (divine/wicked affix, etc)
            newItemTags = (Tags)lstCards.SelectedItem;

            Enum.TryParse(cboDivineWicked.SelectedItem.ToString(), out divineWicked);
            newItemTags.cardTags.divineWicked = divineWicked;

            switch (newItemTags.cardTags.divineWicked)
            {
                case Tags.CardTags.DivineWicked.None:
                    break;
                case Tags.CardTags.DivineWicked.Divine:
                    newItemTags.cardTags.thirdAffix = (Affix)lstCardDivineMods.SelectedItem;
                    newItemTags.name = newItemTags.name.Insert(0, "Divine ");

                    //Re-populate the description to add on the Divine affix text
                    PopulateItemDescription(ref newItemTags, newItemTags.cardTags.prefix, newItemTags.cardTags.suffix, newItemTags.cardTags.thirdAffix);
                    break;
                case Tags.CardTags.DivineWicked.Wicked:
                    newItemTags.cardTags.thirdAffix = (Affix)lstCardWickedMods.SelectedItem;
                    newItemTags.name = newItemTags.name.Insert(0, "Wicked ");

                    //Re-populate the description to add on the Wicked affix text
                    PopulateItemDescription(ref newItemTags, newItemTags.cardTags.prefix, newItemTags.cardTags.suffix, newItemTags.cardTags.thirdAffix);
                    break;
                default:
                    break;
            }

            //Return to the main form
            this.DialogResult = DialogResult.OK;
        }

		private void PopulateItemDescription(ref Tags itemTags, Affix prefix, Affix suffix, Affix thirdAffix)
		{
			if (prefix != null)
            {
                itemTags.description = prefix.description;
                if (itemTags.itemType == Tags.ItemType.Weapon)
                {
                    itemTags.name = itemTags.name.Insert(0, prefix.name + " ");
				}
            }

            if (suffix != null)
            {
                if (itemTags.description != string.Empty)
                {
                    itemTags.description += Environment.NewLine;
                }

                itemTags.description += suffix.description;

                if (itemTags.itemType == Tags.ItemType.Weapon)
				{
                    itemTags.name += " " + itemTags.weaponTags.suffix.name;
				}
            }

            if (thirdAffix != null)
            {
                if (itemTags.description != string.Empty)
                {
                    itemTags.description += Environment.NewLine;
                }

                itemTags.description += thirdAffix.description;
            }			
		}

        private void lstPrefixes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox tmp = (ListBox)sender;

            //Only allow 1 affix for Common weapon types by clearing the selected suffix, if there is one, and we just selected a prefix
            if ((cboWeaponRarity.SelectedItem.ToString() == "Common") && (lstPrefixes.SelectedIndex > -1) && (lstSuffixes.SelectedIndex > -1))
            {
                lstSuffixes.ClearSelected();
            }
        }

        private void lstSuffixes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox tmp = (ListBox)sender;

            //Only allow 1 affix for Common weapon types by clearing the selected prefix, if there is one, and we just selected a suffix
            if ((cboWeaponRarity.SelectedItem.ToString() == "Common") && (lstSuffixes.SelectedIndex > -1) && (lstPrefixes.SelectedIndex > -1))
            {
                lstPrefixes.ClearSelected();
            }
        }

        private void btnClearWeaponThirdAffix_Click(object sender, EventArgs e)
        {
            lstThirdAffix.ClearSelected();
        }
    }
}
