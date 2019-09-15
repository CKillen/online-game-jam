/*
*Copyright (c) ChrisK
*https://www.youtube.com/channel/UCPu3vmQP5tZ4mnI_E_ezOiQ
*/
using UnityEngine;

public class BushController : MonoBehaviour
{
    private Animator shake;
    private bool shaking = false;
    // Start is called before the first frame update
    void Start()
    {
        shake = gameObject.GetComponentInParent<Animator>();
        transform.parent.transform.position = transform.position;
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
