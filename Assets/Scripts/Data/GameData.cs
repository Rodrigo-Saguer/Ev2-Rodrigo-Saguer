using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Variables
    [SerializeField] private bool m_initialized;
    [SerializeField] private List<Generator.Cell> m_cells;
    [SerializeField] private List<CollectableData> m_collectables;
    [SerializeField] private Vector2 m_playerPosition;
    [SerializeField] private int m_score;

    //Properties
    public bool initialized { get => m_initialized; set => m_initialized = value; }
    public List<Generator.Cell> cells { get => m_cells; set => m_cells = value; }
    public List<CollectableData> collectables { get => m_collectables; set => m_collectables = value; }
    public Vector2 playerPosition { get => m_playerPosition; set => m_playerPosition = value; }
    public int score { get => m_score; set => m_score = value; }

    //Constructor
    public GameData()
    {
        initialized = false;
        cells = null;
        m_collectables = null;
        playerPosition = Vector2.zero;
        m_score = 0;
    }
}
