using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public int LevelId;

    public void playLevel()
    {
        GameMaster.currentLevel = LevelId;
        SceneManager.LoadScene(GameMaster.currentLevel + 1);
    }

    public void updateBoard(GameObject buttonobj)
    {
        Level level = LevelStat.levelList[LevelId];
        if (level.unlocked == false)
        {
            var button = buttonobj.GetComponent<Button>();
            button.interactable = false;
            buttonobj.transform.GetChild(0).GetComponent<Text>().text = "Locked";
        }
        else if (level.beaten == true)
        {
            buttonobj.transform.GetChild(1).GetComponent<Text>().text = level.name + "     Cleared Time: " + GameMaster.clockConvert(level.clearTime);
        }
        else
        {
            buttonobj.transform.GetChild(1).GetComponent<Text>().text = level.name + "     Cleared Time: Not Cleared";
        }
    }
}
