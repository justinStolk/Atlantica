using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadSystem
{
    private static GameData loadedData;
    public static GameData LoadData()
    {
        if (Application.isEditor)
        {
            string path = Application.dataPath + "/DebugSaves/atlantica.txt";
            return LoadAndConvert(path);
        }
        else
        {
            string path = Application.persistentDataPath + "/savedgames/atlantica.txt";
            return LoadAndConvert(path);
        }
    }
    private static GameData LoadAndConvert(string path)
    {
        StreamReader reader = new StreamReader(path);

        string dataAsJSon = reader.ReadToEnd();
        reader.Close();

        loadedData = JsonUtility.FromJson<GameData>(dataAsJSon);

        return loadedData;
        
    }
}
