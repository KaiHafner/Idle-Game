using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColour;
    [SerializeField] private GameObject highlight;

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
