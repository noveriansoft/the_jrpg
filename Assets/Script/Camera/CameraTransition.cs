using UnityEngine;
using System.Collections;

public class CameraTransition : MonoBehaviour
{
    public Transform cameraTransform;
    public PlayerMovement player;
    public float moveTime = 0.5f;
    bool moving;

    public void MoveDown()
    {
        if (moving)
            return;

        //StartCoroutine(MoveCamera(cameraTransform.position +new Vector3(0, -10, 0)));
        StartCoroutine(MoveDownSequence());
    }

    public void MoveUp()
    {
        if (moving)
            return;

        StartCoroutine(MoveUpSequence());
    }

    #region Move
    IEnumerator MoveDownSequence()
    {
        moving = true;
        EnemyManager.Instance.StopAllEnemies();
        player.DisableMovement();

        Vector3 start = cameraTransform.position;
        Vector3 target = start + new Vector3(0, -10, 0);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / moveTime;

            cameraTransform.position =
                Vector3.Lerp(start, target, t);

            yield return null;
        }

        cameraTransform.position = target;
        yield return StartCoroutine(player.MoveDownCutscene(3f));

        player.EnableMovement();
        EnemyManager.Instance.ResumeAllEnemies();

        moving = false;
    }

    IEnumerator MoveUpSequence()
    {
        moving = true;

        EnemyManager.Instance.StopAllEnemies();
        player.DisableMovement();

        Vector3 start = cameraTransform.position;
        Vector3 target = start + new Vector3(0, 10, 0);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / moveTime;

            cameraTransform.position =
                Vector3.Lerp(start, target, t);

            yield return null;
        }

        cameraTransform.position = target;

        yield return StartCoroutine(player.MoveUpCutscene(3f));

        player.EnableMovement();
        EnemyManager.Instance.ResumeAllEnemies();

        moving = false;
    }
    #endregion
}
