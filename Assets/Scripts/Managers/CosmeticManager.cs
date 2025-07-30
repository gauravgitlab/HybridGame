using System.Collections.Generic;
using UnityEngine;

public class CosmeticManager : Singleton<CosmeticManager>
{
    [SerializeField]
    private CosmeticDatabaseScriptable m_cosmeticDatabase;
    
    private Dictionary<CosmeticCategory, List<CosmeticItemScriptable>> m_cosmeticCategorizedItems;
    
    public IReadOnlyDictionary<CosmeticCategory, List<CosmeticItemScriptable>> CosmeticCategorizedItems => m_cosmeticCategorizedItems;

    private void Awake()
    {
        LoadAndGroupCosmeticItems();
    }

    private void LoadAndGroupCosmeticItems()
    {
        m_cosmeticCategorizedItems = new Dictionary<CosmeticCategory, List<CosmeticItemScriptable>>();

        foreach (var item in m_cosmeticDatabase.m_cosmeticItems)
        {
            if (!m_cosmeticCategorizedItems.ContainsKey(item.m_cosmeticCategory))
                m_cosmeticCategorizedItems[item.m_cosmeticCategory] = new List<CosmeticItemScriptable>();

            m_cosmeticCategorizedItems[item.m_cosmeticCategory].Add(item);
        }
    }
    
    public List<CosmeticItemScriptable> GetItemsByCategory(CosmeticCategory category)
    {
        return m_cosmeticCategorizedItems.TryGetValue(category, out var categorizedItems) ? 
            categorizedItems : new List<CosmeticItemScriptable>();
    }
    
    public CosmeticItemScriptable GetEquippedItem(CosmeticCategory category)
    {
        //var id = PlayerDataManager.Instance.GetEquipped(category);
        //return categorizedItems[category].FirstOrDefault(i => i.id == id);
        return null;
    }
}