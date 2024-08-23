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
        //UI関連
        public GameObject pickUpComment;
        public GameObject inventoryNotEmptyComent;
        public GameObject itemDatabase;
        public GameObject inventoryCanvas;
        public GameObject inventoryParent;
        private GameObject inventory;
        private CanvasInventory canvasInventory;


        //PlayerInput関連
        public PlayerInput _playerInput;
        private string mapPlayer = "Player";
        private string mapUI = "UI";


        //インベントリ関連
        private int maxIndex = 6;
        public GameObject[] inventoryArray = new GameObject[6];
        public ItemInfo[] itemArray = new ItemInfo[6];
        public static int[] currentItemAmount = new int[4];

        //Flag
        private bool isInventoryFull;
        private bool hasSameItem = false;

        //仮保存用の取得変数
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


                    //Databaseから情報を引き出すための鍵として、tagNameを取得                    
                    checkItemTagName = other.gameObject.GetComponent<ItemInfo>().GetTagName();
                    //Databaseから必要な情報を取得
                    GetItemData();

                    //同じアイテムをすでにインベントリに持っているかチェック
                    hasSameItem = HasSameItem();
                    Debug.Log("hasSameItem" + hasSameItem);


                    //true
                    if (hasSameItem == true) {
                        //そのアイテムが入っているスロットの中にアイテムを入れる
                        //->現在のスタック数から取得数を加算して、インベントリに表示
                        StackItem();
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

                            CheckInventorySlot();
                            Destroy(other.gameObject);
                            pickUpComment.SetActive(false);
                            inventoryNotEmptyComent.SetActive(false);

                        }

                    }
                    //グローバル変数の初期化
                    //Debug.Log("initializeValue");
                    InitializeValue();
                }
            }

            else if (other.gameObject.CompareTag("Door")) {

                //Debug.Log("Near door");
            }


        }

        //同じアイテムをすでにインベントリに持っているかチェックするメソッド
        private bool HasSameItem()
        {
            for (int i = 0; i < CanvasInventory.slotList.Count; i++) {
                Image image = CanvasInventory.slotList[i].GetComponent<Image>();
                //インベントリ内に同じアイテムがあった場合
                if (image.sprite == checkItemIcon) {
                    return true;
                }
            }

            return false;
        }

        private void StackItem()
        {
            for (int i = 0; i < inventoryArray.Length; i++) {
                //インベントリ内に同じアイテムがあった場合
                if (checkItemTagName == itemArray[i].GetTagName()) {
                    //参照値を取得
                    Transform slot = CanvasInventory.slotList[i].transform.GetChild(1);
                    stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();
                    //値のセット
                    Debug.Log("StackItem() : checkStackNum : " + checkStackNum);

                    currentItemAmount[checkItemID] += checkStackNum; //現在の所持数リストの値を更新

                    Debug.Log("StackItem() : currentItemAmount[checkItemID] : " + currentItemAmount[checkItemID]);

                    stackText.text = currentItemAmount[checkItemID].ToString(); //UIのテキストを所持数リストの値に更新
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

        private void GetItemData()
        {

            //拾ったアイテムの情報を、ItemInfoのtag名で取得する
            for (int i = 0; i < ItemDatabase.itemDatabase.Count; i++) {

                if (checkItemTagName == ItemDatabase.itemDatabase[i].itemName) {

                    checkItemIcon = ItemDatabase.itemDatabase[i].itemIcon;
                    checkItem = ItemDatabase.itemDatabase[i].itemObject;
                    checkItemID = ItemDatabase.itemDatabase[i].itemID;
                    checkStackNum = ItemDatabase.itemDatabase[i].itemStackNum;
                }
            }

        }



        //Inventoryの空きがあるかチェックするメソッド
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

        //空いているスロットにアイテムのアイコンを入れる + スタック数を表示
        private void CheckInventorySlot()
        {
            Image icon;

            for (int i = 0; i < CanvasInventory.slotList.Count; i++) {

                icon = CanvasInventory.slotList[i].GetComponent<Image>();
                Transform slot = CanvasInventory.slotList[i].transform.GetChild(1);
                stackText = slot.GetChild(0).GetComponent<TextMeshProUGUI>();

                //icon がないスロットを見つけて               
                if (icon.sprite == null) {
                    icon.sprite = checkItemIcon; //アイコンを設定

                    //アイテムをリストに追加
                    inventoryArray[i] = checkItem;
                    itemArray[i] = inventoryArray[i].GetComponent<ItemInfo>();

                    Debug.Log("CheckInventorySlot() : checkStackNum : " + checkStackNum);

                    currentItemAmount[checkItemID] = checkStackNum; //現在の所持数リストの値を更新

                    Debug.Log("CheckInventorySlot() : currentItemAmount[checkItemID] : " + currentItemAmount[checkItemID]);

                    stackText.text = currentItemAmount[checkItemID].ToString(); //UIのテキストを所持数リストの値に更新
                    slot.gameObject.SetActive(true); //所持数のUIを表示
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

            //現在のmapが「Player」ならUIに変更
            if (currnetMap == mapPlayer) {
                inventoryParent.SetActive(true);
                _playerInput.SwitchCurrentActionMap(mapUI);
            }

            //現在のmapが「UI」ならPlayerに変更
            else if (currnetMap == mapUI) {
                inventoryParent.SetActive(false);
                _playerInput.SwitchCurrentActionMap(mapPlayer);
            }
        }
    }
}
