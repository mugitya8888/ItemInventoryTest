using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestUI
{
    public class CanvasInventory:MonoBehaviour
    {        
        public List<GameObject> slotFrameList = new List<GameObject>();
        public List<GameObject> slotList = new List<GameObject>();

        public List<GameObject> weaponSlotFrameList = new List<GameObject>();
        public List<GameObject> weaponSlotList = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            InitializeInventorySlotList();
            InitializeWeaponSlotList();

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
    }
}
