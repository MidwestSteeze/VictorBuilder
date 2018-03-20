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
        public Tags newItemTags;

        public class Affix
        {
            public enum Modifier
            { 
                Armor,
                ArmorPenetration,
                Damage,
                CritChance,
                CritMulti,
                NA
            }

            public string name;
            public Modifier modifier;            
            public int value;
            public string weaponDescription;
            public string listBoxDisplay { get; set; }

            public Affix(string aName, Modifier aModifier, int aValue, string aWeaponDescription)
            {
                name = aName;
                modifier = aModifier;
                value = aValue;
                weaponDescription = aWeaponDescription;
                listBoxDisplay = aWeaponDescription;
                listBoxDisplay.Replace("#", value.ToString()); //TODOSSG not working
            }
        }

        public CreateItemForm()
        {
            InitializeComponent();
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Enable the Rarity combo box
            cboRarity.Enabled = true;
        }

        private void cboRarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            tcWeaponMods.Enabled = true;
            switch (cboRarity.SelectedItem.ToString())
            {
                case "Common":
                    //Load up the Prefixes and Suffixes list boxes with values
                    FillAffixesLists();
                    tcWeaponMods.SelectedTab = tpAffixes;
                    //Don't allow a third affix to be added
                    lstThirdAffix.Enabled = false;
                    break;
                case "Uncommon":
                case "Rare":
                    //Load up the Prefixes and Suffixes list boxes with values
                    FillAffixesLists();
                    tcWeaponMods.SelectedTab = tpAffixes;
                    //Allow a third affix to be added
                    lstThirdAffix.Enabled = true;
                    break;
                case "Legendary":
                    tcWeaponMods.SelectedTab = tpLegendaries;
                    break;
                default:
                    break;
            }
        }

        private void FillAffixesLists()
        {
            //Prefixes
            lstPrefixes.DisplayMember = "listBoxDisplay";
            lstPrefixes.Items.Add(new Affix("Cursed", Affix.Modifier.Damage, 22, "Damaged increased by #%, but decreases gold drops"));
            lstPrefixes.Items.Add(new Affix("Dancing", Affix.Modifier.NA, 22, "#% increased attack speed"));
            lstPrefixes.Items.Add(new Affix("Devastating", Affix.Modifier.Damage, 24, "#% increased damage"));
            lstPrefixes.Items.Add(new Affix("Efficient", Affix.Modifier.NA, 30, "#% faster weapon skill cooldowns"));
            lstPrefixes.Items.Add(new Affix("Elder", Affix.Modifier.NA, 0, "While equipped, gain more experience points"));
            lstPrefixes.Items.Add(new Affix("Essence Slayer", Affix.Modifier.NA, 0, "Increased damage against Essences"));
            lstPrefixes.Items.Add(new Affix("Executioner", Affix.Modifier.NA, 0, "Increased damage when health is above 90%"));
            lstPrefixes.Items.Add(new Affix("Gargoyle Slayer", Affix.Modifier.NA, 0, "Increased damage against Gargoyles"));
            lstPrefixes.Items.Add(new Affix("Gided", Affix.Modifier.NA, 4, "Gold cost increased # times"));
            lstPrefixes.Items.Add(new Affix("Greedy", Affix.Modifier.NA, 0, "Killed monsters drop extra gold"));
            lstPrefixes.Items.Add(new Affix("Inquisitor", Affix.Modifier.NA, -1, "#% increased damage when Overdrive meter is full"));
            lstPrefixes.Items.Add(new Affix("Piercing", Affix.Modifier.ArmorPenetration, -1, "+# armor penetration"));
            lstPrefixes.Items.Add(new Affix("Skeleton Slayer", Affix.Modifier.NA, 0, "#% increased damage against Skeletons"));
            lstPrefixes.Items.Add(new Affix("Spider Slayer", Affix.Modifier.NA, 0, "#% increased damage against Spiders"));
            lstPrefixes.Items.Add(new Affix("Survivor", Affix.Modifier.NA, 44, "Damage is increased by #% while health is below 50%"));
            lstPrefixes.Items.Add(new Affix("Vampire Slayer", Affix.Modifier.NA, 0, "#% increased damage against Vampires"));
            lstPrefixes.Items.Add(new Affix("Vicious", Affix.Modifier.CritMulti, -1, "#% increased critical damage"));
            lstPrefixes.Items.Add(new Affix("Wraith Slayer", Affix.Modifier.NA, 0, "#% increased damage against Wraiths"));
            lstPrefixes.Items.Add(new Affix("Zealous", Affix.Modifier.NA, 0, "Overdrive meter fills #% faster"));

            //Suffixes
            lstSuffixes.DisplayMember = "listBoxDisplay";
            lstSuffixes.Items.Add(new Affix("of the Assassin", Affix.Modifier.CritChance, -1, "#% increased critical strike chance"));
            lstSuffixes.Items.Add(new Affix("of the Bear", Affix.Modifier.NA, 0, "Knock back enemies on critical hit (has cooldown)"));
            lstSuffixes.Items.Add(new Affix("of the Duelist", Affix.Modifier.ArmorPenetration, 68, "+# armor penetration"));
            lstSuffixes.Items.Add(new Affix("of Extraordinary Luck", Affix.Modifier.NA, 0, "While equipped, find better items"));
            lstSuffixes.Items.Add(new Affix("of the Fox", Affix.Modifier.NA, 0, "Reduces weapon cooldowns on overkill (has cooldown)"));
            lstSuffixes.Items.Add(new Affix("of the Leech", Affix.Modifier.NA, 0, "Gain a lot of health on critical hit (has cooldown)"));
            lstSuffixes.Items.Add(new Affix("of Luck", Affix.Modifier.NA, -1, "While equipped, find #% more items"));
            lstSuffixes.Items.Add(new Affix("of Mauling", Affix.Modifier.NA, 0, "Knock back enemies with #% chance"));
            lstSuffixes.Items.Add(new Affix("of the Ram", Affix.Modifier.NA, 0, "Inflict Daze on critical hit (has cooldown)"));
            lstSuffixes.Items.Add(new Affix("of Value", Affix.Modifier.NA, 4, "Gold cost increases # times"));
            lstSuffixes.Items.Add(new Affix("Vampirism", Affix.Modifier.NA, 68, "# health gained on hit"));
            lstSuffixes.Items.Add(new Affix("of the Vulture", Affix.Modifier.NA, 0, "Gain a lot of health on overkill (has cooldown)"));
            lstSuffixes.Items.Add(new Affix("of the Wolf", Affix.Modifier.NA, 5, "#% chance to inflict Vulnerable on hit"));

            //Third Affix
            lstThirdAffix.DisplayMember = "listBoxDisplay";
            lstThirdAffix.Items.Add(new Affix("Cursed", Affix.Modifier.Damage, 22, "Damaged increased by #%, but decreases gold drops"));
            lstThirdAffix.Items.Add(new Affix("Dancing", Affix.Modifier.NA, 22, "#% increased attack speed"));
            lstThirdAffix.Items.Add(new Affix("Devastating", Affix.Modifier.Damage, 24, "#% increased damage"));
            lstThirdAffix.Items.Add(new Affix("Efficient", Affix.Modifier.NA, 30, "#% faster weapon skill cooldowns"));
            lstThirdAffix.Items.Add(new Affix("Elder", Affix.Modifier.NA, 0, "While equipped, gain more experience points"));
            lstThirdAffix.Items.Add(new Affix("Essence Slayer", Affix.Modifier.NA, 0, "Increased damage against Essences"));
            lstThirdAffix.Items.Add(new Affix("Executioner", Affix.Modifier.NA, 0, "Increased damage when health is above 90%"));
            lstThirdAffix.Items.Add(new Affix("Gargoyle Slayer", Affix.Modifier.NA, 0, "Increased damage against Gargoyles"));
            lstThirdAffix.Items.Add(new Affix("Greedy", Affix.Modifier.NA, 0, "Killed monsters drop extra gold"));
            lstThirdAffix.Items.Add(new Affix("Inquisitor", Affix.Modifier.NA, -1, "#% increased damage when Overdrive meter is full"));
            lstThirdAffix.Items.Add(new Affix("Piercing", Affix.Modifier.ArmorPenetration, -1, "+# armor penetration"));
            lstThirdAffix.Items.Add(new Affix("Skeleton Slayer", Affix.Modifier.NA, 0, "#% increased damage against Skeletons"));
            lstThirdAffix.Items.Add(new Affix("Spider Slayer", Affix.Modifier.NA, 0, "#% increased damage against Spiders"));
            lstThirdAffix.Items.Add(new Affix("Survivor", Affix.Modifier.NA, 44, "Damage is increased by #% while health is below 50%"));
            lstThirdAffix.Items.Add(new Affix("Vampire Slayer", Affix.Modifier.NA, 0, "#% increased damage against Vampires"));
            lstThirdAffix.Items.Add(new Affix("Vicious", Affix.Modifier.CritMulti, -1, "#% increased critical damage"));
            lstThirdAffix.Items.Add(new Affix("Wraith Slayer", Affix.Modifier.NA, 0, "#% increased damage against Wraiths"));
            lstThirdAffix.Items.Add(new Affix("Zealous", Affix.Modifier.NA, 0, "Overdrive meter fills #% faster"));
            lstThirdAffix.Items.Add(new Affix("of the Assassin", Affix.Modifier.CritChance, -1, "#% increased critical strike chance"));
            lstThirdAffix.Items.Add(new Affix("of the Bear", Affix.Modifier.NA, 0, "Knock back enemies on critical hit (has cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of the Fox", Affix.Modifier.NA, 0, "Reduces weapon cooldowns on overkill (has cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of the Leech", Affix.Modifier.NA, 0, "Gain a lot of health on critical hit (has cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of Luck", Affix.Modifier.NA, -1, "While equipped, find #% more items"));
            lstThirdAffix.Items.Add(new Affix("of Mauling", Affix.Modifier.NA, 0, "Knock back enemies with #% chance"));
            lstThirdAffix.Items.Add(new Affix("of the Ram", Affix.Modifier.NA, 0, "Inflict Daze on critical hit (has cooldown)"));
            lstThirdAffix.Items.Add(new Affix("Vampirism", Affix.Modifier.NA, 68, "# health gained on hit"));
            lstThirdAffix.Items.Add(new Affix("of the Vulture", Affix.Modifier.NA, 0, "Gain a lot of health on overkill (has cooldown)"));
            lstThirdAffix.Items.Add(new Affix("of the Wolf", Affix.Modifier.NA, 5, "#% chance to inflict Vulnerable on hit"));
        }

        private void btnCreateWeapon_Click(object sender, EventArgs e)
        {
            //Find the base item in the database (type and rarity) and calculate on the affixes (if applicable)
            GetWeaponFromTable();

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

            //Weapon data
            Tags.WeaponTags.WeaponType wpnType;
            Tags.RarityType wpnRarity;
            int wpnDmgMin;
            int wpnDmgMax;
            int wpnArmorPenetration;
            int wpnCritChance;
            int wpnCritMulti;
            Tags.WeaponTags.WeaponDistance wpnDistance;
            string wpnName;
            Affix prefix;
            Affix suffix;
            Affix thirdAffix;
            string wpnDescription = string.Empty;
            string wpnImage;

            //Attack data
            string attackSlot;
            string attackName;
            string attackImageURL;
            string attackImageHoverTextURL;

            //Get the user-selected weapon affixes
            prefix = (Affix)lstPrefixes.SelectedItem; 
            suffix = (Affix)lstSuffixes.SelectedItem;
            thirdAffix = (Affix)lstThirdAffix.SelectedItem;

			//Populate the hover text
            if (prefix != null)
            {
                wpnDescription = prefix.listBoxDisplay;
            }
            if (suffix != null)
            {
                if (wpnDescription != string.Empty)
                {
                    wpnDescription += Environment.NewLine;
                }

                wpnDescription += suffix.listBoxDisplay;
            }
            if (thirdAffix != null)
            {
                if (wpnDescription != string.Empty)
                {
                    wpnDescription += Environment.NewLine;
                }

                wpnDescription += thirdAffix.listBoxDisplay;
            }

            string connectionString = Properties.Settings.Default.ItemsConnectionString;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
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

                //Query the Weapons table for base values
                string weaponQuery = "SELECT * FROM Weapons WHERE "
                     + "Type = '" + cboType.SelectedItem + "'"
                     + "AND Rarity = '" + cboRarity.SelectedItem + "';";

                command = new OleDbCommand(weaponQuery, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Enum.TryParse(reader[0].ToString(), out wpnType);
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
    }
}
