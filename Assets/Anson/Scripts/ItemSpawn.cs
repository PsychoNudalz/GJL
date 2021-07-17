using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] Transform position;

    public void Spawn()
    {
        Instantiate(spawnObject, position.position, position.rotation);
    }
}
