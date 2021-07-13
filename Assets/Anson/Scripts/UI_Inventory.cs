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

    void Start()
    {
        if (!playerInventory)
        {
            playerInventory = FindObjectOfType<PlayerHandler>().PlayerInventory;
        }
    }

    public void UpdateInventoryList()
    {
        ResetInventoryList();
        foreach(ItemScript i in playerInventory.Items)
        {
            UI_ItemCard temp = Instantiate(baseItemCard, transform).GetComponent<UI_ItemCard>();
            temp.UpdateCard(i);
            allItemCards.Add(temp);
        }
        //UpdateEquip();
    }

    public void UpdateEquip()
    {
        SetEquip(playerInventory.CurrentItem);

    }

    private void ResetInventoryList()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (!t.Equals(transform))
            {
                Destroy(t.gameObject);
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
                print("Getting " + i);
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
            Debug.LogError("passed card i null");

            return;
        }
        currentCard = GetItemCard(i);
        if (!currentCard)
        {
            Debug.LogWarning("Failed to get item from UI");
            return;
        }
        currentCard.SetEquipEffect(true);
    }
}
