using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapPlayer : MonoBehaviour
{
    public DisableSpinTrap spinTrap;
    public SegmentManager segMan;
    public OverallManager ovrMan;

    // Start is called before the first frame update
    void Start()
    {
        ovrMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OverallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && spinTrap.isinfected)
        {
            ovrMan.FailState();
            //segMan.RestartSegment();
        }
    }
}
