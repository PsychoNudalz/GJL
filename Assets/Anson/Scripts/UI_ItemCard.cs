using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemCard : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] ItemScript item;

    [Header("UI")]
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] GameObject equipEffect;
    [SerializeField] GameObject usableEffect;

    public ItemScript Item { get => item;}

    // Start is called before the first frame update
    void Awake()
    {
        ResetEffects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCard(ItemScript item)
    {
        if (item.UISprite != null)
        {
            image.sprite = item.UISprite;
        }
        nameField.text = ToolEnumConvertor.ToName(item.ToolType);
        this.item = item;
    }

    public void ResetEffects()
    {
        if (equipEffect)
        {
            equipEffect.SetActive(false);
        }
        if (usableEffect)
        {
            usableEffect.SetActive(false);
        }
    }

    public void SetEquipEffect(bool b)
    {
        equipEffect.SetActive(b);
    }
    public void SetUsableEffect(bool b)
    {
        usableEffect.SetActive(b);
    }
}
