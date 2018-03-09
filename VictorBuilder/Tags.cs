using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorBuilder
{
    public class Tags
    {
        public enum ItemType
        { 
            Card,
            Consumable,
            DemonPower,
            Empty,
            Outfit,
            Weapon
        }

        public enum RarityType
        { 
            Common,
            Uncommon,
            Rare,
            Legendary
        }

        public ItemType itemType;
        public RarityType rarity;
        public WeaponTags weaponTags;
        public CardTags cardTags;

        /**********************
        /* Constructors START *
        /**********************/
        public Tags() { }

        public Tags(ItemType aItemType, RarityType aRarityType)
        {
            itemType = aItemType;
            rarity = aRarityType;
        }

        public Tags(ItemType aItemType, RarityType aRarityType, WeaponTags aWeaponTags)
        {
            itemType = aItemType;
            rarity = aRarityType;
            weaponTags = aWeaponTags;
        }

        public Tags(ItemType aItemType, RarityType aRarityType, CardTags aCardTags)
        {
            itemType = aItemType;
            rarity = aRarityType;
            cardTags = aCardTags;
        }

        /**********************
        /* Constructors END   *
        /**********************/

        public class WeaponTags : Tags
        {

            public enum WeaponType
            {
                Hammer,
                HandMortar,
                Shotgun,
                LightningGun,
                Rapier,
                Sword,
                Scythe,
                Tome
            }

            public int armorPenetration;
            public int critChance;
            public int critMulti;
            public int dmgMax;
            public int dmgMin;
            public WeaponType weapon;

            /**********************
            /* Constructors START *
            /**********************/
            public WeaponTags(WeaponType aWeapon)
            {
                weapon = aWeapon;
            }

            public WeaponTags(WeaponType aWeapon, int aDmgMin, int aDmgMax, int aArmorPenetration, int aCritChance, int aCritMulti)
            {
                armorPenetration = aArmorPenetration;
                critChance = aCritChance;
                critMulti = aCritMulti;
                dmgMax = aDmgMax;
                dmgMin = aDmgMin;
                weapon = aWeapon;
            }

            /**********************
            /* Constructors END   *
            /**********************/
        }

        public class CardTags 
        {
            //enums here

            public string name;
            public string stat;
            public int points;

            public bool divine;
            public bool wicked = false;
            public bool unique = false;

            public int health;
            public int armor;
            public int armorPenetration;
            public int critChance;
            public int critMulti;

            /**********************
            /* Constructors START *
            /**********************/
            public CardTags(string aName, string aStat, int aPoints)
            {
                name = aName;
                stat = aStat;
                points = aPoints;
            }

            public CardTags(string aName, string aStat, int aPoints, bool aDivine, bool aWicked, bool aUnique, int aHealth, int aArmor, int aArmorPenetration, int aCritChance, int aCritMulti)
            {
                name = aName;
                stat = aStat;
                points = aPoints;

                divine = aDivine;
                wicked = aWicked;
                unique = aUnique;

                health = aHealth;
                armor = aArmor;
                armorPenetration = aArmorPenetration;
                critChance = aCritChance;
                critMulti = aCritMulti;
            }

            /**********************
            /* Constructors END   *
            /**********************/
        }
    }
}
