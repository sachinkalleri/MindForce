using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class SegmentManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] powerSwitches;
    public GameObject floor;

    public Material greenGlow;
    
    public OverallManager ovrMan;

    public int currSegment = 1;

    // Start is called before the first frame update
    void Start()
    {
        ovrMan = gameObject.GetComponent<OverallManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticData.choseLevel)
        {
            ovrMan.isStartSequence = false;
            SegmentLoad(StaticData.level);
            StaticData.choseLevel = false;
        }

        if(currSegment == 5)
        {
            floor.GetComponent<MeshRenderer>().material = greenGlow;
            ovrMan.levelClearUI.SetActive(false);
            ovrMan.demoOverUI.SetActive(true);
        }
    }

    public void RestartSegment()
    {
        ovrMan.FPSToggle();
        ovrMan.failBoard.SetActive(false);        
        
        Vector3 rotate = new Vector3(0.0f, 0.0f, 0.0f);
        Debug.Log("Current Segment:" + currSegment);
        player.SetActive(false);
        player.transform.position = powerSwitches[currSegment - 1].GetComponentInChildren<PowerSwitch>().playerSpawnPosition.transform.position; //Player spawn positions are stored as empty game objects in power switches for each segment.        
        player.SetActive(true);        
        powerSwitches[currSegment - 1].GetComponentInChildren<PowerSwitch>().ResetPowerSwitch(); //Reset function in corresponding power switch is called        
    }

    public void SegmentLoad(int level)
    {
        switch (level)
        {
            case 1: currSegment = 1;
                    ovrMan.pearlCount = 0;                    
                    RestartSegment();
                    break;

            case 2:
                currSegment = 2;
                ovrMan.pearlCount = 1;
                RestartSegment();
                break;

            case 3:
                currSegment = 3;
                ovrMan.pearlCount = 2;
                RestartSegment();
                break;

            case 4:
                currSegment = 4;
                ovrMan.pearlCount = 3;
                RestartSegment();
                break;

            default:Debug.Log("Invalid Level");
                break;
        }

        for (int i = 0; i < level - 1; i++)
        {
            powerSwitches[i].GetComponentInChildren<PowerSwitch>().ClearSegment();
        }

        ovrMan.isLevelPicked = true;
    }
}
