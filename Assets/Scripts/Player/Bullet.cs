
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 20f;
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Untagged")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "enemy")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
