using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5f;
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * speed * Time.deltaTime);
            //transform.position += transform.up * Time.deltaTime * speed;
    }
}
