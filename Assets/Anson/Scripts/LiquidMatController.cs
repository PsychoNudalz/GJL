using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidMatController : MonoBehaviour
{
    [SerializeField] Renderer liquid;
    [Range(0f, 1f)]
    [SerializeField] float fill;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        liquid.gameObject.SetActive(true);
    }

    void Update()
    {
        liquid.material.SetFloat("_Fill", fill);
    }
}
