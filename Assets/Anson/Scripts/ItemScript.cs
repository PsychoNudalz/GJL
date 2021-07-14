using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    [SerializeField] ToolType toolType;
    [SerializeField] Sprite uISprite;
    [SerializeField] Rigidbody rb;
    [SerializeField] List<Collider> colliders;

    public ToolType ToolType { get => toolType;}
    public Sprite UISprite { get => uISprite; set => uISprite = value; }

    private void Awake()
    {
        if (!TryGetComponent(out ObjectHighlighter o))
        {
            gameObject.AddComponent<Outline>().enabled = false;
            gameObject.AddComponent<ObjectHighlighter>();
        }
    }


    void Start()
    {
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
    /*public void OnPickUp()
    {
        SetOnPlayer(false);
        Debug.Log("Picked Up: " + ToolEnumConvertor.ToName(toolType));
    }*/
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
        foreach (Collider c in colliders)
        {
            c.enabled = b;
        }
    }
}
