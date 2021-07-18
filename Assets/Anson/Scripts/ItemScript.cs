using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemScript : MonoBehaviour
{

    [SerializeField] ToolType toolType;
    [SerializeField] Sprite uISprite;
    [SerializeField] Rigidbody rb;
    [SerializeField] List<Collider> colliders;
    [SerializeField] Outline outline;
    [SerializeField] UnityEvent pickUpEvent;

    public ToolType ToolType { get => toolType; }
    public Sprite UISprite { get => uISprite; set => uISprite = value; }

    private void Awake()
    {
        if (!TryGetComponent(out ObjectHighlighter o))
        {
            gameObject.AddComponent<Outline>().enabled = false;
            gameObject.AddComponent<ObjectHighlighter>();
        }
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (colliders.Count == 0)
        {
            colliders = new List<Collider>(GetComponentsInChildren<Collider>());
        }
    }


    void Start()
    {
        if (!outline)
        {
            outline = GetComponent<Outline>();
        }


        if (transform.parent == null || !transform.parent.name.Equals("Tools"))
        {
            /*
            if (PlayerHandler.PlayerInstance.GetComponent<PlayerInventory>().Items.Contains(toolType))
            {
                Destroy(gameObject);
            }
            */
        }
    }
    public void OnPickUp()
    {
        pickUpEvent.Invoke();
    }
    public void OnUse()
    {
        gameObject.SetActive(false);
    }
    /*public void OnDrop()
    {
        SetOnPlayer(true);
    }*/

    /// <summary>
    /// set the physics of the item
    /// True means activating phycis
    /// </summary>
    /// <param name="b"></param>
    internal void SetOnPlayer(bool b)
    {
        rb.isKinematic = !false;
        if (colliders.Count > 0)
        {

            foreach (Collider c in colliders)
            {
                c.enabled = b;
            }
        }
    }

    public void SetOutline(bool b)
    {
        outline.enabled = b;
    }

    public void GivePlayerImmunity(int damageType)
    {
        FindObjectOfType<PlayerLifeSystemScript>().AddImmunity((DamageType)damageType);
    }

    public void GivePlayerMask(int i)
    {
        PlayerHandler.handler.SetMask(i);
    }
}
