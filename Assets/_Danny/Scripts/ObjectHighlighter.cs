using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ObjectHighlighter : MonoBehaviour
{
    private Outline outline;
    private ToggleObjectHighlights highlightToggler;

    void Awake()
    {
        highlightToggler = FindObjectOfType<ToggleObjectHighlights>();
        outline = GetComponent<Outline>();
        outline.OutlineWidth = highlightToggler.OutlineWidth;
        outline.enabled = false;
    }

    void Start()
    {
        SetOutlineColour();
    }

    private void SetOutlineColour()
    {
        if(highlightToggler == null){return;}
        switch (gameObject.layer)
        {
            case 3:
                outline.OutlineColor = highlightToggler.PushableOutlineColour;
                break;
            case 6:
                outline.OutlineColor = highlightToggler.InteractableOutlineColour;
                break;
            case 7:
                outline.OutlineColor = highlightToggler.ToolOutlineColour;
                break;
            default:
                break;
        }
    }

    public void SetHighlighted(bool isHighlighted)
    {
        outline.enabled = isHighlighted;
    }
}
