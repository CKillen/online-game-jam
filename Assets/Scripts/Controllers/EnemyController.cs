using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool canMove = true;
    public Rigidbody2D body;
    private Animator enemy;
    private bool attacking = false;
    private float timeBetweenAttack;
    public Transform attackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;
    public float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            Debug.Log("here");
            if (Input.GetButtonDown("Fire1"))
            {
                enemy.SetBool("attack", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<PlayerController>().Hit(transform.position);
                }
                timeBetweenAttack = attackTimer;
            }
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }

    }

    IEnumerator HitTiming(Vector2 difference)
    {
        yield return new WaitForSeconds(0.2f);
        enemy.SetBool("hit", true);
        canMove = false;
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
    }
}
