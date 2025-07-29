using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject m_character;

    private void Awake()
    {
        GameObject player = Instantiate(m_character, transform);
        GameManager.Instance.SetPlayer(player.transform);
    }
}
