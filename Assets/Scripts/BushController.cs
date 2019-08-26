using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushController : MonoBehaviour
{
    private Animator shake;
    private bool shaking = false;
    // Start is called before the first frame update
    void Start()
    {
        shake = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shake.Play("bushBump");
 
    }
}
