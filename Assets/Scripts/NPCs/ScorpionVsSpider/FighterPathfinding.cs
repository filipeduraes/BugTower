using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FighterPathfinding : MonoBehaviour
{
    public bool CanWalk { get; set; } = false;

    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance = 4f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float speed = 5f;

    private float newDistance = 2f;
    private Rigidbody2D fighterRigidbody;

    private void Awake()
    {
        fighterRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(GenerateNewDistance(minDistance, maxDistance, 5f));
    }

    private void Update()
    {
        if (!target.gameObject.activeSelf || target == null || !CanWalk)
        {
            fighterRigidbody.velocity = Vector2.zero;
            return;
        }

        GoToTarget();
    }

    private void GoToTarget()
    {
        float distanceX = Mathf.Abs(target.position.x - transform.position.x);

        if (transform.position.x != target.position.x)
        {
            if (distanceX > newDistance)
            {
                float xDirection = (target.position - transform.position).normalized.x;
                Vector2 velocity = Vector2.right * xDirection * speed * Time.deltaTime;
                fighterRigidbody.velocity = velocity;
            }
            else if (distanceX < newDistance)
            {
                float xDirection = (target.position - transform.position).normalized.x;
                Vector2 velocity = Vector2.left * xDirection * speed * Time.deltaTime;
                fighterRigidbody.velocity = velocity;
            }
        }
        else
        {
            fighterRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator GenerateNewDistance(float min, float max, float time)
    {
        while (true)
        {
            newDistance = Random.Range(min, max);
            yield return new WaitForSeconds(time);
        }
    }
}
