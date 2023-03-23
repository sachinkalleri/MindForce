using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableRogue : MonoBehaviour
{
    RogueController rogueController;
    PlayerController playerController;
    OverallManager ovrMan;
    public GameObject deadRogue;

    // Start is called before the first frame update
    void Start()
    {
        ovrMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OverallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "VulnerableEnemy")
        {
            Vector3 direction = other.transform.position - transform.position;
            RaycastHit hit;
            rogueController = gameObject.GetComponent<RogueController>();
            playerController = gameObject.GetComponent<RogueController>().player;

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, GetComponent<SphereCollider>().radius))
            {
                if (hit.collider.gameObject == other.gameObject)
                {
                    ovrMan.DisplayInstruction("Enemy is vulnerable as segment is powered up. Press Q to eliminate the rogue");
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        if (rogueController.seenTarget)
                        {
                            rogueController.seenTarget = false;
                            rogueController.player.isSeenBy--;                           
                        }

                        rogueController.isDead = true;
                        Instantiate(deadRogue, gameObject.transform.position, gameObject.transform.rotation);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }   
}
