using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] Transform position;

    public void Spawn()
    {
        if (position == null)
        {
            Instantiate(spawnObject, transform.position, transform.rotation);

        }
        else
        {
        Instantiate(spawnObject, position.position, position.rotation);
        }
    }


}
