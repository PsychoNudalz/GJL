using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> toolsOnPlayer;
    void Awake()
    {

        toolsOnPlayer = new List<GameObject>();
        foreach (ToolType tool in Enum.GetValues(typeof(ToolType)))
        {
            //print($"Resources/Tools/{tool.ToString()}");
            if (!tool.Equals(ToolType.None))
            {
                try
                {
                    GameObject toolPrefab = Instantiate(Resources.Load<GameObject>($"Tools/{tool}"), transform.position, transform.rotation, transform);
                    toolPrefab.name = tool.ToString();
                    //if (tool.Equals(ToolType.Apple))
                    //{
                    //    print("Apple");
                    //}
                    toolPrefab.GetComponent<ItemScript>().SetOnPlayer(false);
                    toolPrefab.SetActive(false);
                    toolsOnPlayer.Add(toolPrefab);
                    toolPrefab.layer = LayerMask.NameToLayer("HandTool");
                    foreach(Transform t in toolPrefab.transform.GetComponentsInChildren<Transform>())
                    {
                        t.gameObject.layer = LayerMask.NameToLayer("HandTool");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning($"Failed to find {tool} - {e}");
                }
            }
        }
    }

    public void SetToolEnabled(ToolType toolEnabled, bool none = false)
    {
        foreach (GameObject tool in toolsOnPlayer)
        {
            tool.SetActive(false);
        }
        if (none) { return; }

        foreach (GameObject tool in toolsOnPlayer)
        {
            if (tool.GetComponent<ItemScript>().ToolType.Equals(toolEnabled))
            {
                tool.SetActive(true);
                return;
            }
        }
    }

    public ItemScript GetItemFromEnum(ToolType tooltype)
    {
        foreach (GameObject tool in toolsOnPlayer)
        {
            if (tool.GetComponent<ItemScript>().ToolType.Equals(tooltype))
            {
                return tool.GetComponent<ItemScript>();
            }
        }
        Debug.LogError("Error Getting " + tooltype);
        return null;
    }
}
