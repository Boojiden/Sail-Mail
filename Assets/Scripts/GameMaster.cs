using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
    public Transform spawnPoint;
    public List<GameObject> WinPanel;
    public List<GameObject> LosePanel;
    public static float currentTime = 0;
    public static int currentLevel = 0;
    public static int maxLevel;
    private float startTime;
    public int objs;

    private void Awake()
    {
        maxLevel = SceneManager.sceneCountInBuildSettings - 1;
    }
    void Start()
    {
        Instance = this;
        startTime = Time.time;
        if(currentLevel > maxLevel)
        {
            currentLevel = 0;
        }
    }

    private void Update()
    {
        currentTime = Time.time - startTime;
    }
    public void tickObjs()
    {
        objs--;
        if(objs <= 0)
        {
            //Win Level
            Debug.Log(currentLevel);
            Debug.Log("Max: "+maxLevel);
            Level compare = LevelStat.levelList[currentLevel];
            if (compare.beaten == false || compare.clearTime > currentTime)
            {
                record(compare);
            }
            Destroy(PlayerHealth.player);
            Debug.Log("here");
            foreach(GameObject go in WinPanel)
            {
                Instantiate(go).transform.SetParent(Camera.main.transform.GetChild(0), false);
            }
        }
    }

    public void record(Level level)
    {
        level.beaten = true;
        level.clearTime = currentTime;
        LevelStat.levelList[currentLevel] = level;
        LevelStat.saveLevel(level);

        if(currentLevel+1 < maxLevel)
        {
            LevelStat.levelList[currentLevel + 1].unlocked = true;
        }
    }

    public void onLevelLose()
    {
        Destroy(PlayerHealth.player);
        foreach (GameObject go in LosePanel)
        {
            Instantiate(go).transform.SetParent(Camera.main.transform.GetChild(0), false);
        }
    }
    public static int getNextLevel()
    {
        return currentLevel + 1;
    }

    public static string clockConvert(float time)
    {
        TimeSpan span = TimeSpan.FromSeconds(time);

        return span.ToString(@"mm\:ss\:ff");
    }
}
