using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingsScreen : MonoBehaviour
{
    [SerializeField] private Transform textsParent;
    [SerializeField] private GameObject endingTextPrefab;


    private void OnEnable()
    {
        /*
         * For testing
         */
        /*PlayerPrefs.SetInt(Ending.Ending0.ToString(),1);
        PlayerPrefs.SetInt(Ending.Ending2.ToString(),1);
        PlayerPrefs.SetInt(Ending.Ending4.ToString(),1);*/

        RefreshEndings();
    }

    private void RefreshEndings()
    {
        foreach (Transform text in textsParent.transform)
        {
            Destroy(text.gameObject);
        }
        foreach (Ending ending in Enum.GetValues(typeof(Ending)))
        {
            GameObject endingText = Instantiate(endingTextPrefab, Vector3.zero, Quaternion.identity, textsParent);
            TMP_Text tmpText = endingText.GetComponent<TMP_Text>();
            tmpText.text = $"Ending {EndingEnum.GetStringFromEnding(ending)} - {EndingEnum.GetEndingNameFromEnding(ending)}";
            if (PlayerPrefs.GetInt(ending.ToString(), 0).Equals(0))
            {
                tmpText.color = Color.grey;
            }
            else
            {
                tmpText.color = Color.white;
            }
        }
    }

    public void ResetEndings()
    {
        foreach (Ending ending in Enum.GetValues(typeof(Ending)))
        {
            PlayerPrefs.SetInt(ending.ToString(),0);
        }
        RefreshEndings();
    }
}
