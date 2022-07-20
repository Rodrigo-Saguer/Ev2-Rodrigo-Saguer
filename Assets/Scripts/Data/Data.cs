using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    //Properties
    public static GameData game { get; private set; }

    //Methods
    /// <summary>
    /// Load the data.
    /// </summary>
    public static void Load()
    {
        game = SaveManager.Load<GameData>();
    }

    /// <summary>
    /// Save the data.
    /// </summary>
    public static void Save()
    {
        SaveManager.Save(game);
    }

    public static void Reset()
    {
        game = new GameData();
    }
}
