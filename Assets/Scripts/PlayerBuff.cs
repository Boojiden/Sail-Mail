using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    public static bool buffed = false;

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerBuff.plr"))
        {
            buffed = readFile();
        }
        else
        {
            buffed = false;
            saveFile(buffed);
        }
    }

    public static void saveFile(bool buff)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerBuff.plr";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, new Buff(buff));
        stream.Close();
    }

    public bool readFile()
    {
       string path = Application.persistentDataPath + "/PlayerBuff.plr";
        BinaryFormatter formatter = new BinaryFormatter();
       FileStream stream = new FileStream(path, FileMode.Open);

       Buff toReturn = formatter.Deserialize(stream) as Buff;
       stream.Close();
       return toReturn.Buffed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Buff
{
    public bool Buffed;

    public Buff(bool bin)
    {
        Buffed = bin;
    }
}
