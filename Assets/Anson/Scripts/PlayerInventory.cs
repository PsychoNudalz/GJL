using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInventory : MonoBehaviour
{
    [Header("ItemScript")]
    //[SerializeField] List<ItemScript> items;
    [SerializeField] int index = 0;
    [SerializeField] Tools currentItem;

    [Header("Player")]
    [SerializeField] Transform handPostision;
    [Header("UI")]
    [SerializeField] UI_Inventory uI_Inventory;

    private List<Tools> items;
    private ToolHandler toolHandler;

    public Tools CurrentItem { get => currentItem; }
    public List<Tools> Items { get => items; set => items = value; }
    public UI_Inventory UI_Inventory { get => uI_Inventory; set => uI_Inventory = value; }


    void Start()
    {
        items = new List<Tools>();
        toolHandler = GetComponentInChildren<ToolHandler>();
    }

    public void AddItem(Tools tool)
    {
        if (!items.Contains(tool))
        {
            items.Add(tool);
            if (uI_Inventory == null)
            {
                uI_Inventory = FindObjectOfType<UI_Inventory>();
            }
            uI_Inventory.UpdateInventoryList();
            UpdateItem();
        }
        else
        {
            Debug.LogError("found duplicate Item: " + tool);
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
            toolHandler.SetToolEnabled(Tools.Stick,true);
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
        SetIndex((index - 1+items.Count) % items.Count);
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
        //HosterItem();
        if (items.Count > 0)
        {
            currentItem = items[index];
            EquipItem();
        }
        UI_Inventory.SetEquip(toolHandler.GetItemFromEnum(currentItem));
    }

    /*
    void HosterItem()
    {
        if (currentItem != null)
        {
            GetComponentInChildren<ToolHandler>().SetToolEnabled(Tools.None);
        }
    }*/

    void EquipItem()
    {
        GetComponentInChildren<ToolHandler>().SetToolEnabled(currentItem);
        try
        {
            uI_Inventory.UpdateEquip();
        }catch(System.Exception e)
        {
            Debug.LogWarning(e.StackTrace);
        }
    }

    public void RemoveItem()
    {
        if (items.Contains(currentItem))
        {
            items.Remove(currentItem);
        }
        toolHandler.GetItemFromEnum(currentItem).OnUse();
        PrevItem();
        uI_Inventory.UpdateInventoryList();

    }

    public void ShowUsableTools(List<Tools> tools)
    {
        uI_Inventory.HighlightUsable(tools);
    }
    
}
