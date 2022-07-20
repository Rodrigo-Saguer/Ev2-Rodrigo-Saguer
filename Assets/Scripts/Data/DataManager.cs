using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Properties
    public static bool initialized = false;

    //Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (initialized) return;
        Data.Load();
        initialized = true;
    }

    /// <summary>
    /// Save the game.
    /// </summary>
    public void Save()
    {
        Data.Save();
    }
}
