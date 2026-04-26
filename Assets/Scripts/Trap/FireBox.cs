using System.Collections;
using UnityEngine;

public class FireBox : MonoBehaviour
{
    [Header("Cài đặt thời gian")]
    [Tooltip("Thời gian nghỉ giữa các lần phun lửa")]
    [SerializeField] private float attackDelay = 5f;

    [Tooltip("Thời gian chờ trước khi phun lửa lần đầu tiên")]
    [SerializeField] private float startAttackDelay = 0f;

    [Header("Thành phần")]
    [SerializeField] private GameObject fireBoxCollider;
    [SerializeField] private AnimationClip attackAnimation;

    private float attackAnimationLength;
    private Animator animator;

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
        StartCoroutine(FireBoxRoutine());
        Debug.Log("Attack Animation FireBox Length : " + attackAnimationLength);
    }

    private IEnumerator FireBoxRoutine()
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
    private void Attack()
    {
        animator.SetTrigger("FireBoxAttack");
    }
    public void ActivateTrap()
    {
        fireBoxCollider.SetActive(true);
    }

    public void DeactivateTrap()
    {
        fireBoxCollider.SetActive(false);
    }
}