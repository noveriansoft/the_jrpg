using UnityEngine;
using System.Collections;
using Fungus;

public class IntroCutscene : MonoBehaviour
{
    public PlayerMovement player;
    public Flowchart flowchart;
    public float moveTile = 2;

    IEnumerator Start()
    {
        yield return StartCoroutine(
            player.MoveDownCutscene(moveTile)
        );

        // fungus
        flowchart.ExecuteBlock("Intro");
        Debug.Log("Intro cutscene started");
    }
}
