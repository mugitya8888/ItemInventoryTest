using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    [CreateAssetMenu(menuName = "CreateAsset/Items")]
    public class Items:ScriptableObject
    {
        public enum ItemType
        {
            WEAPON,
            HEALING,
            BULLET,
            POWERUP,
            KEY
        }

        public GameObject itemObject;
        public string itemName;
        public int itemID;
        public Sprite itemIcon;
        public string itemDocument;
        public ItemType itemType;
        public int itemStackNum;
        public int itemValue;

        public Items(GameObject obj,string name, int id, Sprite icon, string document, ItemType type, int stackLimit, int value)
        {
            itemObject = obj;
            itemName = name;
            itemID = id;
            itemIcon = icon;
            itemDocument = document;
            itemType = type;
            itemStackNum = stackLimit;
            itemValue = value;
        }

    }
}
