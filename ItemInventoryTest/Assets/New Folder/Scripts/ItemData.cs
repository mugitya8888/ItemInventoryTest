using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create ItemData")]
public class ItemData:ScriptableObject
{
    public enum ItemType
    {
        Sword,
        rod,
        Clothes,
        Armor,
        Recovery,
        Normal,
        Important
    }

    [SerializeField]
    private string m_ItemName;

    [SerializeField]
    private ItemType m_ItemType;

    [SerializeField]
    private Sprite m_ItemIcon;

    [SerializeField]
    private string m_ItemExplanation;

    [SerializeField]
    private int m_ItemLimit;

    public string GetItemName()
    {
        return m_ItemName;
    }

    public ItemType GetItemType()
    {
        return m_ItemType;
    }

    public Sprite GetItemIcon()
    {
        return m_ItemIcon;
    }

    public string GetItemExplanation()
    {
        return m_ItemExplanation;
    }

    public int GetItemLimit()
    {
        return m_ItemLimit;
    }
}
