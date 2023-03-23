using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public uint pearlsRequired = 2;//Have to set this for each level
    public uint pearlsPlaced = 0;
    public OverallManager ovrMan;
    public GameObject[] pearlOnSwitch;
    public bool isSwitchedOn = false;
    public Material greenGlow;

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
                }
            }
        }
    }
}
