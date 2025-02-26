using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int length = 100, height = 100;
    [SerializeField] private float tileSize = 1;
    [SerializeField] GameObject tilePrefab;

    [SerializeField] GameObject gridHolder;

    private List<Tile> tileList = new List<Tile>(); // Store references
    public Tile[] Tiles { get; private set; } // Expose as an array

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

                Tile tileComponent = tile.GetComponent<Tile>(); // Get the Tile script
                if (tileComponent != null)
                {
                    tileList.Add(tileComponent); // Store it
                }
            }
        }

        Tiles = tileList.ToArray(); // Convert list to array

        float gridLength = length * tileSize;
        float gridHeight = height * tileSize;
        gridHolder.transform.position = new Vector2(-gridLength / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2);
    }
}
