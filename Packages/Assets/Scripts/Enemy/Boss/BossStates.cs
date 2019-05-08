using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossStatesID
{
    protected Dictionary<BossTransition, BossStateID> map = new Dictionary<BossTransition, BossStateID>();

    protected BossStateID stateID;

    public BossStateID ID { get { return stateID; } }

    protected Vector3 destPos;

    protected Transform[] wayPoints;

    protected float currentSpeed;
    protected float currentRotSpeed;

    public void AddTransition(BossTransition transition, BossStateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == BossTransition.None || id == BossStateID.None)
        {
            Debug.LogWarning("BossState : Null transition not allowed");
            return;
        }

        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("BossState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        Debug.Log("Added : " + transition + " with ID : " + id);
    }


    public void DeleteTransition(BossTransition trans)
    {
        // Check for NullTransition
        if (trans == BossTransition.None)
        {
            Debug.LogError("BossState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("BossState ERROR: Transition passed was not on this State´s List");
    }



    public BossStateID GetOutputState(BossTransition trans)
    {
        // Check for NullTransition
        if (trans == BossTransition.None)
        {
            Debug.LogError("BossState ERROR: NullTransition is not allowed");
            return BossStateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("BossState ERROR: " + trans + " Transition passed to the State was not on the list");
        return BossStateID.None;
    }



    public abstract void Reason(Transform player, Transform npc);

    public abstract void Act(Transform player, Transform npc, Animator anim, NavMeshAgent nav, float dmg);


    public void FindNextPoint()
    {
        int rndIndex = Random.Range(0, wayPoints.Length);
        Vector3 rndPosition = Vector3.zero;
        destPos = wayPoints[rndIndex].position + rndPosition;
    }


}
