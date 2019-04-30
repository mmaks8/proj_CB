using System.Collections;
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

    public GameObject bullet;
    public Transform spawnPoint;

    float range = 100f;

    float movementSpeed;

    public float hp;

    float fireRate;


    // Start is called before the first frame update
    void Start()
    {
        hp = 100f;
        Debug.Log("Player hp: " + hp);
        fireRate = 20f;
        elapsedTime = 0f;
        anim = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        speed = 5f;
        movementSpeed = 5f;
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
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            anim.SetFloat("WalkSpeed", 0.5f);
            inMove = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = inMove * speed;
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetFloat("WalkSpeed", 0.5f);
            inMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            moveVelocity = inMove * speed;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetFloat("WalkSpeed", 0f);
            moveVelocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            anim.SetFloat("WalkSpeed", 0f);
            moveVelocity = Vector3.zero;
        }

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
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        }

    }


    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Projectile")
        {
            hp -= 25;
            Debug.Log("Player hp: " + hp);
            if (hp <= 0)
            {
                Debug.Log("Player is dead.");
            }
        }
        if(collision.gameObject.tag == "MinionSlap")
        {
            hp -= 10;
            Debug.Log("Player hp: " + hp);
            if (hp <= 0)
            {
                Debug.Log("Player is dead.");
            }
        }


    }
}
