using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = 0.2f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;
        float xDirection = (transform.position.x - damageSource.position.x) > 0f ? 1f : -1f;
        Vector2 fource = new Vector2(xDirection * knockBackThrust, Vector2.up.y);
        rb.AddForce(fource, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
