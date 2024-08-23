using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemInventoryTest
{
    [Serializable]
    public class Item
    {
        public string itemName;
        public int itemID;
        public string itemDesc;
        public Texture2D itemIcon;
        public int itemPower;
        public int itemSpeed;
        public ItemType itemType;

        public enum ItemType
        {
            Weapon,
            Consumable,
            Quest
        }

        

        public Item(string name, int id, string desc, string icon, int power, int speed, ItemType type)
        {
            itemName = name;
            itemID = id;
            itemDesc = desc;
            itemIcon = Resources.Load<Texture2D>("ItemIcon/" + name);
            itemPower = power;
            itemSpeed = speed;
            itemType = type;
        }

        public Item()
        {
        }
    }
}
