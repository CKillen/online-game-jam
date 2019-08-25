using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerControl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        //TODO Add logic for detecting if hero is over pivots to fix glitch issues
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

}
