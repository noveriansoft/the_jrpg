using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            BattleData.CurrentPlayer = playerData;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
