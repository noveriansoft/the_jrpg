using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [Header("Enemy UI")]
    public Image enemyImage;
    public TMP_Text enemyHpText;
    public Slider enemyHpBar;

    private EnemyData enemy;
    private int currentHP;

    [Header("Player Stats UI")]    
    public TMP_Text playerHpText;
    public Slider playerHpBar;

    private PlayerData player;
    private int playerCurrentHP;

    [Header("BG UI")]
    public Image backgroundImage;

    [Header("BG Asset")]
    public Sprite forestBG;
    public Sprite dungeonBG;

    [Header("TXT UI")]
    public TMP_Text battleLogText;
    [SerializeField] private float typeSpeed = 0.03f;

    [Header("Script")]
    public BattleMenu battleMenu;

    [Header("Attack Effect")]
    public GameObject attackEffect;
    public Animator attackAnimator;

    private bool playerTurn = true;

    void Start()
    {
        EnemyManager.Instance.StopAllEnemies();
        attackEffect.SetActive(false);

        switch (BattleData.CurrentBattleArea)
        {
            case BattleArea.Forest:
                backgroundImage.sprite = forestBG;
                break;

            case BattleArea.Dungeon:
                backgroundImage.sprite = dungeonBG;
                break;
        }

        #region Player & Enemy Data Load
        //Enemy data
        enemy = BattleData.CurrentEnemy;
        currentHP = enemy.maxHP;
        enemyImage.sprite = enemy.battleSprite;
        enemyHpBar.maxValue = enemy.maxHP;
        enemyHpBar.value = currentHP;

        //Player data
        player = BattleData.CurrentPlayer;
        //playerCurrentHP = player.maxHP;
        playerCurrentHP = BattleData.PlayerCurrentHP;
        playerHpBar.maxValue = player.maxHP;
        playerHpBar.value = playerCurrentHP;
        #endregion

        UpdateUI();
        StartCoroutine(TypeBattleLog("A wild " + enemy.enemyName + " appeared!",OnIntroFinished));
    }

    void UpdateUI()
    {
        enemyHpText.text = enemy.enemyName + "\nHP: " + currentHP + "/" + enemy.maxHP;
        enemyHpBar.value = currentHP;

        playerHpText.text = playerCurrentHP + "/" + player.maxHP; //player.playerName; + "\nHP: " + playerCurrentHP + "/" + player.maxHP;     
        playerHpBar.value = playerCurrentHP;

        Debug.Log("Player HP: " + playerCurrentHP);
        Debug.Log("Enemy HP: " + currentHP);
    }

    #region Battle Log
    void SetBattleLog(string message)
    {
        battleLogText.text = message;
    }

    IEnumerator TypeBattleLog(string message, System.Action onComplete = null)
    {
        battleLogText.text = "";

        foreach (char letter in message)
        {
            battleLogText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        onComplete?.Invoke();
    }

    void OnIntroFinished()
    {
        battleMenu.enableButton();
    }
    #endregion

    #region Battle Functions
    public void Attack()
    {
        if (!playerTurn)
            return;

        playerTurn = false;
        StartCoroutine(PlayerAttackSequence());

        #region backup
        //Debug.Log("Player Attacking!");

        //int damage = player.attack;
        //currentHP -= damage;
        //SetBattleLog(player.playerName + " attacked " + enemy.enemyName + "!\n" + enemy.enemyName + " lost " + damage + " HP!");

        ////Enemies hp 0 is win baby
        //if (currentHP <= 0)
        //{
        //    currentHP = 0;
        //    UpdateUI();

        //    WinBattle();
        //    return;
        //}

        //UpdateUI();

        //playerTurn = false;

        //StartCoroutine(EnemyTurn());
        #endregion
    }

    IEnumerator PlayerAttackSequence()
    {
        attackEffect.SetActive(true);
        attackAnimator.Play("Attack");

        yield return new WaitForSeconds(0.5f);

        attackEffect.SetActive(false);
        int damage = player.attack;
        currentHP -= damage;
        SetBattleLog(player.playerName + " attacked " + enemy.enemyName + "!\n" + enemy.enemyName + " lost " + damage + " HP!");

        if (currentHP <= 0)
        {
            currentHP = 0;
            UpdateUI();

            WinBattle();
            yield break;
        }

        UpdateUI();
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

        playerTurn = false;
        battleMenu.attackButton.interactable = false;
        battleMenu.runButton.interactable = false;

        StartCoroutine(WinBattleSequence());
    }

    void ExitBattle()
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();

        player.EnableMovement();

        SceneManager.UnloadSceneAsync("BattleScene");

        EnemyManager.Instance.ResumeAllEnemies();
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
        BattleData.PlayerCurrentHP = playerCurrentHP; //Save player hp data
        SetBattleLog(enemy.enemyName + " attacked " + player.playerName + "!\n" + player.playerName + " lost " + damage + " HP!");

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

    IEnumerator WinBattleSequence()
    {
        Color color = enemyImage.color;

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * 2f;
            enemyImage.color = color;

            yield return null;
        }

        StartCoroutine(
            TypeBattleLog(
                enemy.enemyName + " defeated!\n" +
                player.playerName + " wins!"
            )
        );

        float textDuration =
            (enemy.enemyName + " defeated!\n" +
            player.playerName + " wins!").Length
            * typeSpeed;

        yield return new WaitForSeconds(textDuration + 1f);

        Destroy(BattleData.CurrentEnemyWorld.gameObject);

        ExitBattle();
    }

    #endregion
}
