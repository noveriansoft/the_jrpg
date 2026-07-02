using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWorld : MonoBehaviour
{
    public EnemyData enemyData;
    public BattleArea battleArea;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Battle start: " + enemyData.enemyName);
        BattleData.CurrentEnemy = enemyData;
        BattleData.CurrentBattleArea = battleArea;

        SceneManager.LoadScene("BattleScene");
    }
}
