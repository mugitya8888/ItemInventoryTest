using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class Equipment:MonoBehaviour
    {
        private Transform equipmentParent;
        private List<GameObject> equipmentSlotFrameList = new List<GameObject>();
        public List<GameObject> equipmentSlotList = new List<GameObject>();

        void Start()
        {
            equipmentParent = transform.GetChild(2);

            for (int i = 0; i < equipmentParent.childCount; i++) {
                equipmentSlotFrameList.Add(equipmentParent.GetChild(i).gameObject);
            }

            for (int j = 0; j < equipmentSlotFrameList.Count; j++) {
                equipmentSlotList.Add(equipmentSlotFrameList[j].transform.GetChild(0).gameObject);
            }

        }

        public void SetSlotWeapon()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
