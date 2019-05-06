using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MLTransition
{
    None = 0,
    FoundPlayer,
    ReachedPlayer,
    LostPlayer,
    NoHP
}


public enum MLStateID
{
    None = 0,
    Patrolling,
    Chasing,
    Attacking,
    Dead
}
public class MLFSM : FSM
{
    private List<MLStatesID> fsmStates;

    //The fsmStates are not changing directly but updated by using transitions
    private MLStateID currentStateID;
    public MLStateID CurrentStateID { get { return currentStateID; } }

    private MLStatesID currentState;
    public MLStatesID CurrentState { get { return currentState; } }

    public MLFSM()
    {
        fsmStates = new List<MLStatesID>();
    }


    public void AddMLState(MLStatesID fsmState)
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
        foreach (MLStatesID state in fsmStates)
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



    public void DeleteState(MLStateID fsmState)
    {
        // Check for NullState before deleting
        if (fsmState == MLStateID.None)
        {
            Debug.LogError("FSM ERROR: bull id is not allowed");
            return;
        }

        // Search the List and delete the state if it´s inside it
        foreach (MLStatesID state in fsmStates)
        {
            if (state.ID == fsmState)
            {
                fsmStates.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: The state passed was not on the list. Impossible to delete it");
    }


    public void PerformTransition(MLTransition trans)
    {
        // Check for NullTransition before changing the current state
        if (trans == MLTransition.None)
        {
            Debug.LogError("FSM ERROR: Null transition is not allowed");
            return;
        }

        // Check if the currentState has the transition passed as argument
        MLStateID id = currentState.GetOutputState(trans);
        if (id == MLStateID.None)
        {
            Debug.LogError("FSM ERROR: Current State does not have a target state for this transition");
            return;
        }

        // Update the currentStateID and currentState		
        currentStateID = id;
        foreach (MLStatesID state in fsmStates)
        {
            if (state.ID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }
}

