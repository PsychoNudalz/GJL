using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;

public enum Tools {Stick, Hammer};

[System.Serializable]
public class InteractableEvent
{
    [SerializeField] Tools tool;
    [SerializeField] UnityEvent uEvent;

    public Tools Tool { get => tool; set => tool = value; }
    public UnityEvent UEvent { get => uEvent; set => uEvent = value; }
}
