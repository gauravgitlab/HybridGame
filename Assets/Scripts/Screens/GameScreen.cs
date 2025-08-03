using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    private const string ENEMY_COUNT = "x {0}";
    
    [SerializeField] private GameObject m_joystickVisuals;
    [SerializeField] private Text m_enemyCount;
    [SerializeField] private GameObject m_tickGameObject;

    private void Awake()
    {
        GameplayEvents.EnemyCountUpdated += GameplayEventsOnEnemyCountUpdated;
        m_tickGameObject.SetActive(false);
    }

    private void Start()
    {
        m_enemyCount.text = string.Format(ENEMY_COUNT, EnemyManager.Instance.GetEnemyCount());
        
        Transform player = GameManager.Instance.GetPlayer();
        if (player != null)
        {
            player.GetComponent<VirtualJoystick>().RegisterUIVisuals(m_joystickVisuals);
        }
    }

    private void OnDestroy()
    {
        GameplayEvents.EnemyCountUpdated -= GameplayEventsOnEnemyCountUpdated;
    }
    
    private void GameplayEventsOnEnemyCountUpdated(int count)
    {
        if (count <= 0)
        {
            m_tickGameObject.SetActive(true);
            m_enemyCount.gameObject.SetActive(false);
        }
        else
        {
            m_tickGameObject.SetActive(false);
            m_enemyCount.gameObject.SetActive(true);
            m_enemyCount.text = string.Format(ENEMY_COUNT, count);    
        }
    }
}