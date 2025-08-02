using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTypeDatabase", menuName = "ScriptableObjects/Enemy/Enemy Type Database")]
public class EnemyTypeDatabaseScriptable : ScriptableObject
{
    public List<EnemyTypeScriptable> m_enemyTypes;
}
