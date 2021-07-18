using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] private InstructionImage[] InstructionImages;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private Image instructionImage;
    [SerializeField] private GameObject startGameButton;

    private int imageIndex = 0;

    private void OnEnable()
    {
        imageIndex = 0;
        instructionImage.sprite = InstructionImages[imageIndex].Image;
        titleText.text = InstructionImages[imageIndex].Title;
        SetButtonsEnabled();
    }

    /// <summary>
    /// Move to the next image if available
    /// </summary>
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
        previousButton.GetComponent<Button>().interactable = (!(imageIndex <= 0));
        nextButton.GetComponent<Button>().interactable = (!(imageIndex >= InstructionImages.Length - 1));
        startGameButton.SetActive((imageIndex >= InstructionImages.Length - 1));

        SetButtonFontColour(previousButton);
        SetButtonFontColour(nextButton);
        
    }

    private void SetButtonFontColour(GameObject button)
    {
        if (button.GetComponent<Button>().interactable)
        {
            button.GetComponentInChildren<TMP_Text>().color = Color.white;
        }
        else
        {
            button.GetComponentInChildren<TMP_Text>().color = Color.grey;
        }
    }
}
