using Fungus;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Interact();
        }
    }

    void Interact()
    {
        Vector2 facingDirection = new Vector2(playerMovement.LastMoveX,playerMovement.LastMoveY);

        Vector2 checkPos = (Vector2)transform.position + facingDirection;

        Collider2D hit = Physics2D.OverlapPoint(checkPos);

        if (hit != null)
        {
            NpcInteract npc = hit.GetComponent<NpcInteract>();

            if (npc != null)
            {
                npc.Talk();
                Debug.Log("Start Talk");
            }
        }
    }
}
