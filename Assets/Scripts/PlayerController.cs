using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float attackRange;
    public int damage;
    public float attackTimer;
    private float timeBetweenAttack;

    public float attackXOffset;
    public LayerMask whatIsEnemy;
    private Animator player;
    public Rigidbody2D body;

    public Transform attackPos;
    private bool hit = false;
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(timeBetweenAttack <= 0)
        {
            Debug.Log("here");
            if (Input.GetButtonDown("Fire1"))
            {
                player.SetBool("attack", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().Hit(transform.position);
                    
                }
                timeBetweenAttack = attackTimer;
            }
            
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
