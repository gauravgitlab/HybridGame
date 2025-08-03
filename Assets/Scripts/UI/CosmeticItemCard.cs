using UnityEngine;
using UnityEngine.UI;

public class CosmeticItemCard : MonoBehaviour
{
    [SerializeField] private Image m_cosmeticImage;
    [SerializeField] private GameObject m_lockGameObject;
    [SerializeField] private GameObject m_equippedGameObject;
    [SerializeField] private Text m_nameText;
    [SerializeField] private Text m_statusText;
    [SerializeField] private CosmeticItemCost m_cost;
    [SerializeField] private Button m_equipButton;

    private int m_cosmeticCost = 0;
    private string m_cosmeticCategory;
    
    public string m_cosmeticId;
    
    public void SetItemCard(CosmeticItemScriptable cosmeticItem)
    {
        name = cosmeticItem.m_cosmeticId;
        m_nameText.text = cosmeticItem.m_displayName;
        m_cosmeticImage.sprite = cosmeticItem.m_icon;
        
        m_cosmeticCategory = cosmeticItem.m_cosmeticCategory;
        m_cosmeticId = cosmeticItem.m_cosmeticId;
        m_cosmeticCost = cosmeticItem.m_cost;
        
        m_cost.SetCost(m_cosmeticCost, BuyCosmeticItem);
        
        m_equipButton.onClick.RemoveAllListeners();
        m_equipButton.onClick.AddListener(EquipCosmeticItem);
        m_equipButton.gameObject.SetActive(false);
        
        m_lockGameObject.SetActive(false);
        
        RefreshUI();
    }

    private void BuyCosmeticItem()
    {
        if (!IsAffordable())
        {
            return;
        }
        
        bool isPurchased = GameBank.Purchase(GameManager.CURRENCY_COIN, m_cosmeticCost);
        if (isPurchased)
        {
            GameManager.Instance.OnPurchaseCosmeticItem(m_cosmeticCategory, m_cosmeticId);
            ShopEvents.SendCosmeticPurchased(m_cosmeticCategory, m_cosmeticId);
        }
    }
    
    private void EquipCosmeticItem()
    {
        bool isEquipped = IsCosmeticEquipped();
        if (isEquipped)
            return;
        
        var alreadyEquippedCosmeticId = GameManager.Instance.m_playerProgressionData.GetCurrentEquippedCosmetic(m_cosmeticCategory);
        if (!string.IsNullOrEmpty(alreadyEquippedCosmeticId))
        {
            if(CustomUtils.CompareIDs(alreadyEquippedCosmeticId, m_cosmeticId))
            {
                // Already equipped, do nothing
                return;
            }
        }
            
        GameManager.Instance.OnEquipCosmeticItem(m_cosmeticCategory, m_cosmeticId);
        ShopEvents.SendCosmeticEquipped(m_cosmeticCategory, alreadyEquippedCosmeticId, m_cosmeticId);
    }

    public void RefreshUI()
    {
        bool isCosmeticPurchased = GameManager.Instance.m_playerProgressionData.IsCosmeticPurchased(m_cosmeticCategory, m_cosmeticId);
        bool isCosmeticEquipped = IsCosmeticEquipped();
        bool isAffordable = IsAffordable();
        
        // Reset common UI state
        m_equippedGameObject.SetActive(false);
        m_statusText.text = "";
        m_equipButton.gameObject.SetActive(false);
        m_cost.Hide();
        
        if (isCosmeticPurchased)
        {
            if (isCosmeticEquipped)
            {
                OnCosmeticEquip();
            }
            else
            {
                OnCosmeticUnEquip();
            }
        }
        else
        {
            m_cost.RefreshUI(isAffordable);
        }
    }
    
    private bool IsAffordable()
    {
        return m_cosmeticCost <= GameBank.GetCurrency(GameManager.CURRENCY_COIN);
    }
    
    private bool IsCosmeticEquipped()
    {
        return GameManager.Instance.m_playerProgressionData.IsCosmeticEquipped(m_cosmeticCategory, m_cosmeticId);
    }

    public void OnCosmeticEquip()
    {
        m_equippedGameObject.SetActive(true);
        m_statusText.text = "Equipped";
        m_equipButton.gameObject.SetActive(false);
    }
    
    public void OnCosmeticUnEquip()
    {
        m_equippedGameObject.SetActive(false);
        m_statusText.text = "";
        m_equipButton.gameObject.SetActive(true);
    }
}