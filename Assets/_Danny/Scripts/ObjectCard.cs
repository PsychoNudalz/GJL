using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCard : MonoBehaviour
{
    [SerializeField] private Image cardImage; 
    [SerializeField] private TMP_Text cardText;

    public void SetCardDetails(ToolType toolType)
    {
        cardImage.sprite = ToolEnumConvertor.GetSprite(toolType);
        cardText.text = ToolEnumConvertor.ToName(toolType);
    }
}
