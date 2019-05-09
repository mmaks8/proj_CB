using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactEffect;
    public Rigidbody rigidbody;
    public float speed = 2f;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //transform.LookAt(targetPos);
        //transform.position += transform.forward * Time.deltaTime * speed;

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Untagged")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
