/*
*Copyright (c) ChrisK
*https://www.youtube.com/channel/UCPu3vmQP5tZ4mnI_E_ezOiQ
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool canMove = true;
    public Rigidbody2D body;
    private Animator enemy;
    private bool attacking = false;
    public float timeBetweenAttack;
    public float attackDelayMin = .0f;
    public float attackDelayMax = .5f;
    public Transform attackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;
    public float attackTimer;
    public float moveSpeed;
    public float lookRadius;
    private float attackDelay;
    //TODO Move player over to a singleton pattern maybe
    public GameObject player;
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Animator>();
        attackDelay = Random.Range(attackDelayMin, attackDelayMax);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        move();
    }

    IEnumerator HitTiming(Vector2 difference)
    {
        canMove = false;
        yield return new WaitForSeconds(0.2f);
        enemy.SetBool("hit", true);

        if (difference.x > 0)
        {
            body.AddForce(new Vector2(-300, (difference.y - .2f) * -150));
        }
        else
        {
            body.AddForce(new Vector2(300, (difference.y - .2f) * -150));
        }
        yield return new WaitForSeconds(.2f);
        body.velocity = new Vector2(0, 0);
        canMove = true;
    }

    public void Hit(Vector3 position)
    {
        Vector2 difference = position - gameObject.transform.position;
        IEnumerator hitTimer = HitTiming(difference);
        StartCoroutine(hitTimer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(gameObject.transform.position, lookRadius);
    }

    private void Attack()
    {
        if (timeBetweenAttack <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
            if (enemiesToDamage.Length > 0)
            {
                Debug.Log("Attack");
                StartCoroutine(AttackTiming());
                timeBetweenAttack = attackTimer;

            }
            //StartCoroutine(AttackTiming());
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void move()
    {
        //change to singleton for better peformance later
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance <= lookRadius)
        {

            float xDifference = player.transform.position.x - transform.position.x;
            float yDifference = player.transform.position.y - transform.position.y;
            if (xDifference > .5f || xDifference < -.5f || yDifference > .5f || yDifference < -.7f)
            {
                if (xDifference < 0)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                enemy.SetBool("moving", true);
            }
        }
        else
        {
            enemy.SetBool("moving", false);
        }

    }
    private void move2()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            enemy.SetBool("moving", false);
        }
        if (Input.GetAxisRaw("Horizontal") > .5f && canMove && !enemy.GetBool("attack"))
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            enemy.SetBool("moving", true);
        }
        if (Input.GetAxisRaw("Horizontal") < -.5f && canMove && !enemy.GetBool("attack"))
        {
            transform.Translate(new Vector3(-1 * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            enemy.SetBool("moving", true);
        }
        if ((Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f) && !enemy.GetBool("attack") && canMove)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            enemy.SetBool("moving", true);
        }
    }


    IEnumerator AttackTiming()
    {

        attackDelay = Random.Range(attackDelayMin, attackDelayMax);
        yield return new WaitForSeconds(attackDelay);
        timeBetweenAttack = attackTimer;
        enemy.SetBool("attack", true);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<PlayerController>().Hit(transform.position);
        }
    }

}
