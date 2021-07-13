using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] float previewRate = 0.5f;
    [SerializeField] float lastPreview;
    [SerializeField] bool wasPreview;

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
        if (Time.time - lastPreview >= previewRate && playerInventory.CurrentItem != null)
        {
            PreviewInteractable();
            lastPreview = Time.time;
        }
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
                        lastPreview = Time.time;
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to get interactable script");
            }
        }

    }

    private void PreviewInteractable()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out HitInfo, interactDistance))
        {

            if (HitInfo.collider.gameObject.CompareTag("Interactable"))
            {
                Debug.DrawLine(mainCamera.transform.position, HitInfo.point, Color.green, 1f);
                print($"{HitInfo.collider.gameObject} is Preview");
                if (HitInfo.collider.gameObject.TryGetComponent(out InteractableObjectScript interactableObjectScript))
                {

                    if (interactableObjectScript != null)
                    {
                        interactableObjectScript.Preview(playerInventory.CurrentItem.ToolType);
                        playerInventory.ShowUsableTools(interactableObjectScript.GetTools());
                        wasPreview = true;
                    }
                }
                else
                {
                    Debug.LogError("Failed to get interactable script");
                }
            }
            else if (wasPreview)
            {
                wasPreview = false;
                playerInventory.ShowUsableTools(new List<Tools>());

            }
        }
        else if (wasPreview)
        {
            wasPreview = false;
            playerInventory.ShowUsableTools(new List<Tools>());

        }
    }
}
