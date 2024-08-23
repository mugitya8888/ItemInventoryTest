using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestUI
{
    public class CanvasInventory:MonoBehaviour
    {
        private Transform inventoryParent;
        public static List<GameObject> slotFrameList = new List<GameObject>();
        public static List<GameObject> slotList = new List<GameObject>();        

        // Start is called before the first frame update
         void Start()
        {
            inventoryParent = transform.GetChild(0);
            //SlotFrame�I�u�W�F�N�g�����X�g�Ɋi�[����
            for (int i = 0; i < inventoryParent.childCount; i++) {
                slotFrameList.Add(inventoryParent.GetChild(i).gameObject);
            }

            //Slot�I�u�W�F�N�g�����X�g�Ɋi�[����
            for (int j = 0; j < slotFrameList.Count; j++) {
                slotList.Add(slotFrameList[j].transform.GetChild(0).gameObject);
            }

        }



    }
}
