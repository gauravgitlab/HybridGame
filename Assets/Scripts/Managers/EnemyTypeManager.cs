using UnityEngine;

public class EnemyTypeManager : Singleton<EnemyTypeManager>
{
    [SerializeField]
    private EnemyTypeDatabaseScriptable m_enemyTypeDatabase;
    
    public EnemyTypeScriptable GetRandomEnemyType()
    {
        if (m_enemyTypeDatabase == null || m_enemyTypeDatabase.m_enemyTypes.Count == 0)
        {
            Debug.LogError("EnemyTypeDatabase is not set or empty.");
            return null;
        }

        int randomIndex = Random.Range(0, m_enemyTypeDatabase.m_enemyTypes.Count);
        return m_enemyTypeDatabase.m_enemyTypes[randomIndex];
    }
}
