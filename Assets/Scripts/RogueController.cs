using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RogueController : MonoBehaviour
{

    NavMeshAgent agent;

    public PlayerController player;

    public Waypoint waypoint;
    public StateMachine stateMachine = new StateMachine();

    float sightFoV = 110.0f;
    public bool seenTarget = false;
    public Vector3 lastSeenPosition;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.ChangeState(new StatePatrol(this));
        //agent = GetComponent<NavMeshAgent>();
        //agent.destination = waypoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        //if(!agent.pathPending && agent.remainingDistance < 0.5f)
        //{
        //    Waypoint nextWaypoint = waypoint.nextWaypoint;
        //    waypoint = nextWaypoint;
        //    agent.destination = waypoint.transform.position;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            RaycastHit hit;
            //seenTarget = false;

            if(angle < sightFoV * 0.5)
            {
                if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, GetComponent<SphereCollider>().radius))
                {
                    if(hit.collider.gameObject == other.gameObject)
                    {
                        if(seenTarget != true)
                        {
                            seenTarget = true;
                            ++player.isSeenBy;
                        }                        

                        lastSeenPosition = other.transform.position;
                    }

                    else
                    {
                        if(seenTarget != false)
                        {
                            seenTarget = false;
                            player.isSeenBy--;
                        }
                    }
                }

                //else
                //{
                //    seenTarget = false;
                //}
            }

            else
            {
                if (seenTarget != false)
                {
                    seenTarget = false;
                    player.isSeenBy--;
                }
            }
        }
    }
}
