﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class FSMStates
{
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();

    protected FSMStateID stateID;

    public FSMStateID ID { get { return stateID; } }

    protected Vector3 destPos;

    protected Transform[] wayPoints;

    protected float currentSpeed;
    protected float currentRotSpeed;

    public void AddTransition(Transition transition, FSMStateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == Transition.None || id == FSMStateID.None)
        {
            Debug.LogWarning("FSMState : Null transition not allowed");
            return;
        }

        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        Debug.Log("Added : " + transition + " with ID : " + id);
    }


    public void DeleteTransition(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition passed was not on this State´s List");
    }



    public FSMStateID GetOutputState(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return FSMStateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the State was not on the list");
        return FSMStateID.None;
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
