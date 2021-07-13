using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ObjectHighlighter : MonoBehaviour
{
    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void SetHighlighted(bool isHighlighted)
    {
        outline.enabled = isHighlighted;
    }
}
