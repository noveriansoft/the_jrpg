using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StopAllEnemies()
    {
        EnemyWander[] enemies = FindObjectsByType<EnemyWander>(FindObjectsSortMode.None);

        foreach (EnemyWander enemy in enemies)
        {
            enemy.canMove = false;
        }
    }

    public void ResumeAllEnemies()
    {
        EnemyWander[] enemies = FindObjectsByType<EnemyWander>(FindObjectsSortMode.None);

        foreach (EnemyWander enemy in enemies)
        {
            enemy.canMove = true;
        }
    }
}
