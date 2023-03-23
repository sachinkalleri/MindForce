using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    public OverallManager ovrMan;

    public Material redGlow;
    public Material greenGlow;

    public MeshRenderer[] spikyKeyElements;

    public GameObject spikyKey;
    Vector3 moveVector;
    Vector3 moveX;
    Vector3 moveZ;

    public float speed = 6.0f;
    public float motionLimit = 6.0f;
    public float greenDuration = 2.0f;

    float centerPosition = 3.0f;
    float positionStatus = 0.0f;
    float inverter = 1;

    bool isGreen = false;
    public bool isAlongX = false;

    // Start is called before the first frame update
    void Start()
    {
        ovrMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OverallManager>();

        moveZ = new Vector3(0, 0, speed);
        moveX = new Vector3(speed, 0, 0);
        if(isAlongX)
        {
            moveVector = moveX;
        }

        else
        {
            moveVector = moveZ;
        }
        //initialPosition = spikyKey.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(positionStatus <= motionLimit)
        {
            spikyKey.transform.position += moveVector * Time.deltaTime * inverter;
            positionStatus += speed * Time.deltaTime;
            //Debug.Log("ps"+positionStatus);
        }

        else
        {
            inverter = inverter * -1.0f;
            positionStatus = 0.0f;
        }

        if (positionStatus >= (centerPosition - greenDuration -0.3f) && positionStatus <= (centerPosition + greenDuration + 0.3f))
        {
            if (!isGreen)
            {
                foreach (MeshRenderer m in spikyKeyElements)
                {
                    m.material = greenGlow;
                }
                //spikyKey.GetComponentInChildren<MeshRenderer>().material = greenGlow;
                isGreen = true;
            }
        }

        else
        {
            if (isGreen)
            {
                foreach (MeshRenderer m in spikyKeyElements)
                {
                    m.material = redGlow;
                }
                //spikyKey.GetComponentInChildren<MeshRenderer>().material = redGlow;
                isGreen = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            ovrMan.DisplayInstruction("Press Q when green to disable the barrier");

            if (Input.GetKeyDown(KeyCode.Q) && isGreen)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
