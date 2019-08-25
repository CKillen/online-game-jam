using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inanimateObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 firstRotation = new Vector3(10f, 0f, 10f);
        Vector3 secondRotation = new Vector3(10f, 0f, 10f);
        Vector3 thirdRotation = new Vector3(10f, 0f, 10f);
        Vector3 fourthRotation = new Vector3(10f, 0f, 10f);
        gameObject.transform.Rotate(firstRotation * (.5f * Time.deltaTime), Space.Self);
        gameObject.transform.Rotate(secondRotation * (.5f * Time.deltaTime), Space.Self);

        Debug.Log(gameObject.name);
    }

}
