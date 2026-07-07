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

        Debug.Log("Battle start: " + enemyData.enemyName);
        battleStarted = true;
        BattleData.CurrentEnemy = enemyData;
        BattleData.CurrentBattleArea = battleArea;
        BattleData.CurrentEnemyWorld = this;

        //SceneManager.LoadScene("BattleScene");
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);

        PlayerMovement player =
        other.GetComponent<PlayerMovement>();
        player.DisableMovement();
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
