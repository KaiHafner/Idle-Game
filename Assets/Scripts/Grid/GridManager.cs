using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int length = 100, height = 100;
    [SerializeField] private float tileSize = 1;
    [SerializeField] GameObject tilePrefab;

    [SerializeField] GameObject gridHolder;

    private List<Tile> tileList = new List<Tile>();
    public Tile[] Tiles { get; private set; }

    public void GenerateGrid()
    {
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = Instantiate(tilePrefab, gridHolder.transform);
                float posX = x * tileSize;
                float posY = y * -tileSize;
                tile.transform.position = new Vector2(posX, posY);

                Tile tileComponent = tile.GetComponent<Tile>();
                if (tileComponent != null)
                {
                    tileList.Add(tileComponent);
                }
            }
        }

        Tiles = tileList.ToArray();

        float gridLength = length * tileSize;
        float gridHeight = height * tileSize;
        gridHolder.transform.position = new Vector2(-gridLength / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2);
    }
}
