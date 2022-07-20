using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableData
{
    //Variables
    [SerializeField] private Vector2 m_position;

    //Properties
    public Vector2 position => m_position;

    //Constructor
    public CollectableData(Vector2 position)
    {
        m_position = position;
    }
}
