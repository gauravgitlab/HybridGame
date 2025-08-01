using System.Collections.Generic;
using UnityEngine;

public class PlayerCosmeticHandler : MonoBehaviour
{
    [SerializeField] private List<PlayerCosmetic> m_playerCosmetics = new();
    
    private void Awake()
    {
        ShopEvents.CosmeticEquipped += OnCosmeticEquipped;
    }

    private void OnDestroy()
    {
        ShopEvents.CosmeticEquipped -= OnCosmeticEquipped;
    }

    private void OnCosmeticEquipped(CosmeticCategory cosmeticCategory, string lastEquippedCosmeticId, string cosmeticId)
    {
        var playerCosmetic = m_playerCosmetics.Find(pc => pc.m_cosmeticCategory == cosmeticCategory);
        if (playerCosmetic != null)
        {
            playerCosmetic.EnableCosmetic(cosmeticId);
        }
        else
        {
            Debug.LogWarning($"No PlayerCosmetic found for category: {cosmeticCategory}");
        }
    }
}