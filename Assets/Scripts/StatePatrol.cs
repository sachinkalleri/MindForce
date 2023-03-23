using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : IState
{
    RogueController owner;
    NavMeshAgent agent;
    Waypoint waypoint;

    public StatePatrol(RogueController owner)
    {
        this.owner = owner;
    }

    public void Enter()
    {
        Debug.Log("Entering the patrol state");

        waypoint = owner.waypoint;
        agent = owner.GetComponent<NavMeshAgent>();
        agent.destination = waypoint.transform.position;
        agent.isStopped = false;
    }

    public void Execute()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Waypoint nextWaypoint = waypoint.nextWaypoint;
            waypoint = nextWaypoint;
            agent.destination = waypoint.transform.position;
        }

        if(owner.seenTarget)
        {
            owner.stateMachine.ChangeState(new StateChase(owner));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting patrol state");
        agent.isStopped = true;
    }
}
