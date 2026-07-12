using UnityEngine;
using System.Collections;
using Fungus;

public class EventTrigger : MonoBehaviour
{
    [Header("Event Setting")]
    public PlayerMovement player;
    public Flowchart flowchart;

    public string blockName = "Event";
    public float moveDownTile = 2f;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        StartCoroutine(EventSequence());
    }

    IEnumerator EventSequence()
    {
        while (player.isMoving)
            yield return null;

        player.DisableMovement();

        flowchart.ExecuteBlock(blockName);
    }

    public void StartMoveCutscene()
    {
        Debug.Log("StartMoveCutscene Called");
        StartCoroutine(MoveSequence());
    }

    IEnumerator MoveSequence()
    {
        yield return StartCoroutine(
            player.MoveDownCutscene(moveDownTile)
        );

        player.EnableMovement();
        triggered = false;
    }
}
