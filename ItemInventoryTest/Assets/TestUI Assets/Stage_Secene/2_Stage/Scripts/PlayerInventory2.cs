using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

namespace TestUI
{
    public class PlayerInventory2:MonoBehaviour
    {
        //UI�֘A
        public CanvasInventory2 canvasInventory2;
        public EventUI eventUI;
        //public CanvasInventory canvasInventory;

        //Animator
        private Animator _animator;


        //PlayerInput�֘A
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
        //private bool isDilde = false;

        //���ۑ��p�̎擾�ϐ�
        private TextMeshProUGUI stackText = null;
        private Vector3 dildePos = Vector3.zero;


        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            for (int i = 0; i < maxIndex; i++) {
                inventoryItems[i] = null;
            }
        }


        private void OnTriggerStay(Collider other)
        {

            if (other.gameObject.CompareTag("Item")) {

                eventUI.ShowHideItemPickUpComent(true);


                if (_playerInput.actions["Interact"].IsPressed()) {

                    ItemInfo itemInfo = other.gameObject.GetComponent<ItemInfo>();

                    //�����A�C�e�������łɃC���x���g���Ɏ����Ă��邩�`�F�b�N
                    hasSameItem = HasSameItem(itemInfo.itemsObj);

                    //true
                    if (hasSameItem == true) {
                        //���̃A�C�e���������Ă���X���b�g�̒��ɃA�C�e��������
                        //->���݂̃X�^�b�N������擾�������Z���āA�C���x���g���ɕ\��
                        StackItem(itemInfo.itemsObj);
                        Destroy(other.gameObject);
                        eventUI.ShowHideItemPickUpComent(false);
                        eventUI.ShowHideInventoryNotEmptyComent(false);

                    }

                    //false
                    else {

                        //�X���b�g�ɋ󂫂����邩�`�F�b�N
                        CheckInventoryFull();

                        //���t�Ȃ�ǉ������ɁA�R�����g�̂ݕ\��
                        if (isInventoryFull) {
                            eventUI.ShowHideInventoryNotEmptyComent(true);
                            Debug.Log("Inventory is not empty");
                        }

                        //�󂢂Ă���Ȃ�A�A�C�e�����i�[
                        //InventoryList�ɃA�C�e����ǉ��AitemAmountArray�ɃA�C�e����������ǉ��B�z��̏��Ԃ̓A�C�e��ID�ɏ����B
                        else if (isInventoryFull == false) {

                            CheckInventorySlot(itemInfo.itemsObj);
                            Destroy(other.gameObject);
                            eventUI.ShowHideItemPickUpComent(false);
                            eventUI.ShowHideInventoryNotEmptyComent(false);

                        }

                    }

                }
            }
            else if (other.gameObject.CompareTag("Dilde")) {

                if (PlayerState.GetIsDilde() == false) {

                    eventUI.ShowHideDildeCowGirlComent(true);

                }


                if (_playerInput.actions["Interact"].IsPressed()) {
                    PlayerState.SetIsDilde(true);
                    eventUI.ShowHideDildeCowGirlComent(false);                    
                    SetDildePos(other.transform.position);

                }
            }

            else if (other.gameObject.CompareTag("Door")) {

                //Debug.Log("Near door");
            }


        }

        public void SetDildePos(Vector3 pos)
        {
            dildePos = pos;
        }

        public Vector3 GetDildePos()
        {
            return dildePos;
        }

