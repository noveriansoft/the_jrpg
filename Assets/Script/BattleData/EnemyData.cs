using UnityEngine;

[CreateAssetMenu(menuName = "JRPG/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public int maxHP;
    public int attack;

    public Sprite battleSprite;
}
