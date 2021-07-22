using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UI_Handler : MonoBehaviour
{
    [SerializeField] UI_Inventory uI_Inventory;

    public UI_Inventory UI_Inventory { get => uI_Inventory; set => uI_Inventory = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if (!uI_Inventory)
        {
            uI_Inventory = GetComponentInChildren<UI_Inventory>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory()
    {
        uI_Inventory.RefreshUI();
    }


}
