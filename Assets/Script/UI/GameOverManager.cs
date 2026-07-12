using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Image dim;
    public TMP_Text gameOverText;

    private IEnumerator Start()
    {
        yield return FadeIn(0.5f);
    }

    public void Retry()
    {
        StartCoroutine(RetrySequence());
    }

    IEnumerator RetrySequence()
    {
        yield return FadeOut(0.5f);
        SceneManager.LoadScene("WorldScene");
    }

    public void Exit()
    {
        StartCoroutine(ExitSequence());
    }

    IEnumerator ExitSequence()
    {
        yield return FadeOut(0.5f);

        Application.Quit();
    }

    IEnumerator FadeIn(float duration)
    {
        if (GameEndData.isGameEnded)
        {
            gameOverText.text = "Game Ended";
            Debug.Log("Game Ended");
        }
        else
        {
            gameOverText.text = "Game Over";
            Debug.Log("Game Over");
        }

        dim.gameObject.SetActive(true);
        Color color = dim.color;

        float t = duration;

        while (t > 0)
        {
            t -= Time.deltaTime;

            color.a = t / duration;
            dim.color = color;

            yield return null;
        }

        color.a = 0;
        dim.color = color;

        GameEndData.isGameEnded = false; //reset
    }

    IEnumerator FadeOut(float duration)
    {
        Color color = dim.color;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            color.a = t / duration;
            dim.color = color;
            yield return null;
        }

        color.a = 1;
        dim.color = color;
    }
}
