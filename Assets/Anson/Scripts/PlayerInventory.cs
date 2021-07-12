using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("ItemScript")]
    [SerializeField] List<ItemScript> items;
    [SerializeField] int index = 0;
    [SerializeField] ItemScript currentItem;

    [Header("Player")]
    [SerializeField] Transform handPostision;

    public ItemScript CurrentItem { get => currentItem; }

    public void AddItem(ItemScript i)
    {
        if (!items.Contains(i))
        {
            items.Add(i);
            i.OnPickUp();
            i.transform.SetParent(handPostision);
            i.transform.rotation = handPostision.rotation;
            i.transform.position = handPostision.position;
            index = items.Count - 1;
            UpdateItem();
        }
        else
        {
            Debug.LogError("found duplicate Item: " + i);
        }
    }

    public void SetIndex(int i)
    {
        if (i < items.Count && i >= 0)
        {
            index = i;
        }else if (items.Count == 0)
        {
            index = 0;
        }
        else
        {
            Debug.LogWarning("Index out of bounds");
        }
    }

    void NextIndex()
    {
        if (items.Count == 0)
        {
            SetIndex(0);
            return;
        }
        SetIndex((index + 1) % items.Count);
    }
    void PrevIndex()
    {
        if (items.Count == 0)
        {
            SetIndex(0);
            return;
        }
        SetIndex((index - 1) % items.Count);
    }

    public void NextItem()
    {
        NextIndex();
        UpdateItem();
    }

    public void PrevItem()
    {
        PrevIndex();
        UpdateItem();
    }

    void UpdateItem()
    {
        HosterItem();
        if (items.Count > 0)
        {
            currentItem = items[index];
            EquipItem();
        }
    }

    void HosterItem()
    {
        if (currentItem != null)
        {
            currentItem.gameObject.SetActive(false);
        }
    }

    void EquipItem()
    {
        currentItem.gameObject.SetActive(true);
    }

    public void RemoveItem()
    {
        if (items.Contains(currentItem))
        {
            items.Remove(currentItem);
        }
        currentItem.OnUse();
        PrevItem();
    }

}
