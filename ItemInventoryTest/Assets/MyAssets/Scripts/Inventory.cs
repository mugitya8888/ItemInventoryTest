using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemInventoryTest
{
    public class Inventory:MonoBehaviour
    {
        public int slotsX = 5, slotsY = 4;
        public GUISkin skin;
        public List<Item> inventory = new List<Item>();
        public List<Item> slots = new List<Item>();
        private ItemDatabase database;
        private bool showInventory = false;
        private bool showTooltip = false;
        private string toolTip;

        private bool draggingItem = false;
        private Item draggedItem;
        private int prevIndex;

        public int slotSize = 80;
        public int lineSpacingX = 100, lineSpacingY = 100;


        // Start is called before the first frame update
        void Start()
        {

            for (int i = 0; i < (slotsX * slotsY); i++) {
                slots.Add(new Item());
                inventory.Add(new Item());
            }
            database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
            AddItem(1);
            AddItem(0);
            AddItem(1);
            AddItem(1);
            RemoveItem(1);

        }
        private void Update()
        {
            if (Input.GetButtonDown("Inventory")) {
                showInventory = !showInventory;
            }
        }

        private void OnGUI()
        {
            toolTip = "";
            GUI.skin = skin;
            if (showInventory) {

                DrawInventory();
                if (showTooltip) {

                    GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200),
                        toolTip,
                        skin.GetStyle("Tooltip"));
                }
            }
            if (draggingItem) {
                GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
            }

            

        }

        void DrawInventory()
        {
            Event e = Event.current;
            int i = 0;
            for (int y = 0; y < slotsY; y++) {

                for (int x = 0; x < slotsX; x++) {

                    Rect slotRect = new Rect(x * lineSpacingX, y * lineSpacingY, slotSize, slotSize);
                    GUI.Box(slotRect, "", skin.GetStyle("Slot"));
                    slots[i] = inventory[i];

                    if (slots[i].itemName != null) {

                        GUI.DrawTexture(slotRect, slots[i].itemIcon);

                        if (slotRect.Contains(e.mousePosition)) {

                            toolTip = CreatTooltip(slots[i]);
                            showTooltip = true;

                            if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem) {

                                prevIndex = i;
                                draggingItem = true;
                                draggedItem = slots[i];
                                inventory[i] = new Item();
                            }

                            if (e.type == EventType.MouseUp && draggingItem) {

                                inventory[prevIndex] = inventory[i];
                                inventory[i] = draggedItem;
                                draggingItem = false;
                                draggedItem = null;

                            }
                        }
                    }
                    else {
                        if (slotRect.Contains(e.mousePosition)) {

                            if (e.type == EventType.MouseUp && draggingItem) {
                                
                                inventory[i] = draggedItem;
                                draggingItem = false;
                                draggedItem = null;

                            }

                        }
                    }


                    if (toolTip == "") {
                        showTooltip = false;
                    }
                    i++;
                }
            }
        }

        string CreatTooltip(Item item)
        {
            toolTip = "<color=#ffffff>" + item.itemName + "</color>\n\n" + item.itemDesc;
            return toolTip;
        }

        void RemoveItem(int id)
        {
            for (int i = 0; i < inventory.Count; i++) {
                if (inventory[i].itemID == id) {
                    inventory[i] = new Item();
                    break;
                }
            }
        }

        void AddItem(int id)
        {
            for (int i = 0; i < inventory.Count; i++) {

                if (inventory[i].itemName == null) {

                    for (int j = 0; j < database.items.Count; j++) {

                        if (database.items[j].itemID == id) {

                            inventory[i] = database.items[j];
                        }

                    }

                    break;

                }


            }
        }

        bool HasInventoryContains(int id)
        {
            bool result = false;
            for (int i = 0; i < inventory.Count; i++) {

                if (inventory[i].itemID == id) {
                    result = true;
                    break;
                }

            }
            return result;

        }
    }
}

