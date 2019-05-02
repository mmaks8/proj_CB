using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossTransition
{
    None = 0,
    FoundPlayer,
    ReachedPlayer,
    LostPlayer,
    NoHP
}

public enum BossStateID
{
    None = 0,
    Patrolling,
    Chasing,
    Attacking,
    Dead
}
public class BossFSM : FSM
{
    private List<BossStatesID> fsmStates;

    //The fsmStates are not changing directly but updated by using transitions
    private BossStateID currentStateID;
    public BossStateID CurrentStateID { get { return currentStateID; } }

    private BossStatesID currentState;
    public BossStatesID CurrentState { get { return currentState; } }

    public BossFSM()
    {
        fsmStates = new List<BossStatesID>();
    }


    public void AddBossState(BossStatesID fsmState)
    {
        // Check for Null reference before deleting
        if (fsmState == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        // First State inserted is also the Initial state
        //   the state the machine is in when the simulation begins
        if (fsmStates.Count == 0)
        {
            fsmStates.Add(fsmState);
            currentState = fsmState;
            currentStateID = fsmState.ID;
            return;
        }

        // Add the state to the List if it´s not inside it
        foreach (BossStatesID state in fsmStates)
        {
            if (state.ID == fsmState.ID)
            {
                Debug.LogError("FSM ERROR: Trying to add a state that was already inside the list");
                return;
            }
        }

        //If no state in the current then add the state to the list
        fsmStates.Add(fsmState);
    }



    public void DeleteState(BossStateID fsmState)
    {
        // Check for NullState before deleting
        if (fsmState == BossStateID.None)
        {
            Debug.LogError("FSM ERROR: bull id is not allowed");
            return;
        }

        // Search the List and delete the state if it´s inside it
        foreach (BossStatesID state in fsmStates)
        {
            if (state.ID == fsmState)
            {
                fsmStates.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: The state passed was not on the list. Impossible to delete it");
    }


    public void PerformTransition(BossTransition trans)
    {
        // Check for NullTransition before changing the current state
        if (trans == BossTransition.None)
        {
            Debug.LogError("FSM ERROR: Null transition is not allowed");
            return;
        }

        // Check if the currentState has the transition passed as argument
        BossStateID id = currentState.GetOutputState(trans);
        if (id == BossStateID.None)
        {
            Debug.LogError("FSM ERROR: Current State does not have a target state for this transition");
            return;
        }

        // Update the currentStateID and currentState		
        currentStateID = id;
        foreach (BossStatesID state in fsmStates)
        {
            if (state.ID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }
}

