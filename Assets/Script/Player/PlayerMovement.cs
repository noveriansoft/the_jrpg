using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float tileSize = 1f;

    private Animator animator;

    public bool isMoving;
    private Vector3 targetPosition;

    private float lastMoveX = 0;
    private float lastMoveY = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }

        MoveToTarget();

        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MoveX", lastMoveX);
        animator.SetFloat("MoveY", lastMoveY);
    }

    void HandleInput()
    {
        int moveX = 0;
        int moveY = 0;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Cegah diagonal
        if (Mathf.Abs(horizontal) > 0)
        {
            moveX = (int)Mathf.Sign(horizontal);
        }
        else if (Mathf.Abs(vertical) > 0)
        {
            moveY = (int)Mathf.Sign(vertical);
        }

        if (moveX != 0 || moveY != 0)
        {
            lastMoveX = moveX;
            lastMoveY = moveY;

            targetPosition = transform.position +
                             new Vector3(
                                 moveX * tileSize,
                                 moveY * tileSize,
                                 0);

            isMoving = true;
        }
    }

    void MoveToTarget()
    {
        if (!isMoving)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
