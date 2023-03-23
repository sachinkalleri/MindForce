using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Acts as a Segment(Level) Manager
public class PowerSwitch : MonoBehaviour
{
    public uint pearlsRequired;//Have to set this for each level
    public uint pearlsPlaced = 0;

    public SegmentManager segMan;
    public OverallManager ovrMan;
    public GameObject[] pearlOnSwitch;
    public GameObject[] blockade;
    public GameObject[] roguesInSegment;
    public GameObject[] pearlsInSegment;
    public GameObject playerSpawnPosition;

    public uint roguesCount;
    public int pearlsInSegmentCount;

    public bool flag;
    public bool islevelClear = false;
    public bool isSwitchedOn = false;
    public Material greenGlow;
    public Material infectedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!islevelClear)
        {
            flag = true;

            for (int i = 0; i < roguesCount; i++)
            {
                if(roguesInSegment[i].activeInHierarchy)
                {
                    flag = false;
                }
            }

            if(flag)
            {
                islevelClear = true;
                segMan.currSegment++;
            }
        }

        else
        {
            blockade[0].GetComponent<BoxCollider>().enabled = false;
            blockade[1].GetComponent<BoxCollider>().enabled = false;

            blockade[0].GetComponent<MeshRenderer>().material = greenGlow;
            blockade[1].GetComponent<MeshRenderer>().material = greenGlow;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.Q) && !isSwitchedOn)
        {
            if(ovrMan.pearlCount > 0)
            {
                Debug.Log("Pearl Placed");
                pearlsPlaced++;                
                ovrMan.pearlCount--;
                pearlOnSwitch[pearlsPlaced - 1].GetComponent<MeshRenderer>().material = greenGlow;
                if (pearlsPlaced >= pearlsRequired)
                {
                    isSwitchedOn = true;

                    for(int i = 0; i < roguesCount; i++)
                    {
                        roguesInSegment[i].tag = "VulnerableEnemy";
                        roguesInSegment[i].transform.GetChild(2).gameObject.SetActive(false);
                    }                    
                }
            }
        }
    }

    public void ResetPowerSwitch()
    {
        if(isSwitchedOn)
        {
            isSwitchedOn = false;
        }

        int temp = (int)pearlsPlaced;

        for(int i = 0; i < temp; i++)
        {
            pearlsPlaced--;
            pearlOnSwitch[pearlsPlaced].GetComponent<MeshRenderer>().material = infectedMaterial;
        }

        for (int i = 0; i < roguesCount; i++)
        {
            roguesInSegment[i].GetComponent<RogueController>().ResetRogue();
        }

        for(int i = 0; i < pearlsInSegmentCount; i++)
        {
            pearlsInSegment[i].GetComponent<CollectPearl>().ResetPearl();
        }
    }
}
