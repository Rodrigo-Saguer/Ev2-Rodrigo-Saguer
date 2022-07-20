using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    //Properties
    public static string path => Application.persistentDataPath + "/save.json";

    //Methods
    /// <summary>
    /// Method to save a object to a json file.
    /// </summary>
    public static void Save(object data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    /// <summary>
    /// Try to load from the data
    /// </summary>
    /// <typeparam name="T"> The type of the data to load from the json object </typeparam>
    /// <returns> The data </returns>
    public static T Load<T>() where T : new()
    {
        T data = new T();

        bool fileExists = File.Exists(path);
        if (fileExists)
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, data);
        }

        return data;
    }
}
