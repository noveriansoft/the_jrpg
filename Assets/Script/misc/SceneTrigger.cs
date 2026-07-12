using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTrigger : MonoBehaviour
{
    public string sceneName;
    private bool triggered;
    public bool gameEndedTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
        GameEndData.isGameEnded = true;

        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        player.DisableMovement();
        EnemyManager.Instance.StopAllEnemies();
        yield return ScreenFader.Instance.FadeOut(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
