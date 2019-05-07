using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : FSMTwo
{

    private Animator anim;

    private NavMeshAgent nav;

    public float damage;
    public AudioSource AudioSource2;
    public AudioClip AlienSlap;

    float hp;

    float elapsedTime;

    protected override void Initialize()
    {
        hp = 100f;
        elapsedTime = 0f;
        damage = 1f;
        AudioSource2.clip = AlienSlap;

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
        //nothing yet
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(playerTransform, transform);
        CurrentState.Act(playerTransform, transform, anim, nav, damage);
    }

    public void SetTransition(Transition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        pointList = GameObject.FindGameObjectsWithTag("DestinationPoint");

        Transform[] wayPoints = new Transform[pointList.Length];

        int i = 0;

        foreach(GameObject obj in pointList)
        {
            wayPoints[i] = obj.transform;
            i++;
        }

        PatrolState patrol = new PatrolState(wayPoints);
        patrol.AddTransition(Transition.FoundPlayer, FSMStateID.Chasing);
        patrol.AddTransition(Transition.NoHP, FSMStateID.Dead);

        ChaseState chase = new ChaseState(wayPoints);
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        chase.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);
        chase.AddTransition(Transition.NoHP, FSMStateID.Dead);

        AttackState attack = new AttackState(wayPoints);
        attack.AddTransition(Transition.FoundPlayer, FSMStateID.Chasing);
        attack.AddTransition(Transition.NoHP, FSMStateID.Dead);

        DeadState dead = new DeadState();
        dead.AddTransition(Transition.NoHP, FSMStateID.Dead);

        AddFSMState(patrol);
        AddFSMState(chase);
        AddFSMState(attack);
        AddFSMState(dead);

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            anim.SetTrigger("isHit");
            hp -= 50;

            if (hp <= 0)
            {
                anim.SetBool("isDead", true);
                Debug.Log("Minion  is dead");
                SetTransition(Transition.NoHP);

                Destroy(gameObject, CONSTANTS.GLOBAL.TIME_BEFORE_DESTROY);
                elapsedTime = 0f;
                if (elapsedTime >= 3f)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
            AudioSource2.Play();
    }
}
