using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_Inventory : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] UI_ItemCard currentCard;

    [Header("UI")]
    [SerializeField] GameObject baseItemCard;
    [SerializeField] List<UI_ItemCard> allItemCards;
    [SerializeField] Animator animator;

    void Start()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        if (!playerInventory)
        {
            playerInventory = FindObjectOfType<PlayerHandler>().PlayerInventory;
        }
    }

    public void UpdateInventoryList()
    {
        ResetInventoryList();
        foreach(ToolType i in playerInventory.Items)
        {
            ItemScript itemScript = FindObjectOfType<ToolHandler>().GetItemFromEnum(i).GetComponent<ItemScript>();
            UI_ItemCard temp = Instantiate(baseItemCard, transform).GetComponent<UI_ItemCard>();
            temp.UpdateCard(itemScript);
            allItemCards.Add(temp);
        }
        animator.SetTrigger("Up");
        //UpdateEquip();
    }

    public void UpdateEquip()
    {
        ItemScript itemScript = FindObjectOfType<ToolHandler>().GetItemFromEnum(playerInventory.CurrentItem);
        SetEquip(itemScript);
    }

    public void ResetInventoryList()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (!t.Equals(transform))
            {
                t.gameObject.SetActive(false);
            }
        }
        allItemCards = new List<UI_ItemCard>();
    }


    UI_ItemCard GetItemCard(ItemScript i)
    {
        foreach (UI_ItemCard ic in allItemCards)
        {
            if (ic && ic.Item.Equals(i))
            {
                return ic;
            }
        }
        return null;
    }

    public void SetEquip(ItemScript i)
    {
        if (currentCard)
        {
            currentCard.SetEquipEffect(false);
        }
        if (!i)
        {
            Debug.LogError("passed card "+ i +" null");
            animator.SetTrigger("Up");

            return;
        }
        currentCard = GetItemCard(i);
        if (!currentCard)
        {
            Debug.LogWarning("Failed to get item from UI");
            return;
        }
        currentCard.SetEquipEffect(true);
        animator.SetTrigger("Up");

    }
    public void HighlightUsable(List<ToolType> itemTools)
    {
        foreach(UI_ItemCard ic in allItemCards)
        {

            ic.SetUsableEffect(itemTools.Contains(ic.ToolType));
        }

    }
}
