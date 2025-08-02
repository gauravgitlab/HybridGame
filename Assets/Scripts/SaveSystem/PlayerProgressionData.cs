using System.Collections.Generic;

[System.Serializable]
public class CosmeticCategoryEntry
{
    public string m_category;
    public List<string> m_purchasedItems = new();
}

[System.Serializable]
public class EquippedCosmeticEntry
{
    public string m_category;
    public string m_equippedItem;
}

[System.Serializable]
public class PlayerProgressionData : SaveDataBase, ICustomSerializable
{
    [System.NonSerialized]
    public Dictionary<CosmeticCategory, List<string>> m_purchasedCosmetics = new();

    [System.NonSerialized] public Dictionary<CosmeticCategory, string> m_equippedCosmetics = new();
    
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
                m_category = kvp.Key.ToString(),
                m_purchasedItems = new List<string>(kvp.Value)
            });
        }
        
        m_equippedCosmeticsEntries.Clear();
        foreach (var kvp in m_equippedCosmetics)
        {
            m_equippedCosmeticsEntries.Add(new EquippedCosmeticEntry
            {
                m_category = kvp.Key.ToString(),
                m_equippedItem = kvp.Value
            });
        }
    }
    
    public void RestoreAfterLoad()
    {
        m_purchasedCosmetics.Clear();
        foreach (var entry in m_purchasedCosmeticsEntries)
        {
            if (System.Enum.TryParse(entry.m_category, out CosmeticCategory parsedCategory))
            {
                m_purchasedCosmetics[parsedCategory] = new List<string>(entry.m_purchasedItems);
            }
        }
        
        m_equippedCosmetics.Clear();
        foreach (var entry in m_equippedCosmeticsEntries)
        {
            if (System.Enum.TryParse(entry.m_category, out CosmeticCategory parsedCategory))
            {
                m_equippedCosmetics[parsedCategory] = entry.m_equippedItem;
            }
        }
    }
    
    public void AddPurchasedCosmetic(CosmeticCategory category, string cosmeticId)
    {
        if (!m_purchasedCosmetics.ContainsKey(category))
        {
            m_purchasedCosmetics[category] = new List<string>();
        }
        
        if (!m_purchasedCosmetics[category].Contains(cosmeticId))
        {
            m_purchasedCosmetics[category].Add(cosmeticId);
        }
    }
    
    public void EquipCosmetic(CosmeticCategory category, string cosmeticId)
    {
        if (m_purchasedCosmetics.ContainsKey(category) && m_purchasedCosmetics[category].Contains(cosmeticId))
        {
            m_equippedCosmetics[category] = cosmeticId;
        }
        else
        {
            UnityEngine.Debug.LogError($"Cosmetic {cosmeticId} in category {category} is not purchased.");
        }
    }
    
    public bool IsCosmeticPurchased(CosmeticCategory category, string cosmeticId)
    {
        return m_purchasedCosmetics.ContainsKey(category) && m_purchasedCosmetics[category].Contains(cosmeticId);
    }
    
    public bool IsCosmeticEquipped(CosmeticCategory category, string cosmeticId)
    {
        return m_equippedCosmetics.ContainsKey(category) &&
               CustomUtils.CompareIDs(m_equippedCosmetics[category], cosmeticId);
    }
    
    public string GetCurrentEquippedCosmetic(CosmeticCategory category)
    {
        return m_equippedCosmetics.GetValueOrDefault(category);
    }
}
