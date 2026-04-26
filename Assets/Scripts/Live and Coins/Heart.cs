using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject heartEffectPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.AddHealth(1);
            Instantiate(heartEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
