using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestUI
{
    public class ItemDatabase:MonoBehaviour
    {
        public List<Items> list = new List<Items>();
        public static List<Items> itemDatabase = new List<Items>();

        void Start()
        {
            foreach (Items item in list) {
                itemDatabase.Add(item);
            }            
        }



    }
}
