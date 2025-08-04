using System.Collections.Generic;
using UnityEngine;

public class CosmeticManager : Singleton<CosmeticManager>
{
    [SerializeField]
    private CosmeticDatabaseScriptable m_cosmeticDatabase;
    
    public Dictionary<string, List<CosmeticItemScriptable>> m_cosmeticCategorizedItems { get; private set; }
    
    private void Awake()
    {
        LoadAndGroupCosmeticItems();
    }

    private void LoadAndGroupCosmeticItems()
    {
        m_cosmeticCategorizedItems = new Dictionary<string, List<CosmeticItemScriptable>>();

        foreach (var item in m_cosmeticDatabase.m_cosmeticItems)
        {
            if (!m_cosmeticCategorizedItems.ContainsKey(item.m_cosmeticCategory))
                m_cosmeticCategorizedItems[item.m_cosmeticCategory] = new List<CosmeticItemScriptable>();

            m_cosmeticCategorizedItems[item.m_cosmeticCategory].Add(item);
        }
    }
    
    public List<CosmeticItemScriptable> GetItemsByCategory(string cosmeticCategory)
    {
        return m_cosmeticCategorizedItems.TryGetValue(cosmeticCategory, out var categorizedItems) ? 
            categorizedItems : new List<CosmeticItemScriptable>();
    }
}