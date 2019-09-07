using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float attackRange;
    public int damage;
    public float attackTimer;

    public LayerMask whatIsEnemy;
    public Rigidbody2D body;
    public Transform attackPos;

    private bool canMove = true;
    private bool attacking = false;
    private Animator player;
    private float timeBetweenAttack;
    private bool currentlyAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        //Should I move this to a drag?
        player = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0 || currentlyAttacking)
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
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            player.SetBool("moving", false);
        }
        if (Input.GetAxisRaw("Horizontal") > .5f && canMove && !player.GetBool("attack"))
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            player.SetBool("moving", true);
        }
        if (Input.GetAxisRaw("Horizontal") < -.5f && canMove && !player.GetBool("attack"))
        {
            transform.Translate(new Vector3(-1 * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            player.SetBool("moving", true);
        }
        if ((Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f) && !player.GetBool("attack") && canMove)
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

    IEnumerator HitTiming(Vector2 difference)
    {
        yield return new WaitForSeconds(0.2f);
        player.SetBool("hit", true);
        canMove = false;
        body.velocity = new Vector2(0, 0);
        if (difference.x > 0)
        {
            body.AddForce(new Vector2(-150, (difference.y - .2f) * -150));
        }
        else
        {
            body.AddForce(new Vector2(150, (difference.y - .2f) * -150));
        }
        yield return new WaitForSeconds(.2f);
        canMove = true;
        body.velocity = new Vector2(0, 0);
    }

    public void Hit(Vector3 position)
    {
        Vector2 difference = position - gameObject.transform.position;
        IEnumerator hitTimer = HitTiming(difference);
        StartCoroutine(hitTimer);
    }

}
