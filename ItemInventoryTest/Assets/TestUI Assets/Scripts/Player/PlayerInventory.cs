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
        public GameObject itemDatabase;
        public GameObject inventoryCanvas;
        public GameObject inventoryParent;
        private GameObject inventory;
        private CanvasInventory canvasInventory;


        //PlayerInput�֘A
        public PlayerInput _playerInput;
        private string mapPlayer = "Player";
        private string mapUI = "UI";


        //�C���x���g���֘A
        private int maxIndex = 6;
        public GameObject[] inventoryArray = new GameObject[6];
        public ItemInfo[] itemArray = new ItemInfo[6];
        public static int[] currentItemAmount = new int[4];

        //Flag
        private bool isInventoryFull;
        private bool hasSameItem = false;

        //���ۑ��p�̎擾�ϐ�
        private GameObject checkItem;
        private Sprite checkItemIcon;
        private string checkItemTagName;
        private int checkItemID;
        private int checkStackNum;
        TextMeshProUGUI stackText = null;

        private void Start()
        {
            for (int i = 0; i < maxIndex; i++) {
                inventoryArray[i] = null;
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Item")) {

                pickUpComment.SetActive(true);

                if (Input.GetButtonDown("PickUp")) {


                    //Database������������o�����߂̌��Ƃ��āAtagName���擾                    
                    checkItemTagName = other.gameObject.GetComponent<ItemInfo>().GetTagName();
                    //Database����K�v�ȏ����擾
                    GetItemData();

                    //�����A�C�e�������łɃC���x���g���Ɏ����Ă��邩�`�F�b�N
                    hasSameItem = HasSameItem();
                    Debug.Log("hasSameItem" + hasSameItem);


                    //true
                    if (hasSameItem == true) {
                        //���̃A�C�e���������Ă���X���b�g�̒��ɃA�C�e��������
                        //->���݂̃X�^�b�N������擾�������Z���āA�C���x���g���ɕ\��
                        StackItem();
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

                            CheckInventorySlot();
                            Destroy(other.gameObject);
                            pickUpComment.SetActive(false);
                            inventoryNotEmptyComent.SetActive(false);

                        }

                    }
                    //�O���[�o���ϐ��̏�����
                    //Debug.Log("initializeValue");
                    InitializeValue();
                }
            }

            else if (other.gameObject.CompareTag("Door")) {

                //Debug.Log("Near door");
            }


        }

        //�����A�C�e�������łɃC���x���g���Ɏ����Ă��邩�`�F�b�N���郁�\�b�h
        private bool HasSameItem()
        {
            for (int i = 0; i < CanvasInventory.slotList.Count; i++) {
                Image image = CanvasInventory.slotList[i].GetComponent<Image>();
                //�C���x���g�����ɓ����A�C�e�����������ꍇ
                if (image.sprite == checkItemIcon) {
                    return true;
                }
            }

            return false;
        }

        private void StackItem()
        {
            for (int i = 0; i < inventoryArray.Length; i++) {
                //�C���x���g�����ɓ����A�C�e�����������ꍇ
                if (checkItemTagName == itemArray[i].GetTagName()) {
                    //�Q�ƒl���擾
                    Transform slot = CanvasInventory.slotList[i].transform.GetChild(1);
                    stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();
                    //�l�̃Z�b�g
                    Debug.Log("StackItem() : checkStackNum : " + checkStackNum);

                    currentItemAmount[checkItemID] += checkStackNum; //���݂̏��������X�g�̒l���X�V

                    Debug.Log("StackItem() : currentItemAmount[checkItemID] : " + currentItemAmount[checkItemID]);

                    stackText.text = currentItemAmount[checkItemID].ToString(); //UI�̃e�L�X�g�����������X�g�̒l�ɍX�V
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

        private void GetItemData()
        {

            //�E�����A�C�e���̏����AItemInfo��tag���Ŏ擾����
            for (int i = 0; i < ItemDatabase.itemDatabase.Count; i++) {

                if (checkItemTagName == ItemDatabase.itemDatabase[i].itemName) {

                    checkItemIcon = ItemDatabase.itemDatabase[i].itemIcon;
                    checkItem = ItemDatabase.itemDatabase[i].itemObject;
                    checkItemID = ItemDatabase.itemDatabase[i].itemID;
                    checkStackNum = ItemDatabase.itemDatabase[i].itemStackNum;
                }
            }

        }



        //Inventory�̋󂫂����邩�`�F�b�N���郁�\�b�h
        private void CheckInventoryFull()
        {
            for (int i = 0; i < inventoryArray.Length; i++) {

                if (inventoryArray[i] == null) {
                    isInventoryFull = false;
                    return;
                }
            }
            isInventoryFull = true;
        }

        //�󂢂Ă���X���b�g�ɃA�C�e���̃A�C�R�������� + �X�^�b�N����\��
        private void CheckInventorySlot()
        {
            Image icon;

            for (int i = 0; i < CanvasInventory.slotList.Count; i++) {

                icon = CanvasInventory.slotList[i].GetComponent<Image>();
                Transform slot = CanvasInventory.slotList[i].transform.GetChild(1);
                stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();

                //icon ���Ȃ��X���b�g��������               
                if (icon.sprite == null) {
                    icon.sprite = checkItemIcon; //�A�C�R����ݒ�

                    //�A�C�e�������X�g�ɒǉ�
                    inventoryArray[i] = checkItem;
                    itemArray[i] = inventoryArray[i].GetComponent<ItemInfo>();

                    Debug.Log("CheckInventorySlot() : checkStackNum : " + checkStackNum);

                    currentItemAmount[checkItemID] = checkStackNum; //���݂̏��������X�g�̒l���X�V

                    Debug.Log("CheckInventorySlot() : currentItemAmount[checkItemID] : " + currentItemAmount[checkItemID]);

                    stackText.text = currentItemAmount[checkItemID].ToString(); //UI�̃e�L�X�g�����������X�g�̒l�ɍX�V
                    slot.gameObject.SetActive(true); //��������UI��\��
                    break;
                }
            }

        }

        private void InitializeValue()
        {
            checkItem = null;
            checkItemIcon = null;
            checkItemTagName = null;
            checkItemID = 0;
            checkStackNum = 0;
            stackText = null;
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
