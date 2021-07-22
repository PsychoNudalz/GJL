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

    [SerializeField] private Transform leftCardsParent;
    [SerializeField] private Transform equippedCard;
    [SerializeField] private Transform rightCardsParent;

    List<ToolType> allToolTypes = new List<ToolType>();
    List<ToolType> leftToolTypes = new List<ToolType>();
    private ToolType equippedToolType = ToolType.None;
    List<ToolType> rightToolTypes = new List<ToolType>();
    //[SerializeField] Animator animator;



    void Start()
    {
        /*if (!animator)
        {
            animator = GetComponent<Animator>();
        }*/
        if (!playerInventory)
        {
            playerInventory = FindObjectOfType<PlayerHandler>().PlayerInventory;
        }
    }

    /*public void UpdateInventoryList()
    {
        ResetInventoryList();
        foreach(ToolType i in playerInventory.Items)
        {
            ItemScript itemScript = FindObjectOfType<ToolHandler>().GetItemFromEnum(i).GetComponent<ItemScript>();
            ObjectCard temp = Instantiate(baseItemCard, transform).GetComponent<ObjectCard>();
            temp.UpdateCard(itemScript);
            allItemCards.Add(temp);
        }
        //animator.SetTrigger("Up");
        //UpdateEquip();
    }*/

    public void UpdateEquip()
    {
        ItemScript itemScript = FindObjectOfType<ToolHandler>().GetItemFromEnum(playerInventory.CurrentItem);
        //SetEquip(itemScript);
    }

    /*public void ResetInventoryList()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (!t.Equals(transform))
            {
                t.gameObject.SetActive(false);
            }
        }
        allItemCards = new List<ObjectCard>();
    }*/


    /*ObjectCard GetItemCard(ItemScript i)
    {
        foreach (ObjectCard ic in allItemCards)
        {
            if (ic && ic.Item.Equals(i))
            {
                return ic;
            }
        }
        return null;
    }*/

    /*public void SetEquip(ItemScript i)
    {
        if (currentCard)
        {
            currentCard.SetEquipEffect(false);
        }
        if (!i)
        {
            Debug.LogError("passed card "+ i +" null");
            //animator.SetTrigger("Up");

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

    }*/

    public void HighlightUsable(List<ToolType> itemTools)
    {
        foreach(ObjectCard ic in allItemCards)
        {
            bool isUsable = itemTools.Contains(ic.ToolType);
            ic.SetUsableEffect(isUsable);
            if (isUsable)
            {
                //TODO add up animation to card
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

        //SetCards();
    }

    private void RefreshLists()
    {
        
        allToolTypes = new List<ToolType>(playerInventory.Items);
        leftToolTypes = new List<ToolType>();
        rightToolTypes = new List<ToolType>();
        if (allToolTypes.Count > 0)
        {
            equippedToolType = allToolTypes[0];
            allToolTypes.Remove(equippedToolType);

            if (allToolTypes.Count.Equals(1))
            {
                rightToolTypes.Add(allToolTypes[0]);
            }
            else if (allToolTypes.Count.Equals(2))
            {
                rightToolTypes.Add(allToolTypes[0]);
                leftToolTypes.Add(allToolTypes[1]);
            }
            else if (allToolTypes.Count > 2)
            {
                int splitIndex = Mathf.FloorToInt(allToolTypes.Count / 2);

                for (int i = 0; i < splitIndex; i++)
                {
                    rightToolTypes.Add(allToolTypes[i]);
                    allToolTypes.Remove(allToolTypes[i]);
                }

                leftToolTypes = new List<ToolType>(allToolTypes);
            }
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

            foreach (ToolType tool in rightToolTypes)
            {
                temp = Instantiate(baseItemCard, rightCardsParent).GetComponent<ObjectCard>();
                temp.SetCardDetails(equippedToolType);
                allItemCards.Add(temp);
            }
            foreach (ToolType tool in leftToolTypes)
            {
                temp = Instantiate(baseItemCard, leftCardsParent).GetComponent<ObjectCard>();
                temp.SetCardDetails(equippedToolType);
                allItemCards.Add(temp);
            }
            
        }
    }

    private void SetCards()
    {
        if (equippedToolType != ToolType.None)
        {
            currentCard.SetCardDetails(equippedToolType);
        }

        ObjectCard[] leftCards = leftCardsParent.GetComponentsInChildren<ObjectCard>();
        for (int i = 0; i < leftToolTypes.Count; i++)
        {
            leftCards[i].SetCardDetails(leftToolTypes[i]);
        }

        ObjectCard[] rightCards = rightCardsParent.GetComponentsInChildren<ObjectCard>();
        for (int i = 0; i < rightToolTypes.Count; i++)
        {
            rightCards[i].SetCardDetails(rightToolTypes[i]);
        }
    }
}
