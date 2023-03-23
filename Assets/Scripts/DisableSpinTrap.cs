using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpinTrap : MonoBehaviour
{

    public GameObject spinEffect;
    public GameObject pearl;
    public GameObject trap;
    
    public Material greenGlow;

    public Vector3 pearlPosition;
    public float speed = 1.0f;
    public bool isinfected = true;

    // Start is called before the first frame update
    void Start()
    {
        pearlPosition = new Vector3(transform.position.x, 2.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (!isinfected)
        {
            pearl.transform.position = Vector3.MoveTowards(transform.position, pearlPosition, step);
            pearl.GetComponent<MeshRenderer>().material = greenGlow;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(isinfected)
            {
                spinEffect.SetActive(true);
            }
            
            if(Input.GetKeyDown(KeyCode.Q))
            {
                isinfected = false;
                spinEffect.SetActive(false);
                trap.SetActive(false);
                //Destroy(trap);
            }
        }
    }
}
