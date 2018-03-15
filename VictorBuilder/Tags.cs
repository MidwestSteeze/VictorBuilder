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

        public string description;
        public string name;
        public ItemType itemType;
        public RarityType rarity;
        public WeaponTags weaponTags;
        public CardTags cardTags;

        /**********************
        /* Constructors START *
        /**********************/
        public Tags() { }

        public Tags(ItemType aItemType, RarityType aRarityType, string aName, string aDescription)
        {
            itemType = aItemType;
            rarity = aRarityType;
            name = aName;
            description = aDescription;
        }

        public Tags(ItemType aItemType, RarityType aRarityType, string aName, string aDescription, WeaponTags aWeaponTags)
        {
            itemType = aItemType;
            rarity = aRarityType;
            name = aName;
            description = aDescription;
            weaponTags = aWeaponTags;
        }

        public Tags(ItemType aItemType, RarityType aRarityType, string aName, string aDescription, CardTags aCardTags)
        {
            itemType = aItemType;
            rarity = aRarityType;
            name = aName;
            description = aDescription;
            cardTags = aCardTags;
        }

        /**********************
        /* Constructors END   *
        /**********************/

        public class WeaponTags : Tags
        {

            public enum WeaponDistance
            {
                Melee,
                Ranged
            }

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

            public WeaponDistance weaponDistance;
            public WeaponType weaponType;

            public AttackTags attack1;
            public AttackTags attack2;
            public AttackTags attack3;

            /**********************
            /* Constructors START *
            /**********************/
            public WeaponTags() { }

            public WeaponTags(WeaponType aWeaponType)
            {
                weaponType = aWeaponType;
            }

            public WeaponTags(WeaponType aWeaponType, WeaponDistance aWeaponDistance, int aDmgMin, int aDmgMax, int aArmorPenetration, int aCritChance, int aCritMulti,
                                AttackTags aAttackTags1, AttackTags aAttackTags2, AttackTags aAttackTags3)
            {
                armorPenetration = aArmorPenetration;
                critChance = aCritChance;
                critMulti = aCritMulti;
                dmgMax = aDmgMax;
                dmgMin = aDmgMin;
                weaponDistance = aWeaponDistance;
                weaponType = aWeaponType;
                attack1 = aAttackTags1;
                attack2 = aAttackTags2;
                attack3 = aAttackTags3;
            }

            /**********************
            /* Constructors END   *
            /**********************/

            public class AttackTags : WeaponTags
            {
                public string attackName;
                public string attackImageURL;
                public string attackImageHoverTextURL;
                public int attackDmgMin;
                public int attackDmgMax;

                /**********************
                /* Constructors START *
                /**********************/
                public AttackTags(string aAttackName, string aAttackImageURL, string aAttackImageHoverTextURL)
                {
                    attackName = aAttackName;
                    attackImageURL = aAttackImageURL;
                    attackImageHoverTextURL = aAttackImageHoverTextURL;
                }

                /**********************
                /* Constructors END   *
                /**********************/
            }
        }

        public class CardTags 
        {
            //enums here
            public enum CardMod
            { 
                Health,
                Armor,
                ArmorPenetration,
                CritChance,
                CritMulti,
                Damage,
                MeleeDamage,
                RangedDamage
            }

            public int points = 0;

            public CardMod mod;
            public int modValue = 0;

            public bool divine = false;
            public bool wicked = false;
            public bool unique = false;

            ////Card stats that can affect the character sheet
            //public int health = 0; //Hope, -%Death
            //public int armor = 0; //The Knight, The Smith
            //public int armorPenetration = 0; //The Beast
            //public int critChance = 0; //Strength
            //public int critMulti = 0; //The Rogue, Wildcard, The Blademaster

            ////Card stats that can affect the skill sheet
            //public int damage = 0; //Death
            //public int meleeDamage = 0; //The Warrior
            //public int rangedDamage = 0; //The Archer

            /**********************
            /* Constructors START *
            /**********************/
            public CardTags(int aPoints)
            {
                points = aPoints;
            }

            public CardTags(int aPoints, CardMod aMod, int aModValue)
            {
                points = aPoints;

                mod = aMod;
                modValue = aModValue;

                //Let these default, and only set them based on the card you're creating then (ie. no need to set ALL attributes for a card that only has 1 mod)
                //divine = aDivine;
                //wicked = aWicked;
                //unique = aUnique;

                //health = aHealth;
                //armor = aArmor;
                //armorPenetration = aArmorPenetration;
                //critChance = aCritChance;
                //critMulti = aCritMulti;
            }

            /**********************
            /* Constructors END   *
            /**********************/
        }
    }
}
