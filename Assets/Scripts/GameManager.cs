using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] ShopUpgrades[] shopUpgrades;
    [SerializeField] int updatesPerSecond = 5;

    [HideInInspector] public float count = 0;
    float nextTimeCheck = 1;
    float lastIncomeValue = 0;

    private Building buildingToPlace;
    [SerializeField] GameObject grid;
    public CustomCursor customCursor;
    public Tile[] tiles;

    private void Start()
    {
        GridManager gridManager = UnityEngine.Object.FindFirstObjectByType<GridManager>(); // Get GridManager instance
        if (gridManager != null)
        {
            gridManager.GenerateGrid();
            tiles = gridManager.Tiles; // Assign tiles from GridManager
            if (tiles == null || tiles.Length == 0)
            {
                Debug.LogError("Tiles array is empty in GridManager. Ensure tiles are generated correctly.");
            }
        }
        else
        {
            Debug.LogError("GridManager not found in the scene.");
        }
        UpdateUI();
    }

    void Update()
    {
        if (nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + 1f / updatesPerSecond; 
        }

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null) 
        {
            Tile nearestTile = null;
            float nearestDistance = float.MaxValue;
            foreach (Tile tile in tiles)
            { 
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearestTile = tile;
                }
            }
            if (nearestTile.isOccupied == false)
            {
                Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);
                buildingToPlace = null;
                nearestTile.isOccupied = true;
                grid.SetActive(false);
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }

        }
    }

    void IdleCalculate()
    {
        float sum = 0;
        foreach (var shopUpgrade in shopUpgrades)
        {
            sum += shopUpgrade.CalculateIncomePerSecond();
            shopUpgrade.updateUI();
        }
        lastIncomeValue = sum;
        count += sum / updatesPerSecond;
        UpdateUI();
    }

    public void ClickAction()
    {
        count++;
        UpdateUI();
        print("Clicked Rock/Clicker");
    }

    public Boolean PurchaseAction(int cost)
    {
        if(count >= cost)
        {
            count -= cost;
            UpdateUI();
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        countText.text = Mathf.RoundToInt(count).ToString();
        incomeText.text = lastIncomeValue.ToString() + "/s";
    }

    public void buyBuilding(Building building)
    {
        if (count >= building.price)
        {
            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;    
            Cursor.visible = false;

            count -= building.price;
            buildingToPlace = building;
            grid.SetActive(true);
        }
    }
}
