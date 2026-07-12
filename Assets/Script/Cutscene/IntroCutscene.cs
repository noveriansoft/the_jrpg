using UnityEngine;
using System.Collections;
using Fungus;

public class IntroCutscene : MonoBehaviour
{
    public PlayerMovement player;
    public Flowchart flowchart;
    public float moveTile = 2;

    public EventTrigger eventTrigger;

    IEnumerator Start()
    {
        yield return StartCoroutine(
            player.MoveDownCutscene(moveTile)
        );

        BattleData.PlayerCurrentHP = 20;
        // fungus
        flowchart.ExecuteBlock("Intro");
        Debug.Log("Intro cutscene started");
    }

    public void enableEvent()
    {
        eventTrigger.gameObject.SetActive(true);
    }
}
