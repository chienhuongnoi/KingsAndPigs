using UnityEngine;

public class DamageSource : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        Box box = collision.gameObject.GetComponent<Box>();
        if (box != null)
        {
            Debug.Log("Box hit!");
            box.TakeDamage(1, gameObject.transform);
        }
        if (enemy != null)
        {
            enemy.TakeDamage(1);
        }
    }
}
