using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallManager : MonoBehaviour
{
    public GameObject player;
    public GameObject rogue;

    public uint pearlCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = new Vector3(player.transform.position.x, 1, player.transform.position.z);

        if(rogue != null)
        {
            rogue.transform.position = new Vector3(rogue.transform.position.x, 1, rogue.transform.position.z);
        }        
    }
}
