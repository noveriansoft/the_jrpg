using UnityEngine;
using System.Collections;

public class EnemyWander : MonoBehaviour
{
    [Header("Enemy Movement Setting")]
    public float moveSpeed = 3f;
    public float tileSize = 1f;
    public float moveInterval = 2f;
    public bool canMove = true;

    public LayerMask obstacleLayer;

    private bool isMoving;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;

        StartCoroutine(WanderRoutine());
    }

    public void enemyManualWander()
    {
        StartCoroutine(WanderRoutine());
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);

            if (!canMove)
                continue;

            if (!isMoving)
            {
                TryMoveRandom();
            }
        }
    }

    void Update()
    {
        if (!canMove)
            return;

        MoveToTarget();
    }

    void TryMoveRandom()
    {
        Vector2[] directions =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        Vector2 dir = directions[Random.Range(0, directions.Length)];
        Vector3 nextPosition = transform.position + (Vector3)(dir * tileSize);
        Collider2D hit = Physics2D.OverlapPoint(nextPosition,obstacleLayer);

        if (hit == null)
        {
            targetPosition = nextPosition;
            isMoving = true;
        }
    }

    void MoveToTarget()
    {
        if (!isMoving)
            return;

        transform.position = Vector3.MoveTowards(transform.position,targetPosition,moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
