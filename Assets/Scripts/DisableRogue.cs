using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableRogue : MonoBehaviour
{
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
        if(other.tag == "Player")
        {
            if(gameObject.tag == "VulnerableEnemy")
            {
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    Destroy(gameObject);
                }                
            }

            //else if(gameObject.tag == "InvincibleEnemy")
            //{
            //    Debug.Log("Player Killed with proximity");
            //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //}
        }
    }
}
