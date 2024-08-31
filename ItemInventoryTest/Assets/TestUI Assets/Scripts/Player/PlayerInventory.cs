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
using static UnityEngine.Rendering.DebugUI;

namespace TestUI
{
    public class PlayerInventory:MonoBehaviour
    {
        //UI関連
        public GameObject pickUpComment;
        public GameObject inventoryNotEmptyComent;
        public GameObject inventoryCanvas;
        public GameObject inventoryParent;
        public GameObject equipmentParent;
        public CanvasInventory canvasInventory;



        //PlayerInput関連
        [SerializeField]
        private PlayerInput _playerInput;
        private string mapPlayer = "Player";
        private string mapUI = "UI";


        //インベントリ関連
        private int maxIndex = 6;
        public static int[] currentItemAmount = new int[4];
        public static Items[] inventoryItems = new Items[6];

        //Flag
        private bool isInventoryFull;
        private bool hasSameItem = false;

        //仮保存用の取得変数

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

                if (_playerInput.actions["Interact"].IsPressed()) {

                    ItemInfo itemInfo = other.gameObject.GetComponent<ItemInfo>();

                    //同じアイテムをすでにインベントリに持っているかチェック
                    hasSameItem = HasSameItem(itemInfo.itemsObj);

                    //true
                    if (hasSameItem == true) {
                        //そのアイテムが入っているスロットの中にアイテムを入れる
                        //->現在のスタック数から取得数を加算して、インベントリに表示
                        StackItem(itemInfo.itemsObj);
                        Destroy(other.gameObject);
                        pickUpComment.SetActive(false);
                        inventoryNotEmptyComent.SetActive(false);
                    }

                    //false
                    else {

                        //スロットに空きがあるかチェック
                        CheckInventoryFull();

                        //満杯なら追加せずに、コメントのみ表示
                        if (isInventoryFull) {
                            inventoryNotEmptyComent.SetActive(true);
                            Debug.Log("Inventory is not empty");
                        }

                        //空いているなら、アイテムを格納
                        //InventoryListにアイテムを追加、itemAmountArrayにアイテム所持数を追加。配列の順番はアイテムIDに準拠。
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

        //同じアイテムをすでにインベントリに持っているかチェックするメソッド
        private bool HasSameItem(Items items)
        {
            for (int i = 0; i < canvasInventory.slotList.Count; i++) {
                Image image = canvasInventory.slotList[i].GetComponent<Image>();
                //インベントリ内に同じアイテムがあった場合
                if (image.sprite == items.itemIcon) {
                    return true;
                }
            }

            return false;
        }

        private void StackItem(Items items)
        {
            for (int i = 0; i < inventoryItems.Length; i++) {
                //インベントリ内に同じアイテムがあった場合
                if (items.itemName == inventoryItems[i].itemName) {
                    //参照値を取得
                    Transform slot = canvasInventory.slotList[i].transform.GetChild(1);
                    stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();

                    //値のセット
                    currentItemAmount[items.itemID] += items.itemStackNum; //現在の所持数リストの値を更新

                    stackText.text = currentItemAmount[items.itemID].ToString(); //UIのテキストを所持数リストの値に更新

                    slot.gameObject.SetActive(true); //所持数のUIを表示
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

        //Inventoryの空きがあるかチェックするメソッド
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

        //空いているスロットにアイテムのアイコンを入れる + スタック数を表示
        private void CheckInventorySlot(Items items)
        {
            Image icon;

            for (int i = 0; i < canvasInventory.slotList.Count; i++) {

                icon = canvasInventory.slotList[i].GetComponent<Image>();
                Transform slot = canvasInventory.slotList[i].transform.GetChild(1);
                stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();

                //icon がないスロットを見つけて               
                if (icon.sprite == null) {
                    icon.sprite = items.itemIcon; //アイコンを設定

                    //アイテムをリストに追加
                    inventoryItems[i] = items;

                    currentItemAmount[items.itemID] = items.itemStackNum; //現在の所持数リストの値を更新

                    stackText.text = currentItemAmount[items.itemID].ToString(); //UIのテキストを所持数リストの値に更新
                    slot.gameObject.SetActive(true); //所持数のUIを表示
                    break;
                }
            }

        }

        public void ItemUse(GameObject slotObj)
        {
            //ボタンが押されたスロットのイメージを所得
            Image slotIconImage = slotObj.GetComponent<Image>();

            //必要な情報をinventoryItemから取得
            Items items = CheckSlotItems(slotObj);

            if (items != null) {

                int index = 0;
                Transform counts;
                int amount = currentItemAmount[items.itemID];


                for (int i = 0; i < canvasInventory.slotList.Count; i++) {

                    if (slotObj == canvasInventory.slotList[i]) {

                        //例外処理：アイテムが無いスロットに対して使うを押した場合、何もせずに終了
                        if (inventoryItems[i] == null) {
                            Debug.Log("no item");
                            return;
                        }
                        //このスロットのindexを取得
                        index = i;
                        break;
                    }
                }

                //孫オブジェクトのTextMeshProコンポーネントを取得
                counts = canvasInventory.slotList[index].transform.GetChild(1);
                stackText = counts.GetChild(0).GetComponent<TextMeshProUGUI>();

                //アイテムがスロットに残っているとき
                if (amount > 1) {
                    currentItemAmount[items.itemID] -= 1;
                    stackText.text = currentItemAmount[items.itemID].ToString();
                }
                //アイテムの残りが最後の１個の時
                else if (amount == 1) {
                    currentItemAmount[items.itemID] = 0;  //所持数を0にする
                    stackText.text = amount.ToString();
                    counts.gameObject.SetActive(false);
                    inventoryItems[index] = null;  //inventory配列のindex番目を空にする
                    slotIconImage.sprite = null;  //slotのiconイメージを削除
                }

                //アイテムごとの用途に応じて、効果を変更
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

            //拾ったアイテムの情報を、Itemsで取得する
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
            if (inventoryParent == null)
                return;

            string currnetMap = _playerInput.currentActionMap.name;

            //現在のmapが「Player」ならUIに変更
            if (currnetMap == mapPlayer) {
                inventoryParent.SetActive(true);
                equipmentParent.SetActive(true);
                _playerInput.SwitchCurrentActionMap(mapUI);
            }

            //現在のmapが「UI」ならPlayerに変更
            else if (currnetMap == mapUI) {
                inventoryParent.SetActive(false);
                equipmentParent.SetActive(false);
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

            for (int i = 0; i < canvasInventory.weaponSlotList.Count; i++) {

                Image icon = canvasInventory.weaponSlotList[i].GetComponent<Image>();

                if (icon.sprite == null) {
                    Debug.Log(canvasInventory.weaponSlotList[i]);
                    Debug.Log("i : " + i);
                    weaponSlot = canvasInventory.weaponSlotList[i];
                    break;
                }
            }
            return weaponSlot;
        }

        private Items CheckSlotItems(GameObject slotObj)
        {
            Items items = null;

            for (int i = 0; i < canvasInventory.slotList.Count; i++) {

                if (canvasInventory.slotList[i] == slotObj) {
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

                        GameObject count = canvasInventory.slotList[i].transform.GetChild(1).GetChild(0).gameObject;
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
