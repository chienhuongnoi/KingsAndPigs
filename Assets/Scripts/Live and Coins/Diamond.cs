using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private GameObject diamondEffectPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController1 player = collision.GetComponent<PlayerController1>();
        if (player != null)
        {
            GameManager.Instance.AddScore(1);
            Instantiate(diamondEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
