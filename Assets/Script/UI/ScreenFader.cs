using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;
    public Image fadeImage;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator FadeOut(float duration)
    {
        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            color.a = t / duration;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn(float duration)
    {
        fadeImage.gameObject.SetActive(true);
        Debug.Log("FADE IN START");

        Color color = fadeImage.color;

        float t = duration;

        while (t > 0)
        {
            t -= Time.deltaTime;

            color.a = t / duration;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;

        fadeImage.gameObject.SetActive(false);
    }

    public void StartExitBattle(bool destroyEnemy, EnemyWorld enemy)
    {
        StartCoroutine(ExitBattleRoutine(destroyEnemy, enemy));
    }

    IEnumerator ExitBattleRoutine(bool destroyEnemy, EnemyWorld enemy)
    {
        //yield return FadeOut(0.5f);

        if (destroyEnemy)
            Destroy(enemy.gameObject);
        else
            enemy.StartCooldown();

        EnemyManager.Instance.ResumeAllEnemies();
        FindFirstObjectByType<PlayerMovement>().EnableMovement();
        yield return SceneManager.UnloadSceneAsync("BattleScene");
        yield return FadeIn(0.5f);
    }
}
