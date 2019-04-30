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

    private Camera mainCamera;

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

            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.SetFloat("WalkSpeed", 0.5f);
                direction = new Vector3(0, 0, 1);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetFloat("WalkSpeed", 0.5f);
                direction = new Vector3(1, 0, 0);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetFloat("WalkSpeed", 0.5f);
                direction = new Vector3(0, 0, -1);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetFloat("WalkSpeed", 0.5f);
                direction = new Vector3(-1, 0, 0);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                anim.SetFloat("WalkSpeed", 0f);
                direction = new Vector3(0, 0, 0);
            }

        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);


            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

