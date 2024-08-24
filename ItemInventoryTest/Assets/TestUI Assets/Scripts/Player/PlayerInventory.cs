using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TestUI
{
    public class PlayerInventory:MonoBehaviour
    {
        //UI�֘A
        public GameObject pickUpComment;
        public GameObject inventoryNotEmptyComent;
        public GameObject inventoryCanvas;
        public GameObject inventoryParent;
        protected GameObject inventory;
        public CanvasInventory canvasInventory;


        //PlayerInput�֘A
        [SerializeField]
        private PlayerInput _playerInput;
        private string mapPlayer = "Player";
        private string mapUI = "UI";


        //�C���x���g���֘A
        private int maxIndex = 6;      
        public static int[] currentItemAmount = new int[4];
        public static Items[] inventoryItems = new Items[6];

        //Flag
        private bool isInventoryFull;
        private bool hasSameItem = false;

        //���ۑ��p�̎擾�ϐ�
        
        protected TextMeshProUGUI stackText = null;

        private void Start()
        {
            for (int i = 0; i < maxIndex; i++) {
                inventoryItems[i] = null;
            }
        }


        private void OnTriggerStay(Collider other)
        {
            bool isItem = other.gameObject.CompareTag("Item");

            if (isItem) {

                pickUpComment.SetActive(true);

                if (Input.GetButtonDown("PickUp")) {

                    ItemInfo itemInfo = other.gameObject.GetComponent<ItemInfo>();

                    //�����A�C�e�������łɃC���x���g���Ɏ����Ă��邩�`�F�b�N
                    hasSameItem = HasSameItem(itemInfo.itemsObj);
                    Debug.Log("hasSameItem" + hasSameItem);


                    //true
                    if (hasSameItem == true) {
                        //���̃A�C�e���������Ă���X���b�g�̒��ɃA�C�e��������
                        //->���݂̃X�^�b�N������擾�������Z���āA�C���x���g���ɕ\��
                        StackItem(itemInfo.itemsObj);
                        Destroy(other.gameObject);
                        pickUpComment.SetActive(false);
                        inventoryNotEmptyComent.SetActive(false);
                    }

                    //false
                    else {

                        //�X���b�g�ɋ󂫂����邩�`�F�b�N
                        CheckInventoryFull();

                        //���t�Ȃ�ǉ������ɁA�R�����g�̂ݕ\��
                        if (isInventoryFull) {
                            inventoryNotEmptyComent.SetActive(true);
                            Debug.Log("Inventory is not empty");
                        }

                        //�󂢂Ă���Ȃ�A�A�C�e�����i�[
                        //InventoryList�ɃA�C�e����ǉ��AitemAmountArray�ɃA�C�e����������ǉ��B�z��̏��Ԃ̓A�C�e��ID�ɏ����B
                        else if (isInventoryFull == false) {

                            CheckInventorySlot(itemInfo.itemsObj);
                            Destroy(other.gameObject);
                            pickUpComment.SetActive(false);
                            inventoryNotEmptyComent.SetActive(false);

                        }

                    }
                    
                }
            }

            else if (other.gameObject.CompareTag("Door")) {

                //Debug.Log("Near door");
            }


        }

        private Items HasInventoryItems()
        {
            for (int i = 0; i < inventoryItems.Length; i++) {

                if (inventoryItems[i] != null) {
                    return inventoryItems[i];
                }
            }
            return null;
        }

        //�����A�C�e�������łɃC���x���g���Ɏ����Ă��邩�`�F�b�N���郁�\�b�h
        private bool HasSameItem(Items items)
        {
            for (int i = 0; i < canvasInventory.slotList.Count; i++) {
                Image image = canvasInventory.slotList[i].GetComponent<Image>();
                //�C���x���g�����ɓ����A�C�e�����������ꍇ
                if (image.sprite == items.itemIcon) {
                    return true;
                }
            }

            return false;
        }

        private void StackItem(Items items)
        {
            for (int i = 0; i < inventoryItems.Length; i++) {
                //�C���x���g�����ɓ����A�C�e�����������ꍇ
                if (items.itemName == inventoryItems[i].itemName) {
                    //�Q�ƒl���擾
                    Transform slot = canvasInventory.slotList[i].transform.GetChild(1);
                    stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();
                    //�l�̃Z�b�g
                    Debug.Log("StackItem() : items.itemStackNum : " + items.itemStackNum);

                    currentItemAmount[items.itemID] += items.itemStackNum; //���݂̏��������X�g�̒l���X�V

                    Debug.Log("StackItem() : currentItemAmount[items.itemID] : " + currentItemAmount[items.itemID]);

                    stackText.text = currentItemAmount[items.itemID].ToString(); //UI�̃e�L�X�g�����������X�g�̒l�ɍX�V
                    slot.gameObject.SetActive(true); //��������UI��\��
                    break;
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Item")) {
                pickUpComment.SetActive(false);
                inventoryNotEmptyComent.SetActive(false);
            }

        }

        //Inventory�̋󂫂����邩�`�F�b�N���郁�\�b�h
        private void CheckInventoryFull()
        {
            for (int i = 0; i < inventoryItems.Length; i++) {

                if (inventoryItems[i] == null) {
                    isInventoryFull = false;
                    return;
                }
            }
            isInventoryFull = true;
        }

        //�󂢂Ă���X���b�g�ɃA�C�e���̃A�C�R�������� + �X�^�b�N����\��
        private void CheckInventorySlot(Items items)
        {
            Image icon;

            for (int i = 0; i < canvasInventory.slotList.Count; i++) {

                icon = canvasInventory.slotList[i].GetComponent<Image>();
                Transform slot = canvasInventory.slotList[i].transform.GetChild(1);
                stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();

                //icon ���Ȃ��X���b�g��������               
                if (icon.sprite == null) {
                    icon.sprite = items.itemIcon; //�A�C�R����ݒ�

                    //�A�C�e�������X�g�ɒǉ�
                    inventoryItems[i] = items;
                    Debug.Log(inventoryItems[i]);

                    Debug.Log("CheckInventorySlot() : items.itemStackNum : " + items.itemStackNum);

                    currentItemAmount[items.itemID] = items.itemStackNum; //���݂̏��������X�g�̒l���X�V

                    Debug.Log("CheckInventorySlot() : currentItemAmount[items.itemID] : " + currentItemAmount[items.itemID]);

                    stackText.text = currentItemAmount[items.itemID].ToString(); //UI�̃e�L�X�g�����������X�g�̒l�ɍX�V
                    slot.gameObject.SetActive(true); //��������UI��\��

                    Debug.Log("Last" + inventoryItems[i]);
                    break;
                }
            }

        }

        public void ItemUse(GameObject slotObj)
        {
            //�{�^���������ꂽ�X���b�g�̃C���[�W������
            Image slotIconImage = slotObj.GetComponent<Image>();
            Sprite icon = slotIconImage.sprite;

            //�K�v�ȏ���inventoryItem����擾
            Items items = GetItemData(icon);

            if (items != null) {

                int index = 0;
                Transform counts;
                int amount = currentItemAmount[items.itemID];
               

                for (int i = 0; i < canvasInventory.slotList.Count; i++) {
                    
                    if (slotObj == canvasInventory.slotList[i]) {

                        //��O�����F�A�C�e���������X���b�g�ɑ΂��Ďg�����������ꍇ�A���������ɏI��
                        if (inventoryItems[i] == null) {
                            Debug.Log("no item");
                            return;
                        }
                        //���̃X���b�g��index���擾
                        index = i;
                        break;
                    }
                }
                
                //���I�u�W�F�N�g��TextMeshPro�R���|�[�l���g���擾
                counts = canvasInventory.slotList[index].transform.GetChild(1);
                stackText = counts.GetChild(0).GetComponent<TextMeshProUGUI>();

                //�A�C�e�����X���b�g�Ɏc���Ă���Ƃ�
                if (amount > 1) {
                    currentItemAmount[items.itemID] -= 1;
                    stackText.text = currentItemAmount[items.itemID].ToString();
                }
                //�A�C�e���̎c�肪�Ō�̂P�̎�
                else if (amount == 1) {
                    currentItemAmount[items.itemID] = 0;  //��������0�ɂ���
                    stackText.text = amount.ToString();
                    counts.gameObject.SetActive(false);
                    inventoryItems[index] = null;  //inventory�z���index�Ԗڂ���ɂ���
                    slotIconImage.sprite = null;  //slot��icon�C���[�W���폜
                }

                //�A�C�e�����Ƃ̗p�r�ɉ����āA���ʂ�ύX
                switch (items.itemType.ToString()) {

                case "HEALING":
                PlayerState.IncreaseHP(items.itemValue);
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



        }

         private Items GetItemData(Sprite icon)
        {
            Items item = null;
            if (icon == null) {
                Debug.Log("Not icon. This slot has no Item. ");
                return null;
            }

            //�E�����A�C�e���̏����AItemInfo��tag���Ŏ擾����
            for (int i = 0; i < inventoryItems.Length; i++) {

                if (inventoryItems[i].itemIcon == icon) {
                    item = inventoryItems[i];
                    break;
                }
            }
            return item;
            
        }

        public void ItemDelete(GameObject slotObj)
        {
            for (int i = 0; i < canvasInventory.slotList.Count; i++) {

                if (canvasInventory.slotList[i] == slotObj) {

                    Image slotIcon = canvasInventory.slotList[i].GetComponent<Image>();

                    if (slotIcon.sprite != null) {
                        slotIcon.sprite = null;
                        inventoryItems[i] = null;
                        return;
                    }

                }

            }
        }

        public void OnInventory()
        {
            if (inventoryParent == null)
                return;

            string currnetMap = _playerInput.currentActionMap.name;

            //���݂�map���uPlayer�v�Ȃ�UI�ɕύX
            if (currnetMap == mapPlayer) {
                inventoryParent.SetActive(true);
                _playerInput.SwitchCurrentActionMap(mapUI);
            }

            //���݂�map���uUI�v�Ȃ�Player�ɕύX
            else if (currnetMap == mapUI) {
                inventoryParent.SetActive(false);
                _playerInput.SwitchCurrentActionMap(mapPlayer);
            }
        }
    }
}
