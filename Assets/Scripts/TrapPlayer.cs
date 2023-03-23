using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapPlayer : MonoBehaviour
{
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
        if(other.tag == "Player" && spinTrap.isinfected)
        {
            Debug.Log("Player Trapped");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
