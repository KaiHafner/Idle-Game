using Unity.VisualScripting;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    //[SerializeField] GameObject target;
    [SerializeField] private float speed = 1f;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Base").transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
