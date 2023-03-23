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
    public float senseRange = 5.0f;//Range to which rogues can sense player even if they are not facing them. Can be adjusted for each rogues.
    public bool seenTarget = false;
    public Vector3 lastSeenPosition;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.ChangeState(new StatePatrol(this));        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        
    }

    public void ResetRogue()
    {
        seenTarget = false;
        gameObject.SetActive(true);
        Start();
        gameObject.tag = "InvincibleEnemy";
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            RaycastHit hit;            

            if(angle <= sightFoV * 0.5)
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
            }

            
            else
            {
                if (seenTarget != false)
                {
                    seenTarget = false;
                    player.isSeenBy--;
                }
            }

            if (direction.magnitude < senseRange && gameObject.tag == "InvincibleEnemy")
            {
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, GetComponent<SphereCollider>().radius))
                {
                    if (hit.collider.gameObject == other.gameObject)
                    {
                        if (seenTarget != true)
                        {
                            seenTarget = true;
                            ++player.isSeenBy;
                        }

                        lastSeenPosition = other.transform.position;
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

            else
            {
                if(angle > sightFoV * 0.5 && seenTarget != false)
                {
                    seenTarget = false;
                    player.isSeenBy--;
                }
            }
        }
    }
}
