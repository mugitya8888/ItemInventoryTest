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
        

        //仮保存用の取得変数
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

            //拾ったアイテムの情報を、ItemInfoのtag名で取得する
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
            //ボタンが押されたスロットのイメージを所得
            Image slotIconImage = GetComponent<Image>();
            Sprite icon = slotIconImage.sprite;

            //必要な情報をItemDatabaseから取得
            GetItemData(icon);

            int index = 0;
            Transform counts;
            int amount = PlayerInventory.currentItemAmount[checkItemID];
            Debug.Log("amount : " + amount);

            for (int i = 0; i < CanvasInventory.slotList.Count; i++) {

                if (gameObject == CanvasInventory.slotList[i]) {

                    //例外処理：アイテムが無いスロットに対して使うを押した場合、何もせずに終了
                    if (playerInventory.inventoryArray[i] == null) {
                        Debug.Log("no item");
                        return;
                    }
                    //このスロットのindexを取得
                    index = i;
                    break;
                }
            }
            Debug.Log("index : " + index);
            //孫オブジェクトのTextMeshProコンポーネントを取得
            counts = CanvasInventory.slotList[index].transform.GetChild(1);
            stackText = counts.GetChild(0).GetComponent<TextMeshProUGUI>();

            //アイテムがスロットに残っているとき
            if (amount > 1) {
                PlayerInventory.currentItemAmount[checkItemID] -= 1;
                stackText.text = PlayerInventory.currentItemAmount[checkItemID].ToString();
            }
            //アイテムの残りが最後の１個の時
            else if (amount == 1) {
                PlayerInventory.currentItemAmount[checkItemID] = 0;  //所持数を0にする
                stackText.text = amount.ToString();
                counts.gameObject.SetActive(false);
                playerInventory.inventoryArray[index] = null;  //inventory配列のindex番目を空にする
                slotIconImage.sprite = null;  //slotのiconイメージを削除
            }

            //アイテムごとの用途に応じて、効果を変更
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
