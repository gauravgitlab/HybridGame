using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CosmeticItem
{
    public GameObject m_containerGameObject;
    public List<CosmeticItemCard> m_cosmeticItemCards;
}

public class ShopScreen : MonoBehaviour
{
    [Header("Cosmetic Container Tabs")]
    [SerializeField] private GameObject m_categoryTabPrefab;
    [SerializeField] private Transform m_categoryTabContainer;
    [SerializeField] private Color NormalTabColor;
    [SerializeField] private Color SelecteTabColor;

    [Header("Cosmetic Items")] 
    [SerializeField] private GameObject m_cosmeticItemScrollViewPrefab;
    [SerializeField] private GameObject m_categoryItemPrefab;
    [SerializeField] private Transform m_categoryItemsTransform;
    
    private List<CategoryTab> m_categoryTabs = new();
    private Dictionary<CosmeticCategory, CosmeticItem> m_categoryItemsContainers;
    private CosmeticCategory m_currentCategory;

    private void Awake()
    {
        ShopEvents.CosmeticPurchased += OnCosmeticPurchased;
        ShopEvents.CosmeticEquipped += OnCosmeticEquipped;
    }

    private void OnDestroy()
    {
        ShopEvents.CosmeticPurchased -= OnCosmeticPurchased;
        ShopEvents.CosmeticEquipped -= OnCosmeticEquipped;
    }
    
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
        
        m_categoryTabs.Clear();
        foreach (var category in CosmeticManager.Instance.CosmeticCategorizedItems.Keys)
        {
            var tab = Instantiate(m_categoryTabPrefab, m_categoryTabContainer);
            var categoryTab = tab.GetComponent<CategoryTab>();
            categoryTab.SetTab(category, SetCategory);
            categoryTab.ChangeTabColor(NormalTabColor);
            
            m_categoryTabs.Add(categoryTab);
        }
    }
    
    private void PopulateCosmeticItems()
    {
        foreach (Transform child in m_categoryItemsTransform)
        {
            Destroy(child.gameObject);
        }

        m_categoryItemsContainers = new Dictionary<CosmeticCategory, CosmeticItem>();
        foreach (var category in CosmeticManager.Instance.CosmeticCategorizedItems.Keys)
        {
            var itemsContainerGameObject = Instantiate(m_cosmeticItemScrollViewPrefab, m_categoryItemsTransform);
            var itemsContainer = itemsContainerGameObject.GetComponent<CategoryItemsContainer>();
            itemsContainer.SetContainer(category);

            CosmeticItem cosmeticItem = new()
            {
                m_containerGameObject = itemsContainerGameObject,
                m_cosmeticItemCards = new List<CosmeticItemCard>()
            };

            foreach (var item in CosmeticManager.Instance.GetItemsByCategory(category))
            {
                var cosmeticItemGo = Instantiate(m_categoryItemPrefab, itemsContainer.m_content);
                var cosmeticItemCard = cosmeticItemGo.GetComponent<CosmeticItemCard>();
                cosmeticItemCard.SetItemCard(item);
                
                cosmeticItem.m_cosmeticItemCards.Add(cosmeticItemCard);
            }
            
            m_categoryItemsContainers[category] = cosmeticItem;
        }
    }

    private void EnableCategory(CosmeticCategory category, bool enable = true)
    {
        if (m_categoryItemsContainers.ContainsKey(category))
        {
            m_categoryItemsContainers[category].m_containerGameObject.SetActive(enable);
            if (enable)
            {
                m_currentCategory = category;
            }
        }
        
        // Change tab color
        m_categoryTabs.Find(c => c.m_category == category)?.
            ChangeTabColor(enable ? SelecteTabColor : NormalTabColor);
    }

    private void SetCategory(CosmeticCategory category)
    {
        if(m_currentCategory == category)
        {
            return; // Already selected
        }
        
        EnableCategory(m_currentCategory, false);
        EnableCategory(category);
    }
    
    private void OnCosmeticPurchased(CosmeticCategory category, string cosmeticItemId)
    {
        foreach (var itemCard in m_categoryItemsContainers.SelectMany(kvp => kvp.Value.m_cosmeticItemCards))
        {
            itemCard.RefreshUI();
        }
    }
    
    private void OnCosmeticEquipped(CosmeticCategory category, string lastEquippedCosmeticId, string cosmeticItemId)
    {
        // un-equip the last equipped cosmetic if it exists
        if (!string.IsNullOrEmpty(lastEquippedCosmeticId))
        {
            CosmeticItemCard lastEquippedItemCard = m_categoryItemsContainers[category].m_cosmeticItemCards
                .FirstOrDefault(card => card.m_cosmeticId == lastEquippedCosmeticId);
            if(lastEquippedItemCard != null)
            {
                lastEquippedItemCard.OnCosmeticUnEquip();
            }
        }
        
        // equip the new cosmetic item
        CosmeticItemCard newEquippedItemCard = m_categoryItemsContainers[category].m_cosmeticItemCards
            .FirstOrDefault(card => card.m_cosmeticId == cosmeticItemId);
        if (newEquippedItemCard != null)
        {
            newEquippedItemCard.OnCosmeticEquip();
        }
    }

    public void OnDonePressed()
    {
        ScreenManager.Instance.GoToScreen(ScreenManager.Screen.START_SCREEN);
    }
}
