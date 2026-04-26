using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxCollision : MonoBehaviour
{
    private DestrucableBox destrucableBox;
    void Awake()
    {
        destrucableBox = GetComponent<DestrucableBox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TilemapCollider2D>())
        {
            destrucableBox.Break();
        }
        if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(1, transform);
            destrucableBox.Break();
        }
    }
}
