using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class SegmentManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] powerSwitches;
    //public FirstPersonController fpController;

    //public bool restartInProgress = false;

    public OverallManager ovrMan;

    //public Vector3[] playerSpawnLocations;
        
    public int currSegment = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartSegment()
    {
        Debug.Log("Current Segment:" + currSegment);
        player.SetActive(false);
        player.transform.position = powerSwitches[currSegment - 1].GetComponentInChildren<PowerSwitch>().playerSpawnPosition.transform.position;
        player.SetActive(true);
        //fpController.Start();
        //player.GetComponent<FirstPersonController>().Start()
        powerSwitches[currSegment - 1].GetComponentInChildren<PowerSwitch>().ResetPowerSwitch();

    }
}
