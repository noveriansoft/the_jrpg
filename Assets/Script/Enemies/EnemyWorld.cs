using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWorld : MonoBehaviour
{
    public EnemyData enemyData;
    public BattleArea battleArea;
    private bool cooldown;
    private bool battleStarted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (battleStarted)
            return;

        if (cooldown)
            return;

        if (!other.CompareTag("Player"))
            return;

        StartCoroutine(StartBattle(other));

        #region backup
        //Debug.Log("Battle start: " + enemyData.enemyName);
        //battleStarted = true;
        //BattleData.CurrentEnemy = enemyData;
        //BattleData.CurrentBattleArea = battleArea;
        //BattleData.CurrentEnemyWorld = this;

        ////SceneManager.LoadScene("BattleScene");
        //SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);

        //PlayerMovement player =
        //other.GetComponent<PlayerMovement>();
        //player.DisableMovement();
        #endregion
    }

    IEnumerator StartBattle(Collider2D other)
    {
        battleStarted = true;

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        player.DisableMovement();

        Debug.Log("Battle start: " + enemyData.enemyName);
        BattleData.CurrentEnemy = enemyData;
        BattleData.CurrentBattleArea = battleArea;
        BattleData.CurrentEnemyWorld = this;

        yield return StartCoroutine(ScreenFader.Instance.FadeOut(0.3f));
        SceneManager.LoadScene("BattleScene",LoadSceneMode.Additive);
    }

    public void StartCooldown()
    {
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        cooldown = true;
        yield return new WaitForSeconds(2f);

        cooldown = false;
        battleStarted = false;
    }
}
