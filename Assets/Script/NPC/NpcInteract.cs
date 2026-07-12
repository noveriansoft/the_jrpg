using UnityEngine;
using Fungus;

public class NpcInteract : MonoBehaviour
{
    public Flowchart flowchart;
    public string firstBlock = "StartTalk";
    public string secondBlock = "TalkAgain";
    public bool canInteract = true;

    [SerializeField] private int talkCount = 0;

    public void Talk()
    {
        if (!canInteract)
            return;

        canInteract = false;
        talkCount++;

        if (talkCount <= 1)
        {
            flowchart.ExecuteBlock(firstBlock);
        }
        else
        {
            if (!string.IsNullOrEmpty(secondBlock))
            {
                flowchart.ExecuteBlock(secondBlock);
            }
            else
            {
                flowchart.ExecuteBlock(firstBlock);
            }
        }
    }

    public void EndTalk()
    {
        canInteract = true;
    }
}
