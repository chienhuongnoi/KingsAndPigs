using UnityEngine;

public class BombCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>()?.TakeDamage(1, transform);
        }
    }
    private void DestroyBomb()
    {
        Destroy(gameObject);
    }
    private void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    private void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
