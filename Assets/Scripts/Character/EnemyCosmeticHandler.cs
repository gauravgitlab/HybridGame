using System.Collections.Generic;
using UnityEngine;

public class EnemyCosmeticHandler : MonoBehaviour
{
    [SerializeField] private List<CharacterCosmetic> m_enemyCosmetics = new();

    private void Start()
    {
        foreach (var characterCosmetic in m_enemyCosmetics)
        {
            characterCosmetic.SetRandomCosmetic();
        }    
    }
}
