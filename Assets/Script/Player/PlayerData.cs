using UnityEngine;

[CreateAssetMenu(menuName = "JRPG/Player")]
public class PlayerData : ScriptableObject
{
    public string playerName;

    public int maxHP;
    public int attack;
    public int level;
    public int exp;
    public int expToNextLevel;
}
