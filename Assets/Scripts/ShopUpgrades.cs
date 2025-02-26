using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrades : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TMP_Text PriceText;
    [SerializeField] TMP_Text incomeInfoText;
    [SerializeField] Button button;

    [Header("Managers")]
    [SerializeField] GameManager gameManager;

    public int startPrice = 10;
    public float upgradePriceMultiplier;
    public float amountPerUpgrade = 0.1f;

    int level = 0;

    private void Start()
    {
        updateUI();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if (purchaseSuccess)
        {
            level++;
            updateUI();
            print("Shop Item Bought");
        }
    }

    public void updateUI()
    {
        PriceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + " x " + amountPerUpgrade + "/s";
        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;
    }

    int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
        return price;
    }

    public float CalculateIncomePerSecond()
    {
        return amountPerUpgrade * level;
    }
}
