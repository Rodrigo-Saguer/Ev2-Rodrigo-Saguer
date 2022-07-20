using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    //Structs
    [System.Serializable]
    public struct Cell
    {
        //Variables
        [SerializeField] private Vector2Int m_position;
        [SerializeField] private int m_type;

        //Properties
        public Vector2Int position => m_position;
        public int type => m_type;

        //Constructors
        public Cell(Vector2Int position, int type)
        {
            m_position = position;
            m_type = type;
        }
    }

    [System.Serializable]
    public struct GenerationTile
    {
        //Variables
        [SerializeField] private TileBase m_tile;
        [SerializeField] private int m_influence;

        //Properties
        public TileBase tile => m_tile;
        public int influence => m_influence;

        //Constructors
        public GenerationTile(TileBase tile, int influence)
        {
            m_tile = tile;
            m_influence = influence;
        }
    }

    //Variables
    [Header("Settings")]
    [SerializeField] private int m_generationSize = 20;
    [SerializeField] private float m_perlinMultiplier = 0.25f;

    [Header("References")]
    [SerializeField] private Tilemap m_tilemap = null;
    [SerializeField] private List<GenerationTile> m_generationTiles = new List<GenerationTile>();

    //Methods
    /// <summary>
    /// Generate a new generation values.
    /// </summary>
    public List<Cell> Generate()
    {
        List<Cell> cells = new List<Cell>();

        for (int y = 0; y < m_generationSize; y++)
        {
            for (int x = 0; x < m_generationSize; x++)
            {
                cells.Add(CreatePerlinCell(new Vector2Int(x, y)));
            }
        }

        return cells;
    }

    /// <summary>
    /// Place the tiles on the tilemap.
    /// </summary>
    /// <param name="cells"></param>
    public void PlaceGeneration(List<Cell> cells)
    {
        m_tilemap.ClearAllTiles();

        foreach (Cell cell in cells)
        {
            Vector3Int position = Vector3Int.zero;
            position.x = cell.position.x;
            position.y = cell.position.y;

            TileBase tile = m_generationTiles[cell.type].tile;
            m_tilemap.SetTile(position, tile);
        }
    }

    /// <summary>
    /// Create a perlin based tile.
    /// </summary>
    /// <param name="position"> The position to set </param>
    /// <returns> The cell </returns>
    private Cell CreatePerlinCell(Vector2Int position)
    {
        float perlinValue = Mathf.PerlinNoise(position.x * m_perlinMultiplier, position.y * m_perlinMultiplier);
        perlinValue = Mathf.Clamp01(perlinValue);

        int totalInfluence = 0;
        m_generationTiles.ForEach(c => totalInfluence += c.influence);

        int influenceValue = Mathf.FloorToInt(perlinValue * totalInfluence);
        int accumulatedInfluence = 0;
        int type = -1;

        for (int i = 0; i < m_generationTiles.Count; i++)
        {
            accumulatedInfluence += m_generationTiles[i].influence;
            if (influenceValue <= accumulatedInfluence)
            {
                type = i;
                break;
            }
        }

        return new Cell(position, type);
    }
}
