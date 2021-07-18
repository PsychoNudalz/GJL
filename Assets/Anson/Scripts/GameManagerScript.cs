using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] int maxFrame = 200;
    private void Awake()
    {
        Application.targetFrameRate = maxFrame;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
