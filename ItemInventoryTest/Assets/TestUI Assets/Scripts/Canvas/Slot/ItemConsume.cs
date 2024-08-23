using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace TestUI
{
    public class ItemConsume:MonoBehaviour
    {
        private GameObject inventory;
        private CanvasInventory canvasInventory;
        public GameObject player;
        private PlayerInventory playerInventory;
        private Sprite itemIconSprite;
        

        //���ۑ��p�̎擾�ϐ�
        private GameObject checkItem;
        private Sprite checkItemIcon;
        private string checkItemTagName;
        private int checkItemID;
        private int checkStackNum;
        private string checkItemType;
        private int checkItemValue;


        private TextMeshProUGUI stackText;


        // Start is called before the first frame update
        void Start()
        {      
            playerInventory = GetComponent<PlayerInventory>();
            itemIconSprite = GetComponent<Sprite>();
        }
        private void GetItemData(Sprite icon)
        {

            if (icon == null) {
                Debug.Log("Not icon. This slot has no Item. ");
                return;
            }

            //�E�����A�C�e���̏����AItemInfo��tag���Ŏ擾����
            for (int i = 0; i < ItemDatabase.itemDatabase.Count; i++) {

                if (icon == ItemDatabase.itemDatabase[i].itemIcon) {

                    checkItemIcon = ItemDatabase.itemDatabase[i].itemIcon;
                    checkItem = ItemDatabase.itemDatabase[i].itemObject;
                    checkItemID = ItemDatabase.itemDatabase[i].itemID;
                    checkStackNum = ItemDatabase.itemDatabase[i].itemStackNum;
                    checkItemTagName = ItemDatabase.itemDatabase[i].itemName;
                    checkItemType = ItemDatabase.itemDatabase[i].itemType.ToString();
                    checkItemValue = ItemDatabase.itemDatabase[i].itemValue;
                }
            }
        }

        public void ItemUse()
        {
            //�{�^���������ꂽ�X���b�g�̃C���[�W������
            Image slotIconImage = GetComponent<Image>();
            Sprite icon = slotIconImage.sprite;

            //�K�v�ȏ���ItemDatabase����擾
            GetItemData(icon);

            int index = 0;
            Transform counts;
            int amount = PlayerInventory.currentItemAmount[checkItemID];
            Debug.Log("amount : " + amount);

            for (int i = 0; i < CanvasInventory.slotList.Count; i++) {

                if (gameObject == CanvasInventory.slotList[i]) {

                    //��O�����F�A�C�e���������X���b�g�ɑ΂��Ďg�����������ꍇ�A���������ɏI��
                    if (playerInventory.inventoryArray[i] == null) {
                        Debug.Log("no item");
                        return;
                    }
                    //���̃X���b�g��index���擾
                    index = i;
                    break;
                }
            }
            Debug.Log("index : " + index);
            //���I�u�W�F�N�g��TextMeshPro�R���|�[�l���g���擾
            counts = CanvasInventory.slotList[index].transform.GetChild(1);
            stackText = counts.GetChild(0).GetComponent<TextMeshProUGUI>();

            //�A�C�e�����X���b�g�Ɏc���Ă���Ƃ�
            if (amount > 1) {
                PlayerInventory.currentItemAmount[checkItemID] -= 1;
                stackText.text = PlayerInventory.currentItemAmount[checkItemID].ToString();
            }
            //�A�C�e���̎c�肪�Ō�̂P�̎�
            else if (amount == 1) {
                PlayerInventory.currentItemAmount[checkItemID] = 0;  //��������0�ɂ���
                stackText.text = amount.ToString();
                counts.gameObject.SetActive(false);
                playerInventory.inventoryArray[index] = null;  //inventory�z���index�Ԗڂ���ɂ���
                slotIconImage.sprite = null;  //slot��icon�C���[�W���폜
            }

            //�A�C�e�����Ƃ̗p�r�ɉ����āA���ʂ�ύX
            switch (checkItemType) {

            case "HEALING":
            PlayerState.IncreaseHP(checkItemValue);
            break;

            case "POWERUP":
            Debug.Log("power up");
            break;

            case "KEY":
            EventFlag.SetHasKey(true);
            //SetHasKey(true);
            //ShowFlag();
            Debug.Log("open the door");
            break;

            default:
            Debug.Log("default");
            break;
            }

        }

        public void ItemDelete()
        {
            for (int i = 0; i < CanvasInventory.slotList.Count; i++) {

                if (CanvasInventory.slotList[i] == gameObject) {
                    Image slotIcon = CanvasInventory.slotList[i].GetComponent<Image>();
                    if (slotIcon.sprite != null) {
                        slotIcon.sprite = null;
                        playerInventory.inventoryArray[i] = null;
                        return;
                    }

                }

            }
        }
    }
}
