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

    void  OnInteract()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out HitInfo, interactDistance))
        {
            if (HitInfo.collider.gameObject.CompareTag("Interactable"))
            {
                Debug.DrawLine(mainCamera.transform.position,HitInfo.point,Color.green,1f);
                print($"{HitInfo.collider.gameObject} is interactable");
                InteractableObjectScript interactableObjectScript = HitInfo.collider.gameObject.GetComponent<InteractableObjectScript>();
                if (interactableObjectScript != null)
                {
                    interactableObjectScript.Interact();
                }
            }
        }
    }
}
