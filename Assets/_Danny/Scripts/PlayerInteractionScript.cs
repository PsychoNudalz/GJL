using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out HitInfo, interactDistance))
        {
            if (HitInfo.collider.gameObject.CompareTag("Interactable"))
            {
                Debug.DrawLine(mainCamera.transform.position,HitInfo.point,Color.green);
                print($"{HitInfo.collider.gameObject} is interactable");
            }
        }
    }
}
