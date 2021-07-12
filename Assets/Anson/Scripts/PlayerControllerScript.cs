using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private float interactDistance = 5f;


    [Header("Other Components")]
    [SerializeField] PlayerInventory playerInventory;

    private Camera mainCamera;

    public PlayerInventory PlayerInventory { get => playerInventory; set => playerInventory = value; }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnInteract()
    {
        Interact();
    }
    public void OnNextItem()
    {
        playerInventory.NextItem();
    }
    public void OnPrevItem()
    {
        playerInventory.PrevItem();
    }


    public void Interact()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out HitInfo, interactDistance))
        {
            InteractInteractable(HitInfo);
            InteractItem(HitInfo);
        }
    }

    private void InteractItem(RaycastHit HitInfo)
    {
        if (HitInfo.collider.gameObject.CompareTag("Tool"))
        {
            if (HitInfo.collider.gameObject.TryGetComponent(out ItemScript itemScript))
            {
                if (itemScript != null)
                {
                    playerInventory.AddItem(itemScript);
                }
            }
            else
            {
                Debug.LogError("Failed to get item script");
            }
        }
    }

    private void InteractInteractable(RaycastHit HitInfo)
    {
        if (HitInfo.collider.gameObject.CompareTag("Interactable"))
        {
            Debug.DrawLine(mainCamera.transform.position, HitInfo.point, Color.green, 1f);
            print($"{HitInfo.collider.gameObject} is interactable");
            if (HitInfo.collider.gameObject.TryGetComponent(out InteractableObjectScript interactableObjectScript))
            {

                if (interactableObjectScript != null)
                {
                    if (interactableObjectScript.Interact(playerInventory.CurrentItem.ToolType))
                    {
                        playerInventory.RemoveItem();
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to get interactable script");
            }
        }

    }
}
