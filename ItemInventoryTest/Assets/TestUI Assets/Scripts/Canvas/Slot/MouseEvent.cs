using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TestUI
{
    public class MouseEvent:MonoBehaviour
    {
        public GameObject inventory;
        private GameObject subMenu;
        private CanvasInventory canvasInventory;
        private Selectable[] selectables;

        private void Start()
        {
            subMenu = transform.GetChild(0).gameObject;
            canvasInventory = inventory.GetComponent<CanvasInventory>();

            //�eSlot�ȉ��ɂ���Selectable�I�u�W�F�N�g��S�Ď擾���āA�z��Ɋi�[����
            //[0]=Slot, [1]=UseButton [2]=DeleteButto�@���i�[����Ă���
            selectables = GetComponentsInChildren<Selectable>(true);         

        }

        public void OnSelect(bool isShow)
        {
            subMenu.SetActive(isShow);
        }

        public void MouseLeftClic()
        {            
            selectables[0].Select();
        }

    }
}
