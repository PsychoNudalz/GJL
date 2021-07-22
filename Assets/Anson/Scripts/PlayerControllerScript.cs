using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] float previewRate = 0.5f;
    [SerializeField] float lastPreview;
    [SerializeField] bool wasPreview;
    [SerializeField] ItemScript focusedTool;

    [Header("Item Scroll")]
    [SerializeField] float scrollWaitTime = 0.5f;
    [SerializeField] float scrollTime;

    [Header("Other Components")]
    [SerializeField] PlayerInventory playerInventory;

    [SerializeField] private Sound pickupSound;

    private Camera mainCamera;
    private bool highlightObjects = false;
    

    public PlayerInventory PlayerInventory { get => playerInventory; set => playerInventory = value; }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastPreview >= previewRate)
        {
            if (!playerInventory.CurrentItem.Equals(ToolType.None))
            {
                PreviewInteractable();
            }
            HightlightTool();
            lastPreview = Time.time;
        }
    }
    public void OnInteract()
    {
        Interact();
    }

    public void OnPause()
    {
        FindObjectOfType<PauseMenu>().TogglePause();
    }

    public void OnNextItem()
    {
        playerInventory.NextItem();
    }
    public void OnPrevItem()
    {
        playerInventory.PrevItem();
    }

    public void OnScrollItem(InputValue value)
    {
        //print(value.Get<Vector2>());
        if (scrollWaitTime + scrollTime < Time.time)
        {
            scrollTime = Time.time;
            if (value.Get<Vector2>().y > 0)
            {
                OnNextItem();
            }else if (value.Get<Vector2>().y < 0)
            {
                OnPrevItem();
            }
        }
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
                    pickupSound.PlayF();
                    itemScript.OnPickUp();
                    playerInventory.AddItem(itemScript.ToolType);
                    Destroy(itemScript.gameObject);

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
                    if (interactableObjectScript.Interact(playerInventory.CurrentItem))
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
        if (playerInventory.CurrentItem.Equals(ToolType.None))
        {
            return;
        }
        RaycastHit HitInfo;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out HitInfo, interactDistance))
        {

            if (HitInfo.collider.gameObject.CompareTag("Interactable"))
            {
                Debug.DrawLine(mainCamera.transform.position, HitInfo.point, Color.green, 1f);
                //print($"{HitInfo.collider.gameObject} is Preview");
                if (HitInfo.collider.gameObject.TryGetComponent(out InteractableObjectScript interactableObjectScript))
                {

                    if (interactableObjectScript != null)
                    {
                        interactableObjectScript.Preview(playerInventory.CurrentItem);
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
                playerInventory.ShowUsableTools(new List<ToolType>());

            }
        }
        else if (wasPreview)
        {
            wasPreview = false;
            playerInventory.ShowUsableTools(new List<ToolType>());

        }
    }

    private void HightlightTool()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out HitInfo, interactDistance))
        {

            if (HitInfo.collider.gameObject.CompareTag("Tool"))
            {
                if (HitInfo.collider.gameObject.TryGetComponent(out ItemScript itemScript))
                {
                    if (focusedTool &&!focusedTool.Equals(itemScript))
                    {
                        focusedTool.SetOutline(false);
                    }
                    if (itemScript != null)
                    {
                        focusedTool = itemScript;
                        itemScript.SetOutline(true);
                    }
                }
                else
                {
                    Debug.LogError("Failed to get item script");
                }
            }
        }
        else
        {
            if (focusedTool)
            {
                focusedTool.SetOutline(false);
                focusedTool = null;
            }
        }
    }

}
