using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RogueLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool goingRogue = false;
    public float rogueLevel = 0;
    public uint SeenBy = 0;
    public GameObject playerObject;
    Color playerColor;
    Color blue = new Color(0.24f, 0.375f, 0.5f, 1f);
    Color red = new Color(0.5f, 0.03f, 0.14f, 1f);


    // Update is called once per frame
    void Update()
    {
        if(goingRogue)
        {
            if(rogueLevel < 1)
            {
                rogueLevel += Time.deltaTime * SeenBy * 0.9f;
            }

            else
            {
                Debug.Log("Gone Rogue!");
                rogueLevel = 1;
                goingRogue = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        else
        {
            if(rogueLevel > 0)
            {
                rogueLevel -= Time.deltaTime * 0.4f;
            }

            else
            {
                rogueLevel = 0;
            }
        }

        playerColor = Color.Lerp(blue, red, rogueLevel);
        playerObject.GetComponent<Renderer>().material.SetColor("_Color", playerColor);

    }
}
