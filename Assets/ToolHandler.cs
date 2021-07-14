using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    private List<GameObject> toolsOnPlayer;
    void Awake()
    {
        toolsOnPlayer = new List<GameObject>();
        foreach (Tools tool in Enum.GetValues(typeof(Tools)))
        {
            //print($"Resources/Tools/{tool.ToString()}");
            GameObject toolPrefab = Instantiate(Resources.Load<GameObject>($"Tools/{tool.ToString()}"),transform.position,transform.rotation,transform);
            toolPrefab.name = tool.ToString();
            toolPrefab.GetComponent<ItemScript>().SetOnPlayer(false);
            toolPrefab.SetActive(false);
            toolsOnPlayer.Add(toolPrefab);
        }
    }

    public void SetToolEnabled(Tools toolEnabled, bool none = false)
    {
        foreach (GameObject tool in toolsOnPlayer)
        {
            tool.SetActive(false);
        }
        if(none){return;}

        foreach (GameObject tool in toolsOnPlayer)
        {
            if (tool.GetComponent<ItemScript>().ToolType.Equals(toolEnabled))
            {
                tool.SetActive(true);
                return;
            }
        }
    }

    public ItemScript GetItemFromEnum(Tools tooltype)
    {
        foreach (GameObject tool in toolsOnPlayer)
        {
            if (tool.GetComponent<ItemScript>().ToolType.Equals(tooltype))
            {
                return tool.GetComponent<ItemScript>();
            }
        }
        return null;
    }
}
