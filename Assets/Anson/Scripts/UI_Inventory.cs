using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_Inventory : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] ObjectCard currentCard;

    [Header("UI")]
    [SerializeField] GameObject baseItemCard;
    [SerializeField] List<ObjectCard> allItemCards;

    [SerializeField] private Transform otherCards;
    [SerializeField] private Transform equippedCard;

    List<ToolType> allToolTypes = new List<ToolType>();
    List<ToolType> otherToolTypes = new List<ToolType>();
    private ToolType equippedToolType = ToolType.None;

    void Start()
    {
        if (!playerInventory)
        {
            playerInventory = FindObjectOfType<PlayerHandler>().PlayerInventory;
        }
    }

    public void HighlightUsable(List<ToolType> itemTools)
    {
        foreach(ObjectCard ic in allItemCards)
        {
            bool isUsable = itemTools.Contains(ic.ToolType);
            ic.SetUsableEffect(isUsable);
            if (ic.ToolType != currentCard.ToolType)
            {
                ic.GetComponent<Animator>().SetBool("CardUp",isUsable);
            }
        }
    }

    public void RefreshUI(bool rebuild = true)
    {
        RefreshLists();
        if (rebuild)
        {
            CreateCards();
        }
    }

    private void RefreshLists()
    {
        
        allToolTypes = new List<ToolType>(playerInventory.Items);
        otherToolTypes = new List<ToolType>();
        if (allToolTypes.Count > 0)
        {
            equippedToolType = allToolTypes[0];
            allToolTypes.Remove(equippedToolType);
            
            otherToolTypes = new List<ToolType>(allToolTypes);
        }
        else
        {
            equippedToolType = ToolType.None;
        }
    }

    private void CreateCards()
    {
        foreach (ObjectCard card in GetComponentsInChildren<ObjectCard>())
        {
            Destroy(card.gameObject);
        }

        allItemCards = new List<ObjectCard>();

        if (!equippedToolType.Equals(ToolType.None))
        {
            ObjectCard temp = Instantiate(baseItemCard, equippedCard).GetComponent<ObjectCard>();
            temp.SetCardDetails(equippedToolType);
            allItemCards.Add(temp);
            currentCard = temp;

            foreach (ToolType tool in otherToolTypes)
            {
                temp = Instantiate(baseItemCard, otherCards).GetComponent<ObjectCard>();
                temp.SetCardDetails(tool);
                allItemCards.Add(temp);
            }
            
        }
    }
}
