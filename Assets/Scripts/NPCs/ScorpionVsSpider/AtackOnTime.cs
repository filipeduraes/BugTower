using BugTower.Patterns;
using System.Collections;
using UnityEngine;

public class AtackOnTime : MonoBehaviour
{
    public bool CanAtack { get; set; } = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string atackTrigger = "atack";

    [Header("Atack")]
    [SerializeField] private Transform atackPoint;
    [SerializeField] private float atackRadius = 3f;
    [SerializeField] private float strength = 2f;
    [SerializeField] private LayerMask layer;

    private Collider2D isInAtackRange = null;
    private float timeToAtack = 1f;
    private float timer = 0f;

    private void Update()
    {
        if (!CanAtack)
            return;

        AtackOnRandomTime();
    }

    private void AtackOnRandomTime()
    {
        bool lastIsInAtackRange = isInAtackRange;
        isInAtackRange = Physics2D.OverlapCircle(atackPoint.position, atackRadius, layer);

        if (isInAtackRange)
        {
            if (lastIsInAtackRange != isInAtackRange)
                timeToAtack = Random.Range(0.5f, 2f);

            timer += Time.deltaTime;

            if (timer >= timeToAtack)
            {
                Atack();
                timer = 0f;
                isInAtackRange = null;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void Atack()
    {
        animator.SetTrigger(atackTrigger);
        float duration = animator.GetCurrentAnimatorStateInfo(0).length;
        Health health = isInAtackRange.GetComponent<Health>();

        StartCoroutine(TakeDamageOnTime(health, duration));
    }

    private IEnumerator TakeDamageOnTime(Health health, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        health.TakeDamage(strength);
    }

    private void OnDrawGizmosSelected()
    {
        if (atackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atackPoint.position, atackRadius);
    }
}
