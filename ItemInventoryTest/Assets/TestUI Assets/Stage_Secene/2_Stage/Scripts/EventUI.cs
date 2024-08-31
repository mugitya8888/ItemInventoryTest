using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class EventUI:MonoBehaviour
    {
        private GameObject itemPickUpComent;
        private GameObject inventoryNotEmptyComent;
        private GameObject dildeCowGirlComent;
        // Start is called before the first frame update
        void Start()
        {
            itemPickUpComent = transform.GetChild(0).gameObject;
            inventoryNotEmptyComent = transform.GetChild(1).gameObject;
            dildeCowGirlComent = transform.GetChild(2).gameObject;
            Debug.Log(dildeCowGirlComent);

        }

        public void ShowHideItemPickUpComent(bool isActive)
        {
            itemPickUpComent.SetActive(isActive);
        }

        public void ShowHideInventoryNotEmptyComent(bool isActive)
        {
            inventoryNotEmptyComent.SetActive(isActive);
        }

        public void ShowHideDildeCowGirlComent(bool isActive)
        {
            dildeCowGirlComent.SetActive(isActive);
        }


    }
}
