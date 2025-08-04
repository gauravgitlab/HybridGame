using System.Collections.Generic;

[System.Serializable]
public class CosmeticCategoryEntry
{
    public string m_cosmeticCategory;
    public List<string> m_purchasedCosmeticIds = new();
}

[System.Serializable]
public class EquippedCosmeticEntry
{
    public string m_cosmeticCategory;
    public string m_equippedCosmeticId;
}

[System.Serializable]
public class PlayerProgressionData : SaveDataBase, ICustomSerializable
{
    // key: CosmeticCategory, value: List of purchased cosmetic IDs.
    [System.NonSerialized]
    public Dictionary<string, List<string>> m_purchasedCosmetics = new();

    // key: CosmeticCategory, value: Equipped cosmetic ID.
    [System.NonSerialized] 
    public Dictionary<string, string> m_equippedCosmetics = new();
    
    public int m_level = 0;
    
    // used for serialization
    public List<CosmeticCategoryEntry> m_purchasedCosmeticsEntries = new();
    
    // used for serialization
    public List<EquippedCosmeticEntry> m_equippedCosmeticsEntries = new();
    
    public void PrepareForSave()
    {
        m_purchasedCosmeticsEntries.Clear();
        foreach (var kvp in m_purchasedCosmetics)
        {
            m_purchasedCosmeticsEntries.Add(new CosmeticCategoryEntry
            {
                m_cosmeticCategory = kvp.Key,
                m_purchasedCosmeticIds = new List<string>(kvp.Value)
            });
        }
        
        m_equippedCosmeticsEntries.Clear();
        foreach (var kvp in m_equippedCosmetics)
        {
            m_equippedCosmeticsEntries.Add(new EquippedCosmeticEntry
            {
                m_cosmeticCategory = kvp.Key,
                m_equippedCosmeticId = kvp.Value
            });
        }
    }
    
    public void RestoreAfterLoad()
    {
        m_purchasedCosmetics.Clear();
        foreach (CosmeticCategoryEntry entry in m_purchasedCosmeticsEntries)
        {
            if(!string.IsNullOrEmpty(entry.m_cosmeticCategory))
            {
                m_purchasedCosmetics[entry.m_cosmeticCategory] = new List<string>(entry.m_purchasedCosmeticIds);
            }
        }
        
        m_equippedCosmetics.Clear();
        foreach (var entry in m_equippedCosmeticsEntries)
        {
            if (!string.IsNullOrEmpty(entry.m_cosmeticCategory))
            {
                m_equippedCosmetics[entry.m_cosmeticCategory] = entry.m_equippedCosmeticId;
            }
        }
    }
    
    public void AddPurchasedCosmetic(string cosmeticCategory, string cosmeticId)
    {
        if (!m_purchasedCosmetics.ContainsKey(cosmeticCategory))
        {
            m_purchasedCosmetics[cosmeticCategory] = new List<string>();
        }
        
        if (!m_purchasedCosmetics[cosmeticCategory].Contains(cosmeticId))
        {
            m_purchasedCosmetics[cosmeticCategory].Add(cosmeticId);
        }
    }
    
    public void EquipCosmetic(string cosmeticCategory, string cosmeticId)
    {
        if (m_purchasedCosmetics.ContainsKey(cosmeticCategory) && 
            m_purchasedCosmetics[cosmeticCategory].Contains(cosmeticId))
        {
            m_equippedCosmetics[cosmeticCategory] = cosmeticId;
        }
        else
        {
            UnityEngine.Debug.LogError($"Cosmetic {cosmeticId} in category {cosmeticCategory} is not purchased.");
        }
    }
    
    public bool IsCosmeticPurchased(string cosmeticCategory, string cosmeticId)
    {
        return m_purchasedCosmetics.ContainsKey(cosmeticCategory) && 
               m_purchasedCosmetics[cosmeticCategory].Contains(cosmeticId);
    }
    
    public bool IsCosmeticEquipped(string cosmeticCategory, string cosmeticId)
    {
        return m_equippedCosmetics.ContainsKey(cosmeticCategory) &&
               CustomUtils.CompareIDs(m_equippedCosmetics[cosmeticCategory], cosmeticId);
    }
    
    public string GetCurrentEquippedCosmetic(string cosmeticCategory)
    {
        return m_equippedCosmetics.GetValueOrDefault(cosmeticCategory);
    }
}
