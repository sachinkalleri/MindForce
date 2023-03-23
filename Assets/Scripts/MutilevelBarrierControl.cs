using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutilevelBarrierControl : MonoBehaviour
{
    public OverallManager ovrMan;

    public Material redGlow;
    public Material greenGlow;
    public Material greenGlowTransparent;
    public Material purpleGlowTransparent;

    public MeshRenderer[] lv1Elements;
    public MeshRenderer[] lv2Elements;
    public MeshRenderer[] lv3Elements;
    public MeshRenderer[] currElements;

    public GameObject[] spikyKey = new GameObject[3];

    GameObject currSpikyKey;

    public GameObject[] barrier = new GameObject[6];

    Vector3 moveVector;
    Vector3 moveX;
    Vector3 moveZ;

    Vector3[] initialPositions = new Vector3[3];

    public float speed = 6.0f;
    public float motionLimit = 6.0f;
    public float greenDuration = 2.0f;

    float centerPosition = 3.0f;
    float positionStatus = 0.0f;
    float inverter = 1;

    int levelWonCount = 0;

    bool isGreen = false;
    public bool levelClearedFlag = false;

    public bool isAlongX = false;

    // Start is called before the first frame update
    void Start()
    {
        ovrMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OverallManager>();
        moveVector = new Vector3(0, 0, speed);
        currSpikyKey = spikyKey[levelWonCount];
        currElements = lv1Elements;

        for(int i = 0; i < 3; i++)
        {
            initialPositions[i] = spikyKey[i].transform.position;
        }

        moveZ = new Vector3(0, 0, speed);
        moveX = new Vector3(speed, 0, 0);
        if (isAlongX)
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
        if (positionStatus <= motionLimit)
        {
            currSpikyKey.transform.position += moveVector * Time.deltaTime * inverter;
            positionStatus += speed * Time.deltaTime;
            //Debug.Log("ps"+positionStatus);
        }

        else
        {
            inverter = inverter * -1.0f;
            positionStatus = 0.0f;
        }

        if (positionStatus >= (centerPosition - greenDuration - 0.3f) && positionStatus <= (centerPosition + greenDuration + 0.3f))
        {
            if (!isGreen)
            {
                if(levelClearedFlag)
                {
                    levelClearedFlag = false;
                }

                foreach (MeshRenderer m in currElements)
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
                foreach (MeshRenderer m in currElements)
                {
                    m.material = redGlow;
                }
                //spikyKey.GetComponentInChildren<MeshRenderer>().material = redGlow;
                isGreen = false;
            }
        }

        if(levelWonCount >= 3)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {   
        if (other.tag == "Player")
        {
            ovrMan.DisplayInstruction("Press Q when the key goes green to unlock each levels of the multilevel barrier");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(isGreen)
                {
                    levelClearedFlag = true;
                    positionStatus = 0.0f;
                    isGreen = false;
                    inverter = 1.0f;
                    barrier[levelWonCount * 2].GetComponent<MeshRenderer>().material = greenGlowTransparent;
                    barrier[(levelWonCount * 2) + 1].GetComponent<MeshRenderer>().material = greenGlowTransparent;
                    levelWonCount++;

                    switch (levelWonCount)
                    {
                        case 1:
                            currElements = lv2Elements;
                            break;
                        case 2:
                            currElements = lv3Elements;
                            break;
                        default: break;
                    }

                    currSpikyKey = spikyKey[levelWonCount];
                }

                if(!isGreen)
                {
                    if(!levelClearedFlag)
                    {
                        RestartBarrier();
                    }
                }
                //levelClearedFlag = false;
            }
        }
    }

    public void RestartBarrier()
    {
        for(int i = 0; i < 6; i++)
        {
            barrier[i].GetComponent<MeshRenderer>().material = purpleGlowTransparent;
        }
        isGreen = false;
        positionStatus = 0.0f;
        levelWonCount = 0;
        inverter = 1.0f;
        for(int i = 0; i < 3; i++)
        {
            spikyKey[i].transform.position = initialPositions[i];
        }
        currElements = lv1Elements;
        currSpikyKey = spikyKey[0];

        foreach (MeshRenderer m in lv1Elements)
        {
            m.material = redGlow;
        }

        foreach (MeshRenderer m in lv2Elements)
        {
            m.material = redGlow;
        }

        foreach (MeshRenderer m in lv3Elements)
        {
            m.material = redGlow;
        }
    }
}
