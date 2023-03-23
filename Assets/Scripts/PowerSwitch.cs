using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Acts as a Segment(Level) Manager
public class PowerSwitch : MonoBehaviour
{
    public uint pearlsRequired;//Have to set this for each level
    public int pearlsPlaced = 0;

    public SegmentManager segMan;
    public OverallManager ovrMan;
    public GameObject[] pearlOnSwitch;
    public GameObject[] blockade;
    public GameObject[] roguesInSegment;
    public GameObject[] pearlsInSegment;
    public GameObject[] barriersInSegment;
    public GameObject[] mBarriersInSegment;
    public GameObject playerSpawnPosition;

    public int roguesCount;
    public int pearlsInSegmentCount;
    public int barriersInSegmentCount;
    public int mBarriersInSegmentCount;
    public bool flag;
    public bool islevelClear = false;
    public bool isSwitchedOn = false;
    public bool waitForDeathFX = true;
    public Material greenGlow;
    public Material infectedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        roguesCount = roguesInSegment.Count();
        pearlsInSegmentCount = pearlsInSegment.Count();
        barriersInSegmentCount = barriersInSegment.Count();
        mBarriersInSegmentCount = mBarriersInSegment.Count();
    }

    // Update is called once per frame
    void Update()
    {
        if(!islevelClear)
        {
            flag = true;

            for (int i = 0; i < roguesCount; i++)
            {
                if(!roguesInSegment[i].GetComponent<RogueController>().isDead)
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

        else if(blockade[1].GetComponent<BoxCollider>().enabled == true)
        {
            blockade[0].GetComponent<BoxCollider>().enabled = false;
            blockade[1].GetComponent<BoxCollider>().enabled = false;

            blockade[0].GetComponent<MeshRenderer>().material = greenGlow;
            blockade[1].GetComponent<MeshRenderer>().material = greenGlow;            

            ovrMan.FPSToggle();
            ovrMan.levelClearUI.SetActive(true);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(ovrMan.pearlCount > 0 && !isSwitchedOn)
            {
                ovrMan.DisplayInstruction("Press Q to place the pearl on the power switch");

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("Pearl Placed");
                    pearlsPlaced++;
                    ovrMan.pearlCount--;
                    pearlOnSwitch[pearlsPlaced - 1].GetComponent<MeshRenderer>().material = greenGlow;
                    if (pearlsPlaced >= pearlsRequired)
                    {
                        isSwitchedOn = true;

                        for (int i = 0; i < roguesCount; i++)
                        {
                            roguesInSegment[i].tag = "VulnerableEnemy";
                            roguesInSegment[i].transform.GetChild(2).gameObject.SetActive(false);
                        }
                    }
                }
            }

            else if(!isSwitchedOn)
            {
                ovrMan.DisplayInstruction("Revive and collect pearls and place them back here to power up the segment");
            }

            else
            {
                ovrMan.DisplayInstruction("Segment powered up! Now go kill the rogues to clear the level");
            }
        }
    }

    public void ResetPowerSwitch()
    {
        if(isSwitchedOn)
        {
            isSwitchedOn = false;
        }

        int temp = pearlsPlaced;

        for(int i = 0; i < temp; i++)
        {
            pearlsPlaced--;
            ovrMan.pearlCount++;
            pearlOnSwitch[pearlsPlaced].GetComponent<MeshRenderer>().material = infectedMaterial;
        }

        for (int i = 0; i < roguesCount; i++)
        {
            roguesInSegment[i].GetComponent<RogueController>().ResetRogue();
        }

        for(int i = 0; i < pearlsInSegmentCount; i++)
        {
            //if (pearlsInSegment[i].activeSelf)
            //{
            //    ovrMan.pearlCount--;
            //}
                
            pearlsInSegment[i].GetComponent<CollectPearl>().ResetPearl();
        }

        for (int i = 0; i < barriersInSegmentCount; i++)
        {            
            barriersInSegment[i].SetActive(true);
        }

        for (int i = 0; i < mBarriersInSegmentCount; i++)
        {
            mBarriersInSegment[i].SetActive(true);
            mBarriersInSegment[i].GetComponent<MutilevelBarrierControl>().RestartBarrier();
        }
    }

    public void ClearSegment()
    {
        isSwitchedOn = true;
        islevelClear = true;

        pearlsPlaced = (int)pearlsRequired;

        for (int i = 0; i < pearlsPlaced; i++)
        {
            pearlOnSwitch[i].GetComponent<MeshRenderer>().material = greenGlow;
        }

        for (int i = 0; i < roguesCount; i++)
        {
            roguesInSegment[i].SetActive(false);
        }

        for (int i = 0; i < pearlsInSegmentCount; i++)
        {
            pearlsInSegment[i].SetActive(false);
        }

        for (int i = 0; i < barriersInSegmentCount; i++)
        {
            barriersInSegment[i].SetActive(false);
        }

        for (int i = 0; i < mBarriersInSegmentCount; i++)
        {
            mBarriersInSegment[i].SetActive(false);
        }
    }
}
