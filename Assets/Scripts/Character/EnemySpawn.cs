using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private EnemyController m_enemy;
    [SerializeField] private WeaponDropScriptable m_weaponDropScriptable;

    private void Awake()
    {
        EnemyController enemy = Instantiate(m_enemy, transform);
        EnemyManager.Instance.RegisterEnemy(enemy);
        
        EnemyTypeScriptable enemyType = EnemyTypeManager.Instance.GetRandomEnemyType();
        if (enemyType != null)
        {
            enemy.SetEnemyTypeConfig(enemyType);
        }
        
        if (m_weaponDropScriptable)
        {
            enemy.Config(m_weaponDropScriptable);
        }
    }
}
