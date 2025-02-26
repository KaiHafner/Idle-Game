using Unity.VisualScripting;
using UnityEngine;

public class ClickerNonUI : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] private GameManager gameManager;

    private void OnMouseDown()
    {
        animator.SetBool("Pressed", true);
        gameManager.ClickAction();
    }
}
