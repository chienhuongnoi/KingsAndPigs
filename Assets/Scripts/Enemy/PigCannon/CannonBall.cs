using UnityEngine;
public class CannonBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Initialize(Vector2 direction, float shootForce)
    {
        direction = direction.normalized;
        rb.linearVelocity = direction * shootForce;
    }
}