using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<EnemyController> m_enemies;

    private void Awake()
    {
        m_enemies = new List<EnemyController>();
        GameplayEvents.LevelReset += GameplayEventsOnLevelReset;
    }
    
    private void GameplayEventsOnLevelReset()
    {
        m_enemies.Clear();
    }
    
    private void OnDestroy()
    {
        GameplayEvents.LevelReset -= GameplayEventsOnLevelReset;
    }

    public void RegisterEnemy(EnemyController enemy)
    {
        m_enemies.Add(enemy);
        GameplayEvents.SendEnemyCountUpdated(m_enemies.Count);
    }

    public void DeRegisterEnemy(EnemyController enemy)
    {
        m_enemies.Remove(enemy);
        GameplayEvents.SendEnemyCountUpdated(m_enemies.Count);
    }

    public int GetEnemyCount()
    {
        return m_enemies.Count;
    }
}