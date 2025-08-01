using System.Collections.Generic;
using UnityEngine;

public class PlayerCosmetic : MonoBehaviour
{
    public CosmeticCategory m_cosmeticCategory;
    private List<GameObject> m_cosmetics = new();

    private void Awake()
    {
        m_cosmetics = new List<GameObject>();
        foreach (Transform child in transform)
        {
            m_cosmetics.Add(child.gameObject);
        }
    }

    private void Start()
    {
        var equippedCosmeticId = GameManager.Instance.m_playerProgressionData.
            GetCurrentEquippedCosmetic(m_cosmeticCategory);
        
        EnableCosmetic(equippedCosmeticId);
    }

    public void EnableCosmetic(string cosmeticId)
    {
        foreach (var cosmetic in m_cosmetics)
        {
            if (!string.IsNullOrEmpty(cosmeticId) && cosmetic.name == cosmeticId)
            {
                cosmetic.SetActive(true);
            }
            else
            {
                cosmetic.SetActive(false);
            }
        }
    }
}
