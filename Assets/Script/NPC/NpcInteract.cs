using UnityEngine;
using Fungus;

public class NpcInteract : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName = "StartTalk";

    public void Talk()
    {
        flowchart.ExecuteBlock(blockName);
    }
}
