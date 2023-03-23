using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateChase : IState
{
    RogueController owner;
    NavMeshAgent agent;

    public StateChase(RogueController owner)
    {
        this.owner = owner;
    }
    public void Enter()
    {
        agent = owner.GetComponent<NavMeshAgent>();

        if(owner.seenTarget)
        {
            agent.destination = owner.lastSeenPosition;
            agent.isStopped = false;
        }

    }

    public void Execute()
    {
        agent.destination = owner.lastSeenPosition;
        agent.isStopped = false;

        if(!agent.pathPending && agent.remainingDistance < 5.0f)
        {
            agent.isStopped = true;
        }

        if (owner.seenTarget != true || owner.isDead)
        {
            owner.stateMachine.ChangeState(new StatePatrol(owner));
        }

    }

    public void Exit()
    {
        agent.isStopped = true;
    }
}
