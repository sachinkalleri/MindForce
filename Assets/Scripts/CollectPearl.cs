using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPearl : MonoBehaviour
{
    public OverallManager ovrMan;
    public DisableSpinTrap spinTrap;
    Vector3 initialPosition;

    public Material infectedMaterial;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPearl()
    {
        spinTrap.isinfected = true;
        if(!gameObject.activeSelf)
        {
            ovrMan.pearlCount--;
        }
        gameObject.SetActive(true);
        spinTrap.spinEffect.SetActive(true);
        spinTrap.trap.SetActive(true);
        gameObject.transform.position = initialPosition;
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
