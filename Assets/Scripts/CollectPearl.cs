using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPearl : MonoBehaviour
{
    public OverallManager ovrMan;
    public DisableSpinTrap spinTrap;

    public Material infectedMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPearl()
    {
        spinTrap.isinfected = true;
        spinTrap.spinEffect.SetActive(true);
        spinTrap.trap.SetActive(true);
        gameObject.transform.position = spinTrap.pearlPosition;
        gameObject.GetComponent<MeshRenderer>().material = infectedMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !spinTrap.isinfected)
        {
            Debug.Log("Pearl Collected");
            ovrMan.pearlCount++;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
