using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private EnemyController m_enemy;
    [SerializeField] private WeaponDropScriptable m_weaponDropScriptable;

    private void Awake()
    {
        EnemyController enemy = Instantiate(m_enemy, transform);
        EnemyManager.Instance.RegisterEnemy(enemy);
        
        if (m_weaponDropScriptable)
        {
            enemy.Config(m_weaponDropScriptable);
        }
    }
}
