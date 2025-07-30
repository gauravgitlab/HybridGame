using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [Header("Cosmetic Container Tabs")]
    [SerializeField] private GameObject m_categoryTabPrefab;
    [SerializeField] private Transform m_categoryTabContainer;

    [Header("Cosmetic Items")] 
    [SerializeField] private GameObject m_cosmeticItemScrollViewPrefab;
    [SerializeField] private GameObject m_categoryItemPrefab;
    [SerializeField] private Transform m_categoryItemsTransform;
    
    private Dictionary<CosmeticCategory, GameObject> m_categoryItemsContainers;
    private CosmeticCategory m_currentCategory;
    
    private void Start()
    {
        PopulateCosmeticTabs();
        PopulateCosmeticItems();
        EnableCategory(CosmeticCategory.Hats);
    }

    private void PopulateCosmeticTabs()
    {
        foreach (Transform child in m_categoryTabContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var category in CosmeticManager.Instance.CosmeticCategorizedItems.Keys)
        {
            var tab = Instantiate(m_categoryTabPrefab, m_categoryTabContainer);
            tab.GetComponent<CategoryTab>().SetTab(category, SetCategory);
        }
    }
    
    private void PopulateCosmeticItems()
    {
        foreach (Transform child in m_categoryItemsTransform)
        {
            Destroy(child.gameObject);
        }

        m_categoryItemsContainers = new Dictionary<CosmeticCategory, GameObject>();
        foreach (var category in CosmeticManager.Instance.CosmeticCategorizedItems.Keys)
        {
            var itemsContainerGameObject = Instantiate(m_cosmeticItemScrollViewPrefab, m_categoryItemsTransform);
            var itemsContainer = itemsContainerGameObject.GetComponent<CategoryItemsContainer>();
            itemsContainer.SetContainer(category);
            
            m_categoryItemsContainers[category] = itemsContainerGameObject;

            foreach (var item in CosmeticManager.Instance.GetItemsByCategory(category))
            {
                var cosmeticItem = Instantiate(m_categoryItemPrefab, itemsContainer.m_content);
                var cosmeticItemCard = cosmeticItem.GetComponent<CosmeticItemCard>();
                cosmeticItemCard.SetItemCard(item);
            }
        }
    }

    private void EnableCategory(CosmeticCategory category, bool enable = true)
    {
        if (m_categoryItemsContainers.ContainsKey(category))
        {
            m_categoryItemsContainers[category].SetActive(enable);
            if (enable)
            {
                m_currentCategory = category;
            }
        }
    }

    private void SetCategory(CosmeticCategory category)
    {
        EnableCategory(m_currentCategory, false);
        EnableCategory(category);
    }
}
