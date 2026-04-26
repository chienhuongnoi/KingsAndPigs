using System.Collections;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [Header("Cài đặt thời gian")]
    [Tooltip("Khoảng thời gian giữa mỗi lần đâm")]
    [SerializeField] private float attackDelay = 3f;

    [Tooltip("Thời gian chờ trước khi đâm lần đầu tiên")]
    [SerializeField] private float startAttackDelay = 2f;

    [Header("Thành phần")]
    [SerializeField] private GameObject spearCollider;
    [SerializeField] private AnimationClip attackAnimation;

    private Animator animator;
    private float attackAnimationLength;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (attackAnimation != null)
        {
            attackAnimationLength = attackAnimation.length;
        }
        DeactivateTrap();
    }

    private void Start()
    {
        StartCoroutine(SpearRoutine());
        Debug.Log("Attack Animation Spear Length : " + attackAnimationLength);
    }

    private IEnumerator SpearRoutine()
    {
        if (startAttackDelay > 0)
        {
            yield return new WaitForSeconds(startAttackDelay);
        }

        while (true)
        {
            Attack();
            yield return new WaitForSeconds(attackAnimationLength);

            yield return new WaitForSeconds(attackDelay);
        }
    }

    public void ActivateTrap()
    {
        if (spearCollider != null)
        {
            spearCollider.SetActive(true);
        }
    }

    public void DeactivateTrap()
    {
        if (spearCollider != null)
        {
            spearCollider.SetActive(false);
        }
    }

    private void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("SpearAttack");
        }
    }
}