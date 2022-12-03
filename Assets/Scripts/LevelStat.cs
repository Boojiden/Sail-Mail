using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelStat : MonoBehaviour
{
    public static List<Level> levelList;
    [SerializeField] private List<Level> reff;
    private static bool loaded = false;
    private static int sceneCount;
    void Awake()
    {
        if (!loaded)
        {
            sceneCount = SceneManager.sceneCountInBuildSettings;
            levelList = new List<Level>(sceneCount);
            loadLevels();
        }
        reff = levelList;
    }

    private void Start()
    {
        loadLevels();
        reff = levelList;
    }

    // Update is called once per frame


    private void loadLevels()
    {
        levelList = new List<Level>(sceneCount);
        for (int i = 1; i < sceneCount; i++)
        {
            string name = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            //Debug.Log(sceneCount + " " + levelList.Capacity + " " + i);
            if (File.Exists(Application.persistentDataPath + "/" + name + ".lvl"))
            {
                levelList.Add(loadLevel(name));
            }
            else
            {
                levelList.Add((new Level(name)));
            }
        }

        if (levelList[0].unlocked == false)
        {
            levelList[0].unlocked = true;
        }
        loaded = true;
    }
    public static void saveLevel(Level level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + level.name + ".lvl";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, level);
        stream.Close();
    }

    public static Level loadLevel(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".lvl";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Level toReturn = formatter.Deserialize(stream) as Level;
            stream.Close();
            return toReturn;
        }
        else
        {
            return null;
        }
    }

    public static void clearData()
    {
        for (int i = 1; i < sceneCount; i++)
        {
            string name = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (File.Exists(Application.persistentDataPath + "/" + name + ".lvl"))
            {
                File.Delete(Application.persistentDataPath + "/" + name + ".lvl");
            }
        }
    }
}
[System.Serializable]
public class Level
{
    public string name;
    public bool unlocked;
    public bool beaten;
    public float clearTime;

    public Level(string name)
    {
        this.name = name;
        beaten = false;
        unlocked = false;
        clearTime = 0;  
    }


}
