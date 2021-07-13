using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectHighlights : MonoBehaviour
{
    [SerializeField] private float timeToHighlight = 5f;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private AnimationCurve fadeCurve;

    private bool highlightObjects = false;
    private float highlightTimer;


    void Start()
    {
        SetHighlightObjects(highlightObjects);
        highlightTimer = 0;
    }

    void FixedUpdate()
    {
        if (highlightObjects)
        {
            highlightTimer += Time.deltaTime;
            highlightMaterial.SetFloat("_Alpha",fadeCurve.Evaluate(highlightTimer/timeToHighlight));
        }
        if (highlightTimer >= timeToHighlight)
        {
            highlightObjects = false;
            SetHighlightObjects(highlightObjects);
            highlightTimer = 0;
        }
    }

    public void OnHighlight()
    {
        highlightObjects = true;
        SetHighlightObjects(highlightObjects);

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
