using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColour,occupiedColor;
    [SerializeField] private GameObject highlight;

    public bool isOccupied;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isOccupied) 
        {
            rend.color = occupiedColor;
        }
        else
        {
            rend.color = baseColour;
        }
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
