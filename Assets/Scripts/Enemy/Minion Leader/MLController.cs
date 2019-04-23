using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MLController : MLFSM
{
    private Animator anim;

    private NavMeshAgent nav;

    public Transform spawnPoint;

    public GameObject projectile;

    public float damage;

    float elapsedTime;
    float shootRate;

    public float hp;

    protected override void Initialize()
    {
        hp = 100f;
        damage = 5f;
        elapsedTime = 0.0f;
        shootRate = 3f;

        anim = GetComponent<Animator>();
        anim.speed = 0.4f;
        nav = GetComponent<NavMeshAgent>();
        //get the tag 'Player'
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        //display error if Player tag is missing
        if (!playerTransform)
            print("No player detected. Add a 'Player' tag.");

        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        elapsedTime += Time.deltaTime;
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(playerTransform, transform);
        CurrentState.Act(playerTransform, transform, anim, nav, damage);
    }

    public void SetTransition(MLTransition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        pointList = GameObject.FindGameObjectsWithTag("GuardPoint");

        Transform[] wayPoints = new Transform[pointList.Length];

        int i = 0;

        foreach (GameObject obj in pointList)
        {
            wayPoints[i] = obj.transform;
            i++;
        }

        MLPatrol patrol = new MLPatrol(wayPoints);
        patrol.AddTransition(MLTransition.FoundPlayer, MLStateID.Chasing);
        patrol.AddTransition(MLTransition.NoHP, MLStateID.Dead);

        MLChase chase = new MLChase(wayPoints);
        chase.AddTransition(MLTransition.LostPlayer, MLStateID.Patrolling);
        chase.AddTransition(MLTransition.ReachedPlayer, MLStateID.Attacking);
        chase.AddTransition(MLTransition.NoHP, MLStateID.Dead);

        MLAttack attack = new MLAttack(wayPoints);
        attack.AddTransition(MLTransition.FoundPlayer, MLStateID.Chasing);
        attack.AddTransition(MLTransition.NoHP, MLStateID.Dead);

        MLDead dead = new MLDead();
        dead.AddTransition(MLTransition.NoHP, MLStateID.Dead);

        AddMLState(patrol);
        AddMLState(chase);
        AddMLState(attack);
        AddMLState(dead);
    }

    public void ShootProjectile()
    {
        if (elapsedTime >= shootRate)
        {
            anim.SetBool("isAttacking", true);
            Instantiate(projectile, spawnPoint.position, Quaternion.identity);

            elapsedTime = 0.0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            hp -= 25;

            if (hp <= 0)
            {
                anim.SetBool("isDead", true);
                Debug.Log("Minion Leader is dead");
                SetTransition(MLTransition.NoHP);
                elapsedTime = 0f;
                if (elapsedTime >= 3f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
        /*
    public void Damage(float amount)
    {
        hp -= amount;
        if (hp <= 0f)
        {
            anim.SetBool("isDead", true);
            Debug.Log("Minion Leader is dead");
            SetTransition(MLTransition.NoHP);
            elapsedTime = 0f;
            if (elapsedTime >= 3f)
            {
                Destroy(gameObject);
            }
        }
    }*/
    
}

