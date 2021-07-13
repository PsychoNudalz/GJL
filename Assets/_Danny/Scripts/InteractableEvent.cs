using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;


[System.Serializable]
public class InteractableEvent
{
    [SerializeField] Tools tool;
    [SerializeField] UnityEvent interactEvent;
    [SerializeField] float interactDelay;
    [Header("Animation")]
    [SerializeField] string interactAnimation;
    [SerializeField] string previewAnimation;
    public Tools Tool { get => tool; set => tool = value; }
    public UnityEvent InteractEvent { get => interactEvent; set => interactEvent = value; }
    public string InteractAnimation { get => interactAnimation; set => interactAnimation = value; }
    public string PreviewAnimation { get => previewAnimation; set => previewAnimation = value; }


}
