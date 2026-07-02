using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public Image enemyImage;
    public TMP_Text hpText;

    private EnemyData enemy;
    private int currentHP;

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldScene");
    }

    void WinBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldScene");
    }
}
