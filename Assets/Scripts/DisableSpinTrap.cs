using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpinTrap : MonoBehaviour
{

    public GameObject spinTrap;
    public GameObject spinEffect;
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
        if(other.tag == "Player")
        {
            spinEffect.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Q))
            {
                //if (spinTrap.transform.position.y < 1.0f)
                //    spinTrap.transform.position.y = spinTrap.transform.position.y++;
                //else
                //    spinTrap.transform.y = 1.0f;
                Destroy(spinTrap);
            }
        }
    }
}
