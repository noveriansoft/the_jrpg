using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [Header("Enemy Asset")]
    public Image enemyImage;
    public TMP_Text hpText;

    private EnemyData enemy;
    private int currentHP;

    [Header("Player Stats")]
    private int playerMaxHP = 20;

    private PlayerData player;
    private int playerCurrentHP;

    [Header("BG Asset")]
    public Image backgroundImage;
    public Sprite forestBG;
    public Sprite dungeonBG;
    public TMP_Text battleLogText;

    private bool playerTurn = true;

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

        //Enemy data
        enemy = BattleData.CurrentEnemy;
        currentHP = enemy.maxHP;
        enemyImage.sprite = enemy.battleSprite;

        //Player data
        player = BattleData.CurrentPlayer;
        playerCurrentHP = player.maxHP;

        UpdateUI();

        SetBattleLog(
            "A wild " +
            enemy.enemyName +
            " appeared!"
        );
    }

    void UpdateUI()
    {
        hpText.text =
            enemy.enemyName +
            "\nEnemy HP: " +
            currentHP +
            "/" +
            enemy.maxHP +
            "\n\nPlayer HP: " +
            playerCurrentHP +
            "/" +
            player.maxHP;

        Debug.Log("Player HP: " + playerCurrentHP);
        Debug.Log("Enemy HP: " + currentHP);
    }

    void SetBattleLog(string message)
    {
        battleLogText.text = message;
    }

    #region Battle Functions
    public void Attack()
    {
        if (!playerTurn)
            return;

        Debug.Log("Player Attacking!");

        int damage = player.attack;

        currentHP -= damage;

        SetBattleLog(
            player.playerName + 
            " attacked " +
            enemy.enemyName +
            "!\n" +
            enemy.enemyName +
            " lost " +
            damage +
            " HP!"
        );

        //Enemies hp 0 is win baby
        if (currentHP <= 0)
        {
            currentHP = 0;
            UpdateUI();

            WinBattle();
            return;
        }

        UpdateUI();

        playerTurn = false;

        StartCoroutine(EnemyTurn());
    }

    public void Run()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("WorldScene");
        BattleData.CurrentEnemyWorld.StartCooldown();
        ExitBattle();
    }

    void WinBattle()
    {
        Debug.Log("Player Win!");
        Debug.Log("Enemy HP when win = " + currentHP);

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

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        EnemyAttack();
    }

    void EnemyAttack()
    {
        Debug.Log("Enemies Attacking!");

        int damage = enemy.attack;

        playerCurrentHP -= damage;

        SetBattleLog(
            enemy.enemyName +
            " attacked " + 
            player.playerName + "!\n" +
            player.playerName + " lost " +
            damage +
            " HP!"
        );

        if (playerCurrentHP <= 0)
        {
            playerCurrentHP = 0;

            UpdateUI();

            LoseBattle();
            return;
        }

        UpdateUI();

        playerTurn = true;
    }

    void LoseBattle()
    {
        Debug.Log("Player Defeated");

        ExitBattle();
    }

    #endregion
}
