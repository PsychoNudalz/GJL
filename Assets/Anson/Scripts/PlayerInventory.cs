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
    [SerializeField] ToolType currentItem;

    [Header("UI")]
    [SerializeField] UI_Inventory uI_Inventory;

    public List<ToolType> items;
    private ToolHandler toolHandler;

    public ToolType CurrentItem { get => currentItem; }
    public List<ToolType> Items { get => items; set => items = value; }
    public UI_Inventory UI_Inventory { get => uI_Inventory; set => uI_Inventory = value; }


    void Start()
    {
        items = new List<ToolType>();
        toolHandler = GetComponentInChildren<ToolHandler>();
    }

    public void AddItem(ToolType tool)
    {
        PlayerPrefs.SetInt(tool.ToString(), 1);
        if (!items.Contains(tool))
        {
            items.Add(tool);
            currentItem = tool;
            RotateList();
            if (uI_Inventory == null)
            {
                uI_Inventory = FindObjectOfType<UI_Inventory>();
            }
            uI_Inventory.RefreshUI(true);
            //SetIndex(Mathf.Clamp(items.Count - 1, 0, items.Count));
            //UpdateItem();
        }
        else
        {
            Debug.LogError("found duplicate Item: " + tool);
        }
    }

    public void NextItem()
    {
        if (items.Count > 1)
        {
            currentItem = items[1];
            RotateList();
        }
        uI_Inventory.RefreshUI();
    }

    public void PrevItem()
    {
        if (items.Count > 1)
        {
            currentItem = items[items.Count - 1];
            RotateList();
        }

        uI_Inventory.RefreshUI();
    }

    private void RotateList()
    {
        while (!currentItem.Equals(items[0]))
        {
            ToolType temp = items[0];
            items.Remove(temp);
            items.Add(temp);
        }
    }

    /*void UpdateItem()
    {
        //HosterItem();
        if (items.Count > 0)
        {
            currentItem = items[0];
            RotateList();
            EquipItem();
        }
        else
        {
            currentItem = ToolType.None;
        }
        UI_Inventory.SetEquip(toolHandler.GetItemFromEnum(currentItem));
    }*/

    void EquipItem()
    {
        GetComponentInChildren<ToolHandler>().SetToolEnabled(currentItem);
        try
        {
            uI_Inventory.UpdateEquip();
        }
        catch (System.Exception e)
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
        if (items.Count > 0)
        {
            PrevItem();
        }
        else
        {
            currentItem = ToolType.None;
        }
        uI_Inventory.RefreshUI(true);

    }

    public void ShowUsableTools(List<ToolType> tools)
    {
        uI_Inventory.HighlightUsable(tools);
    }

    public void ResetInvUI(ToolType[] lastItemCheckpoint)
    {
        items = new List<ToolType>();
        if (items.Count > 0)
        {
            RemoveItem();
        }
        foreach (ToolType tool in lastItemCheckpoint)
        {
            AddItem(tool);
        }
        //SetIndex(0);
        if (items.Count == 0)
        {
            currentItem = ToolType.None;
        }
        else
        {
            currentItem = items[index];
        }
        uI_Inventory.RefreshUI(true);
    }
}
