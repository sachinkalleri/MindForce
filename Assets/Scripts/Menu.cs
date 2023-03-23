using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelsMenu;
    public bool skipIntro = false;

    public void play()
    {
        StaticData.skipIntro = skipIntro;
        SceneManager.LoadScene("Game");
    }

    public void levels(int lev)
    {
        StaticData.level = lev;
        StaticData.choseLevel = true;
        SceneManager.LoadScene("Game");
    }

    public void menuSwitch(int menuID)
    {
        if(menuID == 0)
        {
            levelsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        if (menuID == 1)
        {
            mainMenu.SetActive(false);
            levelsMenu.SetActive(true);
        }
    }

    public void SkipIntroToggle()
    {
        if (skipIntro)
        {
            skipIntro = false;
        }
        else
        {
            skipIntro = true;
        }
    }

    public void quit()
    {
        Application.Quit();
    }
}
