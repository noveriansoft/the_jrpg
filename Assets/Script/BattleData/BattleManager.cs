using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [Header("Enemy Asset")]
    public Image enemyImage;
    public TMP_Text hpText;

    private EnemyData enemy;
    private int currentHP;

    [Header("BG Asset")]
    public Image backgroundImage;
    public Sprite forestBG;
    public Sprite dungeonBG;

    void Start()
    {
        switch (BattleData.CurrentBattleArea)
        {
            case BattleArea.Forest:
                backgroundImage.sprite = forestBG;
                break;

            case BattleArea.Dungeon:
                backgroundImage.sprite = dungeonBG;
                break;
        }

        enemy = BattleData.CurrentEnemy;
        currentHP = enemy.maxHP;
        enemyImage.sprite = enemy.battleSprite;
        UpdateUI();
    }

    void UpdateUI()
    {
        hpText.text =
            enemy.enemyName +
            "\nHP: " +
            currentHP +
            "/" +
            enemy.maxHP;
    }

    public void Attack()
    {
        currentHP -= 5;

        if (currentHP <= 0)
        {
            WinBattle();
            return;
        }

        UpdateUI();
    }

    public void Run()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("WorldScene");
        BattleData.CurrentEnemyWorld.StartCooldown();
        ExitBattle();
    }

    void WinBattle()
    {
        //Destroy current enemy object
        Destroy(BattleData.CurrentEnemyWorld.gameObject);

        //UnityEngine.SceneManagement.SceneManager.LoadScene("WorldScene");
        ExitBattle();
    }

    void ExitBattle()
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();

        player.EnableMovement();

        SceneManager.UnloadSceneAsync("BattleScene");
    }
}
