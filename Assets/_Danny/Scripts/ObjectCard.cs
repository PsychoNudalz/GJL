using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ObjectCard : MonoBehaviour
{
    [SerializeField] private Image cardImage; 
    [SerializeField] private TMP_Text cardText;
    [SerializeField] private Color usableColor;
    [SerializeField] private Image backImage;
    private Color baseColour;
    private ToolType toolType;

    public ToolType ToolType => toolType;

    void Start()
    {
        baseColour = backImage.color;
    }

    public void SetCardDetails(ToolType toolType)
    {
        this.toolType = toolType;
        cardImage.sprite = ToolEnumConvertor.GetSprite(toolType);
        cardText.text = ToolEnumConvertor.ToName(toolType);
    }

    public void SetUsableEffect(bool b)
    {
        if (b)
        {
            backImage.color = usableColor;
        }
        else
        {
            backImage.color = baseColour;
        }
    }
}
