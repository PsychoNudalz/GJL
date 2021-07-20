using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header("Main menu")] 
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private GameObject endingPanel;

    [Header("For Tutorial Screen")]
    [SerializeField] private InstructionImage[] InstructionImages;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private Image instructionImage;
    [SerializeField] private GameObject startGameButton;
    private int imageIndex = 0;

    [Header("For Progress Screen")] 
    [SerializeField] private GameObject resetProgressConfirmPanel;
    [SerializeField] private Transform displayPanel;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject emptyCardPrefab;
    [SerializeField] private TMP_Text collectedAmountText;

    [Header("For Ending Screen")]
    [SerializeField] private GameObject resetEndingsConfirmPanel;
    [SerializeField] private Transform textsParent;
    [SerializeField] private GameObject endingTextPrefab;

    [Header("ForCreditsScreen")] 
    [SerializeField] private GameObject creditsPanel;

    [Space]
    [Header("MenuStartButtons")]
    [SerializeField] private GameObject mainMenuFirstButton, tutorialFirstButton, progressFirstButton, endingsFirstButton, progressConfirmFirstButton, endingsConfirmFirstButton, creditsFirstButton;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
        SetActiveButton(mainMenuFirstButton);
    }

    private void SetActiveButton(GameObject buttonToSetActive)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonToSetActive);
    }

    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        imageIndex = 0;
        instructionImage.sprite = InstructionImages[imageIndex].Image;
        titleText.text = InstructionImages[imageIndex].Title;
        SetButtonsEnabled();
        SetActiveButton(tutorialFirstButton);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        SetActiveButton(mainMenuFirstButton);
    }

    public void StartGameButton()
    {
        PlayerPrefs.SetInt("AlreadyPlayed",1);
        SceneManager.LoadScene(1);
    }

    public void NextInstructionImage()
    {
        if (imageIndex < InstructionImages.Length - 1)
        {
            imageIndex++;
            instructionImage.sprite = InstructionImages[imageIndex].Image;
            titleText.text = InstructionImages[imageIndex].Title;
            SetButtonsEnabled();
        }
    }

    /// <summary>
    /// Move to previous image if available
    /// </summary>
    public void PreviousInstructionImage()
    {
        if (imageIndex > 0)
        {
            imageIndex--;
            instructionImage.sprite = InstructionImages[imageIndex].Image;
            titleText.text = InstructionImages[imageIndex].Title;
            SetButtonsEnabled();
        }
    }

    /// <summary>
    /// Set next and previous buttons active / inactive based on current index in images
    /// </summary>
    private void SetButtonsEnabled()
    {
        previousButton.SetActive(!(imageIndex <= 0));
        nextButton.SetActive(!(imageIndex >= InstructionImages.Length - 1));
        startGameButton.SetActive((imageIndex >= InstructionImages.Length - 1) || PlayerPrefs.GetInt("AlreadyPlayed",0).Equals(1));
        if (!nextButton.activeInHierarchy)
        {
            SetActiveButton(previousButton);
        }

        if (!previousButton.activeInHierarchy)
        {
            SetActiveButton(nextButton);
        }
    }

    public void OpenProgress()
    {
        mainMenuPanel.SetActive(false);
        progressPanel.SetActive(true);
        RefreshCards();
        SetActiveButton(progressFirstButton);
    }

    public void CloseProgress()
    {
        mainMenuPanel.SetActive(true);
        progressPanel.SetActive(false);
        SetActiveButton(mainMenuFirstButton);
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

    public void OpenProgressResetConfirm()
    {
        resetProgressConfirmPanel.SetActive(true);
        progressPanel.SetActive(false);
        SetActiveButton(progressConfirmFirstButton);
    }
    public void CloseProgressResetConfirm()
    {
        resetProgressConfirmPanel.SetActive(false);
        progressPanel.SetActive(true);
        SetActiveButton(progressFirstButton);
        RefreshCards();
    }

    public void ResetCollectedItems()
    {
        foreach (ToolType tool in Enum.GetValues(typeof(ToolType)))
        {
            PlayerPrefs.SetInt(tool.ToString(),0);
        }
        RefreshCards();
    }

    public void OpenEndings()
    {
        progressPanel.SetActive(false);
        endingPanel.SetActive(true);
        RefreshEndings();
        SetActiveButton(endingsFirstButton);
    }

    public void CloseEndings()
    {
        progressPanel.SetActive(true);
        endingPanel.SetActive(false);
        SetActiveButton(progressFirstButton);
    }

    public void OpenEndingResetConfirm()
    {
        resetEndingsConfirmPanel.SetActive(true);
        endingPanel.SetActive(false);
        SetActiveButton(endingsConfirmFirstButton);
    }
    public void CloseEndingResetConfirm()
    {
        resetEndingsConfirmPanel.SetActive(false);
        endingPanel.SetActive(true);
        SetActiveButton(endingsFirstButton);
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
            tmpText.text = $"{EndingEnum.GetStringFromEnding(ending)} - {EndingEnum.GetEndingNameFromEnding(ending)}";
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

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        SetActiveButton(creditsFirstButton);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        SetActiveButton(mainMenuFirstButton);
    }

    public void QuitButton()
    {
        Application.Quit(0);
    }
}

