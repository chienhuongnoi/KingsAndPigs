using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 2f;
    private KnockBack knockBack;
    private int currentHealth;
    private Animator animator;
    private DestrucableBox destrucableBox;
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        animator = GetComponent<Animator>();
        destrucableBox = GetComponent<DestrucableBox>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        animator.SetBool("isHitting", knockBack.GettingKnockedBack);
    }
    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        currentHealth -= damageAmount;
        knockBack.GetKnockedBack(hitTransform, knockBackThrustAmount);

        if (currentHealth <= 0)
        {
            DestroyBox();
        }
    }

    private void DestroyBox()
    {
        destrucableBox.Break();
        Destroy(gameObject);
    }
}
