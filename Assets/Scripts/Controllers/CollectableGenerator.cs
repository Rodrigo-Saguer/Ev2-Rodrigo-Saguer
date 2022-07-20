using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGenerator : MonoBehaviour
{
    //Variables
    [Header("Settings")]
    [SerializeField] private Collectable m_prefab;
    [SerializeField] private int m_count = 10;
    [SerializeField] private int m_cellTypeIndex = 0;
    [SerializeField] private Vector2 m_generationOffset = Vector2.one / 2f;
    [SerializeField] private Transform m_pivot;

    //Methods
    /// <summary>
    /// Generate a list of collectables.
    /// </summary>
    /// <param name="cells"></param>
    /// <returns></returns>
    public List<CollectableData> Generate(List<Generator.Cell> cells)
    {
        List<CollectableData> list = new List<CollectableData>();
        List<Generator.Cell> typeCells = cells.FindAll(c => c.type == m_cellTypeIndex);

        for (int i = 0; i < m_count; i++)
        {
            bool empty;
            Generator.Cell cell;

            do
            {
                empty = true;
                cell = typeCells[Random.Range(0, typeCells.Count)];

                if (list.Find(c => c.position == cell.position) != null) empty = false;
            }
            while (!empty);

            list.Add(new CollectableData(cell.position + m_generationOffset));
        }

        return list;
    }

    /// <summary>
    /// Place the collectables on the tilemaps.
    /// </summary>
    /// <param name="data"> The data to set. </param>
    public void PlaceCollectables(List<CollectableData> collectables)
    {
        foreach (CollectableData collectable in collectables)
        {
            Instantiate(m_prefab, collectable.position, Quaternion.identity, m_pivot);
        }
    }
}
