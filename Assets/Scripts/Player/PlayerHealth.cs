using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float knockBackThrustAmount = 10f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    public bool IsDead { get; private set; }
    private Animator animator;
    private int deadAnimationHash = Animator.StringToHash("isDead");

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        IsDead = false;
        currentHealth = maxHealth;
        UIManager.Instance.UpdateHpBar(currentHealth);
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) return;
        ScreenShakeManager.Instance.ShakeScreen();
        knockBack.GetKnockedBack(hitTransform, knockBackThrustAmount);
        canTakeDamage = false;
        currentHealth -= damageAmount;
        UIManager.Instance.UpdateHpBar(currentHealth);
        StartCoroutine(DamageRecoveryRoutine());
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void AddHealth(int healAmount)
    {
        if (IsDead) return;
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UIManager.Instance.UpdateHpBar(currentHealth);
    }

    private void Die()
    {
        StartCoroutine(DieRoutine());
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    private IEnumerator DieRoutine()
    {
        IsDead = true;
        animator.SetTrigger(deadAnimationHash);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }

}
