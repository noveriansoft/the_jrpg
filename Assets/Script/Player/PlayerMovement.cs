using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float tileSize = 1f;

    private Animator animator;

    public bool isMoving;
    public bool canMove = true;
    private Vector3 targetPosition;

    private float lastMoveX = 0;
    private float lastMoveY = -1;

    void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    void Start()
    {
        targetPosition = transform.position;      
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

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

    public void EnableMovement()
    {
        canMove = true;
        Debug.Log("Player movement enabled.");
    }

    public void DisableMovement()
    {
        canMove = false;
        Debug.Log("Player movement disabled.");
    }

    #region playerMoveCutscene
    public IEnumerator MoveDownCutscene(float distance)
    {
        canMove = false;
        SetFacing(0, -1);

        isMoving = true;
        animator.SetBool("IsMoving", true);

        Vector3 target = transform.position + Vector3.down * distance;
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = target;
        isMoving = false;
        animator.SetBool("IsMoving", false);
    }

    private void SetFacing(float x, float y)
    {
        lastMoveX = x;
        lastMoveY = y;

        animator.SetFloat("MoveX", x);
        animator.SetFloat("MoveY", y);
    }
    #endregion
}
