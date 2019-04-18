using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Attributes")]

    public float movementSpeed;
    float gravity = 8f;
    float rotSpeed = 80f;
    float rot = 0f;

    private Vector3 direction;
    private CharacterController controller;
    private Animator anim;

    public GameObject bullet;
    public Transform spawnPoint;

    float fireRate;
    float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
        fireRate = 20f;
        movementSpeed = 5.0f;
        direction = Vector3.zero;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.speed = 0.8f;
    }

    void Update()
    {
        anim.SetFloat("WalkSpeed", 0f);
        if (controller.isGrounded)
        {

            if (Input.GetKey(KeyCode.W))
            {
                anim.SetFloat("WalkSpeed", 0.5f);
                direction = new Vector3(0, 0, 1);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetFloat("WalkSpeed", 0.5f);
                direction = new Vector3(0, 0, -1);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                anim.SetFloat("WalkSpeed", 0.5f);
                direction = new Vector3(0, 0, 0);
            }
           /* else if (Input.GetKey(KeyCode.Space) && Time.time >= elapsedTime)
            {
                elapsedTime = Time.time + 1f / fireRate;
                Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            }*/
        }


        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
    }
}

