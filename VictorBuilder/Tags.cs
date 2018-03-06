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

            public WeaponType weapon;

            public WeaponTags(WeaponType aWeapon)
            {
                weapon = aWeapon;
            }
        }
    }
}
