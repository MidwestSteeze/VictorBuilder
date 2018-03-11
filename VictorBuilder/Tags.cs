﻿using System;
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

            /**********************
            /* Constructors START *
            /**********************/
            public WeaponTags(WeaponType aWeaponType)
            {
                weaponType = aWeaponType;
            }

            public WeaponTags(WeaponType aWeaponType, WeaponDistance aWeaponDistance, int aDmgMin, int aDmgMax, int aArmorPenetration, int aCritChance, int aCritMulti)
            {
                armorPenetration = aArmorPenetration;
                critChance = aCritChance;
                critMulti = aCritMulti;
                dmgMax = aDmgMax;
                dmgMin = aDmgMin;
                weaponDistance = aWeaponDistance;
                weaponType = aWeaponType;
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
            public int points = 0;

            public bool divine = false;
            public bool wicked = false;
            public bool unique = false;

            //Card stats that can affect the character sheet
            public int health = 0; //Hope, -%Death
            public int armor = 0; //The Knight, The Smith
            public int armorPenetration = 0; //The Beast
            public int critChance = 0; //Strength
            public int critMulti = 0; //The Rogue, Wildcard, The Blademaster

            //Card stats that can affect the skill sheet
            public int damage = 0; //Death
            public int meleeDamage = 0; //The Warrior
            public int rangedDamage = 0; //The Archer

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