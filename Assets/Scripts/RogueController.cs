using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RogueController : MonoBehaviour
{

    NavMeshAgent agent;

    public GameObject interactionAura;
    public float finalFXRadius = 7.0f;
    public float incrementStatus = 0.0f;
    public float intervalFX = 1.0f;
    public float intervalCounter = 0.0f;

    public Vector3 initialFXVector = new Vector3(1.0f, 0.1f, 1.0f);
    public Vector3 stepFXVector = new Vector3(10.0f, 0.0f, 10.0f);

    public PlayerController player;

    public Waypoint waypoint;
    public StateMachine stateMachine = new StateMachine();

    public float sightFoV = 180.0f;
    public float senseRange = 5.0f;//Range to which rogues can sense player even if they are not facing them. Can be adjusted for each rogues.
    public bool seenTarget = false;
    public bool isDead = false;
    bool isFXStart = false;
    bool isFXactivate = false;
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
        if(isFXactivate && intervalCounter == 0.0f) //To produce the power ring effect below the rogues
        {
            interactionFX();
        }
        else
        {
            intervalCounter += Time.deltaTime;
            if(intervalCounter >= intervalFX)
            {
                isFXactivate = true;
                //isFXStart = false;
                intervalCounter = 0.0f;
            }
        }
    }

    private void LateUpdate()
    {
        if(seenTarget)//To rotate the rogue towards player immediately when player is spotted
        {
            transform.LookAt(transform.position + (player.transform.forward * -1.0f));
        }
    }

    public void ResetRogue()
    {        
        if(seenTarget)
        {
            seenTarget = false;
            player.isSeenBy--;
            //stateMachine.ChangeState(new StatePatrol(this));
        }

        stateMachine.ChangeState(new StatePatrol(this));
        //seenTarget = false;
        isDead = false;
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
                        if(!seenTarget && !isDead)
                        {
                            seenTarget = true;
                            player.isSeenBy++;
                        }

                        if(isDead)
                        {
                            if(seenTarget)
                            {
                                player.isSeenBy--;
                                seenTarget = false;
                            }
                        }

                        else
                        {
                            lastSeenPosition = other.transform.position;
                        }                        
                    }

                    else
                    {
                        if(seenTarget)
                        {
                            seenTarget = false;
                            player.isSeenBy--;
                        }
                    }
                }                
            }

            
            else
            {
                if (seenTarget)
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
                        if (!seenTarget && !isDead)
                        {
                            seenTarget = true;
                            player.isSeenBy++;
                        }

                        if (isDead)
                        {
                            if (seenTarget)
                            {
                                player.isSeenBy--;
                                seenTarget = false;
                            }
                        }

                        else
                        {
                            lastSeenPosition = other.transform.position;
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
                }
            }

            else
            {
                if (angle > sightFoV * 0.5 && seenTarget != false)
                {
                    seenTarget = false;
                    player.isSeenBy--;
                }
            }
        }
    }

    void interactionFX()
    {
        if (!isFXStart)
        {
            interactionAura.transform.localScale = initialFXVector;
            interactionAura.SetActive(true);
            isFXStart = true;
            incrementStatus = 0.0f;
        }

        interactionAura.transform.localScale += stepFXVector * Time.deltaTime;
        incrementStatus += 10.0f * Time.deltaTime;

        if (incrementStatus >= finalFXRadius)
        {
            interactionAura.SetActive(false);
            isFXactivate = false;
            isFXStart = false;
        }
    }
}
