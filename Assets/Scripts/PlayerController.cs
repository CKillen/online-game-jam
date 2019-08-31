using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Animator player;
    private bool attacking = false;
    public Rigidbody2D body;
    

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            player.SetBool("attack", true);
            attacking = true;
            StartCoroutine("Attacking");
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            player.SetBool("moving", false);
        }
        if ((Input.GetAxisRaw("Horizontal") > .5f || Input.GetAxisRaw("Horizontal") < -.5f) && !player.GetBool("attack"))
        {
            
            if(Input.GetAxisRaw("Horizontal") > .5f)
            {
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if(Input.GetAxisRaw("Horizontal") < -.5f)
            {
                transform.Translate(new Vector3(-1 * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            player.SetBool("moving", true);
        }
        if ((Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f) && !player.GetBool("attack"))
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            player.SetBool("moving", true);
        }


    }

    public bool getAttackState()
    {
        return attacking;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("entered");
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }
}
