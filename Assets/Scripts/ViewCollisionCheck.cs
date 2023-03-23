using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PROBABLY DELETE THIS

public class ViewCollisionCheck : MonoBehaviour
{

    public RogueLevelManager rgm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Spotted");
            rgm.goingRogue = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Escaped");
            rgm.goingRogue = false;
        }
    }
}
