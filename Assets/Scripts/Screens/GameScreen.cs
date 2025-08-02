using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    private const string ENEMY_COUNT = "x {0}";
    
    [SerializeField] private Text m_enemyCount;

    private void Awake()
    {
        GameplayEvents.EnemyCountUpdated += GameplayEventsOnEnemyCountUpdated;
    }

    private void Start()
    {
        m_enemyCount.text = string.Format(ENEMY_COUNT, EnemyManager.Instance.GetEnemyCount());
    }

    private void OnDestroy()
    {
        GameplayEvents.EnemyCountUpdated -= GameplayEventsOnEnemyCountUpdated;
    }
    
    private void GameplayEventsOnEnemyCountUpdated(int count)
    {
        m_enemyCount.text = string.Format(ENEMY_COUNT, count);
    }
}