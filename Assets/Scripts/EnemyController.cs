using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool hit = false;
    public Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.GetComponent<PlayerController>().getAttackState() && hit == false)
        {
            Vector2 difference = collision.gameObject.transform.position - gameObject.transform.position;
            hit = true;
            IEnumerator knockback = Knockback(difference);
            StartCoroutine(knockback);
            StartCoroutine("HitReset");
        }
        
    }

    IEnumerator Knockback(Vector2 difference)
    {
        yield return new WaitForSeconds(0.2f);
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
    
    IEnumerator HitReset()
    {
        yield return new WaitForSeconds(0.3f);
        hit = false;
    }

}
