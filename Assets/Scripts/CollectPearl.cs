using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPearl : MonoBehaviour
{
    public OverallManager ovrMan;
    public DisableSpinTrap spinTrap;
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
        if(other.tag == "Player" && !spinTrap.isinfected)
        {
            Debug.Log("Pearl Collected");
            ovrMan.pearlCount++;
            Destroy(gameObject);
        }
    }
}
