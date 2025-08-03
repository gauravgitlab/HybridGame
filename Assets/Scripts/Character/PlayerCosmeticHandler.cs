using System.Collections.Generic;
using UnityEngine;

public class PlayerCosmeticHandler : MonoBehaviour
{
    [SerializeField] private List<CharacterCosmetic> m_playerCosmetics = new();
    
    private void Awake()
    {
        ShopEvents.CosmeticEquipped += OnCosmeticEquipped;
    }
    
    private void Start()
    {
        foreach(var characterCosmetic in m_playerCosmetics)
        {
            if (characterCosmetic == null)
            {
                Debug.LogWarning("CharacterCosmetic is null in PlayerCosmeticHandler.");
                continue;
            }
            
            var equippedCosmeticId = GameManager.Instance.m_playerProgressionData.
                GetCurrentEquippedCosmetic(characterCosmetic.m_cosmeticCategory);
            characterCosmetic.EnableCosmetic(equippedCosmeticId);
        }
    }

    private void OnDestroy()
    {
        ShopEvents.CosmeticEquipped -= OnCosmeticEquipped;
    }

    private void OnCosmeticEquipped(string cosmeticCategory, string lastEquippedCosmeticId, string cosmeticId)
    {
        var playerCosmetic = m_playerCosmetics.Find(pc => CustomUtils.CompareIDs(pc.m_cosmeticCategory, cosmeticCategory));
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