using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create ItemeDatabase")]
public class ItemDatabase:ScriptableObject
{
    [SerializeField]
    private List<ItemData> m_ItemList = new List<ItemData>();

    public List<ItemData> GetItemList()
    {
        return m_ItemList;
    }

   
}
