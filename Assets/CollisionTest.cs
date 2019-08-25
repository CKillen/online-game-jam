using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y);
    }
}
