using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartBehavior : MonoBehaviour
{
    public bool isWin;
    public bool doStart;
    private void Start()
    {
        if (doStart)
        {
            if (isWin)
            {
                transform.GetChild(1).GetComponent<Text>().text = "Cleared Time: " + GameMaster.clockConvert(GameMaster.currentTime);
            }
            else
            {
                transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }

    public void nextLevel()
    {
        if (GameMaster.currentLevel + 1 >= GameMaster.maxLevel)
        {
            SceneManager.LoadScene(0);
            GameMaster.currentLevel = 0;
        }
        else
        {
            Debug.Log(GameMaster.currentLevel + 1 + " " + GameMaster.currentLevel);
            GameMaster.currentLevel += 1;
            SceneManager.LoadScene(GameMaster.currentLevel + 1);
            
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void restart()
    {
        SceneManager.LoadScene(GameMaster.currentLevel + 1);
    }
}
