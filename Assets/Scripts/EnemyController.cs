using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool hit = false;
    public Rigidbody2D body;
    private Animator enemy;
    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        

        
    }

    IEnumerator Knockback(Vector2 difference)
    {
        yield return new WaitForSeconds(0.2f);
        enemy.SetBool("hit", true);
        if (difference.x > 0)
        {
            body.AddForce(new Vector2(-200, (difference.y - .2f) * -150));
        }
        else
        {
            body.AddForce(new Vector2(200, (difference.y - .2f) * -150));
        }
        yield return new WaitForSeconds(.2f);
        body.velocity = new Vector2(0, 0);
    }

    public bool getAttackState()
    {
        return attacking;
    }


    IEnumerator HitReset()
    {
        
        yield return new WaitForSeconds(0.3f);
        hit = false;
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }

    public void Hit(Vector3 position)
    {
        Vector2 difference = position - gameObject.transform.position;
        hit = true;
        Debug.Log("hit");
        IEnumerator knockback = Knockback(difference);
        StartCoroutine(knockback);
        StartCoroutine("HitReset");
    }

}
