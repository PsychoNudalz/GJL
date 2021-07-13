using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectHighlights : MonoBehaviour
{
    [Header("Outlines")] 
    [SerializeField][Range(0,50)] private float outlineWidth = 5f;
    [SerializeField] private Color interactableOutlineColour;
    [SerializeField] private Color pushableOutlineColour;
    [SerializeField] private Color toolOutlineColour;

    [Space]
    [SerializeField] private Material[] highlightMaterials;

    [Header("Highlight time and Fade curve")]
    [SerializeField] private float timeToHighlight = 5f;
    [SerializeField] private AnimationCurve fadeCurve;

    private bool highlightObjects = false;
    private float highlightTimer;

    public Color PushableOutlineColour => pushableOutlineColour;
    public Color InteractableOutlineColour => interactableOutlineColour;
    public Color ToolOutlineColour => toolOutlineColour;

    public float OutlineWidth => outlineWidth;


    void Start()
    {
        SetHighlightObjects(highlightObjects);
        highlightTimer = 0;
        SetHighlightMaterialAlphas(0f);
    }

    void FixedUpdate()
    {
        if (highlightObjects)
        {
            highlightTimer += Time.deltaTime;
            SetHighlightMaterialAlphas(fadeCurve.Evaluate(highlightTimer / timeToHighlight));
        }
        if (highlightTimer >= timeToHighlight)
        {
            highlightObjects = false;
            SetHighlightObjects(highlightObjects);
            highlightTimer = 0;
        }
    }

    private void SetHighlightMaterialAlphas(float alphaValue)
    {
        foreach (Material material in highlightMaterials)
        {
            material.SetFloat("_Alpha", alphaValue);
        }
    }

    public void OnHighlight()
    {
        highlightObjects = true;
        SetHighlightObjects(highlightObjects);

    }

    void OnDestroy()
    {
        SetHighlightObjects(false);
    }

    void ToggleObjectHighlighting()
    {
        SetHighlightObjects(!highlightObjects);
    }

    private void SetHighlightObjects(bool areHighlighted)
    {
        highlightObjects = areHighlighted;
        
        /*if (areHighlighted)
        {
            highlightMaterial.SetFloat("_Alpha",1f);
        }
        else
        {
            highlightMaterial.SetFloat("_Alpha",0f);
        }*/


        GameObject[] tools = GameObject.FindGameObjectsWithTag("Tool");
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject tool in tools)
        {
            ObjectHighlighter highlighterScript = tool.GetComponent<ObjectHighlighter>();
            if (highlighterScript)
            {
                highlighterScript.SetHighlighted(highlightObjects);
            }
        }

        foreach (GameObject interactable in interactables)
        {
            ObjectHighlighter highlighterScript = interactable.GetComponent<ObjectHighlighter>();
            if (highlighterScript)
            {
                highlighterScript.SetHighlighted(highlightObjects);
            }
        }
    }
}
