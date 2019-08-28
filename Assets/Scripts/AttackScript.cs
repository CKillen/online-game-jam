using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            attacking = true;
            StartCoroutine("Attacking");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(attacking == true && collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.name);
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }
}
