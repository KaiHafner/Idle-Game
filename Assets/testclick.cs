using UnityEngine;

public class testclick : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>(); // Auto-assign GameManager if not set
        }
    }

    private void OnMouseDown()
    {
        if (anim != null)
        {
            anim.SetTrigger("pressed");
        }
        else
        {
            Debug.LogError("Animator component is missing!");
        }

        if (gameManager != null)
        {
            gameManager.ClickAction();
        }
        else
        {
            Debug.LogError("GameManager reference is missing!");
        }
    }
}
