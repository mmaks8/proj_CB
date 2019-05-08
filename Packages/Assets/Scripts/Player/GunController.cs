using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public bool isFiring;
    public Bullet bullet;

    public float bulletSpeed;

    public float timeBetweenShots;

    private float shotCounter;

    public AudioClip Fire;

    public AudioSource AudioSource;

    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.clip = Fire;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                AudioSource.Play();
                newBullet.speed = bulletSpeed;
            }

        }
        else
        {
            shotCounter = 0f;
        }
    }
}
