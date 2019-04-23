﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    private Vector3 inMove;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    private Animator anim;

    float elapsedTime;

    //public GameObject bullet;
    //public Transform spawnPoint;

    public GunController gun;

    float range = 100f;


    float fireRate;
    // Start is called before the first frame update
    void Start()
    {
        

        fireRate = 20f;
        elapsedTime = 0f;
        anim = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("GunAim", true);

        

        /*if (Input.GetKey(KeyCode.Space) && Time.time >= elapsedTime)
        {
            elapsedTime = Time.time + 1f / fireRate;
            //Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        }*/

        inMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = inMove * speed;





        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if(ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            
             transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            
        }

        if(Input.GetMouseButtonDown(0))
        {
            /*RaycastHit hit;
            if (Physics.Raycast(cameraRay.origin, cameraRay.direction, out hit, range))
            {
                Debug.Log(hit.transform.name);
            }*/
            gun.isFiring = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            gun.isFiring = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }
}
