using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MLStatesID
{
    protected Dictionary<MLTransition, MLStateID> map = new Dictionary<MLTransition, MLStateID>();

    protected MLStateID stateID;

    public MLStateID ID { get { return stateID; } }

    protected Vector3 destPos;

    protected Transform[] wayPoints;

    protected float currentSpeed;
    protected float currentRotSpeed;

    public void AddTransition(MLTransition transition, MLStateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == MLTransition.None || id == MLStateID.None)
        {
            Debug.LogWarning("MLState : Null transition not allowed");
            return;
        }

        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("MLState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        Debug.Log("Added : " + transition + " with ID : " + id);
    }


    public void DeleteTransition(MLTransition trans)
    {
        // Check for NullTransition
        if (trans == MLTransition.None)
        {
            Debug.LogError("MLState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("MLState ERROR: Transition passed was not on this State´s List");
    }



    public MLStateID GetOutputState(MLTransition trans)
    {
        // Check for NullTransition
        if (trans == MLTransition.None)
        {
            Debug.LogError("MLState ERROR: NullTransition is not allowed");
            return MLStateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("MLState ERROR: " + trans + " Transition passed to the State was not on the list");
        return MLStateID.None;
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
