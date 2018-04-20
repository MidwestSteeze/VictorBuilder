using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorBuilder
{
    [Serializable]
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

        public string name;
        public string description;
        public string listBoxDisplay { get; set; }
        public ItemType itemType;
        public RarityType rarity;
        public string imageURL;
        public string imageHoverTextURL;
        public WeaponTags weaponTags;
        public CardTags cardTags;
        public OutfitTags outfitTags;

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

        public Tags(ItemType aItemType, RarityType aRarityType, string aName, string aImageURL, string aImageHoverTextURL)
        {
            itemType = aItemType;
            rarity = aRarityType;
            name = aName;
            imageURL = aImageURL;
            imageHoverTextURL = aImageHoverTextURL;
        }

        public Tags(ItemType aItemType, RarityType aRarityType, string aName, string aDescription, string aImageURL, WeaponTags aWeaponTags)
        {
            itemType = aItemType;
            rarity = aRarityType;
            name = aName;
            description = aDescription;
            imageURL = aImageURL;
            weaponTags = aWeaponTags;
        }

        public Tags(ItemType aItemType, RarityType aRarityType, string aName, string aDescription, string aImageURL, CardTags aCardTags)
        {
            itemType = aItemType;
            rarity = aRarityType;
            name = aName;
            description = aDescription;
            imageURL = aImageURL;
            cardTags = aCardTags;

            //Populate the Tags.Description based on the cards affixes
            if (cardTags.prefix != null)
            {
                description += cardTags.prefix.description.Replace("#", cardTags.prefix.value.ToString());
            }
            if (cardTags.suffix != null)
            {
                description += cardTags.suffix.description.Replace("#", cardTags.suffix.value.ToString());
            }
            if (cardTags.thirdAffix != null)
            {
                description += cardTags.thirdAffix.description.Replace("#", cardTags.thirdAffix.value.ToString());
            }
        }

        public Tags(ItemType aItemType, RarityType aRarityType, string aName, string aDescription, string aImageURL, OutfitTags aOutfitTags)
        {
            itemType = aItemType;
            rarity = aRarityType;
            name = aName;
            description = aDescription;
            imageURL = aImageURL;
            outfitTags = aOutfitTags;
        }

        /**********************
        /* Constructors END   *
        /**********************/

        public class WeaponTags
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

            public Affix prefix;
            public Affix suffix;
            public Affix thirdAffix;
            public string specialStat;

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

            public class AttackTags
            {
                public string attackName;
                public string attackImageURL;
                public string attackImageHoverTextURL;
                public int attackDmgMin;
                public int attackDmgMax;

                /**********************
                /* Constructors START *
                /**********************/
                public AttackTags() { }

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
            public enum DivineWicked
            { 
                None,
                Divine,
                Wicked
            }

            public Affix prefix;
            public Affix suffix;
            public Affix thirdAffix;
            public int points = 0;
            public bool unique = false;
            public bool conditional = false;
            public DivineWicked divineWicked = DivineWicked.None;


            /**********************
            /* Constructors START *
            /**********************/
            public CardTags() { }

            public CardTags(int aPoints)
            {
                points = aPoints;
            }

            /**********************
            /* Constructors END   *
            /**********************/
        }

        public class OutfitTags
        {
            public int armor;
            public int critChance = 0;
            public int health = 0;
            public string urlOutfitBackgroundImage;

            public OutfitTags() { }

            public OutfitTags(int aArmor, string aUrlOutfitBackgroundImage)
            {
                armor = aArmor;
                urlOutfitBackgroundImage = aUrlOutfitBackgroundImage;
            }
        }
    }

    [Serializable]
    public class Affix
    {
        public enum Modifier
        {
            None,
            Health,
            HealthPercent,
            Armor,
            ArmorPenetration,
            CritChance,
            CritMulti,
            Damage,
            MeleeDamage,
            RangedDamage,
            Specific
        }

        public enum ModifierSpecific
        { 
            AttackHammerEnemyFullHealth,
            AttackRapierCharge,
            DestinyPoints
        }

        public string name;
        public Modifier modifier;
        public ModifierSpecific modifierSpecific;
        public int value;
        public string description;
        public string listBoxDisplay { get; set; }

        public Affix() { }

        public Affix(string aName, Modifier aModifier, int aValue, string aDescription)
        {
            name = aName;
            modifier = aModifier;
            value = aValue;
            description = aDescription.Replace("#", value.ToString());
            listBoxDisplay = description.Replace("\r\n", " | ");
        }
    }

    //public class Buff
    //{
    //    public string modifierName;
    //    public int value;

    //    public Buff() { }

    //    public Buff(string aModifierName, int aValue)
    //    {
    //        modifierName = aModifierName;
    //        value = aValue;
    //    }
    //}
}
