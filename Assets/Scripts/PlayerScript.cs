using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            rb2d.velocity = new Vector3(0, 3, 0); 
        }

        if (Input.GetKeyDown("a"))
        {
            rb2d.velocity = new Vector3(-3, 0, 0);
        }

        if (Input.GetKeyDown("s"))
        {
            rb2d.velocity = new Vector3(0, -3, 0);

        }

        if (Input.GetKeyDown("d"))
        {
            rb2d.velocity = new Vector3(3, 0, 0);

        }
    }


    void FixedUpdate()
    {

    }
}
