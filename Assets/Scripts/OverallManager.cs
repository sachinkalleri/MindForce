using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class OverallManager : MonoBehaviour
{

    public GameObject interactionAura;
    public GameObject failBoard;
    public GameObject quitConfirmation;
    public GameObject fpsController;
    public GameObject levelClearUI;
    public GameObject demoOverUI;
    public GameObject onScreenHUD;
    public GameObject introTextUI;

    public Waypoint introCameraWaypoint;

    public Text instructionUI;
    public Text pearlCountText;

    public bool showingInstructions = false;
    float switchOffTimer = 1.0f;

    bool isFXStart = false;
    bool isFXactivate = false;

    public float finalFXRadius = 7.0f;
    public float incrementStatus = 0.0f;

    public Vector3 initialFXVector = new Vector3(1.0f, 0.1f, 1.0f);
    public Vector3 stepFXVector = new Vector3(10.0f, 0.0f, 10.0f);

    public Camera introCamera;
    public Camera mainCamera;

    public AudioListener introListener;
    public AudioListener mainListener;

    public bool isStartSequence = false;
    public bool isLevelPicked = false;

    public float cameraSpeed = 2.0f;

    float step;

    public int pearlCount;//Number pearls in the inventory is stored here

    // Start is called before the first frame update
    void Start()
    {
        isStartSequence = !StaticData.skipIntro;

        if(isStartSequence)
        {
            fpsController.GetComponent<FirstPersonController>().enabled = false;
            mainCamera.enabled = false;
            mainListener.enabled = false;
            introCamera.enabled = true;
            introListener.enabled = true;
            introTextUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pearlCountText.text = pearlCount.ToString();//For displaying on the HUD

        if(showingInstructions)
        {
            showingInstructions = false;
            switchOffTimer = 1.0f;
        }
        else
        {
            switchOffTimer -= Time.deltaTime;
            if(switchOffTimer <= 0.0f)
            {
                instructionUI.enabled = false;
            }
        }
        if(isStartSequence)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                EndIntro();
            }

            step = cameraSpeed * Time.deltaTime;
            
            introCamera.transform.position = Vector3.MoveTowards(introCamera.transform.position, introCameraWaypoint.transform.position, step);            

            if (introCamera.transform.position == introCameraWaypoint.transform.position)
            {
                introCamera.transform.rotation = introCameraWaypoint.transform.rotation;
                introCameraWaypoint = introCameraWaypoint.nextWaypoint;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) //To access menu
        {

            FPSToggle();
            quitConfirmation.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            isFXStart = false;
            isFXactivate = true;
        }

        if(isFXactivate)
        {
            interactionFX();
        }

        if(isLevelPicked && levelClearUI.activeInHierarchy == true) //Workaround to switch off level clear UI during segment load
        {
            levelClearUI.SetActive(false);
            isLevelPicked = false;
        }
    }

    void interactionFX()
    {
        if(!isFXStart)
        {
            interactionAura.transform.localScale = initialFXVector;
            interactionAura.SetActive(true);
            isFXStart = true;
            incrementStatus = 0.0f;
        }

        interactionAura.transform.localScale += stepFXVector * Time.deltaTime;
        incrementStatus += 10.0f * Time.deltaTime;

        if(incrementStatus >= finalFXRadius)
        {
            interactionAura.SetActive(false);
            isFXactivate = false;
            isFXStart = false;
        }
    }

    public void DisplayInstruction(string str)
    {
        showingInstructions = true;
        instructionUI.enabled = true;
        instructionUI.text = str;
    }

    public void FailState()
    {
        FPSToggle();
        failBoard.SetActive(true);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Home");
    }

    public void FPSToggle()
    {
        if(fpsController.activeSelf)
        {
            fpsController.SetActive(false);
            introCamera.enabled = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            onScreenHUD.SetActive(false);
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            introCamera.enabled = false;
            fpsController.SetActive(true);
            onScreenHUD.SetActive(true);
        }
    }

    public void EndIntro()
    {
        fpsController.GetComponent<FirstPersonController>().enabled = true;
        introCamera.enabled = false;
        introListener.enabled = false;
        mainCamera.enabled = true;
        mainListener.enabled = true;
        isStartSequence = false;
        introTextUI.SetActive(false);
    }
}
