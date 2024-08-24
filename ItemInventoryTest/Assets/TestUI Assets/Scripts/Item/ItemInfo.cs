using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class ItemInfo:MonoBehaviour
    {
        public Items itemsObj;


        public enum ItemTagName
        {
            Medicine,
            LittleKey,
            Bullets,
            Handgun
        }

        public ItemTagName tagName;

        public string GetTagName()
        {
            return tagName.ToString();
        }

    }
}
