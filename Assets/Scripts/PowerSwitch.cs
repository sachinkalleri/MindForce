using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Acts as a Segment(Level) Manager
public class PowerSwitch : MonoBehaviour
{
    public uint pearlsRequired = 2;//Have to set this for each level
    public uint pearlsPlaced = 0;

    public OverallManager ovrMan;
    public GameObject[] pearlOnSwitch;
    public GameObject[] blockade;
    public GameObject[] roguesInSegment;
    public uint roguesCount;

    public bool flag = false;
    public bool islevelClear = false;
    public bool isSwitchedOn = false;
    public Material greenGlow;

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
                if(roguesInSegment[i] != null)
                {
                    flag = false;
                }
            }

            if(flag)
            {
                islevelClear = true;
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
                        //roguesInSegment[i].spinEffect.SetActive(false);
                    }                    
                }
            }
        }
    }
}
