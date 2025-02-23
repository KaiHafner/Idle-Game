using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] ShopUpgrades[] shopUpgrades;
    [SerializeField] int updatesPerSecond = 5;

    [HideInInspector] public float count = 0;
    float nextTimeCheck = 1;
    float lastIncomeValue = 0;

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + 1f / updatesPerSecond; 
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
}
