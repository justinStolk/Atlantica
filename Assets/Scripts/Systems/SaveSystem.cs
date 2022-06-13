using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static GameData saveData;
    public static void SaveData(GameData dataToSave)
    {
        saveData = dataToSave;
        if (Application.isEditor)
        {
            string path = Application.dataPath + "/DebugSaves/atlantica.txt";
            ConvertAndSave(path);
        }
        else
        {
            string path = Application.persistentDataPath + "/savedgames/atlantica.txt";
            ConvertAndSave(path);
        }
    }

    private static void ConvertAndSave(string path)
    {
        //Convert data to saveable data
        StreamWriter writer = new StreamWriter(path);
        string data = JsonUtility.ToJson(saveData, true);

        //Save data
        writer.Write(data);
        writer.Flush();
        writer.Close();
    }
}