        //�����A�C�e�������łɃC���x���g���Ɏ����Ă��邩�`�F�b�N���郁�\�b�h
        private bool HasSameItem(Items items)
        {
            for (int i = 0; i < canvasInventory2.slotList.Count; i++) {
                Image image = canvasInventory2.slotList[i].GetComponent<Image>();
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
                    Transform slot = canvasInventory2.slotList[i].transform.GetChild(1);
                    stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();

                    //�l�̃Z�b�g
                    currentItemAmount[items.itemID] += items.itemStackNum; //���݂̏��������X�g�̒l���X�V

                    stackText.text = currentItemAmount[items.itemID].ToString(); //UI�̃e�L�X�g�����������X�g�̒l�ɍX�V

                    slot.gameObject.SetActive(true); //��������UI��\��
                    break;
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Item")) {
                eventUI.ShowHideItemPickUpComent(false);
                eventUI.ShowHideInventoryNotEmptyComent(false);
            }
            else if (other.gameObject.CompareTag("Dilde")) {
                eventUI.ShowHideDildeCowGirlComent(false);
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

            for (int i = 0; i < canvasInventory2.slotList.Count; i++) {

                icon = canvasInventory2.slotList[i].GetComponent<Image>();
                Transform slot = canvasInventory2.slotList[i].transform.GetChild(1);
                stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();

                //icon ���Ȃ��X���b�g��������               
                if (icon.sprite == null) {
                    icon.sprite = items.itemIcon; //�A�C�R����ݒ�

                    //�A�C�e�������X�g�ɒǉ�
                    inventoryItems[i] = items;

                    currentItemAmount[items.itemID] = items.itemStackNum; //���݂̏��������X�g�̒l���X�V

                    stackText.text = currentItemAmount[items.itemID].ToString(); //UI�̃e�L�X�g�����������X�g�̒l�ɍX�V
                    slot.gameObject.SetActive(true); //��������UI��\��
                    break;
                }
            }

        }

        public void ItemUse(GameObject slotObj)
        {
            //�{�^���������ꂽ�X���b�g�̃C���[�W������
            Image slotIconImage = slotObj.GetComponent<Image>();

            //�K�v�ȏ���inventoryItem����擾
            Items items = CheckSlotItems(slotObj);

            if (items != null) {

                int index = 0;
                Transform counts;
                int amount = currentItemAmount[items.itemID];


                for (int i = 0; i < canvasInventory2.slotList.Count; i++) {

                    if (slotObj == canvasInventory2.slotList[i]) {

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
                counts = canvasInventory2.slotList[index].transform.GetChild(1);
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

            //�E�����A�C�e���̏����AItems�Ŏ擾����
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
            for (int i = 0; i < canvasInventory2.slotList.Count; i++) {

                if (canvasInventory2.slotList[i] == slotObj) {

                    Image slotIcon = canvasInventory2.slotList[i].GetComponent<Image>();
                    GameObject countsParent = slotObj.transform.GetChild(1).gameObject;
                    GameObject counts = countsParent.transform.GetChild(0).gameObject;
                    TextMeshProUGUI countsText = counts.GetComponent<TextMeshProUGUI>();

                    if (slotIcon.sprite != null) {
                        slotIcon.sprite = null;
                        int index = inventoryItems[i].itemID;
                        string t = countsText.GetComponent<TextMeshProUGUI>().text;
                        currentItemAmount[index] = 0;
                        inventoryItems[i] = null;
                        countsText.GetComponent<TextMeshProUGUI>().text = "0";
                        countsParent.SetActive(false);
                        return;
                    }

                }

            }
        }

        public void OnInventory()
        {
            if (canvasInventory2 == null) {
                Debug.Log("canvasInventory2 = null");
                return;
            }

            string currnetMap = _playerInput.currentActionMap.name;

            //���݂�map���uPlayer�v�Ȃ�UI�ɕύX
            if (currnetMap == mapPlayer) {
                canvasInventory2.ShowHideInventoryParent(true);
                canvasInventory2.ShowHideEquipmentParent(true);

                _playerInput.SwitchCurrentActionMap(mapUI);
            }

            //���݂�map���uUI�v�Ȃ�Player�ɕύX
            else if (currnetMap == mapUI) {
                canvasInventory2.ShowHideInventoryParent(false);
                canvasInventory2.ShowHideEquipmentParent(false);

                _playerInput.SwitchCurrentActionMap(mapPlayer);
            }
        }



        public void SetWeapon(GameObject slotObj)
        {
            Items item = CheckSlotItems(slotObj);

            if (item != null && item.itemType.ToString() == "WEAPON") {

                GameObject weaponSlot = CheckEmptyWeaponSlotList();

                if (weaponSlot == null) {
                    Debug.Log("Weapon Slot is Full");
                    return;
                }
                weaponSlot.GetComponent<Image>().sprite = item.itemIcon;
                EventFlag.SetHasHandgun(true);
            }
            else {
                Debug.Log("no weapon. can't soubi");
            }

        }

        private GameObject CheckEmptyWeaponSlotList()
        {
            GameObject weaponSlot = null;

            for (int i = 0; i < canvasInventory2.weaponSlotList.Count; i++) {

                Image icon = canvasInventory2.weaponSlotList[i].GetComponent<Image>();

                if (icon.sprite == null) {
                    Debug.Log(canvasInventory2.weaponSlotList[i]);
                    Debug.Log("i : " + i);
                    weaponSlot = canvasInventory2.weaponSlotList[i];
                    break;
                }
            }
            return weaponSlot;
        }

        private Items CheckSlotItems(GameObject slotObj)
        {
            Items items = null;

            for (int i = 0; i < canvasInventory2.slotList.Count; i++) {

                if (canvasInventory2.slotList[i] == slotObj) {
                    items = inventoryItems[i];
                }
            }
            return items;
        }

        public int GetCurrentItemAmount(int index)
        {
            if (currentItemAmount.Length > index && index >= 0) {

                return currentItemAmount[index];
            }
            return -100;
        }

        public void DecleaseBulletsAmount()
        {

            currentItemAmount[2] -= 1;

            for (int i = 0; i < inventoryItems.Length; i++) {

                Items item = inventoryItems[i];

                if (item != null) {

                    int ID = item.itemID;

                    if (ID == 2) {

                        GameObject count = canvasInventory2.slotList[i].transform.GetChild(1).GetChild(0).gameObject;
                        TextMeshProUGUI textMesh = count.GetComponent<TextMeshProUGUI>();
                        int num = int.Parse(textMesh.text);

                        if (currentItemAmount[ID] > 0) {
                            num--;
                            textMesh.text = num.ToString();

                        }
                        else if (currentItemAmount[ID] == 0) {
                            num = 0;
                            textMesh.text = num.ToString();
                        }

                    }

                }


            }


        }
    }
}
