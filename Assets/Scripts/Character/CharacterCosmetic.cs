using System.Collections.Generic;
using UnityEngine;

public class CharacterCosmetic : MonoBehaviour
{
    public string m_cosmeticCategory;
    private List<GameObject> m_cosmetics = new();

    private void Awake()
    {
        m_cosmetics = new List<GameObject>();
        foreach (Transform child in transform)
        {
            m_cosmetics.Add(child.gameObject);
        }
    }

    public void EnableCosmetic(string cosmeticId)
    {
        foreach (var cosmetic in m_cosmetics)
        {
            if (!string.IsNullOrEmpty(cosmeticId) && CustomUtils.CompareIDs(cosmetic.name, cosmeticId))
            {
                cosmetic.SetActive(true);
            }
            else
            {
                cosmetic.SetActive(false);
            }
        }
    }

    public void SetRandomCosmetic()
    {
        var cosmeticId = GetRandomCosmeticId();
        if (string.IsNullOrEmpty(cosmeticId))
            return;
        
        EnableCosmetic(cosmeticId);
    }
    
    private string GetRandomCosmeticId()
    {
        if (m_cosmetics == null || m_cosmetics.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, m_cosmetics.Count);
        return m_cosmetics[randomIndex].name;
    }
}
