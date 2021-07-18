using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressScreen : MonoBehaviour
{
    [SerializeField] private Transform displayPanel;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject emptyCardPrefab;
    [SerializeField] private TMP_Text collectedAmountText;

    private void OnEnable()
    {
        //To Test

        /*for (int i = 1; i < Enum.GetValues(typeof(ToolType)).Length; i+=2)
        {
            PlayerPrefs.SetInt(Enum.GetValues(typeof(ToolType)).GetValue(i).ToString(),1);
        }*/

        foreach (ToolType tool in Enum.GetValues(typeof(ToolType)))
        {
            PlayerPrefs.SetInt(tool.ToString(),1);
        }

        RefreshCards();
    }

    private void RefreshCards()
    {
        int collectedCount = 0;
        foreach (Transform card in displayPanel.transform)
        {
            Destroy(card.gameObject);
        }

        foreach (ToolType tool in Enum.GetValues(typeof(ToolType)))
        {
            if (!tool.Equals(ToolType.None))
            {
                if (PlayerPrefs.GetInt(tool.ToString(), 0) == 1)
                {
                    GameObject card = GameObject.Instantiate(cardPrefab, displayPanel);
                    card.GetComponent<ObjectCard>().SetCardDetails(tool);
                    collectedCount++;
                }
                else
                {
                    GameObject card = GameObject.Instantiate(emptyCardPrefab, displayPanel);
                }
            }
        }

        collectedAmountText.text = $"Collected - {collectedCount} / {Enum.GetValues(typeof(ToolType)).Length - 1}";
    }

    public void ResetCollectedItems()
    {
        foreach (ToolType tool in Enum.GetValues(typeof(ToolType)))
        {
            PlayerPrefs.SetInt(tool.ToString(),0);
        }
        RefreshCards();
    }
}
