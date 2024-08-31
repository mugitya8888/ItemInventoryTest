using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class CanvasInventory2:MonoBehaviour
    {
        public List<GameObject> slotFrameList = new List<GameObject>();
        public List<GameObject> slotList = new List<GameObject>();

        public List<GameObject> weaponSlotFrameList = new List<GameObject>();
        public List<GameObject> weaponSlotList = new List<GameObject>();

        private GameObject inventoryParent;
        private GameObject equipmentParent;
        private GameObject menuParent;


        // Start is called before the first frame update
        void Start()
        {
            InitializeInventorySlotList();
            InitializeWeaponSlotList();

            inventoryParent = transform.GetChild(0).gameObject;
            equipmentParent = transform.GetChild(1).gameObject;
            menuParent = transform.GetChild(2).gameObject;            

        }

        private void InitializeWeaponSlotList()
        {
            Transform UIParent = transform.GetChild(1);
            for (int i = 0; i < UIParent.childCount; i++) {
                weaponSlotFrameList.Add(UIParent.GetChild(i).gameObject);
            }
            //Slotオブジェクトをリストに格納する
            for (int j = 0; j < weaponSlotFrameList.Count; j++) {
                weaponSlotList.Add(weaponSlotFrameList[j].transform.GetChild(0).gameObject);
            }
        }

        private void InitializeInventorySlotList()
        {
            Transform UIParent = transform.GetChild(0);
            //SlotFrameオブジェクトをリストに格納する
            for (int i = 0; i < UIParent.childCount; i++) {
                slotFrameList.Add(UIParent.GetChild(i).gameObject);
            }

            //Slotオブジェクトをリストに格納する
            for (int j = 0; j < slotFrameList.Count; j++) {
                slotList.Add(slotFrameList[j].transform.GetChild(0).gameObject);
            }
        }

        public void ShowHideInventoryParent(bool isActive)
        {
            inventoryParent.SetActive(isActive);
        }

        public void ShowHideEquipmentParent(bool isActive)
        {
            equipmentParent.SetActive(isActive);
        }

        public void ShowHidMenuParent(bool isActive)
        {
            menuParent.SetActive(isActive);
        }
    }
}
