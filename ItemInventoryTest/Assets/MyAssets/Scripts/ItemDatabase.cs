using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemInventoryTest
{
    public class ItemDatabase:MonoBehaviour
    {        
        public List<Item> items = new List<Item>();

        private void Start()
        {
            items.Add(new Item("IronSword", 0, "�S�̌�", "IronSword", 5, 3, Item.ItemType.Weapon));
            items.Add(new Item("Spellbook", 1, "������", "Spellbook", 0, 0, Item.ItemType.Quest));
        }
    }
}
