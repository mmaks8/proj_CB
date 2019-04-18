using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : FSMTwo
{

    private Animator anim;

    private NavMeshAgent nav;

    public float damage;

    protected override void Initialize()
    {
        damage = 1f;

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

        ChaseState chase = new ChaseState(wayPoints);
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        chase.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);

        AttackState attack = new AttackState(wayPoints);
        attack.AddTransition(Transition.FoundPlayer, FSMStateID.Chasing);


        AddFSMState(patrol);
        AddFSMState(chase);
        AddFSMState(attack);

    }
}
