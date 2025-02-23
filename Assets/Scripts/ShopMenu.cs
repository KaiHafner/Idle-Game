using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class ShopMenu : MonoBehaviour
{
    //Shop Intro/Outro
    [SerializeField] RectTransform shopMenuRect;
    [SerializeField] float menuTopPosX, menuMiddlePosX;
    [SerializeField] float tweenDuration;

    //Button Intro/Outro
    [SerializeField] RectTransform openShopRect;
    [SerializeField] float buttonTopPosY, buttonBottomPosY;
    [SerializeField] float buttonTweenDuration;

    //UI Elements
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject OpenShopButton;

    private bool isAnimating = false;

    void Start()
    {
        shopMenu.SetActive(false);
    }

    public async void openMenu()
    {
        if (isAnimating) return;
        isAnimating = true;

        shopMenu.SetActive(true);
        await shopMenuIntro();
        OpenShopButton.SetActive(false);

        isAnimating = false;
    }

    public async void closeMenu()
    {
        if (isAnimating) return;
        isAnimating = true;

        OpenShopButton.SetActive(true);
        await shopMenuOutro();
        shopMenu.SetActive(false);

        isAnimating = false;
    }

    async Task shopMenuIntro()
    {
        openShopRect.DOAnchorPosY(buttonTopPosY, buttonTweenDuration);
        await shopMenuRect.DOAnchorPosX(menuMiddlePosX, tweenDuration).AsyncWaitForCompletion();
    }

    async Task shopMenuOutro()
    {
        openShopRect.DOAnchorPosY(buttonBottomPosY, buttonTweenDuration);
        await shopMenuRect.DOAnchorPosX(menuTopPosX, tweenDuration).AsyncWaitForCompletion();
    }
}
