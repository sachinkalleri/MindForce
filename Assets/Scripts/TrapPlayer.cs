using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapPlayer : MonoBehaviour
{
    public DisableSpinTrap spinTrap;
    public SegmentManager segMan;

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
        if(other.tag == "Player" && spinTrap.isinfected)
        {
            Debug.Log("Player Trapped");

            //segMan.restartInProgress = true;
            segMan.RestartSegment();
            //segMan.restartInProgress = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
