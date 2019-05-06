using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class np : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody.AddForce(transform.forward * speed * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * speed;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
