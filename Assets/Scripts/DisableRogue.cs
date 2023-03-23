using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableRogue : MonoBehaviour
{
    RogueController rogueController;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
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

            Debug.Log("Attempting the kill");

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, GetComponent<SphereCollider>().radius))
            {
                if (hit.collider.gameObject == other.gameObject)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        if (rogueController.seenTarget == true)
                        {
                            rogueController.seenTarget = false;
                            if(playerController.isSeenBy == 1)
                            {
                                playerController.rlm.goingRogue = false;
                                playerController.rlm.SeenBy = 0;
                            }
                            playerController.isSeenBy--;
                        }
                        Debug.Log("Killed Rogue");
                        gameObject.SetActive(false);
                        //Destroy(gameObject);
                    }
                }
            }
        }
    }
}
